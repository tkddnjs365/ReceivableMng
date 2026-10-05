namespace ReceivableMng.Views
{
    partial class ClientEditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblName = new Label();
            txtName = new TextBox();
            txtEmail = new TextBox();
            lblEmail = new Label();
            btnOk = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.ForeColor = Color.Black;
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 20);
            lblName.TabIndex = 2;
            lblName.Text = "업체명";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            txtName.Location = new Point(20, 42);
            txtName.Name = "txtName";
            txtName.Size = new Size(320, 24);
            txtName.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(20, 102);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 24);
            txtEmail.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.ForeColor = Color.Black;
            lblEmail.Location = new Point(20, 80);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(80, 20);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "메일 주소";
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.FromArgb(37, 99, 235);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(260, 145);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(80, 30);
            btnOk.TabIndex = 6;
            btnOk.Text = "저장";
            btnOk.UseVisualStyleBackColor = false;
            // 
            // ClientEditDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 194);
            Controls.Add(btnOk);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Font = new Font("맑은 고딕", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ClientEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "업체 정보 수정";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private TextBox txtEmail;
        private Label lblEmail;
        private Button btnOk;
    }
}