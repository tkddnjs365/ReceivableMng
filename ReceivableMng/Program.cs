using ReceivableMng.Data;

namespace ReceivableMng
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 윈도우 화면의 DPI 설정을 시스템 설정에 맞게 사용한다.
            // 고해상도 모니터에서도 화면이 너무 작게 보이지 않도록 해준다.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            // 윈도우에서 사용하는 기본적인 시각적 스타일을 적용한다.
            // 버튼, 체크박스 등의 모양을 윈도우 스타일에 맞게 표시한다.
            Application.EnableVisualStyles();


            // 컨트롤의 글자를 그리는 방식을 설정한다.
            // 기본적으로 false를 사용하면 최신 Windows 스타일과 잘 맞는다.
            Application.SetCompatibleTextRenderingDefault(false);

            DataStore.Load();

            Application.Run(new MainForm());
        }
    }
}