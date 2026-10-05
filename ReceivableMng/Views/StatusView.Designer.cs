namespace ReceivableMng.Views
{
    partial class StatusView
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            label1 = new Label();
            panel1 = new Panel();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            grid = new DataGridView();
            panel6 = new Panel();
            btnUnpaid = new Button();
            btnAll = new Button();
            cboClient = new ComboBox();
            dtpTo = new DateTimePicker();
            dtpFrom = new DateTimePicker();
            cboMonth = new ComboBox();
            cboYear = new ComboBox();
            label10 = new Label();
            panel5 = new Panel();
            lblClientCount = new Label();
            label9 = new Label();
            panel4 = new Panel();
            lblUnpaid = new Label();
            label7 = new Label();
            panel3 = new Panel();
            lblPaid = new Label();
            label5 = new Label();
            panel2 = new Panel();
            lblTotal = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
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
            label1.TabIndex = 1;
            label1.Text = "현황";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(chart);
            panel1.Controls.Add(grid);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1050, 600);
            panel1.TabIndex = 2;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart.Legends.Add(legend1);
            chart.Location = new Point(611, 236);
            chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart.Series.Add(series1);
            chart.Size = new Size(428, 361);
            chart.TabIndex = 8;
            chart.Text = "chart";
            // 
            // grid
            // 
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(20, 236);
            grid.Name = "grid";
            grid.Size = new Size(585, 361);
            grid.TabIndex = 7;
            // 
            // panel6
            // 
            panel6.BackColor = Color.White;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(btnUnpaid);
            panel6.Controls.Add(btnAll);
            panel6.Controls.Add(cboClient);
            panel6.Controls.Add(dtpTo);
            panel6.Controls.Add(dtpFrom);
            panel6.Controls.Add(cboMonth);
            panel6.Controls.Add(cboYear);
            panel6.Controls.Add(label10);
            panel6.Location = new Point(20, 170);
            panel6.Name = "panel6";
            panel6.Size = new Size(1019, 50);
            panel6.TabIndex = 4;
            // 
            // btnUnpaid
            // 
            btnUnpaid.Location = new Point(474, 7);
            btnUnpaid.Name = "btnUnpaid";
            btnUnpaid.Size = new Size(110, 30);
            btnUnpaid.TabIndex = 9;
            btnUnpaid.Text = "미수금만 보기";
            btnUnpaid.UseVisualStyleBackColor = true;
            // 
            // btnAll
            // 
            btnAll.Location = new Point(378, 7);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(90, 30);
            btnAll.TabIndex = 8;
            btnAll.Text = "전체 조회";
            btnAll.UseVisualStyleBackColor = true;
            // 
            // cboClient
            // 
            cboClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClient.FormattingEnabled = true;
            cboClient.Location = new Point(282, 12);
            cboClient.Name = "cboClient";
            cboClient.Size = new Size(90, 23);
            cboClient.TabIndex = 7;
            // 
            // dtpTo
            // 
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(190, 12);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(90, 23);
            dtpTo.TabIndex = 11;
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(90, 12);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(90, 23);
            dtpFrom.TabIndex = 10;
            // 
            // cboMonth
            // 
            cboMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonth.FormattingEnabled = true;
            cboMonth.Location = new Point(186, 12);
            cboMonth.Name = "cboMonth";
            cboMonth.Size = new Size(90, 23);
            cboMonth.TabIndex = 6;
            cboMonth.Visible = false;
            // 
            // cboYear
            // 
            cboYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cboYear.FormattingEnabled = true;
            cboYear.Location = new Point(90, 12);
            cboYear.Name = "cboYear";
            cboYear.Size = new Size(90, 23);
            cboYear.TabIndex = 5;
            cboYear.Visible = false;
            // 
            // label10
            // 
            label10.Font = new Font("맑은 고딕", 10F);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(10, 12);
            label10.Name = "label10";
            label10.Size = new Size(74, 20);
            label10.TabIndex = 4;
            label10.Text = "조회 기간";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(lblClientCount);
            panel5.Controls.Add(label9);
            panel5.Location = new Point(809, 60);
            panel5.Name = "panel5";
            panel5.Size = new Size(230, 90);
            panel5.TabIndex = 6;
            // 
            // lblClientCount
            // 
            lblClientCount.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            lblClientCount.ForeColor = Color.Black;
            lblClientCount.Location = new Point(14, 36);
            lblClientCount.Name = "lblClientCount";
            lblClientCount.Size = new Size(120, 30);
            lblClientCount.TabIndex = 3;
            lblClientCount.Text = "₩ 0";
            lblClientCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label9.ForeColor = Color.Gray;
            label9.Location = new Point(14, 12);
            label9.Name = "label9";
            label9.Size = new Size(120, 20);
            label9.TabIndex = 2;
            label9.Text = "업체 수";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(lblUnpaid);
            panel4.Controls.Add(label7);
            panel4.Location = new Point(546, 60);
            panel4.Margin = new Padding(3, 3, 30, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(230, 90);
            panel4.TabIndex = 5;
            // 
            // lblUnpaid
            // 
            lblUnpaid.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            lblUnpaid.ForeColor = Color.FromArgb(220, 38, 38);
            lblUnpaid.Location = new Point(14, 36);
            lblUnpaid.Name = "lblUnpaid";
            lblUnpaid.Size = new Size(203, 30);
            lblUnpaid.TabIndex = 3;
            lblUnpaid.Text = "₩ 0";
            lblUnpaid.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label7.ForeColor = Color.Gray;
            label7.Location = new Point(14, 12);
            label7.Name = "label7";
            label7.Size = new Size(120, 20);
            label7.TabIndex = 2;
            label7.Text = "미수금";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblPaid);
            panel3.Controls.Add(label5);
            panel3.Location = new Point(283, 60);
            panel3.Margin = new Padding(3, 3, 30, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(230, 90);
            panel3.TabIndex = 4;
            // 
            // lblPaid
            // 
            lblPaid.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            lblPaid.ForeColor = Color.FromArgb(22, 163, 74);
            lblPaid.Location = new Point(14, 36);
            lblPaid.Name = "lblPaid";
            lblPaid.Size = new Size(203, 30);
            lblPaid.TabIndex = 3;
            lblPaid.Text = "₩ 0";
            lblPaid.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(14, 12);
            label5.Name = "label5";
            label5.Size = new Size(120, 20);
            label5.TabIndex = 2;
            label5.Text = "받은 금액";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lblTotal);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(20, 60);
            panel2.Margin = new Padding(3, 3, 30, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(230, 90);
            panel2.TabIndex = 3;
            // 
            // lblTotal
            // 
            lblTotal.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(37, 99, 235);
            lblTotal.Location = new Point(14, 36);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(203, 30);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "₩ 0";
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(14, 12);
            label2.Name = "label2";
            label2.Size = new Size(120, 20);
            label2.TabIndex = 2;
            label2.Text = "전체 업무 금액";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // StatusView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "StatusView";
            Size = new Size(1050, 600);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Label label2;
        private Panel panel6;
        private Panel panel5;
        private Label lblClientCount;
        private Label label9;
        private Panel panel4;
        private Label lblUnpaid;
        private Label label7;
        private Panel panel3;
        private Label lblPaid;
        private Label label5;
        private Label lblTotal;
        private Label label10;
        private Button btnUnpaid;
        private Button btnAll;
        private ComboBox cboClient;
        private ComboBox cboMonth;
        private ComboBox cboYear;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private DataGridView grid;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}
