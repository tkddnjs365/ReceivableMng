namespace ReceivableMng.Models
{
    /// <summary>
    /// 업체(거래처) 정보를 담는 모델 클래스.
    /// SQLite의 "clients" 테이블과 1:1로 대응됩니다.
    /// </summary>
    public class Client
    {
        /// <summary>업체 고유 번호 (기본키, DB가 자동으로 만들어 줌)</summary>
        public int Id { get; set; }

        /// <summary>업체명</summary>
        public string Name { get; set; } = "";

        /// <summary>업체 담당자 이메일</summary>
        public string Email { get; set; } = "";

        /// <summary>업체 등록 일시</summary>
        public DateTime CreatedAt { get; set; }
    }
}
