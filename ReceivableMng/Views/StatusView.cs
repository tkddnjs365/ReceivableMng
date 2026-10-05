using ReceivableMng.Data;
using ReceivableMng.Models;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReceivableMng.Views
{
    public partial class StatusView : UserControl
    {
        private string _mode = "all"; // 현재 조회 모드: "all"(전체) 또는 "unpaid"(미수금만)

        public StatusView()
        {
            InitializeComponent();

            init();
        }

        /// <summary>화면이 생성될 때 필요한 이벤트 연결, 그리드/차트 초기 설정, 데이터 로딩을 한다.</summary>
        public void init()
        {
            var today = DateTime.Today;
            var prevMonth = today.AddMonths(-3);
            try
            {
                dtpFrom.Value = new DateTime(prevMonth.Year, prevMonth.Month, 1);
            }
            catch { dtpFrom.Value = today.AddMonths(-3); }
            try
            {
                dtpTo.Value = today;
            }
            catch { dtpTo.Value = today; }

            // 필터(기간/업체)가 바뀌면 화면을 다시 그린다.
            dtpFrom.ValueChanged += (s, e) => RenderStatus();
            dtpTo.ValueChanged += (s, e) => RenderStatus();
            cboClient.SelectedIndexChanged += (s, e) => RenderStatus();

            // "전체"/"미수금만" 버튼 클릭 시 모드를 바꾸고 다시 그린다.
            btnAll.Click += (s, e) => { _mode = "all"; UpdateModeButtons(); RenderStatus(); };
            btnUnpaid.Click += (s, e) => { _mode = "unpaid"; UpdateModeButtons(); RenderStatus(); };

            GridSetting();
            ChatSetting();

            // DataStore의 데이터가 바뀔 때마다(다른 화면에서 저장 등) 이 화면도 자동 새로고침.
            DataStore.Changed += RefreshData;
            RefreshData();
        }

        /// <summary>업무 현황을 보여줄 그리드(grid)의 열과 기본 옵션을 설정한다.</summary>
        void GridSetting()
        {
            grid.ReadOnly = true;                    // 셀 직접 수정 불가
            grid.AllowUserToAddRows = false;          // 빈 행 추가 불가
            grid.AllowUserToDeleteRows = false;       // 행 삭제 불가
            grid.RowHeadersVisible = false;           // 맨 왼쪽 행 머리글 숨김
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 열 너비를 그리드 폭에 맞게 자동 조절
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   // 클릭 시 행 전체 선택
            grid.BackgroundColor = Color.White;

            grid.Columns.Add("Client", "업체");
            grid.Columns.Add("Date", "업무일");
            grid.Columns.Add("Amount", "업무 금액");
            grid.Columns.Add("Paid", "받은 금액");
            grid.Columns.Add("Remain", "미수금");
            grid.Columns.Add("Status", "상태");
        }

        /// <summary>차트(chart)의 영역(ChartArea)과 제목을 설정한다.</summary>
        void ChatSetting()
        {
            try
            {
                // 기존 디자이너에서 생성된 영역/타이틀을 초기화하고 새로 설정
                chart.ChartAreas.Clear();
                chart.Titles.Clear();
                chart.ChartAreas.Add(new ChartArea("main"));
                chart.Titles.Add("월별 업무 금액");

                // 차트 컨트롤은 디자이너에서 이미 panel에 추가되므로 중복 추가하지 않음
                chart.Visible = true;
                // X축을 범주형으로 처리하고 모든 항목 라벨이 보이도록 설정
                var area = chart.ChartAreas.Count > 0 ? chart.ChartAreas[0] : null;
                if (area != null)
                {
                    area.AxisX.Interval = 1;
                    area.AxisX.LabelStyle.Angle = -45;
                    area.AxisX.MajorGrid.Enabled = false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "오류");
            }
        }

        /// <summary>연도/월/업체 콤보박스의 선택 목록을 최신 데이터 기준으로 다시 채운다.</summary>
        public void RefreshData()
        {
            try
            {
                // 날짜 기본 설정은 init()에서 처리하므로 여기서는 변경하지 않음.

                // 업체 목록도 연도와 같은 방식으로 이전 선택값을 최대한 유지
                var prevClient = cboClient.SelectedItem?.ToString();
                cboClient.Items.Clear();
                cboClient.Items.Add("전체 업체");
                foreach (var c in DataStore.Clients) cboClient.Items.Add(c.Name);
                cboClient.SelectedItem = prevClient != null && cboClient.Items.Contains(prevClient) ? prevClient : "전체 업체";

                UpdateModeButtons();
                RenderStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "오류");
            }
        }

        /// <summary>현재 선택된 필터(연도/월/업체/모드) 기준으로 그리드와 요약 정보, 차트를 다시 그린다.</summary>
        private void RenderStatus()
        {
            if (grid == null) return;

            try
            {
                IEnumerable<Job> list = DataStore.Jobs;

                // 기간 필터 적용 (dtpFrom ~ dtpTo, 날짜 포함)
                if (dtpFrom != null && dtpTo != null)
                {
                    var from = dtpFrom.Value.Date;
                    var to = dtpTo.Value.Date;
                    list = list.Where(j => j.Date.Date >= from && j.Date.Date <= to);
                }
                // 업체 필터 적용
                if (cboClient.SelectedItem != null && cboClient.SelectedItem.ToString() != "전체 업체")
                {
                    var name = cboClient.SelectedItem.ToString();
                    list = list.Where(j => DataStore.ClientName(j.ClientId) == name);
                }

                var jobs = list.ToList();
                if (_mode == "unpaid")
                    jobs = jobs.Where(j => j.Amount - DataStore.PaidOf(j.Id) > 0).ToList();

                grid.Rows.Clear();
                decimal total = 0, paidSum = 0;
                foreach (var j in jobs.OrderBy(j => j.Date))
                {
                    var paid = DataStore.PaidOf(j.Id);
                    var remain = j.Amount - paid;
                    total += j.Amount; paidSum += paid;
                    string status = remain <= 0 ? "완료" : paid > 0 ? "일부수금" : "미수금";
                    // 행 추가 후 상태에 따라 스타일 적용
                    int rowIndex = grid.Rows.Add(DataStore.ClientName(j.ClientId), j.Date.ToString("yyyy-MM-dd"),
                        j.Amount.ToString("N0") + "원", paid.ToString("N0") + "원", remain.ToString("N0") + "원", status);
                    var row = grid.Rows[rowIndex];
                    if (status == "미수금")
                    {
                        // 상태 컬럼(Status) 셀만 스타일 적용
                        var statusCell = row.Cells["Status"] ?? row.Cells[5];
                        statusCell.Style.BackColor = Color.FromArgb(220, 38, 38);
                        statusCell.Style.ForeColor = Color.White;
                        // 선택 시에도 동일하게 보이도록 선택 스타일도 설정
                        statusCell.Style.SelectionBackColor = Color.FromArgb(220, 38, 38);
                        statusCell.Style.SelectionForeColor = Color.White;
                    }
                }

                lblTotal.Text = "₩ " + total.ToString("N0");
                lblPaid.Text = "₩ " + paidSum.ToString("N0");
                lblUnpaid.Text = "₩ " + (total - paidSum).ToString("N0");
                lblClientCount.Text = DataStore.Clients.Count + " 개";

                RenderChart(jobs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "RenderStatus 오류");
            }
        }

        /// <summary>"전체 조회"/"미수금만 보기" 버튼의 선택 표시(배경색/글자색)를 현재 모드에 맞게 바꾼다.</summary>
        private void UpdateModeButtons()
        {
            btnAll.BackColor = _mode == "all" ? Color.FromArgb(30, 41, 59) : Color.FromArgb(238, 241, 245);
            btnAll.ForeColor = _mode == "all" ? Color.White : Color.Black;
            btnUnpaid.BackColor = _mode == "unpaid" ? Color.FromArgb(220, 38, 38) : Color.FromArgb(253, 232, 232);
            btnUnpaid.ForeColor = _mode == "unpaid" ? Color.White : Color.FromArgb(220, 38, 38);
        }

        /// <summary>전달받은 업무 목록을 월별로 합산해서 막대그래프로 그린다.</summary>
        private void RenderChart(List<Job> jobs)
        {
            try
            {
                chart.Series.Clear(); // 이전에 그렸던 막대 데이터를 지우고 새로 그림

                var series = new Series("월별")
                {
                    ChartType = SeriesChartType.Column, // 막대그래프
                    Color = _mode == "unpaid" ? Color.FromArgb(220, 38, 38) : Color.FromArgb(37, 99, 235)
                };
                // 명시적으로 사용할 ChartArea를 지정(디자이너와 코드에서 생성된 영역 이름 불일치 방지)
                series.ChartArea = chart.ChartAreas.Count > 0 ? chart.ChartAreas[0].Name : "main";
                // 문자열 X값(범주형)을 인덱스로 처리하여 여러 막대가 모두 표시되도록 설정
                series.IsXValueIndexed = true;
                series["PointWidth"] = "0.6";

                // 같은 연-월끼리 묶어서(GroupBy) 합계를 구한 뒤, 날짜 순으로 정렬
                var grouped = jobs
                    .GroupBy(j => new { j.Date.Year, j.Date.Month })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .Select(g => new
                    {
                        Label = $"{g.Key.Year}-{g.Key.Month:00}", // 예: "2026-07"
                        Value = _mode == "unpaid"
                            ? g.Sum(j => j.Amount - DataStore.PaidOf(j.Id)) // 미수금 합계
                            : g.Sum(j => j.Amount)                           // 업무 금액 합계
                    });

                foreach (var g in grouped)
                {
                    // Chart control expects numeric Y as double; 안전하게 변환
                    double y = 0;
                    try { y = Convert.ToDouble(g.Value); } catch { y = 0; }
                    series.Points.AddXY(g.Label, y); // X축: 연-월, Y축: 금액
                }

                chart.Series.Add(series);

                // X축 간격 강제 설정 및 Y축 숫자 포맷(천단위 콤마) 적용 후 갱신
                if (chart.ChartAreas.Count > 0)
                {
                    var area = chart.ChartAreas[0];
                    area.AxisX.Interval = 1;
                    area.AxisY.LabelStyle.Format = "N0"; // 예: 1,234,567
                    area.RecalculateAxesScale();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "오류");
            }
        }
    }
}