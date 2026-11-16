namespace ReceivableMng.Models
{
    /// <summary>
    /// 업무(작업 내역) 정보를 담는 모델 클래스.
    /// SQLite의 "jobs" 테이블과 1:1로 대응됩니다.
    /// </summary>
    public class Job
    {
        /// <summary>업무 고유 번호 (기본키, DB가 자동으로 만들어 줌)</summary>
        public int Id { get; set; }

        /// <summary>이 업무를 의뢰한 업체 번호 (Client.Id를 참조)</summary>
        public int ClientId { get; set; }

        /// <summary>업무 발생(작업) 일자</summary>
        public DateTime Date { get; set; }

        /// <summary>업무 금액</summary>
        public decimal Amount { get; set; }

        /// <summary>업무 관련 메모(선택 입력)</summary>
        public string Memo { get; set; } = "";
    }
}
