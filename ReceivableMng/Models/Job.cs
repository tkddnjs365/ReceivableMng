using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ReceivableMng.Models
{
    /// <summary>
    /// 업무(작업 내역) 정보를 담는 모델 클래스.
    /// Supabase의 "jobs" 테이블과 1:1로 매핑됩니다.
    /// </summary>
    [Table("jobs")] // 이 클래스가 Supabase의 jobs 테이블과 연결됨을 나타냄
    public class Job : BaseModel // BaseModel을 상속해야 Supabase 클라이언트가 CRUD 처리 가능
    {
        /// <summary>업무 고유 번호 (기본키)</summary>
        [PrimaryKey("id", false)] // id 컬럼이 기본키이며, false는 "등록 시 직접 값을 넣지 않고 DB가 자동 생성"을 의미
        public int Id { get; set; }

        /// <summary>이 업무를 의뢰한 업체 번호 (Client.Id를 참조)</summary>
        [Column("client_id")]
        public int ClientId { get; set; }

        /// <summary>업무 발생(작업) 일자</summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>업무 금액</summary>
        [Column("amount")]
        public decimal Amount { get; set; }

        /// <summary>업무 관련 메모(선택 입력)</summary>
        [Column("memo")]
        public string Memo { get; set; } = "";
    }
}