using ReceivableMng.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceivableMng.Data
{
    /// <summary>
    /// 데이터 저장 방식을 추상화하는 인터페이스.
    /// 로컬 JSON 파일(LocalJsonStorageProvider)과 Supabase 클라우드(SupabaseStorageProvider)가
    /// 둘 다 이 인터페이스를 구현하고, DataStore는 어느 쪽인지 신경 쓰지 않고 이 인터페이스만 봅니다.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>저장소 사용 준비(파일 읽기, 서버 접속 등)를 한다.</summary>
        Task InitializeAsync();

        /// <summary>업체 전체 목록을 가져온다.</summary>
        Task<List<Client>> GetClientsAsync();

        /// <summary>업무 전체 목록을 가져온다.</summary>
        Task<List<Job>> GetJobsAsync();

        /// <summary>입금 전체 목록을 가져온다.</summary>
        Task<List<Payment>> GetPaymentsAsync();

        /// <summary>업체를 새로 등록한다.</summary>
        Task AddClientAsync(string name, string email);

        /// <summary>기존 업체의 이름/이메일을 수정한다.</summary>
        Task UpdateClientAsync(int id, string name, string email);

        /// <summary>업체를 삭제한다.</summary>
        Task DeleteClientAsync(int id);

        /// <summary>업무를 새로 등록한다.</summary>
        Task AddJobAsync(int clientId, DateTime date, decimal amount, string memo);

        /// <summary>입금 내역을 새로 등록한다.</summary>
        Task AddPaymentAsync(int jobId, DateTime date, decimal amount);
    }
}