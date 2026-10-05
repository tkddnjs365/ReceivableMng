namespace ReceivableMng.Views
{
    partial class PaymentsView
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
            label1 = new Label();
            panel2 = new Panel();
            checkedComboBox1 = new ReceivableMng.Controls.CheckedComboBox();
            label2 = new Label();
            panel1 = new Panel();
            grid = new DataGridView();
            label5 = new Label();
            panel3 = new Panel();
            btnUnpaid = new Button();
            btnAll = new Button();
            ccClient = new ReceivableMng.Controls.CheckedComboBox();
            label3 = new Label();
            label4 = new Label();
            panelControl1 = new Panel();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panel3.SuspendLayout();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("맑은 고딕", 18F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(20, 16);
            label1.Name = "label1";
            label1.Size = new Size(150, 30);
            label1.TabIndex = 4;
            label1.Text = "돈 받은 내역";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(checkedComboBox1);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(20, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(920, 70);
            panel2.TabIndex = 6;
            // 
            // checkedComboBox1
            // 
            checkedComboBox1.Location = new Point(54, 17);
            checkedComboBox1.Name = "checkedComboBox1";
            checkedComboBox1.Size = new Size(170, 30);
            checkedComboBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(10, 22);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 0;
            label2.Text = "업체";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(244, 246, 249);
            panel1.Controls.Add(grid);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label4);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1050, 600);
            panel1.TabIndex = 7;
            // 
            // grid
            // 
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(20, 155);
            grid.Name = "grid";
            grid.Size = new Size(920, 400);
            grid.TabIndex = 9;
            // 
            // label5
            // 
            label5.ForeColor = Color.Red;
            label5.Location = new Point(20, 130);
            label5.Name = "label5";
            label5.Size = new Size(555, 20);
            label5.TabIndex = 7;
            label5.Text = "입금할 건을 찾아 금액을 입력 후 저장하세요. 전액 입금 시 '완료 처리'를 누르면 됩니다.";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(btnUnpaid);
            panel3.Controls.Add(btnAll);
            panel3.Controls.Add(ccClient);
            panel3.Controls.Add(label3);
            panel3.Location = new Point(20, 60);
            panel3.Name = "panel3";
            panel3.Size = new Size(920, 70);
            panel3.TabIndex = 6;
            // 
            // btnUnpaid
            // 
            btnUnpaid.BackColor = Color.FromArgb(220, 38, 38);
            btnUnpaid.ForeColor = Color.White;
            btnUnpaid.Location = new Point(326, 17);
            btnUnpaid.Name = "btnUnpaid";
            btnUnpaid.Size = new Size(90, 30);
            btnUnpaid.TabIndex = 6;
            btnUnpaid.Text = "미수금만";
            btnUnpaid.UseVisualStyleBackColor = false;
            // 
            // btnAll
            // 
            btnAll.BackColor = Color.FromArgb(30, 41, 59);
            btnAll.ForeColor = Color.White;
            btnAll.Location = new Point(230, 17);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(90, 30);
            btnAll.TabIndex = 5;
            btnAll.Text = "전체 조회";
            btnAll.UseVisualStyleBackColor = false;
            // 
            // ccClient
            // 
            ccClient.Location = new Point(54, 17);
            ccClient.Name = "ccClient";
            ccClient.Size = new Size(170, 30);
            ccClient.TabIndex = 1;
            // 
            // label3
            // 
            label3.Location = new Point(10, 22);
            label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 0;
            label3.Text = "업체";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Font = new Font("맑은 고딕", 18F, FontStyle.Bold);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(20, 16);
            label4.Name = "label4";
            label4.Size = new Size(150, 30);
            label4.TabIndex = 4;
            label4.Text = "돈 받은 내역";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelControl1
            // 
            panelControl1.BackColor = Color.FromArgb(244, 246, 249);
            panelControl1.Controls.Add(panel1);
            panelControl1.Controls.Add(panel2);
            panelControl1.Controls.Add(label1);
            panelControl1.Dock = DockStyle.Fill;
            panelControl1.Location = new Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new Size(1050, 600);
            panelControl1.TabIndex = 0;
            // 
            // PaymentsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "PaymentsView";
            Size = new Size(1050, 600);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panel3.ResumeLayout(false);
            panelControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panel2;
        private Controls.CheckedComboBox checkedComboBox1;
        private Label label2;
        private Panel panel1;
        private Panel panel3;
        private Button btnUnpaid;
        private Button btnAll;
        private Controls.CheckedComboBox ccClient;
        private Label label3;
        private Label label4;
        private Panel panelControl1;
        private Label label5;
        private DataGridView grid;
    }
}
