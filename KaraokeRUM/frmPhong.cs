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
        private void frmPhong_Load(object sender, EventArgs e)
        {
            kh = new clsKhachHang();
            clsddp = new clsDonDatPhong();
            tao = new clsTaoButton();
            p = new clsPhong();
            hl = new clsHonLoan();
            MacDinhThoiGian();
            int vip = p.LayDSPhongTheoLoai("LH001").Count() + 1;
            int thuong = p.LayDSPhongTheoLoai("LH002").Count() + 1;
            TaoPhongVip(vip);
            TaoPhongThuong(thuong);
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
            int soSanh2 = DateTime.Compare(dTimeDatPhong.Value, tgian);
            if (soSanh < 0 || soSanh2 < 0)
            {
                Console.WriteLine("Ngày nhận không phù hợp");
                return false;
            }
            return true;
        }
        #region Load Dữ liệu lên
        /** Tạo phòng Vip **/
        void TaoPhongVip(int n)
        {
            tao = new clsTaoButton();
            Button btn;
            for (int i = 1; i <= n; i++)
            {
                btn = new Button();
                btn.Name = "btn" + i.ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.Text = "V00" + i.ToString();
                btn.BackColor = Color.Teal;
                foreach (var item in tao.TraSoPhong())
                {
                    if (btn.Text.Equals(item.TenPhong))
                    {
                        btn.BackColor = Color.Red;
                    }
                }
                btn.Click += new EventHandler(Chon);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        /** Tạo phòng Thường **/
        void TaoPhongThuong(int n)
        {
            tao = new clsTaoButton();
            Button btn;
            for (int i = 1; i <= n; i++)
            {
                btn = new Button();
                btn.Name = "btn" + i.ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.Text = "T00" + i.ToString();
                btn.BackColor = Color.Teal;
                foreach (var item in tao.TraSoPhong())
                {
                    if (btn.Text.Equals(item.TenPhong))
                    {
                        btn.BackColor = Color.Red;
                    }
                }
                btn.Click += new EventHandler(Chon);
                flowLayoutPanel2.Controls.Add(btn);
            }
        }
        /** Cài đặt chú thích cho các phòng **/
        private void Chon(object sender, EventArgs e)
        {
            Button btn = (Button)sender;// ép kiểu;
            if (btn.BackColor == Color.Teal)
            {
                btn.BackColor = Color.Yellow;
                txtTenPhong.Text = btn.Text;
            }
            else if (btn.BackColor == Color.Red)
            {
                MessageBox.Show("Phòng đã đặt, Vui lòng chọn phòng khác", "Thông báo");
            }
            else if (btn.BackColor == Color.Yellow)
            {
                btn.BackColor = Color.Teal;
            }
            else
            {
                btn.BackColor = Color.Teal;
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
            item.SubItems.Add(ddp.NgayDat.ToString());
            item.SubItems.Add(ddp.NgayNhan.ToString());
            item.SubItems.Add(ddp.GioDat.ToString());
            item.Tag = ddp;
            return item;
        }
        /** Tạo listView **/
        private void TaoListView(ListView lvwDSDatPhong)
        {
            lvwDSDatPhong.Columns.Add("Tên Phòng", 130);
            lvwDSDatPhong.Columns.Add("Tên khách hàng", 220);
            lvwDSDatPhong.Columns.Add("Số điện thoại", 150);
            lvwDSDatPhong.Columns.Add("Ngày đặt phòng", 250);
            lvwDSDatPhong.Columns.Add("Ngày nhận phòng", 250);
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
        /** Sự kiện của lvwDanhSachDP **/
        private void lvwDanhSachDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dp = null;
            if (lvwDanhSachDP.SelectedItems.Count > 0)
            {
                btnMoPhong.Enabled = true;
                dp = lvwDanhSachDP.SelectedItems[0].Tag;
                DuLieuLenTextBox(dp);
            }
        }
        #endregion
        #region Đặt phòng
        /** Chức năng đặt phòng **/
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Ngày nhận/Ngày đặt không phù hợp", "Thông báo", MessageBoxButtons.YesNo);
            }
            else
            {
                KhachHang khc = kh.TimKhachHang(txtSoDienThoai.Text);
                int dem = khc.SoLanDen;
                if (khc != null)
                {
                    khc.SoLanDen = dem + 1;
                    if (khc.SoLanDen >= 5)
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
                    kh.SuaSoLanDen(khc);
                    string maKH = khc.MaKH;
                    DonDatPhong ddp = new DonDatPhong();
                    ddp = TaoDonDatPhong(maKH);
                    clsddp.ThemDonDatPhong(ddp);
                }
                else
                {
                    KhachHang khm = new KhachHang();
                    khm = TaoKhachHang();
                    kh.ThemKhachHang(khm);
                    string maKH = khm.MaKH;
                    DonDatPhong ddp = new DonDatPhong();
                    ddp = TaoDonDatPhong(maKH);
                    clsddp.ThemDonDatPhong(ddp);
                }
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();
                TaoPhongThuong(12);
                TaoPhongVip(12);
                IEnumerable<dynamic> danhsDP;
                danhsDP = hl.LayThongTinDonDatPhong();
                TaiDuLieuLenListView(lvwDanhSachDP, danhsDP);
            }
        }
        /** Tạo mã đơn đặt phòng **/
        private string TaoMaDDP()
        {
            string maDDP = "";
            int dem = clsddp.TraTatCaDDP().Count() + 1;
            if (dem < 10)
            {
                maDDP += "DP00" + dem;
            }
            else
            {
                maDDP += "DP0" + dem;
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
            khm.SoLanDen = 1;
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
        }
        
    }
}
