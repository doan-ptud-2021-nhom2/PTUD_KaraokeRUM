
namespace KaraokeRUM
{
    partial class frmThongKeQL
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chrThongKeSKhachHang = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtTDT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSoLuotDen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTKKhachHang = new System.Windows.Forms.Button();
            this.txtSoKhachVip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoHomNay = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstvDSKhachHang = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrThongKeSKhachHang)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chrThongKeSKhachHang);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(853, 77);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(748, 543);
            this.groupBox4.TabIndex = 92;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Biểu đồ";
            // 
            // chrThongKeSKhachHang
            // 
            chartArea6.Name = "ChartArea1";
            this.chrThongKeSKhachHang.ChartAreas.Add(chartArea6);
            this.chrThongKeSKhachHang.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            legend6.Name = "Legend1";
            this.chrThongKeSKhachHang.Legends.Add(legend6);
            this.chrThongKeSKhachHang.Location = new System.Drawing.Point(7, 29);
            this.chrThongKeSKhachHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chrThongKeSKhachHang.Name = "chrThongKeSKhachHang";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series6.Legend = "Legend1";
            series6.Name = "DoanhThu";
            this.chrThongKeSKhachHang.Series.Add(series6);
            this.chrThongKeSKhachHang.Size = new System.Drawing.Size(709, 497);
            this.chrThongKeSKhachHang.TabIndex = 86;
            title6.Name = "chart";
            title6.Text = "Thống kê mặt hàng bán được";
            this.chrThongKeSKhachHang.Titles.Add(title6);
            // 
            // txtTDT
            // 
            this.txtTDT.Location = new System.Drawing.Point(163, 165);
            this.txtTDT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTDT.Name = "txtTDT";
            this.txtTDT.Size = new System.Drawing.Size(190, 30);
            this.txtTDT.TabIndex = 5;
            this.txtTDT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tổng doanh thu:";
            // 
            // txtSoLuotDen
            // 
            this.txtSoLuotDen.Location = new System.Drawing.Point(163, 48);
            this.txtSoLuotDen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoLuotDen.Name = "txtSoLuotDen";
            this.txtSoLuotDen.Size = new System.Drawing.Size(190, 30);
            this.txtSoLuotDen.TabIndex = 5;
            this.txtSoLuotDen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số lượt đến:";
            // 
            // cboNam
            // 
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(108, 114);
            this.cboNam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(195, 30);
            this.cboNam.TabIndex = 4;
            // 
            // cboThang
            // 
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(108, 71);
            this.cboThang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(195, 30);
            this.cboThang.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Năm:";
            // 
            // btnTKKhachHang
            // 
            this.btnTKKhachHang.BackColor = System.Drawing.Color.Teal;
            this.btnTKKhachHang.ForeColor = System.Drawing.Color.White;
            this.btnTKKhachHang.Location = new System.Drawing.Point(108, 169);
            this.btnTKKhachHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTKKhachHang.Name = "btnTKKhachHang";
            this.btnTKKhachHang.Size = new System.Drawing.Size(195, 39);
            this.btnTKKhachHang.TabIndex = 3;
            this.btnTKKhachHang.Text = "Thống kê";
            this.btnTKKhachHang.UseVisualStyleBackColor = false;
            // 
            // txtSoKhachVip
            // 
            this.txtSoKhachVip.Location = new System.Drawing.Point(163, 110);
            this.txtSoKhachVip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoKhachVip.Name = "txtSoKhachVip";
            this.txtSoKhachVip.Size = new System.Drawing.Size(190, 30);
            this.txtSoKhachVip.TabIndex = 5;
            this.txtSoKhachVip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "Số khách Vip:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tháng:";
            // 
            // rdoHomNay
            // 
            this.rdoHomNay.AutoSize = true;
            this.rdoHomNay.Location = new System.Drawing.Point(36, 30);
            this.rdoHomNay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoHomNay.Name = "rdoHomNay";
            this.rdoHomNay.Size = new System.Drawing.Size(101, 26);
            this.rdoHomNay.TabIndex = 0;
            this.rdoHomNay.TabStop = true;
            this.rdoHomNay.Text = "Hôm nay";
            this.rdoHomNay.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboNam);
            this.groupBox2.Controls.Add(this.cboThang);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnTKKhachHang);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rdoHomNay);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(17, 404);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(345, 215);
            this.groupBox2.TabIndex = 90;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lựa chọn";
            // 
            // lstvDSKhachHang
            // 
            this.lstvDSKhachHang.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvDSKhachHang.HideSelection = false;
            this.lstvDSKhachHang.Location = new System.Drawing.Point(5, 32);
            this.lstvDSKhachHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstvDSKhachHang.Name = "lstvDSKhachHang";
            this.lstvDSKhachHang.Size = new System.Drawing.Size(788, 254);
            this.lstvDSKhachHang.TabIndex = 0;
            this.lstvDSKhachHang.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstvDSKhachHang);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 77);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(800, 300);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách khách hàng ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSoKhachVip);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtTDT);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtSoLuotDen);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(439, 404);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(373, 215);
            this.groupBox3.TabIndex = 91;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Teal;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1625, 50);
            this.label11.TabIndex = 88;
            this.label11.Text = "Thống kê";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmThongKeQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1625, 789);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKeQL";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.Text = "Thống kê - Quán Lý";
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chrThongKeSKhachHang)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrThongKeSKhachHang;
        private System.Windows.Forms.TextBox txtTDT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSoLuotDen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTKKhachHang;
        private System.Windows.Forms.TextBox txtSoKhachVip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoHomNay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstvDSKhachHang;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
    }
}