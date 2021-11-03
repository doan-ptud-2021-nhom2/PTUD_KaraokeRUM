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
        private clsHonLoan hl;
        private clsTaoButton tao;
        private clsKhachHang kh;
        private clsDonDatPhong clsddp;
        private clsPhong p;
        private clsHoaDon hd;
        public string maHD;
        public string maPhong;
        private IEnumerable<Phong> dSachPVip;
        private IEnumerable<Phong> dSachPThuong;
        private void frmPhong_Load(object sender, EventArgs e)
        {
            kh = new clsKhachHang();
            clsddp = new clsDonDatPhong();
            tao = new clsTaoButton();
            p = new clsPhong();
            hl = new clsHonLoan();
            hd = new clsHoaDon();

            radMoPhong.Checked = true;
            MacDinhThoiGian();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            XuLyDatPhong();
            dSachPVip = p.LayDSPhongTheoLoai("LP001");
            dSachPThuong = p.LayDSPhongTheoLoai("LP002");
            TaoPhongVip(dSachPVip);
            TaoPhongThuong(dSachPThuong);
            IEnumerable<dynamic> dsDP;
            dsDP = hl.LayThongTinDonDatPhong();
            TaoListView(lvwDanhSachDP);
            TaiDuLieuLenListView(lvwDanhSachDP, dsDP);           
        }
        /** Mặc định thời gian cho ngày đặt phòng và ngày nhận phòng **/
        private void MacDinhThoiGian()
        {
            dTimeDatPhong.Value = DateTime.Now;
            dTimeNgayNhan.Value = DateTime.Now;
        }
        /** Kiểm tra dữ liệu **/
        private Boolean KiemTra()
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
            tao = new clsTaoButton();
            Button btn;
            for (int i = 0; i < n.Count(); i++)
            {
                btn = new Button();
                btn.Name = "btnVip" + (i +1).ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.Text = n.ElementAt(i).TenPhong;
                btn.BackColor = Color.Teal;
                foreach (dynamic item in tao.LayTrangThaiPhong("LP001"))
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
                btn.Click += new EventHandler(Chon);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        /** Tạo phòng Thường **/
        void TaoPhongThuong(IEnumerable<Phong> n)
        {
            tao = new clsTaoButton();
            Button btn;
            for (int i = 0; i < n.Count(); i++)
            {
                btn = new Button();
                btn.Name = "btnThuong" + (i+1).ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.Text = n.ElementAt(i).TenPhong;
                btn.BackColor = Color.Teal;
                foreach (var item in tao.LayTrangThaiPhong("LP002"))
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
                btn.Click += new EventHandler(Chon);
                flowLayoutPanel2.Controls.Add(btn);
            }
        }
        /** Cài đặt chú thích cho các trạng thái phòng **/
        private void Chon(object sender, EventArgs e)
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
            if (KiemTra() == false)
            {
                MessageBox.Show("Ngày nhận/Ngày đặt không phù hợp", "Thông báo", MessageBoxButtons.OK);
            }
            else if(string.IsNullOrEmpty(txtGioDatPhong.Text)|| string.IsNullOrEmpty(txtHoTen.Text)|| string.IsNullOrEmpty(txtSoDienThoai.Text)|| string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK);
            }    
            else
            {
                KhachHang khc = kh.TimKhachHang(txtSoDienThoai.Text);
                string maKH = "";
                if (khc != null)
                {
                    maKH = khc.MaKH;
                }
                else
                {
                    KhachHang khm = new KhachHang();
                    khm = TaoKhachHang();
                    kh.ThemKhachHang(khm);
                    maKH = khm.MaKH;
                }
                string maP = p.TimTenPhong(txtTenPhong.Text).First().MaPhong;
                string trangThaiPhong = p.TimPhongTheoMa(maP).First().TrangThaiPhong;
                if(trangThaiPhong == "Mở")
                {
                    MessageBox.Show("Phòng đã mở, Vui lòng chọn phòng khác", "Thông báo");
                }    
                else if(trangThaiPhong == "Đóng")
                {
                    DonDatPhong ddp = new DonDatPhong();
                    ddp = TaoDonDatPhong(maKH);
                    clsddp.ThemDonDatPhong(ddp);
                    /*Phong phong = p.TimMotPhongTheoMa(ddp.MaPhong);
                    phong.TrangThaiPhong = "Đặt";
                    p.SuaTrangThaiPhong(phong);*/

                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel2.Controls.Clear();
                    dSachPVip = p.LayDSPhongTheoLoai("LP001");
                    TaoPhongVip(dSachPVip);
                    dSachPThuong = p.LayDSPhongTheoLoai("LP002");
                    TaoPhongThuong(dSachPThuong);
                    IEnumerable<dynamic> danhsDP;
                    danhsDP = hl.LayThongTinDonDatPhong();
                    TaiDuLieuLenListView(lvwDanhSachDP, danhsDP);
                }    
            }
        }
        /** Tạo mã đơn đặt phòng **/
        private string TaoMaDDP()
        {
            string maDDP = "";   
            if(clsddp.TraTatCaDDP().Count() == 0)
            {
                maDDP = "DP001";
            }    
            else
            {
                string maDDPTam = clsddp.TraTatCaDDP().Last().MaDDP.ToString();
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
            dp.MaPhong = p.TimTenPhong(txtTenPhong.Text).First().MaPhong;
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
            int dem = kh.LayDSKH().Count() + 1;
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
            string maPhong = p.TimTenPhong(txtTenPhong.Text).First().MaPhong;
            string trangThaiPhong = p.TimPhongTheoMa(maPhong).First().TrangThaiPhong;
            if(trangThaiPhong == "Đóng")
            {
                //mở phòng trực tiếp
                KhachHang khc = kh.TimKhachHang(txtSoDienThoai.Text);
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
                    HoaDon hoaDon = TaoHoaDonTrucTiep(khc.MaKH);
                    maHD = hoaDon.MaHD;
                    hd.ThemHoaDon(hoaDon);
                    Phong phong = p.TimMotPhongTheoTen(txtTenPhong.Text);
                    maPhong = phong.MaPhong;
                    phong.TrangThaiPhong = "Mở";
                    p.SuaTrangThaiPhong(phong);
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
                    HoaDon hoaDon = TaoHoaDonTrucTiep(khm.MaKH);
                    maHD = hoaDon.MaHD;
                    hd.ThemHoaDon(hoaDon);
                    Phong phong = p.TimMotPhongTheoTen(txtTenPhong.Text);
                    maPhong = phong.MaPhong;
                    phong.TrangThaiPhong = "Mở";
                    p.SuaTrangThaiPhong(phong);
                }
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();
                dSachPVip = p.LayDSPhongTheoLoai("LP001");
                TaoPhongVip(dSachPVip);
                dSachPThuong = p.LayDSPhongTheoLoai("LP002");
                TaoPhongThuong(dSachPThuong);
                //MessageBox.Show(maHD, maPhong);
                frmChiTietPhong frmCTP = new frmChiTietPhong(maHD);
                frmCTP.Show();
            }    
            else if (trangThaiPhong == "Đặt")
            {
                //mở phòng từ đơn đặt phòng
                KhachHang khc = kh.TimKhachHang(txtSoDienThoai.Text);
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
                frmCTP.Show();
            }
            else if(trangThaiPhong =="Mở")
            {
                Phong phong = p.TimMotPhongTheoTen(txtTenPhong.Text);
                maPhong = phong.MaPhong;
                string maHD = hd.LayMaHoaDonTheoMaPhong(maPhong).MaHD;
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
            hd.MaPhong = p.TimTenPhong(LayTenPhongTrongLVW()).First().MaPhong;
            hd.MaKH = kh.TimTenKhachHang(LayTenKhach()).First().MaKH;
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
            hd.MaPhong = p.TimTenPhong(txtTenPhong.Text).First().MaPhong;
            hd.MaKH = maKH;
            hd.MaQL = "NV002";
            return hd;
        }
        /** Tạo mã của hóa đơn mới **/
        private string TaoMaHD()
        {
            string maHD = "";
            int dem = hd.LayToanBoHoaDon().Count() + 1;
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
        private string LayTenPhongTrongLVW()
        {
            dynamic dp = null;
            if (lvwDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lvwDanhSachDP.SelectedItems[0].Tag;
                return dp.TenPhong;
            }
            return null;
        }
        private DonDatPhong LayDonDatPhong()
        {
            DonDatPhong ddp = new DonDatPhong();
            var tenPhong = LayTenPhongTrongLVW();
            string maPhong = p.TimTenPhong(tenPhong).First().MaPhong;
            ddp = clsddp.TimDDPhong(maPhong);
            return ddp;
        }
        private string LayTenKhach()
        {
            dynamic dp = null;
            if (lvwDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lvwDanhSachDP.SelectedItems[0].Tag;
                return dp.SDT;
            }
            return null;
        }

        private void radMoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (radMoPhong.Checked)
            {
                txtGioDatPhong.Enabled = false;
                dTimeDatPhong.Enabled = false;
                dTimeNgayNhan.Enabled = false;
                txtTenPhong.Enabled = false;
                btnDatPhong.Enabled = false;
                btnMoPhong.Enabled = true;
            }
            else if (radDatPhong.Checked)
            {
                btnMoPhong.Enabled = false;
                txtGioDatPhong.Enabled = true;
                dTimeDatPhong.Enabled = true;
                dTimeNgayNhan.Enabled = true;
                txtTenPhong.Enabled = false;
                btnDatPhong.Enabled = true;
            }

        }

        private void radTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (radTatCa.Checked)
            {
                TaiDuLieuLenListView(lvwDanhSachDP, hl.LayThongTinDonDatPhong());

            }
            else if (radHienTai.Checked)
            {
                string homNay = DateTime.Now.ToString("yyyy/MM/dd");
                TaiDuLieuLenListView(lvwDanhSachDP, hl.LayThongTinDonDatPhongTheoNgay(homNay));
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult yn;
            dynamic ddp;
            DonDatPhong ddpdt = new DonDatPhong();
            int index;
            string maPhong;
            Phong phong;
            if (lvwDanhSachDP.SelectedItems.Count > 0)
            {
                yn = MessageBox.Show("Bạn có chắc muốn Hủy?", "Hỏi hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    for (int i = 0; i < lvwDanhSachDP.SelectedItems.Count; ++i)
                    {
                        index = lvwDanhSachDP.SelectedIndices[i];
                        ddp = (dynamic)lvwDanhSachDP.Items[index].Tag;
                        maPhong = p.TimMaPhong(ddp.TenPhong).MaPhong;
                        ddpdt = clsddp.TimDDPhong(maPhong);
                        clsddp.Xoa(ddpdt);
                        phong = p.TimMaPhong(ddp.TenPhong);
                        phong.TrangThaiPhong = "Đóng";
                        p.SuaTrangThaiPhong(phong);

                    }
                    TaiDuLieuLenListView(lvwDanhSachDP, hl.LayThongTinDonDatPhong());
                    dSachPVip = p.LayDSPhongTheoLoai("LP001");
                    dSachPThuong = p.LayDSPhongTheoLoai("LP002");
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel2.Controls.Clear();
                    TaoPhongVip(dSachPVip);
                    TaoPhongThuong(dSachPThuong);
                }
            }
        }

        private void lvwDanhSachDP_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dynamic dp = null;
            if (lvwDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lvwDanhSachDP.SelectedItems[0].Tag;
                DuLieuLenTextBox(dp);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XuLyDatPhong();
            dSachPVip = p.LayDSPhongTheoLoai("LP001");
            dSachPThuong = p.LayDSPhongTheoLoai("LP002");
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();          
            TaoPhongVip(dSachPVip);
            TaoPhongThuong(dSachPThuong);
        }
        private void XuLyDatPhong()
        {
            //string ngay = "2021-11-03";
            string ngay = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;
            TimeSpan tp = dt.TimeOfDay;
            IEnumerable<DonDatPhong> dpTheoNgay = clsddp.TimDonDatPhongTheoNgay(ngay);
            foreach(DonDatPhong i in dpTheoNgay)
            {
                int thoiGian = (int)(i.GioDat - tp).TotalHours;
                Console.WriteLine(thoiGian);
                if(thoiGian <= 1)
                {
                    Phong phong = p.TimPhongTheoMa(i.MaPhong).First();
                    phong.TrangThaiPhong = "Đặt";
                    p.SuaTrangThaiPhong(phong);
                }                  
            }    
        }
    }
}
