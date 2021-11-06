using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaraokeRUM
{
    public partial class frmPhong : Form
    {

        private clsHonLoan HONLOAN;
        private clsTaoButton TAOBUTTON;
        private clsKhachHang KHACHHANG;
        private clsDonDatPhong DONDATPHONG;
        private clsPhong PHONG;
        private clsHoaDon HOADON;
        public string MAHD;
        public string MAPHONG;
        private IEnumerable<Phong> DANHSACHPHONGVIP;
        private IEnumerable<Phong> DANHSACHPHONGTHUONG;
        protected string MAQL;

        public frmPhong(string maQL)
        {
            InitializeComponent();
            this.MAQL = maQL;
        }


        private void frmPhong_Load(object sender, EventArgs e)
        {
            KHACHHANG = new clsKhachHang();
            DONDATPHONG = new clsDonDatPhong();
            TAOBUTTON = new clsTaoButton();
            PHONG = new clsPhong();
            HONLOAN = new clsHonLoan();
            HOADON = new clsHoaDon();

            dtmGioDatPhong.ShowUpDown = true;
            dtmGioDatPhong.Format = DateTimePickerFormat.Custom;
            dtmGioDatPhong.CustomFormat = "HH:mm:ss";

            btnXemPhong.Enabled = false;

            MacDinhThoiGian();
            rdoMoPhong.Checked = true;            
            fpnlPhongVip.Controls.Clear();
            fpnlPhongThuong.Controls.Clear();
            XuLyDatPhong();
            DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
            DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
            TaoPhongVip(DANHSACHPHONGVIP);
            TaoPhongThuong(DANHSACHPHONGTHUONG);
            IEnumerable<dynamic> dsDP;
            dsDP = HONLOAN.LayThongTinDonDatPhong();
            TaoListView(lstvDanhSachDP);
            TaiDuLieuLenListView(lstvDanhSachDP, dsDP);           
        }
        /** Mặc định thời gian cho ngày đặt phòng và ngày nhận phòng **/
        private void MacDinhThoiGian()
        {
            dTimeDatPhong.Value = DateTime.Now;
            dTimeNgayNhan.Value = DateTime.Now;
        }
        /** Kiểm tra thời gian ngày nhận **/
        private Boolean KiemTraNgayNhan()
        {
            DateTime tgian = DateTime.Now.Date;
            int soSanh = DateTime.Compare(dTimeNgayNhan.Value.Date, tgian);
            if (soSanh < 0)
            {
                return false;
            }
            return true;
        }
        #region Load Dữ liệu lên
        /** Tạo phòng Vip **/
        void TaoPhongVip(IEnumerable<Phong> n)
        {
            TAOBUTTON = new clsTaoButton();
            Button btn;
            for (int i = 0; i < n.Count(); i++)
            {
                btn = new Button();
                btn.Name = "btnVip" + (i +1).ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.Text = n.ElementAt(i).TenPhong;
                btn.BackColor = Color.Teal;
                btn.ForeColor = Color.White;
                foreach (dynamic item in TAOBUTTON.LayTrangThaiPhong("LP001"))
                {
                    if (btn.Text.Equals(item.TenPhong))
                    {
                        if (item.TrangThaiPhong == "Mở")
                        {
                            btn.BackColor = Color.Teal;
                        }
                        else if (item.TrangThaiPhong == "Đặt")
                        {
                            btn.BackColor = Color.Orange;
                        }
                        else if (item.TrangThaiPhong == "Đóng")
                        {
                            btn.BackColor = Color.Gray;
                        }
                    }
                }
                btn.Click += new EventHandler(SuKienChonButton);
                fpnlPhongVip.Controls.Add(btn);
            }
        }
        /** Tạo phòng Thường **/
        void TaoPhongThuong(IEnumerable<Phong> n)
        {
            TAOBUTTON = new clsTaoButton();
            Button btn;
            for (int i = 0; i < n.Count(); i++)
            {
                btn = new Button();
                btn.Name = "btnThuong" + (i+1).ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.ForeColor = Color.White;
                btn.Text = n.ElementAt(i).TenPhong;
                btn.BackColor = Color.Teal;
                foreach (var item in TAOBUTTON.LayTrangThaiPhong("LP002"))
                {
                    if (btn.Text.Equals(item.TenPhong))
                    {
                        if (item.TrangThaiPhong == "Mở")
                        {
                            btn.BackColor = Color.Teal;
                        }
                        else if (item.TrangThaiPhong == "Đặt")
                        {
                            btn.BackColor = Color.Orange;
                        }
                        else if (item.TrangThaiPhong == "Đóng")
                        {
                            btn.BackColor = Color.Gray;
                        }
                    }
                }
                btn.Click += new EventHandler(SuKienChonButton);
                fpnlPhongThuong.Controls.Add(btn);
            }
        }
        /** Cài đặt chú thích cho các trạng thái phòng **/
        private void SuKienChonButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;// ép kiểu;
            if (btn.BackColor == Color.Gray)
            {
                btnMoPhong.Enabled = true;
                /*btn.BackColor = Color.Green;*/
                txtTenPhong.Text = btn.Text;
                btnXemPhong.Enabled = false;
            }
            else if (btn.BackColor == Color.Teal)
            {
                txtTenPhong.Text = btn.Text;
                btnMoPhong.Enabled = false;
                btnXemPhong.Enabled = true;
            }          
            else if (btn.BackColor == Color.Orange)
            {
                /*MessageBox.Show("Phòng đã đặt, Vui lòng chọn phòng khác", "Thông báo");*/
                btnMoPhong.Enabled = false;
                txtTenPhong.Text = btn.Text;
                btnXemPhong.Enabled = false;
            }
            else if(btn.BackColor == Color.Green)
            {
                btn.BackColor = Color.Gray;
            }    
            else
            {
                btn.BackColor = Color.Green;
                txtTenPhong.Text = "";
            }
        }
        /** Tải dữ liệu lên listview **/
        private void TaiDuLieuLenListView(ListView lvwDatPhong, IEnumerable<dynamic> dp)
        {
            lvwDatPhong.Items.Clear();
            ListViewItem lvwItem;
            foreach (dynamic i in dp)
            {
                lvwItem = taoItem(i);
                lvwDatPhong.Items.Add(lvwItem);
            }
        }
        /** Tạo item của listView **/
        private ListViewItem taoItem(dynamic ddp)
        {
            ListViewItem item;
            item = new ListViewItem(ddp.TenPhong);
            item.SubItems.Add(ddp.TenKhach);
            item.SubItems.Add(ddp.SDT);
            item.SubItems.Add(ddp.NgayDat.ToString("dd/MM/yyyy"));
            item.SubItems.Add(ddp.NgayNhan.ToString("dd/MM/yyyy"));
            item.SubItems.Add(ddp.GioDat.ToString());
            item.Tag = ddp;
            return item;
        }
        /** Tạo listView **/
        private void TaoListView(ListView lvwDSDatPhong)
        {
            lvwDSDatPhong.Columns.Add("Tên Phòng", 120);
            lvwDSDatPhong.Columns.Add("Tên khách hàng", 200);
            lvwDSDatPhong.Columns.Add("Số điện thoại", 150);
            lvwDSDatPhong.Columns.Add("Ngày đặt phòng", 150);
            lvwDSDatPhong.Columns.Add("Ngày nhận phòng", 150);
            lvwDSDatPhong.Columns.Add("Giờ đặt phòng", 150);
            lvwDSDatPhong.View = View.Details;
            lvwDSDatPhong.GridLines = true;
            lvwDSDatPhong.FullRowSelect = true;
        }
        /** Đẩy dữ liệu lên các TextBox **/
        private void DuLieuLenTextBox(dynamic dp)
        {
            txtHoTen.Text = dp.TenKhach;
            dtmGioDatPhong.Text = dp.GioDat.ToString();
            txtSoDienThoai.Text = dp.SDT;
            txtTenPhong.Text = dp.TenPhong;
            dTimeDatPhong.Value = dp.NgayDat;
            dTimeNgayNhan.Value = dp.NgayNhan;
        }       
        #endregion
        #region Đặt phòng
        private void XoaDuLieuTextBox()
        {
            dtmGioDatPhong.Text = "";
            txtHoTen.Text = "";
            txtSoDienThoai.Text = "";
            txtTenPhong.Text = "";
            dTimeDatPhong.Text = "";
            dTimeNgayNhan.Text = "";

        }    
        private void DatPhong(string maKH)
        {
            DonDatPhong ddp = new DonDatPhong();
            ddp = TaoDonDatPhong(maKH);
            DONDATPHONG.ThemDonDatPhong(ddp);
            fpnlPhongVip.Controls.Clear();
            fpnlPhongThuong.Controls.Clear();
            DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
            TaoPhongVip(DANHSACHPHONGVIP);
            DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
            TaoPhongThuong(DANHSACHPHONGTHUONG);
            IEnumerable<dynamic> danhsDP;
            danhsDP = HONLOAN.LayThongTinDonDatPhong();
            TaiDuLieuLenListView(lstvDanhSachDP, danhsDP);
        }
        /** Chức năng đặt phòng **/
        private void btnDatPhong_Click(object sender, EventArgs e)
        {

            DateTime gioBD = new DateTime(2021, 11, 5, 8, 0, 0);
            int xetGioBatDau = (int)(dtmGioDatPhong.Value.Hour - gioBD.Hour);
            DateTime gioKT = new DateTime(2021, 11, 5, 22, 0, 0);
            int xetGioKetThuc = (int)(dtmGioDatPhong.Value.Hour - gioKT.Hour);

            DateTime dt = DateTime.Now;
            TimeSpan tp = dt.TimeOfDay;
            TimeSpan gioDat = TimeSpan.Parse(dtmGioDatPhong.Text);
            double xetGio1 = (double)(gioDat - tp).TotalHours;

            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text) || string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!KiemTraSoDienThoai())
            {
                MessageBox.Show("Yêu cầu nhập đúng số điện thoại khách hàng", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!KiemTraNgayNhan())
            {
                MessageBox.Show("Ngày nhận không phù hợp", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!((dTimeDatPhong.Value.Date).Equals(DateTime.Now.Date)))
            {
                MessageBox.Show("Ngày đặt phải là ngày hôm nay", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {                             
                if ((dTimeNgayNhan.Value.Date).Equals(dt.Date))
                {
                    if (xetGio1 < 0)
                    {
                        MessageBox.Show("Vì ngày nhận là ngày hôm nay nên giờ đặt phải sau giờ hiện tại", "Thông báo", MessageBoxButtons.OK);
                    }
                    else if (xetGioKetThuc > 0)
                    {
                        MessageBox.Show("Giờ đặt phải trước 23h", "Thông báo", MessageBoxButtons.OK);
                    }                   
                    else
                    {
                        string maP = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
                        string trangThaiPhong = PHONG.TimPhongTheoMa(maP).First().TrangThaiPhong;
                        KhachHang khc = KHACHHANG.TimKhachHang(txtSoDienThoai.Text);
                        string maKH = "";
                        if (khc != null)
                        {
                            maKH = khc.MaKH;
                        }
                        else
                        {
                            KhachHang khm = new KhachHang();
                            khm = TaoKhachHang();
                            KHACHHANG.ThemKhachHang(khm);
                            maKH = khm.MaKH;
                        }
                        if (trangThaiPhong == "Mở")
                        {
                            string maPhong = PHONG.TimMaPhong(txtTenPhong.Text).MaPhong;
                            DateTime ngay = DateTime.Parse(dTimeNgayNhan.Value.ToString("yyyy-MM-dd"));
                            TimeSpan gio = TimeSpan.Parse(dtmGioDatPhong.Text);
                            HoaDon hoaDon = HOADON.TimHoaDonTheoMaPhong(maPhong);
                            double xetGio = (double)(hoaDon.GioVao - gio).TotalHours;
                            if ((hoaDon.NgayLap).Equals(ngay))
                            {
                                if (!KiemTraNgayGioDat() || xetGio > 0 || xetGio == 0 || xetGio > -3)
                                {
                                    MessageBox.Show("Phòng đang sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    DatPhong(maKH);
                                }
                            }
                        }
                        else if (trangThaiPhong == "Đặt")
                        {
                            if (!KiemTraNgayGioDat())
                            {
                                MessageBox.Show("Phòng đã được đặt trong khoảng thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                /*DonDatPhong ddp = new DonDatPhong();
                                ddp = TaoDonDatPhong(maKH);
                                DONDATPHONG.ThemDonDatPhong(ddp);
                                fpnlPhongVip.Controls.Clear();
                                fpnlPhongThuong.Controls.Clear();
                                DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
                                TaoPhongVip(DANHSACHPHONGVIP);
                                DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
                                TaoPhongThuong(DANHSACHPHONGTHUONG);
                                IEnumerable<dynamic> danhsDP;
                                danhsDP = HONLOAN.LayThongTinDonDatPhong();
                                TaiDuLieuLenListView(lstvDanhSachDP, danhsDP);*/
                                DatPhong(maKH);

                            }
                            XoaDuLieuTextBox();
                        }
                        else if (trangThaiPhong == "Đóng")
                        {
                            if (!KiemTraNgayGioDat())
                            {
                                MessageBox.Show("Phòng đã được đặt trong khoảng thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                /*DonDatPhong ddp = new DonDatPhong();
                                ddp = TaoDonDatPhong(maKH);
                                DONDATPHONG.ThemDonDatPhong(ddp);
                                fpnlPhongVip.Controls.Clear();
                                fpnlPhongThuong.Controls.Clear();
                                DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
                                TaoPhongVip(DANHSACHPHONGVIP);
                                DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
                                TaoPhongThuong(DANHSACHPHONGTHUONG);
                                IEnumerable<dynamic> danhsDP;
                                danhsDP = HONLOAN.LayThongTinDonDatPhong();
                                TaiDuLieuLenListView(lstvDanhSachDP, danhsDP);*/
                                DatPhong(maKH);

                            }
                        }
                        XoaDuLieuTextBox();
                    }    
                }
                else
                {
                    if (xetGioBatDau < 0 || xetGioKetThuc > 0)
                    {
                        MessageBox.Show("Giờ đặt phải sau 8 giờ và trước 23h", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        string maP = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
                        string trangThaiPhong = PHONG.TimPhongTheoMa(maP).First().TrangThaiPhong;
                        KhachHang khc = KHACHHANG.TimKhachHang(txtSoDienThoai.Text);
                        string maKH = "";
                        if (khc != null)
                        {
                            maKH = khc.MaKH;
                        }
                        else
                        {
                            KhachHang khm = new KhachHang();
                            khm = TaoKhachHang();
                            KHACHHANG.ThemKhachHang(khm);
                            maKH = khm.MaKH;
                        }
                        /*if (trangThaiPhong == "Mở")
                        {
                            string maPhong = PHONG.TimMaPhong(txtTenPhong.Text).MaPhong;
                            DateTime ngay = DateTime.Parse(dTimeNgayNhan.Value.ToString("yyyy-MM-dd"));
                            TimeSpan gio = TimeSpan.Parse(dtmGioDatPhong.Text);
                            HoaDon hoaDon = HOADON.TimHoaDonTheoMaPhong(maPhong);
                            double xetGio = (double)(hoaDon.GioVao - gio).TotalHours;
                            if ((hoaDon.NgayLap).Equals(ngay))
                            {
                                if (!KiemTraNgayGioDat() || xetGio > 0 || xetGio == 0 || xetGio > -3)
                                {
                                    MessageBox.Show("Phòng đang sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    DatPhong(maKH);
                                }
                            }                         
                        }
                        else if (trangThaiPhong == "Đặt")
                        {
                            if (!KiemTraNgayGioDat())
                            {
                                MessageBox.Show("Phòng đã được đặt trong khoảng thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                DatPhong(maKH);
                            }
                            XoaDuLieuTextBox();
                        }
                        else if (trangThaiPhong == "Đóng")
                        {*/
                        if (!KiemTraNgayGioDat())
                        {
                            MessageBox.Show("Phòng đã được đặt trong khoảng thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            DatPhong(maKH);
                        }
                        /*}*/
                        XoaDuLieuTextBox();
                    }    
                }                
            }                      
        }        
        private bool KiemTraNgayGioDat()
        {
            string maPhong = PHONG.TimMaPhong(txtTenPhong.Text).MaPhong;
            dynamic dsach = HONLOAN.LayThongTinDonDatPhongTheoMaPhong(maPhong);
            DateTime ngay = DateTime.Parse(dTimeNgayNhan.Value.ToString("yyyy-MM-dd"));
            TimeSpan gio = TimeSpan.Parse(dtmGioDatPhong.Text);
            foreach (dynamic i in HONLOAN.LayThongTinDonDatPhongTheoMaPhong(maPhong))
            {                 
                double xetGio = (double)(i.GioDat - gio).TotalHours;
                MessageBox.Show(xetGio.ToString());
                if ((i.NgayNhan).Equals(ngay) && xetGio == 0)
                {
                    return false ;
                }  
                else if(i.NgayNhan.Date.Equals(ngay) && (xetGio > -3 && xetGio < 3) )
                {
                    return false;
                }    
            }
            return true;
        }
        /** Tạo mã đơn đặt phòng **/
        private string TaoMaDDP()
        {
            string maDDP = "";   
            if(DONDATPHONG.TraTatCaDDP().Count() == 0)
            {
                maDDP = "DP001";
            }    
            else
            {
                string maDDPTam = DONDATPHONG.TraTatCaDDP().Last().MaDDP.ToString();
                int dem = Convert.ToInt32(maDDPTam.Split('D', 'P')[2]) + 1;
                MessageBox.Show(dem.ToString());
                if (dem < 10)
                {
                    maDDP += "DP00" + dem;
                }
                else
                {
                    maDDP += "DP0" + dem;
                }
            }           
            return maDDP;
        }
        /** Tạo đơn đặt phòng mới **/
        private DonDatPhong TaoDonDatPhong(string maKH)
        {
            DonDatPhong dp = new DonDatPhong();
            dp.MaDDP = TaoMaDDP();
            dp.NgayDat = dTimeDatPhong.Value;
            dp.NgayNhan = dTimeNgayNhan.Value;
            /*TimeSpan dt = TimeSpan.Parse(txtGioDatPhong.Text);*/
            /*dp.GioDat = dt;*/
            dp.GioDat = TimeSpan.Parse(dtmGioDatPhong.Text);
            dp.MaKH = maKH;
            dp.MaQL = MAQL;
            dp.MaPhong = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
            return dp;
        }
        /** Tạo khách hàng mới **/
        private KhachHang TaoKhachHang()
        {
            KhachHang khm = new KhachHang();
            khm.MaKH = TaoMaKH();
            khm.TenKhach = txtHoTen.Text;
            khm.SDT = txtSoDienThoai.Text;
            khm.SoLanDen = 0;
            khm.MaLoaiKH = "LKH03";
            return khm;
        }
        /** Tạo mã của khách hàng mới **/
        private string TaoMaKH()
        {
            string maKH = "";
            int dem = KHACHHANG.LayDSKH().Count() + 1;
            if (dem < 10)
            {
                maKH += "KH00" + dem;
            }
            else
            {
                maKH += "KH0" + dem;
            }
            return maKH;
        }
        private void MoPhong(KhachHang kh)
        {
            KHACHHANG.SuaSoLanDen(kh);
            HoaDon hoaDon = TaoHoaDonTrucTiep(kh.MaKH);
            MAHD = hoaDon.MaHD;
            HOADON.ThemHoaDon(hoaDon);
            Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
            MAPHONG = phong.MaPhong;
            phong.TrangThaiPhong = "Mở";
            PHONG.SuaTrangThaiPhong(phong);
        }
        private void KetThucMoPhong()
        {
            fpnlPhongVip.Controls.Clear();
            fpnlPhongThuong.Controls.Clear();
            DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
            TaoPhongVip(DANHSACHPHONGVIP);
            DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
            TaoPhongThuong(DANHSACHPHONGTHUONG);
            frmChiTietPhong frmCTP = new frmChiTietPhong(MAHD);
            frmCTP.Show();
        }
        #endregion
        /*Chức năng mở phòng*/
        private void btnMoPhong_Click(object sender, EventArgs e)
        {
            string maPhong = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
            string trangThaiPhong = PHONG.TimPhongTheoMa(maPhong).First().TrangThaiPhong;
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text) || string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!KiemTraSoDienThoai())
            {
                MessageBox.Show("Yêu cầu nhập đúng số điện thoại khách hàng ", "Thông báo", MessageBoxButtons.OK);
            }    
            else
            {
                if (trangThaiPhong == "Đóng")
                {
                    //mở phòng trực tiếp
                    KhachHang khc = KHACHHANG.TimKhachHang(txtSoDienThoai.Text);
                    if (khc != null)
                    {
                        int dem = khc.SoLanDen;
                        khc.SoLanDen = dem + 1;
                        if (khc != null)
                        {
                            if (khc.SoLanDen >= 5 && khc.SoLanDen < 10)
                            {
                                khc.MaLoaiKH = "LKH02";
                            }
                            else if (khc.SoLanDen >= 10)
                            {
                                khc.MaLoaiKH = "LKH01";
                            }
                            else
                            {
                                khc.MaLoaiKH = "LKH03";
                            }
                        }
                        MoPhong(khc);
                    }
                    else
                    {
                        KhachHang khm = new KhachHang();
                        khm = TaoKhachHang();
                        KHACHHANG.ThemKhachHang(khm);
                        int dem = khm.SoLanDen;
                        khm.SoLanDen = dem + 1;
                        if (khm.SoLanDen >= 5 && khm.SoLanDen < 10)
                        {
                            khm.MaLoaiKH = "LKH02";
                        }
                        else if (khm.SoLanDen >= 10)
                        {
                            khm.MaLoaiKH = "LKH01";
                        }
                        else
                        {
                            khm.MaLoaiKH = "LKH03";
                        }
                        MoPhong(khm);
                    }
                    KetThucMoPhong();
                }
                else if (trangThaiPhong == "Đặt")
                {
                    //mở phòng từ đơn đặt phòng
                    string hienTai = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    TimeSpan gioHienTai = DateTime.Now.TimeOfDay;
                    dynamic dp;
                    dp = lstvDanhSachDP.SelectedItems[0].Tag;
                    int thoiGian = (int)(dp.GioDat - gioHienTai).TotalHours;
                    if (dp.NgayNhan.ToString("dd-MM-yyyy") == hienTai)
                    {
                        if (thoiGian <= 1)
                        {
                            KhachHang khc = KHACHHANG.TimKhachHang(txtSoDienThoai.Text);
                            if (khc != null)
                            {
                                int dem = khc.SoLanDen;
                                khc.SoLanDen = dem + 1;
                                if (khc != null)
                                {
                                    if (khc.SoLanDen >= 5 && khc.SoLanDen < 10)
                                    {
                                        khc.MaLoaiKH = "LKH02";
                                    }
                                    else if (khc.SoLanDen >= 10)
                                    {
                                        khc.MaLoaiKH = "LKH01";
                                    }
                                    else
                                    {
                                        khc.MaLoaiKH = "LKH03";
                                    }
                                }
                                MoPhong(khc);
                                DonDatPhong ddp = new DonDatPhong();
                                ddp = DONDATPHONG.TimDDPhong(maPhong);
                                DONDATPHONG.Xoa(ddp);
                                TaiDuLieuLenListView(lstvDanhSachDP, HONLOAN.LayThongTinDonDatPhong());
                            }
                            else
                            {
                                KhachHang khm = new KhachHang();
                                khm = TaoKhachHang();
                                KHACHHANG.ThemKhachHang(khm);
                                int dem = khm.SoLanDen;
                                khm.SoLanDen = dem + 1;
                                if (khm.SoLanDen >= 5 && khm.SoLanDen < 10)
                                {
                                    khm.MaLoaiKH = "LKH02";
                                }
                                else if (khm.SoLanDen >= 10)
                                {
                                    khm.MaLoaiKH = "LKH01";
                                }
                                else
                                {
                                    khm.MaLoaiKH = "LKH03";
                                }
                                MoPhong(khm);
                                DonDatPhong ddp = new DonDatPhong();
                                ddp = DONDATPHONG.TimDDPhong(maPhong);
                                DONDATPHONG.Xoa(ddp);
                                TaiDuLieuLenListView(lstvDanhSachDP, HONLOAN.LayThongTinDonDatPhong());
                            }
                            KetThucMoPhong();
                        }
                        else
                        {
                            MessageBox.Show("Phòng chưa tới giờ đặt", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    XoaDuLieuTextBox();
                }
                else if (trangThaiPhong == "Mở")
                {
                    MessageBox.Show("Phòng đã mở ", "Thông báo", MessageBoxButtons.OK);
                }
            }            
        }
        /** Tạo hóa đơn mới mở phòng qua đặt phòng **/
        private HoaDon TaoHoaDon()
        {
            HoaDon hd = new HoaDon();
            hd.MaHD = TaoMaHD();
            TimeSpan dt = TimeSpan.Parse(dtmGioDatPhong.Text);
            hd.GioVao = dt;
            hd.GioRa = null;
            hd.NgayLap = DateTime.Now;
            hd.TongTien = null;
            hd.MaPhong = PHONG.TimPhongTheoTen(LayTenPhongTrongLstv()).First().MaPhong;
            hd.MaKH = KHACHHANG.TimTenKhachHang(LayTenKhach()).First().MaKH;
            hd.MaQL = MAQL;
            return hd;
        }
        /** Tạo hóa đơn mới mở phòng trực tiếp **/
        private HoaDon TaoHoaDonTrucTiep(string maKH)
        {
            HoaDon hd = new HoaDon();
            hd.MaHD = TaoMaHD();
            DateTime dt = DateTime.Now;
            /*TimeSpan tp = dt.TimeOfDay;*/
            hd.GioVao = TimeSpan.Parse(dtmGioDatPhong.Text);
            hd.GioRa = null;
            hd.NgayLap = dt;
            hd.TongTien = null;
            hd.MaPhong = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
            hd.MaKH = maKH;
            hd.MaQL = MAQL;
            return hd;
        }
        /** Tạo mã của hóa đơn mới **/
        private string TaoMaHD()
        {
            string maHD = "";
            int dem = 0;
            if (lstvDanhSachDP.Items.Count == 0)
            {
                dem = HOADON.LayToanBoHoaDon().Count() + 1;
            }
            else
            {
                dem = Convert.ToInt32(HOADON.LayToanBoHoaDon().Last().MaHD.ToString().Split('H','D')[2]) + 1;
            }           
            if (dem < 10)
            {
                maHD += "HD000000" + dem;
            }
            else if(dem<100 && dem >= 10)
            {
                maHD += "HD00000" + dem;
            }
            else if (dem < 1000 && dem >= 100)
            {
                maHD += "HD0000" + dem;
            }
            else if (dem < 10000 && dem >= 1000)
            {
                maHD += "HD000" + dem;
            }
            else if (dem < 100000 && dem >= 10000)
            {
                maHD += "HD000" + dem;
            }
            else if (dem < 1000000 && dem >= 100000)
            {
                maHD += "HD0" + dem;
            }
            else {
                maHD += "HD" + dem;
            }
            return maHD;
        }
        /*Lấy tên phòng trong danh sách phòng*/
        private string LayTenPhongTrongLstv()
        {
            dynamic dp = null;
            if (lstvDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lstvDanhSachDP.SelectedItems[0].Tag;
                return dp.TenPhong;
            }
            return null;
        }
        /*Lấy đơn đặt phòng*/
        private DonDatPhong LayDonDatPhong()
        {
            DonDatPhong ddp = new DonDatPhong();
            var tenPhong = LayTenPhongTrongLstv();
            string maPhong = PHONG.TimPhongTheoTen(tenPhong).First().MaPhong;
            ddp = DONDATPHONG.TimDDPhong(maPhong);
            return ddp;
        }
        /*Lấy tên khách trong đơn đặt phòng*/
        private string LayTenKhach()
        {
            dynamic dp = null;
            if (lstvDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lstvDanhSachDP.SelectedItems[0].Tag;
                return dp.SDT;
            }
            return null;
        }
        /*Chức năng của rdoMoPhong và rdoDatPhong*/
        private void rdoMoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMoPhong.Checked)
            {
                dtmGioDatPhong.Enabled = false;
                dTimeDatPhong.Enabled = false;
                dTimeNgayNhan.Enabled = false;
                txtTenPhong.Enabled = false;
                btnDatPhong.Enabled = false;
                btnMoPhong.Enabled = true;
            }
            else if (rdoDatPhong.Checked)
            {
                btnMoPhong.Enabled = false;
                dtmGioDatPhong.Enabled = true;
                dTimeDatPhong.Enabled = true;
                dTimeNgayNhan.Enabled = true;
                txtTenPhong.Enabled = false;
                btnDatPhong.Enabled = true;
            }

        }
        /*Chức năng của rdoTatCa và rdoHienTai*/
        private void rdoTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTatCa.Checked)
            {
                TaiDuLieuLenListView(lstvDanhSachDP, HONLOAN.LayThongTinDonDatPhong());

            }
            else if (rdoHienTai.Checked)
            {
                string homNay = DateTime.Now.ToString("yyyy/MM/dd");
                TaiDuLieuLenListView(lstvDanhSachDP, HONLOAN.LayThongTinDonDatPhongTheoNgay(homNay));
            }
        }
        /*Chức năng hủy đơn đặt phòng*/
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult yn;
            dynamic ddp;
            DonDatPhong ddpdt = new DonDatPhong();
            int index;
            string maPhong;
            Phong phong;
            if (lstvDanhSachDP.SelectedItems.Count > 0)
            {
                yn = MessageBox.Show("Bạn có chắc muốn Hủy?", "Hỏi hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    for (int i = 0; i < lstvDanhSachDP.SelectedItems.Count; ++i)
                    {
                        index = lstvDanhSachDP.SelectedIndices[i];
                        ddp = (dynamic)lstvDanhSachDP.Items[index].Tag;
                        maPhong = PHONG.TimMaPhong(ddp.TenPhong).MaPhong;
                        ddpdt = DONDATPHONG.TimDDPhong(maPhong);
                        DONDATPHONG.Xoa(ddpdt);
                        phong = PHONG.TimMaPhong(ddp.TenPhong);
                        if(phong.TrangThaiPhong == "Mở")
                        {
                            XoaDuLieuTextBox();
                        }
                        else
                        {
                            phong.TrangThaiPhong = "Đóng";
                            PHONG.SuaTrangThaiPhong(phong);
                        }
                    }
                    TaiDuLieuLenListView(lstvDanhSachDP, HONLOAN.LayThongTinDonDatPhong());
                    DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
                    DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
                    fpnlPhongVip.Controls.Clear();
                    fpnlPhongThuong.Controls.Clear();
                    TaoPhongVip(DANHSACHPHONGVIP);
                    TaoPhongThuong(DANHSACHPHONGTHUONG);
                }
            }
            XoaDuLieuTextBox();
        }
        /*Chức năng nhấn vào lstv hiển thị thông tin lên textbox*/
        private void lvwDanhSachDP_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dynamic dp = null;
            if (lstvDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lstvDanhSachDP.SelectedItems[0].Tag;
                TimeSpan gio = DateTime.Now.TimeOfDay;
                double xetGio = (double)(dp.GioDat - gio).TotalHours;
                if(dp.NgayNhan.Date.Equals(DateTime.Now.Date) &&( xetGio <= 1 && xetGio > 0))
                {
                    DuLieuLenTextBox(dp);
                }    
            }
        }
        /*Chức năng làm mới lại danh sách đặt phòng*/

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XuLyDatPhong();
            DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
            DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
            fpnlPhongVip.Controls.Clear();
            fpnlPhongThuong.Controls.Clear();          
            TaoPhongVip(DANHSACHPHONGVIP);
            TaoPhongThuong(DANHSACHPHONGTHUONG);
            XoaDuLieuTextBox();
        }
        /*Xử lý màu hiển thị của button theo lịch đặt phòng*/
        private void XuLyDatPhong()
        {
            /*string ngay = "2021-11-04";*/
            string ngay = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;
            TimeSpan tp = dt.TimeOfDay;
            IEnumerable<DonDatPhong> dpTheoNgay = DONDATPHONG.TimDonDatPhongTheoNgay(ngay);
            foreach(DonDatPhong i in dpTheoNgay)
            {
                int thoiGian = (int)(i.GioDat - tp).TotalHours;
                Console.WriteLine(thoiGian);
                if(thoiGian <= 1)
                {
                    Phong phong = PHONG.TimPhongTheoMa(i.MaPhong).First();
                    phong.TrangThaiPhong = "Đặt";
                    PHONG.SuaTrangThaiPhong(phong);
                }                  
            }    
        }
        /*Chức năng vào xem chi tiết của một phòng*/

        private void btnXemPhong_Click(object sender, EventArgs e)
        {
            Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
            if(phong.TrangThaiPhong.Equals("Mở"))
            {
                MAPHONG = phong.MaPhong;
                string MAHD = HOADON.LayMaHoaDonTheoMaPhong(MAPHONG).MaHD;
                frmChiTietPhong frmCTP = new frmChiTietPhong(MAHD);
                frmCTP.Show();
            }               
            else
            {
                MessageBox.Show("Phòng chưa mở, không thể xem", "Thông báo", MessageBoxButtons.OK);
            }
            XoaDuLieuTextBox();
        }
        /*Gán vaitating cho txtSoDienThoai*/
        private void txtSoDienThoai_Validating(object sender, CancelEventArgs e)
        {
            string soDienThoai = txtSoDienThoai.Text;
            if (!clsKiemTra.KiemTraSDT(soDienThoai))
            {
                e.Cancel = true;
                txtSoDienThoai.Focus();
                errSoDienThoai.SetError(txtSoDienThoai, "Số điện thoại chưa hợp lý. Ví dụ: 0879276284");
            }
            else
            {
                e.Cancel = false;
                errSoDienThoai.SetError(txtSoDienThoai, null);
            }
        }
        /*Kiếm tra số điện thoại đã hợp lý hay chưa*/
        private bool KiemTraSoDienThoai()
        {
            string soDienThoai = txtSoDienThoai.Text;
            if (!clsKiemTra.KiemTraSDT(soDienThoai))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /*Chức năng mở form tìm kiếm khách hàng đang sử dụng phòng hát*/
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            frmTimKiemKhachHang frm = new frmTimKiemKhachHang();
            frm.Show();
        }
    }
}
