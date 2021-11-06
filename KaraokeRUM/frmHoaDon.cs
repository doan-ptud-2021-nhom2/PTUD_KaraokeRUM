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
        /*
         * Các biến toàn cục
         * StatusBTN: trạng thái của nút
         * DsCTHD: danh sách chi tiết hóa đơn
         * KhoangTG: thời gian sử dụng phòng
         */
        private string MaHD;
        private bool StatusBTN;
        private clsHoaDon HoaDon;
        private clsPhong Phong;
        private clsLoaiPhong LoaiPhong;
        private clsMatHang MatHang;
        private clsKhachHang KhachHang;
        private clsLoaiKhach LoaiKhach;
        private clsChuyenSoThanhChu SoThanhChu;
        private IEnumerable<ChiTietHoaDon> DsCTHD;
        private string GioVao;
        private string GioRa;
        private int KhoangTG;
        private int TienMatHang;
        private double GiaPhong;
        private double TienPhong;
        private int ChietKhau;
        private double TongTien;
        private int i = 1;
        private frmChiTietPhong frmCTP;

        /*
         * Constructor
         */
        public frmHoaDon(string maHD, bool statusBtn, frmChiTietPhong _frmCTP)
        {
            InitializeComponent();
            this.MaximumSize = new Size(650, 1011);
            this.MinimumSize = new Size(650, 1011);
            this.StartPosition = FormStartPosition.CenterScreen;

            //Gán giá trị biến
            MaHD = maHD;
            StatusBTN = statusBtn;
            frmCTP = _frmCTP;
        }

        /*
         * Form chính 
         */
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            HoaDon = new clsHoaDon();
            Phong = new clsPhong();
            LoaiPhong = new clsLoaiPhong();
            MatHang = new clsMatHang();
            KhachHang = new clsKhachHang();
            LoaiKhach = new clsLoaiKhach();
            SoThanhChu = new clsChuyenSoThanhChu();

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
            KhoangTG = HoaDon.TinhGio(MaHD);
            TienPhong = (KhoangTG / 60.0) * GiaPhong;
            GioVao = hd.GioVao.ToString(@"hh\:mm\:ss");
            TimeSpan ts = TimeSpan.Parse(hd.GioRa.ToString());
            GioRa = ts.ToString(@"hh\:mm\:ss");

            //Thực hiện tính tổng tiền
            if (ChietKhau == 0)
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
            lblGioVao.Text = GioVao;
            lblGioRa.Text = GioRa;
            lblNgayLap.Text = hd.NgayLap.ToString("dd/MM/yyyy");
            lblTenPhong.Text = phong.TenPhong;
            lblLoaiPhong.Text = loaiPhong.TenLoaiPhong;
            lblDonGia.Text = GiaPhong.ToString("#,### VNĐ");
            lblChietKhau.Text = ChietKhau.ToString() + " %";
            lblTienMatHang.Text = TienMatHang.ToString("#,### VNĐ");
            lblTienPhong.Text = TienPhong.ToString("#,### VNĐ");
            lblTongTien.Text = TongTien.ToString("#,### VNĐ");
            lblTenKhach.Text = khachHang.TenKhach;
            lblSoDienThoai.Text = khachHang.SDT;

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
            lstv.Columns.Add("Tên Mặt Hàng", 250);
            lstv.Columns.Add("Số Lượng", 93);
            lstv.Columns.Add("Thành Tiền", 200);

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
            lstvItem.SubItems.Add(MatHang.TimTheoMa(item.MaMH).TenMh);
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

        /**
        * In hóa đơn
        */
        private void InHoaDon()
        {
            pddHoaDon.Document = pdHoaDon;
            pddHoaDon.ShowDialog();
        }
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            InHoaDon();
        }

        /**
         * Sự kiện in hóa đơn.
         */
        private void pdHoaDon_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Tìm để lấy thông tin hiển thị ra giao diện
            var danhSachHoaDon = HoaDon.LayChiTietHoaDon(MaHD);
            var hd = HoaDon.LayHoaDon(MaHD);
            var tenKhachHang = KhachHang.LayThongTinKhach(hd.MaKH).TenKhach;
            var soDienThoai = KhachHang.LayThongTinKhach(hd.MaKH).SDT;
            var phong = Phong.LayThongTinPhong(hd.MaPhong).TenPhong;
            var tam = Phong.LayThongTinPhong(hd.MaPhong);
            var loaiPhong = LoaiPhong.LayLoaiPhong(tam.MaLoaiPhong).TenLoaiPhong;
            var donGiaLoaiPhong = LoaiPhong.LayLoaiPhong(tam.MaLoaiPhong).Gia;

            //Định nghĩa chiều rộng
            var w = pdHoaDon.DefaultPageSettings.PaperSize.Width;
            //Định dạng bút vẽ
            Pen mauDen = new Pen(Color.Black, 1);
            var y = 100;
            //Canh lề trái, phải
            Point p1 = new Point(10, y);
            Point p2 = new Point(w - 10, y);
            //Kẻ đường ngang
            e.Graphics.DrawLine(mauDen, p1, p2);

            /**
             * Phần Header
             */
            //Lấy thông tin của Quán
            e.Graphics.DrawString(
                                "Karaoke RUM",
                                new Font("Courier New", 20, FontStyle.Bold),
                                Brushes.Black,
                                new Point(40, 20));
            e.Graphics.DrawString(
                                "Địa chỉ: 977 Phạm Văn Đồng, Q.Gò Vấp, TP. HCM",
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, 50));
            e.Graphics.DrawString(
                                "Số điện thoại: 0982332001",
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, 70));
            //Lấy Mã hóa đơn
            e.Graphics.DrawString(
                                String.Format("{0}", MaHD),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(w / 2 + 220, 25));
            //Lấy ngày, giờ xuất hóa đơn
            e.Graphics.DrawString(
                                String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(w / 2 + 220, 50));

            /**
            * Phần Body
            */
            //Lấy tên Khách hàng
            y += 20;
            e.Graphics.DrawString(
                                String.Format("Tên khách hàng: {0}", tenKhachHang),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, y));
            //Lấy số điện thoại Khách hàng
            y += 20;
            e.Graphics.DrawString(
                                String.Format("SĐT khách hàng: {0}", soDienThoai),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, y));
            //Lấy Số phòng của khách
            e.Graphics.DrawString(
                                String.Format("Số phòng: {0}", phong),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(w / 2 + 140, y));
            //Lấy Loại phòng
            y += 20;
            e.Graphics.DrawString(
                                String.Format("Loại phòng: {0}", loaiPhong),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, y));
            //Lấy Đơn giá
            e.Graphics.DrawString(
                               String.Format("Đơn giá: {0}", donGiaLoaiPhong.ToString("#,### VNĐ")),
                               new Font("Courier New", 13, FontStyle.Regular),
                               Brushes.Black,
                               new Point(w / 2 + 140, y));
            //Lấy giờ sử dụng Phòng (Giờ vào, Giờ ra)
            y += 20;
            e.Graphics.DrawString(
                                String.Format("Thời gian vào: {0}", GioVao),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, y));
            e.Graphics.DrawString(
                                String.Format("Thời gian ra: {0}", GioRa),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(w / 2 + 140, y));
            //Hiển thị thời gian sử dụng Phòng
            y += 20;
            e.Graphics.DrawString(
                                String.Format("Thời gian sử dụng Phòng: {0} phút", KhoangTG),
                                new Font("Courier New", 13, FontStyle.Regular),
                                Brushes.Black,
                                new Point(40, y));
            //Hiển thị tiền phòng
            e.Graphics.DrawString(
                               String.Format("Tiền phòng: {0}", TienPhong.ToString("#,### VNĐ")),
                               new Font("Courier New", 13, FontStyle.Regular),
                               Brushes.Black,
                               new Point(w / 2 + 140, y));
            y += 55;
            //Tiêu đề
            e.Graphics.DrawString(
                               "STT",
                               new Font("Courier New", 13, FontStyle.Regular),
                               Brushes.Black,
                               new Point(10, y)); 
            e.Graphics.DrawString(
                               "Mặt Hàng",
                               new Font("Courier New", 13, FontStyle.Regular),
                               Brushes.Black,
                               new Point(80, y));
            e.Graphics.DrawString(
                               "Số Lượng",
                               new Font("Courier New", 13, FontStyle.Regular),
                               Brushes.Black,
                               new Point(w / 2, y));
            e.Graphics.DrawString(
                               "Thành tiền",
                               new Font("Courier New", 13, FontStyle.Regular),
                               Brushes.Black,
                               new Point(w - 200, y));

            //Canh lề trái, phải
            y -= 15;
            p1 = new Point(10, y);
            p2 = new Point(w - 10, y);
            //Kẻ đường ngang
            e.Graphics.DrawLine(mauDen, p1, p2);
            int i = 1;
            y += 50;

            foreach (dynamic danhSach in danhSachHoaDon)
            {
                e.Graphics.DrawString(
                                   string.Format("{0}", i++),
                                   new Font("Courier New", 13, FontStyle.Regular),
                                   Brushes.Black,
                                   new Point(30, y));
                e.Graphics.DrawString(
                                   string.Format("{0}", MatHang.TimTheoMa(danhSach.MaMH).TenMh),
                                   new Font("Courier New", 13, FontStyle.Regular),
                                   Brushes.Black,
                                   new Point(80, y));
                e.Graphics.DrawString(
                                   string.Format("{0}", danhSach.SoLuong),
                                   new Font("Courier New", 13, FontStyle.Regular),
                                   Brushes.Black,
                                   new Point(w - 355, y));
                e.Graphics.DrawString(
                                   string.Format("{0}", danhSach.ThanhTien.ToString("#,### VNĐ")),
                                   new Font("Courier New", 13, FontStyle.Regular),
                                   Brushes.Black,
                                   new Point(w - 200, y));
                y += 20;
            }

            /**
            * Phần Footer
            */
            //Canh lề trái, phải
            y += 100;
            p1 = new Point(10, y);
            p2 = new Point(w - 10, y);
            //Kẻ đường ngang
            e.Graphics.DrawLine(mauDen, p1, p2);
            //Tổng tiền
            e.Graphics.DrawString(
                                   string.Format("Tổng tiền: {0}", TongTien.ToString("#,### VNĐ")),
                                   new Font("Courier New", 13, FontStyle.Regular),
                                   Brushes.Black,
                                   new Point(w - 320, y + 8));
            //Đọc số tiền thành chữ
            y += 40;
            e.Graphics.DrawString(
                                   string.Format("Thành chữ: {0} đồng", SoThanhChu.DocTienBangChu((long)TongTien)),
                                   new Font("Courier New", 12, FontStyle.Regular),
                                   Brushes.Black,
                                   new Point(15, y));
            //Text: Cám ơn khách hàng
            y += 100;
            e.Graphics.DrawString(
                                   "Cảm ơn quý khách đã sử dụng dịch vụ của Quán, hẹn gặp lại quý khách!",
                                   new Font("Courier New", 14, FontStyle.Bold),
                                   Brushes.Black,
                                   new Point(15, y));

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult hoiThanhToan;
            hoiThanhToan = MessageBox.Show("Bạn có muốn thanh toán không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(hoiThanhToan == DialogResult.Yes)
            {
                //Cập nhật thành tiền vào hóa đơn.
                HoaDon hoaDon = HoaDon.LayHoaDon(MaHD);
                hoaDon.TongTien = Convert.ToDecimal(TongTien);
                HoaDon.CapNhapHoaDon(hoaDon);

                //Cập nhật tổng tiền cho khách hàng (để thống kê).
                KhachHang maKhachHang = KhachHang.LayThongTinKhach(hoaDon.MaKH);
                if (maKhachHang.SoLanDen == 1)
                {
                    maKhachHang.TongTien = Convert.ToDecimal(TongTien);
                    KhachHang.CapNhatTongTienChoKhach(maKhachHang);
                }
                else
                {
                    maKhachHang.TongTien += Convert.ToDecimal(hoaDon.TongTien);
                    KhachHang.CapNhatTongTienChoKhach(maKhachHang);
                }

                //Cập nhật trạng thái phòng.
                Phong phong = Phong.TimMotPhongTheoMa(hoaDon.MaPhong);
                phong.TrangThaiPhong = "Đóng";
                Phong.SuaTrangThaiPhong(phong);

                MessageBox.Show("Hoàn tất thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Tắt giao diện hóa đơn.
                this.Close();

                //Xử lý tắt giao diện này rồi mở giao diện Phòng lên.
                frmCTP.Close();
            }
           
        }

    }
}
