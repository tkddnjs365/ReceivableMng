using ReceivableMng.Data;
using System.Data;

namespace ReceivableMng.Views
{
    public partial class JobsView : UserControl
    {
        public JobsView()
        {
            InitializeComponent();
            init();
        }

        /// <summary>
        /// 업무 등록 화면에서 사용하는 이벤트 연결,
        /// 그리드 설정 및 초기 데이터 로딩을 처리한다.
        /// </summary>
        public void init()
        {
            btnAdd.Click += async (s, e) => await AddJob();

            GridSetting();

            // DataStore의 데이터가 변경되면 업체 목록을 다시 화면에 표시한다.
            DataStore.Changed += RefreshData;

            RefreshData();
        }

        /// <summary>
        /// 업무 목록을 보여주는 DataGridView의 열과 기본 동작을 설정한다.
        /// </summary>
        void GridSetting()
        {
            grid.ReadOnly = true;

            // 사용자가 그리드에 새로운 빈 행을 직접 추가할 수 없도록 한다.
            grid.AllowUserToAddRows = false;

            // 사용자가 그리드에서 행을 직접 삭제할 수 없도록 한다.
            // 업무 삭제가 필요한 경우 별도의 기능을 통해 처리한다.
            grid.AllowUserToDeleteRows = false;

            // 그리드 왼쪽에 표시되는 행 번호 영역을 숨긴다.
            grid.RowHeadersVisible = false;

            // 그리드의 전체 너비에 맞춰 각 열의 너비를 자동으로 조절한다.
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 셀 하나가 아닌 행 전체가 선택되도록 설정한다.
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grid.Columns.Add("Client", "업체");
            grid.Columns.Add("Date", "일자");
            grid.Columns.Add("Memo", "내용");
            grid.Columns.Add("Amount", "금액");
            grid.Columns.Add("Status", "입금상태");
        }

        /// <summary>
        /// 선택한 업체의 업무 정보를 등록한다.
        /// </summary>
        private async Task AddJob()
        {
            // 업체를 선택하지 않은 경우 업무를 등록할 수 없도록 한다.
            if (cboClient.SelectedItem == null)
            {
                MessageBox.Show("업체를 선택해주세요");
                return;
            }

            // 입력한 금액이 숫자가 아니거나 0 이하인 경우 등록하지 않는다.
            if (!decimal.TryParse(txtAmount.Text, out var amount) || amount <= 0)
            {
                MessageBox.Show("금액을 올바르게 입력해주세요");
                return;
            }

            // 콤보박스에서 현재 선택된 업체명을 가져온다.
            var clientName = cboClient.SelectedItem.ToString();

            // 업체명으로 DataStore에서 실제 업체 정보를 찾는다.
            var client = DataStore.Clients.First(c => c.Name == clientName);

            // 선택한 업체의 업무 정보를 DataStore에 비동기로 저장한다.
            await DataStore.AddJobAsync(
                client.Id,
                dtDate.Value.Date,
                amount,
                txtMemo.Text.Trim());

            // 업무 등록이 완료되면 금액 입력란을 비운다.
            txtAmount.Clear();

            // 업무 등록이 완료되면 내용 입력란을 비운다.
            txtMemo.Clear();
        }

        /// <summary>
        /// DataStore에 저장된 업체와 업무 정보를 가져와 화면을 다시 표시한다.
        /// </summary>
        public void RefreshData()
        {
            // 현재 선택되어 있는 업체명을 임시로 저장한다.
            // 데이터를 다시 불러온 후에도 기존 선택을 최대한 유지하기 위해 사용한다.
            var prev = cboClient.SelectedItem?.ToString();

            // 기존 업체 목록을 모두 제거한다.
            cboClient.Items.Clear();

            // DataStore에 저장된 업체를 콤보박스에 하나씩 추가한다.
            foreach (var c in DataStore.Clients)
                cboClient.Items.Add(c.Name);

            // 이전에 선택했던 업체가 아직 존재한다면 해당 업체를 다시 선택한다.
            if (prev != null && cboClient.Items.Contains(prev))
                cboClient.SelectedItem = prev;

            // 이전에 선택했던 업체가 없고 업체가 하나 이상 존재한다면
            // 첫 번째 업체를 기본으로 선택한다.
            else if (cboClient.Items.Count > 0)
                cboClient.SelectedIndex = 0;

            // 기존에 그리드에 표시되어 있던 업무 목록을 모두 제거한다.
            grid.Rows.Clear();

            // DataStore에 저장된 업무를 날짜가 최신인 순서대로 가져온다.
            foreach (var j in DataStore.Jobs.OrderByDescending(j => j.Date))
            {
                // 해당 업무에서 지금까지 받은 금액을 가져온다.
                var paid = DataStore.PaidOf(j.Id);

                // 업무 금액에서 받은 금액을 빼서 현재 미수금을 계산한다.
                var remain = j.Amount - paid;

                // 입금 금액과 미수금에 따라 현재 상태를 결정한다.
                string status = remain <= 0
                    ? "완료"
                    : paid > 0
                        ? "일부수금"
                        : "미수금";

                // 계산된 업무 정보를 그리드에 한 행씩 추가한다.
                //
                // 업체명, 업무일, 업무 내용, 업무 금액, 입금 상태 순서로 표시한다.
                // 업무 내용이 없는 경우에는 "-"를 표시한다.
                grid.Rows.Add(
                    DataStore.ClientName(j.ClientId),
                    j.Date.ToString("yyyy-MM-dd"),
                    string.IsNullOrEmpty(j.Memo) ? "-" : j.Memo,
                    j.Amount.ToString("N0") + "원",
                    status);
            }
        }
    }
}