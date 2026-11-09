using ReceivableMng.Data;

namespace ReceivableMng.Views
{
    public partial class PaymentsView : UserControl
    {
        // 현재 조회 모드
        // all    : 전체 입금 내역
        // unpaid : 미수금이 남아있는 내역만
        private string _mode = "all";

        public PaymentsView()
        {
            InitializeComponent();

            Init();
        }
        
        private void Init()
        {
            ccClient.SelectionChanged += (s, e) => RenderPayments();

            // "전체" 버튼을 클릭하면 전체 입금 내역을 조회한다.
            btnAll.Click += (s, e) =>
            {
                _mode = "all";
                UpdateModeButtons();
                RenderPayments();
            };

            // "미수금" 버튼을 클릭하면 아직 입금이 완료되지 않은 내역만 조회한다.
            btnUnpaid.Click += (s, e) =>
            {
                _mode = "unpaid";
                UpdateModeButtons();
                RenderPayments();
            };

            GridSetting();

            // DataStore의 데이터가 변경되면 화면도 다시 그린다.
            DataStore.Changed += RefreshData;

            RefreshData();
        }

        /// <summary>
        /// 입금 목록을 보여주는 DataGridView의 기본 설정과 컬럼을 구성한다.
        /// </summary>
        private void GridSetting()
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.CellSelect;

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Client",
                HeaderText = "업체",
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "일자",
                Width = 60,
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Memo",
                HeaderText = "내용",
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "총 금액",
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Paid",
                HeaderText = "입금액",
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Remain",
                HeaderText = "남은 금액",
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Progress",
                HeaderText = "진행률",
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Input",
                HeaderText = "입금 입력",
                Width = 150,

                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightGoldenrodYellow,
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Save",
                HeaderText = ""
            });
            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Complete",
                HeaderText = ""
            });

            grid.CellContentClick += Grid_CellContentClick;


            // 입금 입력 컬럼을 한 번 클릭하면 바로 편집 모드로 진입
            grid.CellClick += (s, e) =>
            {
                // 헤더 클릭은 제외
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                // 입금 입력 컬럼인지 확인
                if (grid.Columns[e.ColumnIndex].Name == "Input")
                {
                    // 셀 편집 시작
                    grid.BeginEdit(true);
                }
            };
        }

        /// <summary>
        /// 현재 조회 모드에 맞춰 전체 / 미수금 버튼의 색상을 변경한다.
        /// </summary>
        private void UpdateModeButtons()
        {
            // "전체" 버튼이 현재 선택된 상태인지 확인한다.
            bool isAllMode = _mode == "all";

            // 전체 버튼의 배경색을 현재 상태에 맞게 변경한다.
            btnAll.BackColor = isAllMode
                ? Color.FromArgb(30, 41, 59)
                : Color.FromArgb(238, 241, 245);

            // 전체 버튼의 글자색도 현재 상태에 맞게 변경한다.
            btnAll.ForeColor = isAllMode
                ? Color.White
                : Color.Black;

            // "미수금" 버튼이 현재 선택된 상태인지 확인한다.
            bool isUnpaidMode = _mode == "unpaid";

            // 미수금 버튼의 배경색을 현재 상태에 맞게 변경한다.
            btnUnpaid.BackColor = isUnpaidMode
                ? Color.FromArgb(220, 38, 38)
                : Color.FromArgb(253, 232, 232);

            // 미수금 버튼의 글자색도 현재 상태에 맞게 변경한다.
            btnUnpaid.ForeColor = isUnpaidMode
                ? Color.White
                : Color.FromArgb(220, 38, 38);
        }

        /// <summary>
        /// 입금 입력 / 저장 / 완료처리 버튼을 클릭했을 때 실행된다.
        /// </summary>
        private async void Grid_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // 헤더를 클릭한 경우 RowIndex가 -1이므로 아무 작업도 하지 않는다.
            if (e.RowIndex < 0)
                return;

            // 사용자가 입금 입력칸에 값을 입력한 후 바로 저장 버튼을 누르는 경우
            // 편집 중인 값을 실제 셀에 먼저 반영한다.
            grid.EndEdit();

            // 사용자가 클릭한 현재 행을 가져온다.
            var row = grid.Rows[e.RowIndex];

            // 각 행의 Tag에는 해당 작업(Job)의 Id를 저장해 두었다.
            // Job Id가 없으면 어떤 작업인지 알 수 없으므로 종료한다.
            if (row.Tag is not int jobId)
                return;

            // 사용자가 클릭한 컬럼의 이름을 가져온다.
            var columnName = grid.Columns[e.ColumnIndex].Name;

            // 저장 또는 완료처리 버튼을 누른 경우만 처리한다.
            if (columnName != "Save" && columnName != "Complete")
                return;

            // 완료된 행은 버튼의 값이 비어 있으므로 아무 작업도 하지 않는다.
            if (string.IsNullOrEmpty(row.Cells[columnName].Value?.ToString()))
                return;

            // 화면의 Job Id를 이용하여 실제 작업 데이터를 찾는다.
            var job = DataStore.Jobs.First(j => j.Id == jobId);

            // 지금까지 입금된 금액을 가져온다.
            var paidAmount = DataStore.PaidOf(job.Id);

            // 전체 작업 금액에서 지금까지 입금된 금액을 빼서 남은 금액을 계산한다.
            var remainAmount = job.Amount - paidAmount;

            // ---------------------------------------------
            // 저장 버튼을 클릭한 경우
            // ---------------------------------------------
            if (columnName == "Save")
            {
                // 사용자가 입력한 입금 금액을 가져온다.
                var inputText = row.Cells["Input"].Value?.ToString();

                // 숫자로 변환할 수 없거나 0 이하라면 잘못된 입력이다.
                if (!decimal.TryParse(inputText, out var inputAmount) || inputAmount <= 0)
                {
                    MessageBox.Show("입금액을 입력해주세요");
                    return;
                }

                // 남은 금액보다 많은 금액을 입금할 수 없도록 한다.
                if (inputAmount > remainAmount)
                {
                    MessageBox.Show(
                        $"남은 금액({remainAmount:N0}원)을 초과할 수 없습니다");

                    return;
                }

                // 입력한 금액만큼 입금 내역을 저장한다.
                await DataStore.AddPaymentAsync(
                    jobId,
                    DateTime.Now,
                    inputAmount);
            }
            // ---------------------------------------------
            // 완료처리 버튼을 클릭한 경우
            // ---------------------------------------------
            else
            {
                // 이미 입금이 완료된 경우에는 아무 작업도 하지 않는다.
                if (remainAmount <= 0)
                    return;

                // 남아있는 금액 전체를 한 번에 입금 처리한다.
                await DataStore.AddPaymentAsync(
                    jobId,
                    DateTime.Now,
                    remainAmount);
            }
        }

        /// <summary>
        /// DataStore의 최신 업체 목록과 입금 데이터를 화면에 다시 표시한다.
        /// </summary>
        public void RefreshData()
        {
            // 업체 선택 콤보박스에 현재 등록된 업체 목록을 설정한다.
            // (업체 Id, 업체명) 형태로 전달한다.
            ccClient.SetItems(
                DataStore.Clients.Select(client => (client.Id, client.Name)));

            // 현재 조회 조건에 맞는 입금 목록을 다시 그린다.
            RenderPayments();
        }

        /// <summary>
        /// 현재 선택된 업체와 조회 모드에 맞춰 입금 목록을 DataGridView에 표시한다.
        /// </summary>
        private void RenderPayments()
        {
            // 화면이 아직 완전히 초기화되지 않은 경우에는 그리드를 사용할 수 없으므로 종료한다.
            if (grid == null)
                return;

            // 업체 선택 컨트롤에서 현재 체크된 업체 Id를 가져온다.
            var selectedClientIds = ccClient.GetCheckedIds();

            // 전체 작업 목록을 조회 대상으로 만든다.
            var jobs = DataStore.Jobs.AsEnumerable();

            // 업체가 선택되어 있다면 해당 업체의 작업만 남긴다.
            // 아무 업체도 선택하지 않은 경우에는 전체 업체를 의미한다.
            if (selectedClientIds.Count > 0)
            {
                jobs = jobs.Where(job =>
                    selectedClientIds.Contains(job.ClientId));
            }
            // 진행률이 100%인(이미 완료된) 작업은 이 그리드에서 조회할 필요가 없으므로
            // 조회 대상 자체에서 제외한다. (버튼이 비활성화되는 행을 미리 제거)
            jobs = jobs.Where(job => job.Amount - DataStore.PaidOf(job.Id) > 0);

            // 기존에 표시되어 있던 행을 모두 제거한다.
            grid.Rows.Clear();

            // 작업 날짜가 최신인 순서대로 화면에 표시한다.
            foreach (var job in jobs.OrderByDescending(job => job.Date))
            {
                // 지금까지 입금된 금액을 계산한다.
                var paidAmount = DataStore.PaidOf(job.Id);

                // 전체 작업 금액에서 입금액을 빼서 남은 금액을 계산한다.
                var remainAmount = job.Amount - paidAmount;

                // 전체 금액 중 몇 %가 입금되었는지 계산한다.
                // 작업 금액이 0인 경우에는 0%로 처리한다.
                var progressPercent = job.Amount == 0
                    ? 0
                    : (int)Math.Round(
                        paidAmount / job.Amount * 100);

                // DataGridView에 한 행을 추가한다.
                int rowIndex = grid.Rows.Add(
                    // 업체명
                    DataStore.ClientName(job.ClientId),

                    // 작업 날짜
                    job.Date.ToString("yyyy-MM-dd"),

                    // 작업 내용이 없으면 "-" 표시
                    string.IsNullOrEmpty(job.Memo)
                        ? "-"
                        : job.Memo,

                    // 전체 금액
                    job.Amount.ToString("N0") + "원",

                    // 입금된 금액
                    paidAmount.ToString("N0") + "원",

                    // 남은 금액
                    remainAmount.ToString("N0") + "원",

                    // 진행률
                    progressPercent + "%",

                    // 사용자가 직접 입력하는 입금 금액
                    "",

                    remainAmount > 0 ? "저장" : "",
                    remainAmount > 0 ? "완료처리" : ""
                );

                // 현재 행의 Tag에 실제 Job Id를 저장한다.
                // 나중에 저장/완료 버튼을 클릭했을 때 어떤 작업인지 찾기 위해 사용한다.
                grid.Rows[rowIndex].Tag = job.Id;

                // 이미 입금이 모두 완료된 작업이라면
                // 입금 입력 칸도 수정할 수 없도록 잠근다.
                if (remainAmount <= 0)
                {
                    grid.Rows[rowIndex].Cells["Input"].ReadOnly = true;
                }
            }
        }
    }
}