using ReceivableMng.Data;

namespace ReceivableMng.Views
{
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();

            init();
        }

        /// <summary>
        /// 업체 관리 화면에서 사용하는 이벤트 연결,
        /// 그리드 설정 및 초기 데이터 로딩을 처리한다.
        /// </summary>
        public void init()
        {
            // 업체 추가 버튼을 클릭하면 AddClient()를 비동기로 실행한다.
            btnAdd.Click += async (s, e) => await AddClient();

            GridSetting();

            // DataStore의 데이터가 변경되면 업체 목록을 다시 화면에 표시한다.
            DataStore.Changed += RefreshData;

            RefreshData();
        }

        /// <summary>
        /// 업체 목록을 보여주는 DataGridView의 열과 기본 동작을 설정한다.
        /// </summary>
        void GridSetting()
        {
            grid.ReadOnly = true;

            // 사용자가 그리드에 새로운 빈 행을 직접 추가할 수 없도록 한다.
            grid.AllowUserToAddRows = false;

            // 사용자가 그리드에서 행을 직접 삭제할 수 없도록 한다.
            // 삭제는 "삭제" 버튼을 통해서만 처리한다.
            grid.AllowUserToDeleteRows = false;

            // 그리드 왼쪽에 표시되는 행 번호 영역을 숨긴다.
            grid.RowHeadersVisible = false;

            // 그리드의 전체 너비에 맞춰 각 열의 너비를 자동으로 조절한다.
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 셀 하나가 아닌 행 전체가 선택되도록 설정한다.
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grid.Columns.Add("Name", "업체명");
            grid.Columns.Add("Email", "메일 주소");
            grid.Columns.Add("CreatedAt", "등록일");
            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "",
                Width = 70
            });
            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "",
                Width = 70
            });

            grid.CellContentClick += Grid_CellContentClick;
        }

        /// <summary>
        /// 입력된 업체명과 메일 주소를 이용하여 새로운 업체를 등록한다.
        /// </summary>
        private async Task AddClient()
        {
            var name = txtName.Text.Trim();
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("업체명을 입력해주세요");
                return;
            }

            // 입력받은 업체 정보를 DataStore에 비동기로 저장한다.
            await DataStore.AddClientAsync(name, email);

            txtName.Clear();
            txtEmail.Clear();
        }

        /// <summary>
        /// 업체 목록의 수정 또는 삭제 버튼을 클릭했을 때 실행된다.
        /// </summary>
        private async void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 헤더 영역을 클릭한 경우 RowIndex가 -1이므로 아무 작업도 하지 않는다.
            if (e.RowIndex < 0) return;

            // 사용자가 클릭한 실제 행을 가져온다.
            var row = grid.Rows[e.RowIndex];

            // 각 행의 Tag에는 업체 ID를 저장해 두었다.
            // Tag에 정수형 업체 ID가 없다면 더 이상 진행하지 않는다.
            if (row.Tag is not int clientId) return;

            // 업체 ID를 이용하여 DataStore에서 해당 업체 정보를 찾는다.
            var client = DataStore.Clients.Find(c => c.Id == clientId);

            // 해당 업체를 찾지 못했다면 더 이상 진행하지 않는다.
            if (client == null) return;

            // 사용자가 클릭한 버튼의 열 이름을 가져온다.
            var colName = grid.Columns[e.ColumnIndex].Name;

            // "삭제" 버튼을 클릭한 경우
            if (colName == "Delete")
            {
                // 업체를 삭제하면 관련 업무도 함께 삭제된다는 내용을 사용자에게 알려준다.
                var confirm = MessageBox.Show(
                    "이 업체를 삭제할까요? 관련 업무도 함께 삭제됩니다.",
                    "확인",
                    MessageBoxButtons.YesNo);

                // 사용자가 "아니오"를 선택하면 삭제하지 않고 종료한다.
                if (confirm != DialogResult.Yes) return;

                // 확인을 눌렀다면 해당 업체를 비동기로 삭제한다.
                await DataStore.DeleteClientAsync(client.Id);
            }
            // "수정" 버튼을 클릭한 경우
            else if (colName == "Edit")
            {
                // 현재 업체명과 메일 주소를 수정 화면에 전달하여 다이얼로그를 생성한다.
                using var dlg = new ClientEditDialog(client.Name, client.Email);

                // 수정 화면에서 "확인"을 눌렀을 때만 업체 정보를 변경한다.
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // 수정된 업체명과 메일 주소를 DataStore에 저장한다.
                    await DataStore.UpdateClientAsync(
                        client.Id,
                        dlg.ClientName,
                        dlg.ClientEmail);
                }
            }
        }

        /// <summary>
        /// DataStore에 저장된 업체 목록을 가져와 그리드에 다시 표시한다.
        /// </summary>
        public void RefreshData()
        {
            // 기존에 그리드에 표시되어 있던 업체 목록을 모두 제거한다.
            grid.Rows.Clear();

            // DataStore에 저장되어 있는 모든 업체를 하나씩 가져온다.
            foreach (var c in DataStore.Clients)
            {
                // 업체 정보를 그리드에 한 행 추가한다.
                //
                // 메일 주소가 없는 경우에는 "-"를 표시한다.
                // 등록일은 yyyy-MM-dd 형식으로 표시한다.
                int idx = grid.Rows.Add(
                    c.Name,
                    string.IsNullOrEmpty(c.Email) ? "-" : c.Email,
                    c.CreatedAt.ToString("yyyy-MM-dd"),
                    "수정",
                    "삭제");

                // 화면에는 보이지 않지만 해당 행에 업체 ID를 저장한다.
                // 나중에 수정/삭제 버튼을 눌렀을 때
                // 어떤 업체를 선택했는지 찾기 위해 사용한다.
                grid.Rows[idx].Tag = c.Id;
            }
        }
    }
}
