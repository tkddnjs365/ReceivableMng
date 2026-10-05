using ReceivableMng.Models;

namespace ReceivableMng.Data
{
    /// <summary>
    /// Supabase(Postgres)를 데이터 저장소로 사용하는 Provider.
    /// 우리 도메인 모델 클래스 이름이 "Client"라서 Supabase 라이브러리의 Client 타입과 이름이 겹칩니다.
    /// 그래서 이 파일에서는 라이브러리 쪽 타입만 Supabase.Client 로 완전한 이름을 써서 구분했습니다.
    /// </summary>
    public class SupabaseStorageProvider : IStorageProvider
    {
        // Supabase 서버와 실제로 통신하는 클라이언트 객체
        private readonly Supabase.Client _client;

        /// <summary>Supabase 프로젝트 주소(url)와 키(key)로 접속 클라이언트를 만든다.</summary>
        public SupabaseStorageProvider(string url, string key)
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false // 실시간 구독 기능은 쓰지 않으므로 꺼서 리소스 절약
            };
            _client = new Supabase.Client(url, key, options);
        }

        /// <summary>Supabase 서버에 실제로 접속한다.</summary>
        public async Task InitializeAsync()
        {
            await _client.InitializeAsync();
        }

        /// <summary>clients 테이블의 전체 데이터를 가져온다.</summary>
        public async Task<List<Client>> GetClientsAsync()
        {
            var result = await _client.From<Client>().Get(); // SELECT * FROM clients 와 같은 역할
            return result.Models;
        }

        /// <summary>jobs 테이블의 전체 데이터를 가져온다.</summary>
        public async Task<List<Job>> GetJobsAsync()
        {
            var result = await _client.From<Job>().Get();
            return result.Models;
        }

        /// <summary>payments 테이블의 전체 데이터를 가져온다.</summary>
        public async Task<List<Payment>> GetPaymentsAsync()
        {
            var result = await _client.From<Payment>().Get();
            return result.Models;
        }

        /// <summary>새 업체 한 건을 clients 테이블에 추가한다.</summary>
        public async Task AddClientAsync(string name, string email)
        {
            await _client.From<Client>().Insert(new Client
            {
                Name = name,
                Email = email,
                CreatedAt = DateTime.Now
            });
        }

        /// <summary>id에 해당하는 업체의 이름/이메일을 수정한다.</summary>
        public async Task UpdateClientAsync(int id, string name, string email)
        {
            await _client.From<Client>()
                .Where(x => x.Id == id)   // WHERE id = ... 와 같은 조건
                .Set(x => x.Name, name)   // SET name = ...
                .Set(x => x.Email, email) // SET email = ...
                .Update();
        }

        /// <summary>id에 해당하는 업체를 삭제한다.</summary>
        public async Task DeleteClientAsync(int id)
        {
            // jobs/payments 테이블을 client_id, job_id에 ON DELETE CASCADE로 만들어두면
            // 업체를 지울 때 관련 업무/입금 내역도 Supabase에서 자동으로 함께 삭제됩니다.
            await _client.From<Client>().Where(x => x.Id == id).Delete();
        }

        /// <summary>새 업무 한 건을 jobs 테이블에 추가한다.</summary>
        public async Task AddJobAsync(int clientId, DateTime date, decimal amount, string memo)
        {
            await _client.From<Job>().Insert(new Job
            {
                ClientId = clientId,
                Date = date,
                Amount = amount,
                Memo = memo
            });
        }

        /// <summary>새 입금 한 건을 payments 테이블에 추가한다.</summary>
        public async Task AddPaymentAsync(int jobId, DateTime date, decimal amount)
        {
            await _client.From<Payment>().Insert(new Payment
            {
                JobId = jobId,
                Date = date,
                Amount = amount
            });
        }
    }
}