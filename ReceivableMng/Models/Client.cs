using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ReceivableMng.Models
{
    /// <summary>
    /// 업체(거래처) 정보를 담는 모델 클래스.
    /// Supabase의 "clients" 테이블과 1:1로 매핑됩니다.
    /// </summary>
    [Table("clients")] // 이 클래스가 Supabase의 clients 테이블과 연결됨을 나타냄
    public class Client : BaseModel
    {
        /// <summary>업체 고유 번호 (기본키)</summary>
        [PrimaryKey("id", false)] // id 컬럼이 기본키이며, false는 "등록 시 직접 값을 넣지 않고 DB가 자동 생성"을 의미
        public int Id { get; set; }

        /// <summary>업체명</summary>
        [Column("name")] // DB의 name 컬럼과 매핑
        public string Name { get; set; } = "";

        /// <summary>업체 담당자 이메일</summary>
        [Column("email")]
        public string Email { get; set; } = "";

        /// <summary>업체 등록 일시</summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}