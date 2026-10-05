using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ReceivableMng.Data
{
    /// <summary>
    /// supabase-config.json 파일 내용을 담는 설정 클래스.
    /// 파일이 없거나 Url/Key가 비어있으면 앱은 자동으로 로컬 JSON 저장 모드로 동작합니다.
    /// </summary>
    public class SupabaseConfig
    {
        public string Url { get; set; } = "";
        public string Key { get; set; } = "";

        // 설정 파일의 실제 경로 (실행 파일과 같은 폴더)
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "supabase-config.json");

        /// <summary>
        /// supabase-config.json 파일을 읽어서 SupabaseConfig 객체로 변환한다.
        /// 파일이 없거나 읽기/변환에 실패하면 null을 반환한다.
        /// </summary>
        public static SupabaseConfig LoadFromFile()
        {
            try
            {
                if (!File.Exists(ConfigPath)) return null; // 파일 자체가 없는 경우
                var json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<SupabaseConfig>(json); // JSON 텍스트 → 객체 변환
            }
            catch
            {
                // 파일 내용이 깨졌거나 형식이 잘못된 경우에도 앱이 죽지 않도록 null 반환
                return null;
            }
        }
    }
}