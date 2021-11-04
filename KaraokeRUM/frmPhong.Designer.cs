
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
            this.lstvDanhSachDP = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpnlPhongVip = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBOx = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoDatPhong = new System.Windows.Forms.RadioButton();
            this.rdoMoPhong = new System.Windows.Forms.RadioButton();
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
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpnlPhongThuong = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoTatCa = new System.Windows.Forms.RadioButton();
            this.rdoHienTai = new System.Windows.Forms.RadioButton();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXemPhong = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dtmGioDatPhong = new System.Windows.Forms.DateTimePicker();
            this.groupBox3.SuspendLayout();
            this.groupBOx.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstvDanhSachDP
            // 
            this.lstvDanhSachDP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvDanhSachDP.BackColor = System.Drawing.Color.White;
            this.lstvDanhSachDP.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvDanhSachDP.HideSelection = false;
            this.lstvDanhSachDP.Location = new System.Drawing.Point(19, 48);
            this.lstvDanhSachDP.Name = "lstvDanhSachDP";
            this.lstvDanhSachDP.Size = new System.Drawing.Size(801, 734);
            this.lstvDanhSachDP.TabIndex = 0;
            this.lstvDanhSachDP.UseCompatibleStateImageBehavior = false;
            this.lstvDanhSachDP.SelectedIndexChanged += new System.EventHandler(this.lvwDanhSachDP_SelectedIndexChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpnlPhongVip);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(12, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 170);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Phòng VIP";
            // 
            // fpnlPhongVip
            // 
            this.fpnlPhongVip.AutoScroll = true;
            this.fpnlPhongVip.Location = new System.Drawing.Point(6, 29);
            this.fpnlPhongVip.Name = "fpnlPhongVip";
            this.fpnlPhongVip.Size = new System.Drawing.Size(505, 135);
            this.fpnlPhongVip.TabIndex = 0;
            // 
            // groupBOx
            // 
            this.groupBOx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBOx.BackColor = System.Drawing.Color.White;
            this.groupBOx.Controls.Add(this.lstvDanhSachDP);
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
            this.label1.Size = new System.Drawing.Size(1628, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Phòng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.dtmGioDatPhong);
            this.groupBox1.Controls.Add(this.rdoDatPhong);
            this.groupBox1.Controls.Add(this.rdoMoPhong);
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
            this.groupBox1.Size = new System.Drawing.Size(710, 347);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phòng";
            // 
            // rdoDatPhong
            // 
            this.rdoDatPhong.AutoSize = true;
            this.rdoDatPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDatPhong.Location = new System.Drawing.Point(291, 33);
            this.rdoDatPhong.Name = "rdoDatPhong";
            this.rdoDatPhong.Size = new System.Drawing.Size(133, 31);
            this.rdoDatPhong.TabIndex = 27;
            this.rdoDatPhong.TabStop = true;
            this.rdoDatPhong.Text = "Đặt phòng";
            this.rdoDatPhong.UseVisualStyleBackColor = true;
            this.rdoDatPhong.CheckedChanged += new System.EventHandler(this.rdoMoPhong_CheckedChanged);
            // 
            // rdoMoPhong
            // 
            this.rdoMoPhong.AutoSize = true;
            this.rdoMoPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMoPhong.Location = new System.Drawing.Point(32, 33);
            this.rdoMoPhong.Name = "rdoMoPhong";
            this.rdoMoPhong.Size = new System.Drawing.Size(132, 31);
            this.rdoMoPhong.TabIndex = 26;
            this.rdoMoPhong.TabStop = true;
            this.rdoMoPhong.Text = "Mở phòng";
            this.rdoMoPhong.UseVisualStyleBackColor = true;
            this.rdoMoPhong.CheckedChanged += new System.EventHandler(this.rdoMoPhong_CheckedChanged);
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDienThoai.Location = new System.Drawing.Point(235, 124);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(404, 30);
            this.txtSoDienThoai.TabIndex = 1;
            this.txtSoDienThoai.Text = "0343220597";
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
            this.txtHoTen.Text = "Lê Tuấn";
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
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Location = new System.Drawing.Point(47, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(710, 419);
            this.panel2.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Teal;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(535, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 41);
            this.label9.TabIndex = 7;
            this.label9.Text = "Phòng Mở";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Green;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(535, 258);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(168, 41);
            this.label10.TabIndex = 8;
            this.label10.Text = "Phòng Đang Chọn";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Orange;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(535, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 41);
            this.label7.TabIndex = 6;
            this.label7.Text = "Phòng Đặt";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(535, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 41);
            this.label6.TabIndex = 5;
            this.label6.Text = "Phòng Đóng";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(529, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 45);
            this.label5.TabIndex = 4;
            this.label5.Text = "Chú thích";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpnlPhongThuong);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(12, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 183);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phòng THƯỜNG";
            // 
            // fpnlPhongThuong
            // 
            this.fpnlPhongThuong.AutoScroll = true;
            this.fpnlPhongThuong.Location = new System.Drawing.Point(6, 29);
            this.fpnlPhongThuong.Name = "fpnlPhongThuong";
            this.fpnlPhongThuong.Size = new System.Drawing.Size(505, 148);
            this.fpnlPhongThuong.TabIndex = 0;
            // 
            // rdoTatCa
            // 
            this.rdoTatCa.AutoSize = true;
            this.rdoTatCa.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoTatCa.Location = new System.Drawing.Point(793, 915);
            this.rdoTatCa.Name = "rdoTatCa";
            this.rdoTatCa.Size = new System.Drawing.Size(93, 31);
            this.rdoTatCa.TabIndex = 26;
            this.rdoTatCa.TabStop = true;
            this.rdoTatCa.Text = "Tất cả";
            this.rdoTatCa.UseVisualStyleBackColor = true;
            this.rdoTatCa.CheckedChanged += new System.EventHandler(this.rdoTatCa_CheckedChanged);
            // 
            // rdoHienTai
            // 
            this.rdoHienTai.AutoSize = true;
            this.rdoHienTai.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoHienTai.Location = new System.Drawing.Point(988, 915);
            this.rdoHienTai.Name = "rdoHienTai";
            this.rdoHienTai.Size = new System.Drawing.Size(109, 31);
            this.rdoHienTai.TabIndex = 27;
            this.rdoHienTai.TabStop = true;
            this.rdoHienTai.Text = "Hiện tại";
            this.rdoHienTai.UseVisualStyleBackColor = true;
            this.rdoHienTai.CheckedChanged += new System.EventHandler(this.rdoTatCa_CheckedChanged);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Teal;
            this.btnLamMoi.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(531, 983);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(203, 52);
            this.btnLamMoi.TabIndex = 28;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXemPhong
            // 
            this.btnXemPhong.BackColor = System.Drawing.Color.Teal;
            this.btnXemPhong.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemPhong.ForeColor = System.Drawing.Color.White;
            this.btnXemPhong.Location = new System.Drawing.Point(47, 983);
            this.btnXemPhong.Name = "btnXemPhong";
            this.btnXemPhong.Size = new System.Drawing.Size(203, 52);
            this.btnXemPhong.TabIndex = 29;
            this.btnXemPhong.Text = "Xem Phòng";
            this.btnXemPhong.UseVisualStyleBackColor = false;
            this.btnXemPhong.Click += new System.EventHandler(this.btnXemPhong_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.Teal;
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(289, 983);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(203, 52);
            this.btnTimKiem.TabIndex = 30;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dtmGioDatPhong
            // 
            this.dtmGioDatPhong.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtmGioDatPhong.Location = new System.Drawing.Point(235, 305);
            this.dtmGioDatPhong.Name = "dtmGioDatPhong";
            this.dtmGioDatPhong.Size = new System.Drawing.Size(403, 34);
            this.dtmGioDatPhong.TabIndex = 28;
            // 
            // frmPhong
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1628, 1097);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnXemPhong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.rdoHienTai);
            this.Controls.Add(this.rdoTatCa);
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
            this.Text = "Phong";
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
        private System.Windows.Forms.ListView lstvDanhSachDP;
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
        private System.Windows.Forms.RadioButton rdoTatCa;
        private System.Windows.Forms.RadioButton rdoHienTai;
        private System.Windows.Forms.RadioButton rdoMoPhong;
        private System.Windows.Forms.FlowLayoutPanel fpnlPhongVip;
        private System.Windows.Forms.FlowLayoutPanel fpnlPhongThuong;
        private System.Windows.Forms.RadioButton rdoDatPhong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnXemPhong;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DateTimePicker dtmGioDatPhong;
    }
}