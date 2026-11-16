using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ReceivableMng.Data
{
    /// <summary>
    /// 프로그램 설정(app-config.json)을 읽어서 DB 파일 경로를 알려주는 설정 클래스.
    /// 설정 파일은 실행 파일과 같은 폴더에 있고, 처음 실행할 때 자동으로 만들어집니다.
    ///
    /// 설정 예시 (app-config.json):
    /// { "DbPath": "D:/Data/receivable.db" }
    ///
    /// DbPath 가 비어 있으면 실행 파일 폴더의 receivable.db 를 사용합니다.
    /// </summary>
    public class AppConfig
    {
        /// <summary>DB 파일 경로. 비워두면 기본 위치를 사용한다. (폴더 구분은 / 를 쓰거나 \\ 로 두 번 써야 함)</summary>
        public string DbPath { get; set; } = @"C:\DataBase\receivableMng.db";

        // 설정 파일의 실제 경로 (실행 파일과 같은 폴더)
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app-config.json");

        // 경로를 따로 지정하지 않았을 때 쓰는 기본 DB 파일 경로
        private const string DefaultDbPath = @"C:\DataBase\receivableMng.db";

        /// <summary>
        /// app-config.json 을 읽어서 AppConfig 객체로 변환한다.
        /// 파일이 없으면 기본값으로 새로 만들어 주고, 읽기에 실패하면 기본값(빈 설정)을 돌려준다.
        /// </summary>
        public static AppConfig Load()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    // 최초 실행: 사용자가 열어서 고칠 수 있도록 빈 설정 파일을 만들어 둔다.
                    var created = new AppConfig();
                    File.WriteAllText(ConfigPath, JsonSerializer.Serialize(created, new JsonSerializerOptions { WriteIndented = true }));
                    return created;
                }

                var json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig(); // JSON 텍스트 → 객체 변환
            }
            catch
            {
                // 파일이 깨졌거나 쓰기 권한이 없어도 앱이 죽지 않도록 기본 설정으로 진행
                return new AppConfig();
            }
        }

        /// <summary>
        /// 실제로 사용할 DB 파일의 전체 경로를 계산해서 돌려준다.
        /// - DbPath 가 비어 있으면: 기본 위치(실행 파일 폴더의 receivable.db)
        /// - 상대경로(예: Data\receivable.db)면: 실행 파일 폴더 기준으로 계산
        /// - 폴더가 아직 없으면 자동으로 만들어 준다.
        /// </summary>
        public string GetDbPath()
        {
            string path = string.IsNullOrWhiteSpace(DbPath)
                ? DefaultDbPath
                : Path.GetFullPath(DbPath.Trim(), AppDomain.CurrentDomain.BaseDirectory); // 상대경로를 전체 경로로 변환

            string? folder = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(folder))
                Directory.CreateDirectory(folder); // 폴더가 이미 있으면 아무 일도 안 함

            return path;
        }
    }
}
