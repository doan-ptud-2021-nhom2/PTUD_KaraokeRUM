
namespace KaraokeRUM
{
    partial class frmQuanLyThietBi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyThietBi));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboDonVi = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.txtSoLuongTon = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstvThietBi = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboTenPhong = new System.Windows.Forms.ComboBox();
            this.cboTenTTB = new System.Windows.Forms.ComboBox();
            this.txtSoLuongTP = new System.Windows.Forms.TextBox();
            this.btnThemTP = new System.Windows.Forms.Button();
            this.btnSuaTP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnXoaTP = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstvThietBiTrongPhong = new System.Windows.Forms.ListView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            this.txtID.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.cboDonVi);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDonGia);
            this.groupBox2.Controls.Add(this.txtSoLuongTon);
            this.groupBox2.Controls.Add(this.btnThem);
            this.groupBox2.Controls.Add(this.btnSua);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtTen);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnXoa);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 167);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(545, 299);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin trang thiết bị";
            // 
            // cboDonVi
            // 
            this.cboDonVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonVi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.Location = new System.Drawing.Point(166, 143);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(357, 30);
            this.cboDonVi.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 22);
            this.label8.TabIndex = 32;
            this.label8.Text = "Đơn vị:";
            // 
            // txtDonGia
            // 
            this.txtDonGia.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonGia.Location = new System.Drawing.Point(166, 182);
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(357, 30);
            this.txtDonGia.TabIndex = 3;
            this.txtDonGia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDonGia_KeyPress);
            // 
            // txtSoLuongTon
            // 
            this.txtSoLuongTon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoLuongTon.Location = new System.Drawing.Point(166, 101);
            this.txtSoLuongTon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoLuongTon.Name = "txtSoLuongTon";
            this.txtSoLuongTon.Size = new System.Drawing.Size(357, 30);
            this.txtSoLuongTon.TabIndex = 1;
            this.txtSoLuongTon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuongTon_KeyPress);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Teal;
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.SystemColors.Control;
            this.btnThem.Location = new System.Drawing.Point(10, 231);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.Teal;
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSua.Location = new System.Drawing.Point(194, 231);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(142, 45);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "Đơn giá:";
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(166, 58);
            this.txtTen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(357, 30);
            this.txtTen.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Số lượng tồn:";
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.Teal;
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.SystemColors.Control;
            this.btnXoa.Location = new System.Drawing.Point(386, 231);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(137, 45);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên trang thiết bị:";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Teal;
            this.btnLamMoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLamMoi.Location = new System.Drawing.Point(451, 66);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(108, 45);
            this.btnLamMoi.TabIndex = 7;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.Silver;
            this.txtID.Controls.Add(this.label11);
            this.txtID.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtID.Location = new System.Drawing.Point(0, 0);
            this.txtID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(1595, 50);
            this.txtID.TabIndex = 43;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.Color.Teal;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1595, 50);
            this.label11.TabIndex = 3;
            this.label11.Text = "Quản lý trang thiết bị";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(141, 75);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(222, 30);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.Color.Teal;
            this.btnTim.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTim.Location = new System.Drawing.Point(369, 66);
            this.btnTim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(76, 45);
            this.btnTim.TabIndex = 1;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 33);
            this.label3.TabIndex = 27;
            this.label3.Text = "Tìm kiếm:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lstvThietBi);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(590, 54);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(979, 412);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách trang thiết bị ";
            // 
            // lstvThietBi
            // 
            this.lstvThietBi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvThietBi.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvThietBi.HideSelection = false;
            this.lstvThietBi.Location = new System.Drawing.Point(26, 43);
            this.lstvThietBi.Name = "lstvThietBi";
            this.lstvThietBi.Size = new System.Drawing.Size(934, 346);
            this.lstvThietBi.TabIndex = 0;
            this.lstvThietBi.UseCompatibleStateImageBehavior = false;
            this.lstvThietBi.SelectedIndexChanged += new System.EventHandler(this.lstvThietBi_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.cboTenPhong);
            this.groupBox3.Controls.Add(this.cboTenTTB);
            this.groupBox3.Controls.Add(this.txtSoLuongTP);
            this.groupBox3.Controls.Add(this.btnThemTP);
            this.groupBox3.Controls.Add(this.btnSuaTP);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnXoaTP);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 507);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(546, 294);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin trang thiết bị trong phòng";
            // 
            // cboTenPhong
            // 
            this.cboTenPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTenPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTenPhong.FormattingEnabled = true;
            this.cboTenPhong.Location = new System.Drawing.Point(162, 47);
            this.cboTenPhong.Name = "cboTenPhong";
            this.cboTenPhong.Size = new System.Drawing.Size(357, 30);
            this.cboTenPhong.TabIndex = 0;
            this.cboTenPhong.SelectedIndexChanged += new System.EventHandler(this.cboTenPhong_SelectedIndexChanged);
            // 
            // cboTenTTB
            // 
            this.cboTenTTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTenTTB.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTenTTB.FormattingEnabled = true;
            this.cboTenTTB.Location = new System.Drawing.Point(162, 100);
            this.cboTenTTB.Name = "cboTenTTB";
            this.cboTenTTB.Size = new System.Drawing.Size(357, 30);
            this.cboTenTTB.TabIndex = 1;
            // 
            // txtSoLuongTP
            // 
            this.txtSoLuongTP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoLuongTP.Location = new System.Drawing.Point(162, 151);
            this.txtSoLuongTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoLuongTP.Name = "txtSoLuongTP";
            this.txtSoLuongTP.Size = new System.Drawing.Size(357, 30);
            this.txtSoLuongTP.TabIndex = 2;
            this.txtSoLuongTP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuongTP_KeyPress);
            // 
            // btnThemTP
            // 
            this.btnThemTP.BackColor = System.Drawing.Color.Teal;
            this.btnThemTP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemTP.ForeColor = System.Drawing.SystemColors.Control;
            this.btnThemTP.Location = new System.Drawing.Point(10, 229);
            this.btnThemTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThemTP.Name = "btnThemTP";
            this.btnThemTP.Size = new System.Drawing.Size(140, 45);
            this.btnThemTP.TabIndex = 3;
            this.btnThemTP.Text = "Thêm";
            this.btnThemTP.UseVisualStyleBackColor = false;
            this.btnThemTP.Click += new System.EventHandler(this.btnThemTP_Click);
            // 
            // btnSuaTP
            // 
            this.btnSuaTP.BackColor = System.Drawing.Color.Teal;
            this.btnSuaTP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaTP.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSuaTP.Location = new System.Drawing.Point(194, 229);
            this.btnSuaTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSuaTP.Name = "btnSuaTP";
            this.btnSuaTP.Size = new System.Drawing.Size(142, 45);
            this.btnSuaTP.TabIndex = 4;
            this.btnSuaTP.Text = "Sửa";
            this.btnSuaTP.UseVisualStyleBackColor = false;
            this.btnSuaTP.Click += new System.EventHandler(this.btnSuaTP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "Số lượng :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tên trang thiết bị:";
            // 
            // btnXoaTP
            // 
            this.btnXoaTP.BackColor = System.Drawing.Color.Teal;
            this.btnXoaTP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaTP.ForeColor = System.Drawing.SystemColors.Control;
            this.btnXoaTP.Location = new System.Drawing.Point(386, 229);
            this.btnXoaTP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoaTP.Name = "btnXoaTP";
            this.btnXoaTP.Size = new System.Drawing.Size(137, 45);
            this.btnXoaTP.TabIndex = 5;
            this.btnXoaTP.Text = "Xóa";
            this.btnXoaTP.UseVisualStyleBackColor = false;
            this.btnXoaTP.Click += new System.EventHandler(this.btnXoaTP_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tên phòng:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.lstvThietBiTrongPhong);
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(590, 507);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(979, 294);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách trang thiết bị trong phòng";
            // 
            // lstvThietBiTrongPhong
            // 
            this.lstvThietBiTrongPhong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvThietBiTrongPhong.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstvThietBiTrongPhong.HideSelection = false;
            this.lstvThietBiTrongPhong.Location = new System.Drawing.Point(26, 51);
            this.lstvThietBiTrongPhong.Name = "lstvThietBiTrongPhong";
            this.lstvThietBiTrongPhong.Size = new System.Drawing.Size(934, 223);
            this.lstvThietBiTrongPhong.TabIndex = 0;
            this.lstvThietBiTrongPhong.UseCompatibleStateImageBehavior = false;
            this.lstvThietBiTrongPhong.SelectedIndexChanged += new System.EventHandler(this.lstvThietBiTrongPhong_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmQuanLyThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1595, 840);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQuanLyThietBi";
            this.Text = "-";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmQuanLyThietBi_Load);
            this.Click += new System.EventHandler(this.frmQuanLyThietBi_Click);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtID.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.TextBox txtSoLuongTon;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel txtID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstvThietBi;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSoLuongTP;
        private System.Windows.Forms.Button btnThemTP;
        private System.Windows.Forms.Button btnSuaTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnXoaTP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView lstvThietBiTrongPhong;
        private System.Windows.Forms.ComboBox cboDonVi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboTenPhong;
        private System.Windows.Forms.ComboBox cboTenTTB;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}