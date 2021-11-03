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

        public frmPhong()
        {
            InitializeComponent();
        }
        private clsHonLoan HONLOAN;
        private clsTaoButton TAOBUTTON;
        private clsKhachHang KHACHHANG;
        private clsDonDatPhong DONDATPHONG;
        private clsPhong PHONG;
        private clsHoaDon HOADON;
        public string maHD;
        public string maPhong;
        private IEnumerable<Phong> DANHSACHPHONGVIP;
        private IEnumerable<Phong> DANHSACHPHONGTHUONG;
        private void frmPhong_Load(object sender, EventArgs e)
        {
            KHACHHANG = new clsKhachHang();
            DONDATPHONG = new clsDonDatPhong();
            TAOBUTTON = new clsTaoButton();
            PHONG = new clsPhong();
            HONLOAN = new clsHonLoan();
            HOADON = new clsHoaDon();

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
        private Boolean KiemTraThoiGian()
        {
            DateTime tgian = DateTime.Now;
            int soSanh = DateTime.Compare(dTimeNgayNhan.Value, tgian);

            if (soSanh < 0)
            {
                Console.WriteLine("Ngày nhận không phù hợp");
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
                            btn.BackColor = Color.Yellow;
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
                            btn.BackColor = Color.Yellow;
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
                btn.BackColor = Color.LightGreen;
                txtTenPhong.Text = btn.Text;
            }
            else if (btn.BackColor == Color.Teal)
            {
                txtTenPhong.Text = btn.Text;
                btnMoPhong.Enabled = true;
            }          
            else if (btn.BackColor == Color.Yellow)
            {
                MessageBox.Show("Phòng đã đặt, Vui lòng chọn phòng khác", "Thông báo");
            }
            else if(btn.BackColor == Color.LightGreen)
            {
                btn.BackColor = Color.Gray;
            }    
            else
            {
                btn.BackColor = Color.LightGreen;
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
            txtGioDatPhong.Text = dp.GioDat.ToString();
            txtSoDienThoai.Text = dp.SDT;
            txtTenPhong.Text = dp.TenPhong;
            dTimeDatPhong.Value = dp.NgayDat;
            dTimeNgayNhan.Value = dp.NgayNhan;
        }       
        #endregion
        #region Đặt phòng
        /** Chức năng đặt phòng **/
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            if (KiemTraThoiGian() == false)
            {
                MessageBox.Show("Ngày nhận/Ngày đặt không phù hợp", "Thông báo", MessageBoxButtons.OK);
            }
            else if(string.IsNullOrEmpty(txtGioDatPhong.Text)|| string.IsNullOrEmpty(txtHoTen.Text)|| string.IsNullOrEmpty(txtSoDienThoai.Text)|| string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }    
            else
            {
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
                string maP = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
                string trangThaiPhong = PHONG.TimPhongTheoMa(maP).First().TrangThaiPhong;
                if(trangThaiPhong == "Mở")
                {
                    MessageBox.Show("Phòng đã mở, Vui lòng chọn phòng khác", "Thông báo");
                }    
                else if(trangThaiPhong == "Đóng")
                {
                    DonDatPhong ddp = new DonDatPhong();
                    ddp = TaoDonDatPhong(maKH);
                    DONDATPHONG.ThemDonDatPhong(ddp);
                    /*Phong phong = p.TimMotPhongTheoMa(ddp.MaPhong);
                    phong.TrangThaiPhong = "Đặt";
                    p.SuaTrangThaiPhong(phong);*/

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
            }
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
            TimeSpan dt = TimeSpan.Parse(txtGioDatPhong.Text);
            dp.GioDat = dt;
            dp.MaKH = maKH;
            dp.MaQL = "NV002";
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
        #endregion

        private void btnMoPhong_Click(object sender, EventArgs e)
        {
            string maPhong = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
            string trangThaiPhong = PHONG.TimPhongTheoMa(maPhong).First().TrangThaiPhong;
            if(trangThaiPhong == "Đóng")
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
                    KHACHHANG.SuaSoLanDen(khc);
                    HoaDon hoaDon = TaoHoaDonTrucTiep(khc.MaKH);
                    maHD = hoaDon.MaHD;
                    HOADON.ThemHoaDon(hoaDon);
                    Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
                    maPhong = phong.MaPhong;
                    phong.TrangThaiPhong = "Mở";
                    PHONG.SuaTrangThaiPhong(phong);
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
                    KHACHHANG.SuaSoLanDen(khm);
                    HoaDon hoaDon = TaoHoaDonTrucTiep(khm.MaKH);
                    maHD = hoaDon.MaHD;
                    HOADON.ThemHoaDon(hoaDon);
                    Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
                    maPhong = phong.MaPhong;
                    phong.TrangThaiPhong = "Mở";
                    PHONG.SuaTrangThaiPhong(phong);
                }
                fpnlPhongVip.Controls.Clear();
                fpnlPhongThuong.Controls.Clear();
                DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
                TaoPhongVip(DANHSACHPHONGVIP);
                DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
                TaoPhongThuong(DANHSACHPHONGTHUONG);
                //MessageBox.Show(maHD, maPhong);
                frmChiTietPhong frmCTP = new frmChiTietPhong(maHD);
                frmCTP.Show();
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
                    if(thoiGian <= 1)
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
                            KHACHHANG.SuaSoLanDen(khc);
                            HoaDon hoaDon = TaoHoaDon();
                            maHD = hoaDon.MaHD;
                            HOADON.ThemHoaDon(hoaDon);
                            Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
                            maPhong = phong.MaPhong;
                            phong.TrangThaiPhong = "Mở";
                            PHONG.SuaTrangThaiPhong(phong);
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
                            KHACHHANG.SuaSoLanDen(khm);
                            HoaDon hoaDon = TaoHoaDon();
                            maHD = hoaDon.MaHD;
                            HOADON.ThemHoaDon(hoaDon);
                            Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
                            maPhong = phong.MaPhong;
                            phong.TrangThaiPhong = "Mở";
                            PHONG.SuaTrangThaiPhong(phong);
                            DonDatPhong ddp = new DonDatPhong();
                            ddp = DONDATPHONG.TimDDPhong(maPhong);
                            DONDATPHONG.Xoa(ddp);
                            TaiDuLieuLenListView(lstvDanhSachDP, HONLOAN.LayThongTinDonDatPhong());
                        }
                        DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
                        fpnlPhongVip.Controls.Clear();
                        fpnlPhongThuong.Controls.Clear();
                        TaoPhongVip(DANHSACHPHONGVIP);
                        DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
                        TaoPhongThuong(DANHSACHPHONGTHUONG);
                        //MessageBox.Show(maHD, maPhong);
                        frmChiTietPhong frmCTP = new frmChiTietPhong(maHD);
                        frmCTP.Show();
                    }
                    else
                    {
                        MessageBox.Show("Phòng chưa tới giờ đặt", "Thông báo", MessageBoxButtons.OK);
                    }
                }                
                /*KhachHang khc = kh.TimKhachHang(txtSoDienThoai.Text);
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
                    kh.SuaSoLanDen(khc);
                    HoaDon hoaDon = TaoHoaDon();
                    maHD = hoaDon.MaHD;
                    hd.ThemHoaDon(hoaDon);
                    Phong phong = p.TimMotPhongTheoTen(txtTenPhong.Text);
                    maPhong = phong.MaPhong;
                    phong.TrangThaiPhong = "Mở";
                    p.SuaTrangThaiPhong(phong);
                    DonDatPhong ddp = new DonDatPhong();
                    ddp = clsddp.TimDDPhong(maPhong);
                    clsddp.Xoa(ddp);
                    TaiDuLieuLenListView(lvwDanhSachDP, hl.LayThongTinDonDatPhong());
                }
                else
                {
                    KhachHang khm = new KhachHang();
                    khm = TaoKhachHang();
                    kh.ThemKhachHang(khm);
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
                    kh.SuaSoLanDen(khm);
                    HoaDon hoaDon = TaoHoaDon();
                    maHD = hoaDon.MaHD;
                    hd.ThemHoaDon(hoaDon);
                    Phong phong = p.TimMotPhongTheoTen(txtTenPhong.Text);
                    maPhong = phong.MaPhong;
                    phong.TrangThaiPhong = "Mở";
                    p.SuaTrangThaiPhong(phong);
                    DonDatPhong ddp = new DonDatPhong();
                    ddp = clsddp.TimDDPhong(maPhong);
                    clsddp.Xoa(ddp);
                    TaiDuLieuLenListView(lvwDanhSachDP, hl.LayThongTinDonDatPhong());
                }
                dSachPVip = p.LayDSPhongTheoLoai("LP001");              
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();
                TaoPhongVip(dSachPVip);
                dSachPThuong = p.LayDSPhongTheoLoai("LP002");
                TaoPhongThuong(dSachPThuong);
                //MessageBox.Show(maHD, maPhong);
                frmChiTietPhong frmCTP = new frmChiTietPhong(maHD);
                frmCTP.Show();*/
            }
            else if(trangThaiPhong =="Mở")
            {
                Phong phong = PHONG.TimMotPhongTheoTen(txtTenPhong.Text);
                maPhong = phong.MaPhong;
                string maHD = HOADON.LayMaHoaDonTheoMaPhong(maPhong).MaHD;
                //MessageBox.Show(maHD, maPhong);
                /*frmChiTietPhong frm = new frmChiTietPhong();
                frm.Show();*/
                frmChiTietPhong frmCTP = new frmChiTietPhong(maHD);
                frmCTP.Show();
            }    
        }
        /** Tạo hóa đơn mới mở phòng qua đặt phòng **/
        private HoaDon TaoHoaDon()
        {
            HoaDon hd = new HoaDon();
            hd.MaHD = TaoMaHD();
            TimeSpan dt = TimeSpan.Parse(txtGioDatPhong.Text);
            hd.GioVao = dt;
            hd.GioRa = null;
            hd.NgayLap = DateTime.Now;
            hd.TongTien = null;
            hd.MaPhong = PHONG.TimPhongTheoTen(LayTenPhongTrongLstv()).First().MaPhong;
            hd.MaKH = KHACHHANG.TimTenKhachHang(LayTenKhach()).First().MaKH;
            hd.MaQL = "NV002";
            return hd;
        }
        /** Tạo hóa đơn mới mở phòng trực tiếp **/
        private HoaDon TaoHoaDonTrucTiep(string maKH)
        {
            HoaDon hd = new HoaDon();
            hd.MaHD = TaoMaHD();
            DateTime dt = DateTime.Now;
            TimeSpan tp = dt.TimeOfDay;
            hd.GioVao = tp;
            hd.GioRa = null;
            hd.NgayLap = dt;
            hd.TongTien = null;
            hd.MaPhong = PHONG.TimPhongTheoTen(txtTenPhong.Text).First().MaPhong;
            hd.MaKH = maKH;
            hd.MaQL = "NV002";
            return hd;
        }
        /** Tạo mã của hóa đơn mới **/
        private string TaoMaHD()
        {
            string maHD = "";
            int dem = HOADON.LayToanBoHoaDon().Count() + 1;
            if (dem < 10)
            {
                maHD += "HD00" + dem;
            }
            else
            {
                maHD += "HD0" + dem;
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
                txtGioDatPhong.Enabled = false;
                dTimeDatPhong.Enabled = false;
                dTimeNgayNhan.Enabled = false;
                txtTenPhong.Enabled = false;
                btnDatPhong.Enabled = false;
                btnMoPhong.Enabled = true;
            }
            else if (rdoDatPhong.Checked)
            {
                btnMoPhong.Enabled = false;
                txtGioDatPhong.Enabled = true;
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
                        phong.TrangThaiPhong = "Đóng";
                        PHONG.SuaTrangThaiPhong(phong);

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
        }
        /*Chức năng nhấn vào lstv hiển thị thông tin lên textbox*/
        private void lvwDanhSachDP_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dynamic dp = null;
            if (lstvDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lstvDanhSachDP.SelectedItems[0].Tag;
                DuLieuLenTextBox(dp);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XuLyDatPhong();
            DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
            DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
            fpnlPhongVip.Controls.Clear();
            fpnlPhongThuong.Controls.Clear();          
            TaoPhongVip(DANHSACHPHONGVIP);
            TaoPhongThuong(DANHSACHPHONGTHUONG);
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
    }
}
