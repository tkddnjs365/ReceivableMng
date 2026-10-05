using ReceivableMng.Views;

namespace ReceivableMng
{
    public partial class MainForm : Form
    {
        Label[] menu_labels = new Label[5];
        private UserControl[] views = new UserControl[]
        {
            new StatusView(),
            new ClientsView(),
            new JobsView(),
            new PaymentsView(),
            //new ChartView()
        };

        public MainForm()
        {
            InitializeComponent();

            init();
        }

        void init()
        {
            setMenuLabels();
        }

        /// <summary>
        /// 메뉴 라벨들을 배열에 담고, 클릭 시 선택 표시(배경색)와
        /// 해당 메뉴에 맞는 화면(view) 전환이 되도록 클릭 이벤트를 연결한다.
        /// </summary>
        void setMenuLabels()
        {
            menu_labels = new Label[5] { menu_1, menu_2, menu_3, menu_4, menu_5 };

            // 시작할 때는 모든 화면을 일단 숨겨둔다.
            for (int i = 0; i < views.Length; i++)
            {
                panel1.Controls.Add(views[i]);
                views[i].Visible = false;
            }

            for (int i = 0; i < menu_labels.Length; i++)
            {
                int index = i;

                menu_labels[i].Click += (s, e) =>
                {
                    // 모든 메뉴 라벨의 배경색을 투명(선택 안 된 상태)으로 초기화
                    for (int j = 0; j < menu_labels.Length; j++)
                        menu_labels[j].BackColor = Color.Transparent;

                    // 클릭한 라벨만 선택된 색으로 표시
                    menu_labels[index].BackColor = Color.FromArgb(37, 99, 235);

                    for (int j = 0; j < views.Length; j++)
                        views[j].Visible = (j == index);
                };
            }

            views[0].Visible = true;
        }
    }
}
