namespace ReceivableMng.Models
{
    /// <summary>
    /// 입금(수금) 내역을 담는 모델 클래스.
    /// SQLite의 "payments" 테이블과 1:1로 대응됩니다.
    /// 하나의 업무(Job)에 대해 입금이 여러 번 나눠서 들어올 수 있으므로,
    /// 입금 1건마다 이 모델의 레코드가 하나씩 생성됩니다.
    /// </summary>
    public class Payment
    {
        /// <summary>입금 고유 번호 (기본키, DB가 자동으로 만들어 줌)</summary>
        public int Id { get; set; }

        /// <summary>이 입금이 어느 업무(Job.Id)에 대한 입금인지</summary>
        public int JobId { get; set; }

        /// <summary>입금 일자</summary>
        public DateTime Date { get; set; }

        /// <summary>입금 금액</summary>
        public decimal Amount { get; set; }
    }
}
