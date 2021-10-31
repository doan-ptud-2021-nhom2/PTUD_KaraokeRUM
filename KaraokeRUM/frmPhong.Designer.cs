
namespace KaraokeRUM
{
    partial class frmPhong
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
            this.lvwDanhSachDP = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBOx = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radDatPhong = new System.Windows.Forms.RadioButton();
            this.txtGioDatPhong = new System.Windows.Forms.TextBox();
            this.radMoPhong = new System.Windows.Forms.RadioButton();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtTenPhong = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dTimeNgayNhan = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dTimeDatPhong = new System.Windows.Forms.DateTimePicker();
            this.btnMoPhong = new System.Windows.Forms.Button();
            this.btnHuyPhong = new System.Windows.Forms.Button();
            this.btnDatPhong = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radTatCa = new System.Windows.Forms.RadioButton();
            this.radHienTai = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.groupBOx.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwDanhSachDP
            // 
            this.lvwDanhSachDP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDanhSachDP.BackColor = System.Drawing.Color.White;
            this.lvwDanhSachDP.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwDanhSachDP.HideSelection = false;
            this.lvwDanhSachDP.Location = new System.Drawing.Point(19, 48);
            this.lvwDanhSachDP.Name = "lvwDanhSachDP";
            this.lvwDanhSachDP.Size = new System.Drawing.Size(801, 734);
            this.lvwDanhSachDP.TabIndex = 0;
            this.lvwDanhSachDP.UseCompatibleStateImageBehavior = false;
            this.lvwDanhSachDP.SelectedIndexChanged += new System.EventHandler(this.lvwDanhSachDP_SelectedIndexChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(12, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(657, 170);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Phòng VIP";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 29);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(645, 135);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBOx
            // 
            this.groupBOx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBOx.BackColor = System.Drawing.Color.White;
            this.groupBOx.Controls.Add(this.lvwDanhSachDP);
            this.groupBOx.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBOx.ForeColor = System.Drawing.Color.Black;
            this.groupBOx.Location = new System.Drawing.Point(774, 53);
            this.groupBOx.Name = "groupBOx";
            this.groupBOx.Size = new System.Drawing.Size(842, 798);
            this.groupBOx.TabIndex = 24;
            this.groupBOx.TabStop = false;
            this.groupBOx.Text = "Danh sách đặt phòng";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label4.Location = new System.Drawing.Point(41, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(693, 45);
            this.label4.TabIndex = 25;
            this.label4.Text = "Danh sách phòng";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1628, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Phòng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.radDatPhong);
            this.groupBox1.Controls.Add(this.txtGioDatPhong);
            this.groupBox1.Controls.Add(this.radMoPhong);
            this.groupBox1.Controls.Add(this.txtSoDienThoai);
            this.groupBox1.Controls.Add(this.txtTenPhong);
            this.groupBox1.Controls.Add(this.txtHoTen);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.dTimeNgayNhan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dTimeDatPhong);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(47, 523);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 328);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phòng";
            // 
            // radDatPhong
            // 
            this.radDatPhong.AutoSize = true;
            this.radDatPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDatPhong.Location = new System.Drawing.Point(291, 33);
            this.radDatPhong.Name = "radDatPhong";
            this.radDatPhong.Size = new System.Drawing.Size(133, 31);
            this.radDatPhong.TabIndex = 27;
            this.radDatPhong.TabStop = true;
            this.radDatPhong.Text = "Đặt phòng";
            this.radDatPhong.UseVisualStyleBackColor = true;
            this.radDatPhong.CheckedChanged += new System.EventHandler(this.radMoPhong_CheckedChanged);
            // 
            // txtGioDatPhong
            // 
            this.txtGioDatPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGioDatPhong.Location = new System.Drawing.Point(235, 273);
            this.txtGioDatPhong.Name = "txtGioDatPhong";
            this.txtGioDatPhong.Size = new System.Drawing.Size(404, 30);
            this.txtGioDatPhong.TabIndex = 17;
            // 
            // radMoPhong
            // 
            this.radMoPhong.AutoSize = true;
            this.radMoPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radMoPhong.Location = new System.Drawing.Point(32, 33);
            this.radMoPhong.Name = "radMoPhong";
            this.radMoPhong.Size = new System.Drawing.Size(132, 31);
            this.radMoPhong.TabIndex = 26;
            this.radMoPhong.TabStop = true;
            this.radMoPhong.Text = "Mở phòng";
            this.radMoPhong.UseVisualStyleBackColor = true;
            this.radMoPhong.CheckedChanged += new System.EventHandler(this.radMoPhong_CheckedChanged);
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDienThoai.Location = new System.Drawing.Point(235, 124);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(404, 30);
            this.txtSoDienThoai.TabIndex = 1;
            this.txtSoDienThoai.Text = "012356789";
            // 
            // txtTenPhong
            // 
            this.txtTenPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenPhong.Location = new System.Drawing.Point(235, 162);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new System.Drawing.Size(404, 30);
            this.txtTenPhong.TabIndex = 16;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoTen.Location = new System.Drawing.Point(235, 86);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(404, 30);
            this.txtHoTen.TabIndex = 1;
            this.txtHoTen.Text = "Lên Văn A";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(28, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(207, 25);
            this.label20.TabIndex = 13;
            this.label20.Text = "Tên Phòng:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(28, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(207, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "Số điện thoại";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(28, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(201, 35);
            this.label13.TabIndex = 0;
            this.label13.Text = "Họ tên";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "Giờ đặt phòng:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(28, 197);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(201, 31);
            this.label14.TabIndex = 11;
            this.label14.Text = "Ngày đặt phòng:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dTimeNgayNhan
            // 
            this.dTimeNgayNhan.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTimeNgayNhan.CustomFormat = "dd/MM/yyyy";
            this.dTimeNgayNhan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTimeNgayNhan.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTimeNgayNhan.Location = new System.Drawing.Point(235, 232);
            this.dTimeNgayNhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dTimeNgayNhan.Name = "dTimeNgayNhan";
            this.dTimeNgayNhan.Size = new System.Drawing.Size(404, 30);
            this.dTimeNgayNhan.TabIndex = 9;
            this.dTimeNgayNhan.Value = new System.DateTime(2021, 5, 21, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 30);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ngày nhận phòng:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dTimeDatPhong
            // 
            this.dTimeDatPhong.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTimeDatPhong.CustomFormat = "dd/MM/yyyy";
            this.dTimeDatPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTimeDatPhong.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTimeDatPhong.Location = new System.Drawing.Point(235, 198);
            this.dTimeDatPhong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dTimeDatPhong.Name = "dTimeDatPhong";
            this.dTimeDatPhong.Size = new System.Drawing.Size(403, 30);
            this.dTimeDatPhong.TabIndex = 8;
            this.dTimeDatPhong.Value = new System.DateTime(2021, 5, 12, 0, 0, 0, 0);
            // 
            // btnMoPhong
            // 
            this.btnMoPhong.BackColor = System.Drawing.Color.Teal;
            this.btnMoPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoPhong.ForeColor = System.Drawing.Color.White;
            this.btnMoPhong.Location = new System.Drawing.Point(289, 903);
            this.btnMoPhong.Name = "btnMoPhong";
            this.btnMoPhong.Size = new System.Drawing.Size(203, 52);
            this.btnMoPhong.TabIndex = 14;
            this.btnMoPhong.Text = "Mở Phòng";
            this.btnMoPhong.UseVisualStyleBackColor = false;
            this.btnMoPhong.Click += new System.EventHandler(this.btnMoPhong_Click);
            // 
            // btnHuyPhong
            // 
            this.btnHuyPhong.BackColor = System.Drawing.Color.Teal;
            this.btnHuyPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyPhong.ForeColor = System.Drawing.Color.White;
            this.btnHuyPhong.Location = new System.Drawing.Point(531, 903);
            this.btnHuyPhong.Name = "btnHuyPhong";
            this.btnHuyPhong.Size = new System.Drawing.Size(203, 52);
            this.btnHuyPhong.TabIndex = 14;
            this.btnHuyPhong.Text = "Hủy Đặt Phòng";
            this.btnHuyPhong.UseVisualStyleBackColor = false;
            this.btnHuyPhong.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDatPhong
            // 
            this.btnDatPhong.BackColor = System.Drawing.Color.Teal;
            this.btnDatPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatPhong.ForeColor = System.Drawing.Color.White;
            this.btnDatPhong.Location = new System.Drawing.Point(47, 903);
            this.btnDatPhong.Name = "btnDatPhong";
            this.btnDatPhong.Size = new System.Drawing.Size(203, 52);
            this.btnDatPhong.TabIndex = 14;
            this.btnDatPhong.Text = "Đặt Phòng";
            this.btnDatPhong.UseVisualStyleBackColor = false;
            this.btnDatPhong.Click += new System.EventHandler(this.btnDatPhong_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Location = new System.Drawing.Point(47, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(687, 419);
            this.panel2.TabIndex = 23;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(12, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(659, 183);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phòng THƯỜNG";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(8, 29);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(645, 148);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // radTatCa
            // 
            this.radTatCa.AutoSize = true;
            this.radTatCa.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radTatCa.Location = new System.Drawing.Point(831, 915);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(93, 31);
            this.radTatCa.TabIndex = 26;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            this.radTatCa.UseVisualStyleBackColor = true;
            // 
            // radHienTai
            // 
            this.radHienTai.AutoSize = true;
            this.radHienTai.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radHienTai.Location = new System.Drawing.Point(1026, 915);
            this.radHienTai.Name = "radHienTai";
            this.radHienTai.Size = new System.Drawing.Size(109, 31);
            this.radHienTai.TabIndex = 27;
            this.radHienTai.TabStop = true;
            this.radHienTai.Text = "Hiện tại";
            this.radHienTai.UseVisualStyleBackColor = true;
            // 
            // frmPhong
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1628, 1082);
            this.Controls.Add(this.radHienTai);
            this.Controls.Add(this.radTatCa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBOx);
            this.Controls.Add(this.btnMoPhong);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnHuyPhong);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnDatPhong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhong";
            this.Text = "Form3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPhong_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBOx.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lvwDanhSachDP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBOx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.Button btnMoPhong;
        private System.Windows.Forms.Button btnHuyPhong;
        private System.Windows.Forms.TextBox txtTenPhong;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDatPhong;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dTimeNgayNhan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dTimeDatPhong;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtGioDatPhong;
        private System.Windows.Forms.RadioButton radTatCa;
        private System.Windows.Forms.RadioButton radHienTai;
        private System.Windows.Forms.RadioButton radMoPhong;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radDatPhong;
    }
}