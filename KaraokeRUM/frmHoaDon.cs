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
    public partial class frmHoaDon : Form
    {
        private string MaHD;
        private bool StatusBTN;
        private clsHoaDon HoaDon;
        private clsPhong Phong;
        private clsLoaiPhong LoaiPhong;
        private clsMatHang MatHang;
        private clsKhachHang KhachHang;
        private clsLoaiKhach LoaiKhach;
        private IEnumerable<ChiTietHoaDon> DsCTHD;
        private int TienMatHang;
        private int GiaPhong;
        private double TienPhong;
        private int ChietKhau;
        private double TongTien;
        private int i = 1;

        public frmHoaDon(string maHD, bool statusBtn)
        {
            InitializeComponent();
            this.MaximumSize = new Size(650, 1011);
            this.MinimumSize = new Size(650, 1011);
            this.StartPosition = FormStartPosition.CenterScreen;

            //Gán giá trị biến
            MaHD = maHD;
            StatusBTN = statusBtn;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            HoaDon = new clsHoaDon();
            Phong = new clsPhong();
            LoaiPhong = new clsLoaiPhong();
            MatHang = new clsMatHang();
            KhachHang = new clsKhachHang();
            LoaiKhach = new clsLoaiKhach();
            //MessageBox.Show(MaHD);
            btnThanhToan.Enabled = StatusBTN;
            var hd = HoaDon.LayHoaDon(MaHD);
            var phong = Phong.KiemTra(hd.MaPhong);
            var loaiPhong = LoaiPhong.LayLoaiPhong(phong.MaLoaiPhong);
            var khachHang = KhachHang.LayThongTinKhach(hd.MaKH);
            var loaiKhach = LoaiKhach.LayLoaiKhach(khachHang.MaLoaiKH);

            //Các biến dùng để tính tiền
            TienMatHang = HoaDon.TinhTongTienMatHang(MaHD);
            ChietKhau = loaiKhach.ChietKhau;
            GiaPhong = (int)loaiPhong.Gia;
            TienPhong = (HoaDon.TinhGio(MaHD) / 60) * GiaPhong;
            if(ChietKhau == 0)
            {
                TongTien = (TienMatHang + TienPhong) * 1.1;
            }
            else if(ChietKhau == 5)
            {
                TongTien = ((TienMatHang + TienPhong) * 1.1) * 0.95;
            }
            else
            {
                TongTien = ((TienMatHang + TienPhong) * 1.1) * 0.9;
            }
            //Gán các giá trị
            lblMaHD.Text = hd.MaHD;
            lblGioVao.Text = hd.GioVao.ToString();
            lblGioRa.Text = hd.GioRa.ToString();
            lblNgayLap.Text = hd.NgayLap.ToString("dd/MM/yyyy");
            lblSoPhong.Text = phong.TenPhong;
            lblLoaiPhong.Text = loaiPhong.TenLoaiPhong;
            lblDonGia.Text = GiaPhong.ToString("#,### VNĐ");
            lblChietKhau.Text = ChietKhau.ToString() + " %";
            lblTienMatHang.Text = TienMatHang.ToString("#,### VNĐ");
            lblTienPhong.Text = TienPhong.ToString("#,### VNĐ");
            lblTongTien.Text = TongTien.ToString("#,### VNĐ");
            lblTenKhach.Text = khachHang.TenKhach;
            //ListView
            DsCTHD = HoaDon.LayChiTietHoaDon(MaHD);
            TaoListView(lstvChiTietHoaDon);
            TaiDuLieuLenListView(lstvChiTietHoaDon, DsCTHD);
        }

        /**
         * Khởi tạo listView tạo các cột
         */
        private void TaoListView(ListView lstv)
        {
            lstv.Columns.Add("STT", 50);
            lstv.Columns.Add("Tên mặt hằng", 250);
            lstv.Columns.Add("Số Lượng", 93);
            lstv.Columns.Add("ThanhTien", 200);

            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /**
         * Tạo các ItemListView để thêm vào listView
         */
        private ListViewItem TaoItem(dynamic item)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem((i++).ToString());
            lstvItem.SubItems.Add(MatHang.TimTheoMa(item.MaMH));
            lstvItem.SubItems.Add(item.SoLuong.ToString());
            lstvItem.SubItems.Add(item.ThanhTien.ToString("#,### VNĐ"));

            lstvItem.Tag = item;
            return lstvItem;
        }

        private void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> dsCTHD)
        {
            lstv.Items.Clear();
            ListViewItem itemHD;
            foreach (dynamic item in dsCTHD)
            {
                itemHD = TaoItem(item);
                lstv.Items.Add(itemHD);
            }
        }
    }
}
