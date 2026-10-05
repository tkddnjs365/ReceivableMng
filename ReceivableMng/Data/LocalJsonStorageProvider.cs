using ReceivableMng.Models;
using System.Text.Json;

namespace ReceivableMng.Data
{
    /// <summary>
    /// 기존 방식: data.json 파일 하나에 전부 저장.
    /// supabase-config.json 이 없거나 값이 비어있을 때 자동으로 이 Provider가 사용됩니다.
    /// </summary>
    public class LocalJsonStorageProvider : IStorageProvider
    {
        // data.json이 저장될 실제 경로 (실행 파일과 같은 폴더)
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");

        // 메모리 위에 올려두고 쓰는 실제 데이터 목록들
        private List<Client> _clients = new();
        private List<Job> _jobs = new();
        private List<Payment> _payments = new();

        // data.json 파일 하나에 세 목록을 한꺼번에 담기 위한 내부 전용 클래스
        private class DataModel
        {
            public List<Client> Clients { get; set; } = new();
            public List<Job> Jobs { get; set; } = new();
            public List<Payment> Payments { get; set; } = new();
        }

        /// <summary>
        /// data.json 파일이 있으면 읽어서 메모리에 올리고,
        /// 없으면(최초 실행) 샘플 데이터를 만들어서 파일로 저장한다.
        /// </summary>
        public Task InitializeAsync()
        {
            if (File.Exists(_path))
            {
                try
                {
                    var json = File.ReadAllText(_path);
                    var model = JsonSerializer.Deserialize<DataModel>(json); // JSON → 객체 변환
                    if (model != null)
                    {
                        _clients = model.Clients;
                        _jobs = model.Jobs;
                        _payments = model.Payments;
                        return Task.CompletedTask;
                    }
                }
                catch
                {
                    // 파일이 손상된 경우 아래로 내려가 샘플 데이터로 새로 시작
                }
            }
            SeedSampleData();
            Persist();
            return Task.CompletedTask;
        }

        /// <summary>현재 메모리에 있는 데이터를 data.json 파일에 그대로 덮어쓴다.</summary>
        private void Persist()
        {
            var model = new DataModel { Clients = _clients, Jobs = _jobs, Payments = _payments };
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true }); // 객체 → JSON 변환
            File.WriteAllText(_path, json);
        }

        /// <summary>업체 전체 목록을 반환한다.</summary>
        public Task<List<Client>> GetClientsAsync() => Task.FromResult(_clients.ToList());

        /// <summary>업무 전체 목록을 반환한다.</summary>
        public Task<List<Job>> GetJobsAsync() => Task.FromResult(_jobs.ToList());

        /// <summary>입금 전체 목록을 반환한다.</summary>
        public Task<List<Payment>> GetPaymentsAsync() => Task.FromResult(_payments.ToList());

        /// <summary>새 업체를 목록에 추가하고 파일에 저장한다.</summary>
        public Task AddClientAsync(string name, string email)
        {
            // 기존 목록에서 가장 큰 Id보다 1 큰 값을 새 Id로 사용 (목록이 비었으면 1부터 시작)
            int id = _clients.Count == 0 ? 1 : _clients.Max(c => c.Id) + 1;
            _clients.Add(new Client { Id = id, Name = name, Email = email, CreatedAt = DateTime.Now });
            Persist();
            return Task.CompletedTask;
        }

        /// <summary>id에 해당하는 업체를 찾아 이름/이메일을 수정하고 파일에 저장한다.</summary>
        public Task UpdateClientAsync(int id, string name, string email)
        {
            var c = _clients.FirstOrDefault(x => x.Id == id); // 목록에서 해당 Id를 가진 업체 찾기
            if (c != null)
            {
                c.Name = name;
                c.Email = email;
                Persist();
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// id에 해당하는 업체와, 그 업체의 업무/입금 내역까지 전부 삭제하고 파일에 저장한다.
        /// </summary>
        public Task DeleteClientAsync(int id)
        {
            _clients.RemoveAll(c => c.Id == id); // 업체 삭제
            var jobIds = _jobs.Where(j => j.ClientId == id).Select(j => j.Id).ToList(); // 삭제될 업무들의 Id 목록 미리 확보
            _jobs.RemoveAll(j => j.ClientId == id); // 해당 업체의 업무 전부 삭제
            _payments.RemoveAll(p => jobIds.Contains(p.JobId)); // 그 업무들에 딸린 입금 내역도 전부 삭제
            Persist();
            return Task.CompletedTask;
        }

        /// <summary>새 업무를 목록에 추가하고 파일에 저장한다.</summary>
        public Task AddJobAsync(int clientId, DateTime date, decimal amount, string memo)
        {
            int id = _jobs.Count == 0 ? 1 : _jobs.Max(j => j.Id) + 1;
            _jobs.Add(new Job { Id = id, ClientId = clientId, Date = date, Amount = amount, Memo = memo });
            Persist();
            return Task.CompletedTask;
        }

        /// <summary>새 입금 내역을 목록에 추가하고 파일에 저장한다.</summary>
        public Task AddPaymentAsync(int jobId, DateTime date, decimal amount)
        {
            int id = _payments.Count == 0 ? 1 : _payments.Max(p => p.Id) + 1;
            _payments.Add(new Payment { Id = id, JobId = jobId, Date = date, Amount = amount });
            Persist();
            return Task.CompletedTask;
        }

        /// <summary>최초 실행 시 보여줄 샘플 업체/업무/입금 데이터를 만든다.</summary>
        private void SeedSampleData()
        {
            _clients = new List<Client>
            {
                new() { Id = 1, Name = "A회사", Email = "a@company.com", CreatedAt = new DateTime(2026, 1, 5) },
                new() { Id = 2, Name = "B회사", Email = "b@company.com", CreatedAt = new DateTime(2026, 2, 10) },
                new() { Id = 3, Name = "C회사", Email = "c@company.com", CreatedAt = new DateTime(2026, 3, 1) },
            };
            _jobs = new List<Job>
            {
                new() { Id = 1, ClientId = 1, Date = new DateTime(2026, 8, 3),  Amount = 30000000, Memo = "홈페이지 제작" },
                new() { Id = 2, ClientId = 2, Date = new DateTime(2026, 8, 8),  Amount = 45000000, Memo = "MES 커스터마이징" },
                new() { Id = 3, ClientId = 3, Date = new DateTime(2026, 8, 15), Amount = 18500000, Memo = "추가 개발" },
                new() { Id = 4, ClientId = 1, Date = new DateTime(2026, 5, 10), Amount = 12000000, Memo = "" },
                new() { Id = 5, ClientId = 2, Date = new DateTime(2026, 6, 12), Amount = 18000000, Memo = "" },
                new() { Id = 6, ClientId = 1, Date = new DateTime(2026, 7, 2),  Amount = 24000000, Memo = "" },
                new() { Id = 7, ClientId = 3, Date = new DateTime(2025, 11, 20), Amount = 9000000, Memo = "" },
                new() { Id = 8, ClientId = 2, Date = new DateTime(2025, 9, 14),  Amount = 15000000, Memo = "" },
            };
            _payments = new List<Payment>
            {
                new() { Id = 1, JobId = 1, Date = new DateTime(2026, 8, 5),  Amount = 30000000 },
                new() { Id = 2, JobId = 2, Date = new DateTime(2026, 8, 10), Amount = 25000000 },
                new() { Id = 3, JobId = 7, Date = new DateTime(2025, 12, 1), Amount = 9000000 },
            };
        }
    }
}