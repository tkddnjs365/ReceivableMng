using Microsoft.Data.Sqlite;
using ReceivableMng.Models;
using System.Globalization;

namespace ReceivableMng.Data
{
    /// <summary>
    /// 로컬 SQLite 파일(receivable.db)을 데이터 저장소로 사용하는 Provider.
    /// 별도의 DB 서버 설치 없이 DB 파일 하나로 동작하며, 파일 위치는 app-config.json 의 DbPath 로 정할 수 있습니다.
    /// </summary>
    public class SqliteStorageProvider : IStorageProvider
    {
        // 날짜를 문자열로 저장할 때 쓰는 형식
        private const string DateFormat = "yyyy-MM-dd";

        // 일시(날짜+시간)를 문자열로 저장할 때 쓰는 형식
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        // SQLite DB 파일에 접속하기 위한 연결 문자열
        private readonly string _connectionString;

        /// <summary>
        /// DB 파일 경로를 받아 연결 문자열을 만든다.
        /// 경로를 주지 않으면 app-config.json 의 DbPath 설정을 따르고, 그것도 비어 있으면 실행 파일 폴더의 receivable.db 를 사용한다.
        /// </summary>
        public SqliteStorageProvider(string? dbPath = null)
        {
            dbPath ??= AppConfig.Load().GetDbPath();

            _connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = dbPath,
                ForeignKeys = true // 업체 삭제 시 업무/입금이 함께 삭제되는 CASCADE 기능을 켠다
            }.ToString();
        }

        /// <summary>
        /// DB 파일을 열고, 테이블이 없으면 새로 만든다. (최초 실행 시 DB 파일도 자동 생성됨)
        /// </summary>
        public async Task InitializeAsync()
        {
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS clients (
                    id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    name       TEXT NOT NULL,
                    email      TEXT,
                    created_at TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS jobs (
                    id        INTEGER PRIMARY KEY AUTOINCREMENT,
                    client_id INTEGER NOT NULL REFERENCES clients(id) ON DELETE CASCADE,
                    date      TEXT NOT NULL,
                    amount    TEXT NOT NULL,
                    memo      TEXT
                );

                CREATE TABLE IF NOT EXISTS payments (
                    id     INTEGER PRIMARY KEY AUTOINCREMENT,
                    job_id INTEGER NOT NULL REFERENCES jobs(id) ON DELETE CASCADE,
                    date   TEXT NOT NULL,
                    amount TEXT NOT NULL
                );";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// DB 연결을 새로 만들어 열고 돌려준다. 사용하는 쪽에서 using 으로 닫아야 한다.
        /// </summary>
        private async Task<SqliteConnection> OpenAsync()
        {
            var conn = new SqliteConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }

        /// <summary>
        /// clients 테이블의 전체 데이터를 가져온다.
        /// </summary>
        public async Task<List<Client>> GetClientsAsync()
        {
            var list = new List<Client>();
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, email, created_at FROM clients ORDER BY id";
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) // 한 줄씩 읽어서 Client 객체로 변환
            {
                list.Add(new Client
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    CreatedAt = DateTime.Parse(reader.GetString(3), CultureInfo.InvariantCulture)
                });
            }
            return list;
        }

        /// <summary>
        /// jobs 테이블의 전체 데이터를 가져온다.</summary>
        public async Task<List<Job>> GetJobsAsync()
        {
            var list = new List<Job>();
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, client_id, date, amount, memo FROM jobs ORDER BY id";
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Job
                {
                    Id = reader.GetInt32(0),
                    ClientId = reader.GetInt32(1),
                    Date = DateTime.Parse(reader.GetString(2), CultureInfo.InvariantCulture),
                    Amount = decimal.Parse(reader.GetString(3), CultureInfo.InvariantCulture),
                    Memo = reader.IsDBNull(4) ? "" : reader.GetString(4)
                });
            }
            return list;
        }

        /// <summary>
        /// payments 테이블의 전체 데이터를 가져온다.
        /// </summary>
        public async Task<List<Payment>> GetPaymentsAsync()
        {
            var list = new List<Payment>();
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, job_id, date, amount FROM payments ORDER BY id";
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Payment
                {
                    Id = reader.GetInt32(0),
                    JobId = reader.GetInt32(1),
                    Date = DateTime.Parse(reader.GetString(2), CultureInfo.InvariantCulture),
                    Amount = decimal.Parse(reader.GetString(3), CultureInfo.InvariantCulture)
                });
            }
            return list;
        }

        /// <summary>
        /// 새 업체 한 건을 clients 테이블에 추가한다.</summary>
        public async Task AddClientAsync(string name, string email)
        {
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO clients (name, email, created_at) VALUES ($name, $email, $createdAt)";
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$email", email ?? "");
            cmd.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture));
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// id에 해당하는 업체의 이름/이메일을 수정한다.
        /// </summary>
        public async Task UpdateClientAsync(int id, string name, string email)
        {
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE clients SET name = $name, email = $email WHERE id = $id";
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$email", email ?? "");
            cmd.Parameters.AddWithValue("$id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// id에 해당하는 업체를 삭제한다.
        /// 테이블을 ON DELETE CASCADE 로 만들어 두었기 때문에 그 업체의 업무/입금 내역도 자동으로 함께 삭제된다.
        /// </summary>
        public async Task DeleteClientAsync(int id)
        {
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM clients WHERE id = $id";
            cmd.Parameters.AddWithValue("$id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// 새 업무 한 건을 jobs 테이블에 추가한다.
        /// </summary>
        public async Task AddJobAsync(int clientId, DateTime date, decimal amount, string memo)
        {
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO jobs (client_id, date, amount, memo) VALUES ($clientId, $date, $amount, $memo)";
            cmd.Parameters.AddWithValue("$clientId", clientId);
            cmd.Parameters.AddWithValue("$date", date.ToString(DateFormat, CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("$amount", amount.ToString(CultureInfo.InvariantCulture)); // 금액은 오차 없이 문자열로 저장
            cmd.Parameters.AddWithValue("$memo", memo ?? "");
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// 새 입금 한 건을 payments 테이블에 추가한다.
        /// </summary>
        public async Task AddPaymentAsync(int jobId, DateTime date, decimal amount)
        {
            using var conn = await OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO payments (job_id, date, amount) VALUES ($jobId, $date, $amount)";
            cmd.Parameters.AddWithValue("$jobId", jobId);
            cmd.Parameters.AddWithValue("$date", date.ToString(DateFormat, CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("$amount", amount.ToString(CultureInfo.InvariantCulture));
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
