using ReceivableMng.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceivableMng.Data
{
    /// <summary>
    /// 화면(View)들이 사용하는 데이터 창구.
    /// supabase-config.json 이 있으면 Supabase(클라우드)를, 없으면 로컬 data.json 파일을 사용합니다.
    /// 화면 코드 입장에서는 어느 쪽이든 DataStore.XxxAsync() 만 호출하면 됩니다.
    /// </summary>
    public static class DataStore
    {
        public static List<Client> Clients { get; private set; } = new();
        public static List<Job> Jobs { get; private set; } = new();
        public static List<Payment> Payments { get; private set; } = new();

        /// <summary>데이터가 바뀔 때마다 발생하는 이벤트. 각 화면이 구독해서 자동 새로고침합니다.</summary>
        public static event Action Changed; // 매개변수 없는 이벤트. 데이터 변경을 "알림"만 해줌

        /// <summary>true면 Supabase 클라우드 모드, false면 로컬 JSON 파일 모드.</summary>
        public static bool IsCloudMode { get; private set; }

        // 실제 저장소 구현체(로컬 or 클라우드)를 들고 있는 필드. 인터페이스 타입이라 둘 다 담을 수 있음
        private static IStorageProvider _provider;

        /// <summary>앱 시작 시 한 번 호출 (동기 래퍼).</summary>
        // Main() 같은 동기 메서드에서 비동기 메서드를 기다리기 위한 트릭
        // GetAwaiter().GetResult()는 await과 비슷하게 결과가 끝날 때까지 블로킹(대기)함
        public static void Load() => LoadAsync().GetAwaiter().GetResult();

        public static async Task LoadAsync()
        {
            // 설정 파일(supabase-config.json)을 읽어서 클라우드 접속 정보가 있는지 확인
            var config = SupabaseConfig.LoadFromFile();
            bool cloudConfigured = config != null
                && !string.IsNullOrWhiteSpace(config.Url)
                && !string.IsNullOrWhiteSpace(config.Key);

            if (cloudConfigured)
            {
                try
                {
                    // Supabase 접속 정보가 있으면 클라우드 저장소로 연결 시도
                    var cloudProvider = new SupabaseStorageProvider(config.Url, config.Key);
                    await cloudProvider.InitializeAsync();
                    _provider = cloudProvider;
                    IsCloudMode = true;
                }
                catch (Exception ex)
                {
                    // 접속 실패 시 프로그램을 멈추지 않고, 로컬 모드로 넘어가기 위해 경고만 띄움
                    MessageBox.Show(
                        $"Supabase 연결에 실패했습니다. 로컬 저장 모드로 전환합니다.\n\n{ex.Message}",
                        "Supabase 연결 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // 클라우드 설정이 없거나, 위에서 연결에 실패했다면 로컬 파일 저장소를 사용
            if (_provider == null)
            {
                var localProvider = new LocalJsonStorageProvider();
                await localProvider.InitializeAsync();
                _provider = localProvider;
                IsCloudMode = false;
            }

            // 저장소가 결정됐으니 실제 데이터를 한 번 읽어옴
            await RefreshAllAsync();
        }

        // 저장소(_provider)로부터 최신 데이터를 다시 받아와 목록을 갱신하는 내부 전용 메서드
        private static async Task RefreshAllAsync()
        {
            Clients = await _provider.GetClientsAsync();
            Jobs = await _provider.GetJobsAsync();
            Payments = await _provider.GetPaymentsAsync();
            Changed?.Invoke(); // 구독 중인 화면들에게 "데이터 바뀌었다"고 알림 (null이면 호출 안 함)
        }

        // 저장소에 변경 작업(추가/수정/삭제)을 요청하고, 성공하면 목록을 새로고침하는 공통 처리 메서드
        // Func<Task>: "실행하면 Task를 반환하는 메서드"를 매개변수로 받는다는 뜻
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