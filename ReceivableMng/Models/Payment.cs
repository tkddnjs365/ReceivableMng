using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ReceivableMng.Models
{
    /// <summary>
    /// 입금(수금) 내역을 담는 모델 클래스.
    /// Supabase의 "payments" 테이블과 1:1로 매핑됩니다.
    /// 하나의 업무(Job)에 대해 입금이 여러 번 나눠서 들어올 수 있으므로,
    /// 입금 1건마다 이 모델의 레코드가 하나씩 생성됩니다.
    /// </summary>
    [Table("payments")] // 이 클래스가 Supabase의 payments 테이블과 연결됨을 나타냄
    public class Payment : BaseModel // BaseModel을 상속해야 Supabase 클라이언트가 CRUD 처리 가능
    {
        /// <summary>입금 고유 번호 (기본키)</summary>
        [PrimaryKey("id", false)] // id 컬럼이 기본키이며, false는 "등록 시 직접 값을 넣지 않고 DB가 자동 생성"을 의미
        public int Id { get; set; }

        /// <summary>이 입금이 어느 업무(Job.Id)에 대한 입금인지</summary>
        [Column("job_id")]
        public int JobId { get; set; }

        /// <summary>입금 일자</summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>입금 금액</summary>
        [Column("amount")]
        public decimal Amount { get; set; }
    }
}