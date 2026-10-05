namespace ReceivableMng.Controls
{
    partial class CheckedComboBox
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
            _btnToggle = new Button();
            SuspendLayout();
            // 
            // _btnToggle
            // 
            _btnToggle.BackColor = Color.White;
            _btnToggle.Dock = DockStyle.Fill;
            _btnToggle.FlatAppearance.BorderColor = Color.Black;
            _btnToggle.FlatStyle = FlatStyle.Flat;
            _btnToggle.Location = new Point(0, 0);
            _btnToggle.Name = "_btnToggle";
            _btnToggle.Size = new Size(170, 30);
            _btnToggle.TabIndex = 0;
            _btnToggle.Text = "전체 업체";
            _btnToggle.TextAlign = ContentAlignment.MiddleLeft;
            _btnToggle.UseVisualStyleBackColor = false;
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_btnToggle);
            Name = "UserControl1";
            Size = new Size(170, 30);
            ResumeLayout(false);
        }

        #endregion

        private Button _btnToggle;
    }
}
