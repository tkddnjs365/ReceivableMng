namespace ReceivableMng.Views
{
    partial class ClientsView
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
            panel1 = new Panel();
            grid = new DataGridView();
            panel2 = new Panel();
            btnAdd = new Button();
            txtEmail = new TextBox();
            label3 = new Label();
            txtName = new TextBox();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("맑은 고딕", 18F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(20, 16);
            label1.Name = "label1";
            label1.Size = new Size(120, 30);
            label1.TabIndex = 2;
            label1.Text = "업체 등록";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(grid);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1050, 600);
            panel1.TabIndex = 3;
            // 
            // grid
            // 
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(20, 150);
            grid.Name = "grid";
            grid.Size = new Size(760, 420);
            grid.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnAdd);
            panel2.Controls.Add(txtEmail);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtName);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(20, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(760, 70);
            panel2.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(37, 99, 235);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(450, 30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 32);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "+ 업체 추가";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(210, 32);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(220, 23);
            txtEmail.TabIndex = 3;
            // 
            // label3
            // 
            label3.Location = new Point(210, 12);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 2;
            label3.Text = "메일 주소";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            txtName.Location = new Point(14, 32);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 23);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(14, 12);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 0;
            label2.Text = "업체명";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ClientsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "ClientsView";
            Size = new Size(1050, 600);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Button btnAdd;
        private TextBox txtEmail;
        private Label label3;
        private TextBox txtName;
        private Label label2;
        private DataGridView grid;
    }
}
