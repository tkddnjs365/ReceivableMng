namespace ReceivableMng.Views
{
    partial class JobsView
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
            grid = new DataGridView();
            panel2 = new Panel();
            label5 = new Label();
            label4 = new Label();
            btnAdd = new Button();
            txtMemo = new TextBox();
            dtDate = new DateTimePicker();
            cboClient = new ComboBox();
            label3 = new Label();
            txtAmount = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(244, 246, 249);
            panel1.Controls.Add(grid);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1050, 600);
            panel1.TabIndex = 0;
            // 
            // grid
            // 
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(20, 150);
            grid.Name = "grid";
            grid.Size = new Size(920, 420);
            grid.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(btnAdd);
            panel2.Controls.Add(txtMemo);
            panel2.Controls.Add(dtDate);
            panel2.Controls.Add(cboClient);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtAmount);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(20, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(920, 70);
            panel2.TabIndex = 5;
            // 
            // label5
            // 
            label5.Location = new Point(470, 12);
            label5.Name = "label5";
            label5.Size = new Size(80, 20);
            label5.TabIndex = 8;
            label5.Text = "내용 (선택)";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Location = new Point(330, 12);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 7;
            label4.Text = "금액 (원)";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(37, 99, 235);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(710, 30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 32);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "+ 업무 등록";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // txtMemo
            // 
            txtMemo.Location = new Point(470, 32);
            txtMemo.Name = "txtMemo";
            txtMemo.Size = new Size(220, 23);
            txtMemo.TabIndex = 3;
            // 
            // dtDate
            // 
            dtDate.Format = DateTimePickerFormat.Short;
            dtDate.Location = new Point(180, 32);
            dtDate.Name = "dtDate";
            dtDate.Size = new Size(130, 23);
            dtDate.TabIndex = 6;
            // 
            // cboClient
            // 
            cboClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClient.FormattingEnabled = true;
            cboClient.Location = new Point(14, 32);
            cboClient.Name = "cboClient";
            cboClient.Size = new Size(150, 23);
            cboClient.TabIndex = 5;
            // 
            // label3
            // 
            label3.Location = new Point(180, 12);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 2;
            label3.Text = "작업 일자";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(330, 32);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(120, 23);
            txtAmount.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(14, 12);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 0;
            label2.Text = "업체 선택";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Font = new Font("맑은 고딕", 18F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(20, 16);
            label1.Name = "label1";
            label1.Size = new Size(120, 30);
            label1.TabIndex = 3;
            label1.Text = "업무 등록";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // JobsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "JobsView";
            Size = new Size(1050, 600);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Button btnAdd;
        private TextBox txtMemo;
        private Label label3;
        private Label label2;
        private TextBox txtAmount;
        private Label label4;
        private DateTimePicker dtDate;
        private ComboBox cboClient;
        private Label label5;
        private DataGridView grid;
    }
}
