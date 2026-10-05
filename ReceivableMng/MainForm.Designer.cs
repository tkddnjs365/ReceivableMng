namespace ReceivableMng
{
    partial class MainForm
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            menu_5 = new Label();
            menu_4 = new Label();
            menu_3 = new Label();
            menu_2 = new Label();
            menu_1 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(244, 246, 249);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(220, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1044, 821);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(30, 41, 59);
            panel2.Controls.Add(menu_5);
            panel2.Controls.Add(menu_4);
            panel2.Controls.Add(menu_3);
            panel2.Controls.Add(menu_2);
            panel2.Controls.Add(menu_1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(220, 821);
            panel2.TabIndex = 1;
            // 
            // menu_5
            // 
            menu_5.Cursor = Cursors.Hand;
            menu_5.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            menu_5.ForeColor = Color.FromArgb(203, 213, 225);
            menu_5.Location = new Point(14, 284);
            menu_5.Margin = new Padding(3);
            menu_5.Name = "menu_5";
            menu_5.Size = new Size(190, 40);
            menu_5.TabIndex = 6;
            menu_5.Text = "📈  그래프";
            menu_5.TextAlign = ContentAlignment.MiddleLeft;
            menu_5.Visible = false;
            // 
            // menu_4
            // 
            menu_4.Cursor = Cursors.Hand;
            menu_4.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            menu_4.ForeColor = Color.FromArgb(203, 213, 225);
            menu_4.Location = new Point(14, 238);
            menu_4.Margin = new Padding(3);
            menu_4.Name = "menu_4";
            menu_4.Size = new Size(190, 40);
            menu_4.TabIndex = 5;
            menu_4.Text = "💵  돈 받은 내역";
            menu_4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menu_3
            // 
            menu_3.Cursor = Cursors.Hand;
            menu_3.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            menu_3.ForeColor = Color.FromArgb(203, 213, 225);
            menu_3.Location = new Point(14, 192);
            menu_3.Margin = new Padding(3);
            menu_3.Name = "menu_3";
            menu_3.Size = new Size(190, 40);
            menu_3.TabIndex = 4;
            menu_3.Text = "📝  업무 등록";
            menu_3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menu_2
            // 
            menu_2.Cursor = Cursors.Hand;
            menu_2.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            menu_2.ForeColor = Color.FromArgb(203, 213, 225);
            menu_2.Location = new Point(14, 146);
            menu_2.Margin = new Padding(3);
            menu_2.Name = "menu_2";
            menu_2.Size = new Size(190, 40);
            menu_2.TabIndex = 3;
            menu_2.Text = "🏢  업체 등록";
            menu_2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menu_1
            // 
            menu_1.Cursor = Cursors.Hand;
            menu_1.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            menu_1.ForeColor = Color.FromArgb(203, 213, 225);
            menu_1.Location = new Point(14, 100);
            menu_1.Margin = new Padding(3);
            menu_1.Name = "menu_1";
            menu_1.Size = new Size(190, 40);
            menu_1.TabIndex = 2;
            menu_1.Text = "📊  현황";
            menu_1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("맑은 고딕", 9F);
            label2.ForeColor = Color.FromArgb(148, 163, 184);
            label2.Location = new Point(18, 54);
            label2.Name = "label2";
            label2.Size = new Size(145, 30);
            label2.TabIndex = 1;
            label2.Text = "매출·수금·미수금 관리";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 24);
            label1.Name = "label1";
            label1.Size = new Size(120, 30);
            label1.TabIndex = 0;
            label1.Text = "💰 돈 관리";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 821);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label menu_1;
        private Label label2;
        private Label label1;
        private Label menu_5;
        private Label menu_4;
        private Label menu_3;
        private Label menu_2;
    }
}
