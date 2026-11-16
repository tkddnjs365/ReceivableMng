using ReceivableMng.Models;

namespace ReceivableMng.Data
{
    /// <summary>
    /// 화면(View)들이 사용하는 데이터 창구.
    /// 로컬 SQLite 파일(receivable.db)에 데이터를 저장합니다.
    /// 화면 코드 입장에서는 DataStore.XxxAsync() 만 호출하면 됩니다.
    /// </summary>
    public static class DataStore
    {
        /// <summary>메모리에 올려 둔 업체 목록. 화면들은 DB 대신 이 목록을 읽어서 보여준다.</summary>
        public static List<Client> Clients { get; private set; } = new();

        /// <summary>메모리에 올려 둔 업무 목록.</summary>
        public static List<Job> Jobs { get; private set; } = new();

        /// <summary>메모리에 올려 둔 입금 목록.</summary>
        public static List<Payment> Payments { get; private set; } = new();

        /// <summary>데이터가 바뀔 때마다 발생하는 이벤트. 각 화면이 구독해서 자동 새로고침합니다.</summary>
        public static event Action Changed; // 매개변수 없는 이벤트. 데이터 변경을 "알림"만 해줌

        // 실제 저장소 구현체(SQLite)를 들고 있는 필드
        private static IStorageProvider _provider;

        /// <summary>앱 시작 시 한 번 호출 (동기 래퍼).</summary>
        // Main() 같은 동기 메서드에서 비동기 메서드를 기다리기 위한 트릭
        // GetAwaiter().GetResult()는 await과 비슷하게 결과가 끝날 때까지 블로킹(대기)함
        public static void Load() => LoadAsync().GetAwaiter().GetResult();

        /// <summary>SQLite 저장소를 준비(DB 파일/테이블 생성)하고 데이터를 처음으로 읽어온다.</summary>
        public static async Task LoadAsync()
        {
            try
            {
                var provider = new SqliteStorageProvider();
                await provider.InitializeAsync(); // DB 파일이 없으면 만들고, 테이블도 준비
                _provider = provider;
            }
            catch (Exception ex)
            {
                // 저장소 없이는 프로그램을 쓸 수 없으므로, 안내 후 종료
                MessageBox.Show(
                    $"데이터베이스(SQLite)를 여는 데 실패했습니다.\n\n{ex.Message}",
                    "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // 저장소가 준비됐으니 실제 데이터를 한 번 읽어옴
            await RefreshAllAsync();
        }

        /// <summary>
        /// 저장소(_provider)로부터 최신 데이터를 다시 받아와 목록을 갱신하는 내부 전용 메서드.
        /// 갱신이 끝나면 Changed 이벤트로 화면들에게 알려 준다.
        /// </summary>
        private static async Task RefreshAllAsync()
        {
            Clients = await _provider.GetClientsAsync();
            Jobs = await _provider.GetJobsAsync();
            Payments = await _provider.GetPaymentsAsync();
            Changed?.Invoke(); // 구독 중인 화면들에게 "데이터 바뀌었다"고 알림 (null이면 호출 안 함)
        }

        /// <summary>
        /// 저장소에 변경 작업(추가/수정/삭제)을 요청하고, 성공하면 목록을 새로고침하는 공통 처리 메서드.
        /// </summary>
        /// <param name="action">실행할 작업. Func&lt;Task&gt; 는 "실행하면 Task를 반환하는 메서드"를 매개변수로 받는다는 뜻</param>
        private static async Task RunAsync(Func<Task> action)
        {
            try
            {
                await action();          // 실제 추가/수정/삭제 작업 실행
                await RefreshAllAsync();  // 성공했으니 화면에 보여줄 데이터도 최신으로 갱신
            }
            catch (Exception ex)
            {
                // 어떤 작업이든 실패하면 여기서 한 번에 에러 메시지를 보여줌
                MessageBox.Show($"작업 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 아래 메서드들은 화면(View)에서 직접 호출하는 공개 API
        // 전부 RunAsync로 감싸서, 성공/실패 처리와 새로고침 로직을 중복해서 안 써도 되게 함

        /// <summary>업체를 새로 등록한다.</summary>
        public static Task AddClientAsync(string name, string email) =>
            RunAsync(() => _provider.AddClientAsync(name, email));

        /// <summary>기존 업체의 이름/이메일을 수정한다.</summary>
        public static Task UpdateClientAsync(int id, string name, string email) =>
            RunAsync(() => _provider.UpdateClientAsync(id, name, email));

        /// <summary>업체를 삭제한다.</summary>
        public static Task DeleteClientAsync(int id) =>
            RunAsync(() => _provider.DeleteClientAsync(id));

        /// <summary>업무(작업 내역)를 새로 등록한다.</summary>
        public static Task AddJobAsync(int clientId, DateTime date, decimal amount, string memo) =>
            RunAsync(() => _provider.AddJobAsync(clientId, date, amount, memo));

        /// <summary>입금(수금) 내역을 새로 등록한다.</summary>
        public static Task AddPaymentAsync(int jobId, DateTime date, decimal amount) =>
            RunAsync(() => _provider.AddPaymentAsync(jobId, date, amount));

        /// <summary>특정 업무(jobId)에 대해 지금까지 입금된 금액 합계</summary>
        public static decimal PaidOf(int jobId) => Payments.Where(p => p.JobId == jobId).Sum(p => p.Amount);

        /// <summary>업체 번호로 업체명을 찾음. 없으면 "-" 반환</summary>
        public static string ClientName(int clientId) => Clients.FirstOrDefault(c => c.Id == clientId)?.Name ?? "-";
    }
}