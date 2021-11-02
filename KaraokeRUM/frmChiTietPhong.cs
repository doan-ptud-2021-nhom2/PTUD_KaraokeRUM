using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Dynamic;


namespace KaraokeRUM
{
    public partial class frmChiTietPhong : Form
    {
        public frmChiTietPhong()
        {
            InitializeComponent();
        }

        /** 
        * Các biến toàn cục.
        * mh: class Mặt Hàng.
        * cthd: class Chi tiết hóa đơn.
        * dsMatHang: danh sách tất cả Mặt hàng.
        * dsTenMatHang: danh sách chỉ có tên Mặt hàng.
        * i: tăng tự động (Số thứ tự).
        */
        private clsMatHang mh;
        private clsChiTietHoaDon cthd;
        private clsHoaDon HoaDon;
        private clsPhong Phong;
        private clsLoaiPhong LoaiPhong;
        private IEnumerable<dynamic> dsMatHang;
        private IEnumerable<MatHang> dsTenMatHang;
        private string tenPhong;
        private string maHoaDon;

        /**
         * Tạo constructor có:
         * 1. Tên phòng.
         * 2. Mã hóa đơn.
        */
        public frmChiTietPhong(string maHoaDon)
        {
            InitializeComponent();
            this.maHoaDon = maHoaDon;
        }

        /**
         * Form chính
        */
        private void frmChiTietPhong_Load(object sender, EventArgs e)
        {
            mh = new clsMatHang();
            cthd = new clsChiTietHoaDon();
            HoaDon = new clsHoaDon();
            Phong = new clsPhong();
            LoaiPhong = new clsLoaiPhong();

            //Khởi tạo
            HoaDon hoaDon = HoaDon.LayHoaDon(maHoaDon);
            Phong phong = Phong.LayThongTinPhong(hoaDon.MaPhong);
            txtMaPhong.Text = hoaDon.MaPhong;
            txtTenPhong.Text = Phong.TimMotPhongTheoMa(hoaDon.MaPhong).TenPhong;
            txtTTP.Text = Phong.TimMotPhongTheoMa(hoaDon.MaPhong).TrangThaiPhong;
            txtLoaiPhong.Text = LoaiPhong.LayLoaiPhong(phong.MaLoaiPhong).TenLoaiPhong;

            //Lấy tên phòng
            lblTenPhong.Text = "Phòng - " + tenPhong;

            //Tải tên mặt hàng lên combobox.
            TaiMatHangLenComboBox();

            TaoTieuDeCot(lstvDanhSachMatHang);
            dsMatHang = HoaDon.LayChiTietHoaDon(maHoaDon);
            TaiDuLieuLenListView(lstvDanhSachMatHang, dsMatHang);

        }

        /** 
         * Xử lý tải tên của tất cả Mặt hàng lên combobox.
        */
        private void TaiMatHangLenComboBox()
        {
            dsTenMatHang = mh.LayTatCaMatHang();
            foreach (MatHang mh in dsTenMatHang)
            {
                cboMatHang.Items.Add(mh.TenMh);
            }
        }

        /** 
         * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lstv)
        {
            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;

            lstv.Columns.Add("STT", 80);
            lstv.Columns.Add("Tên Mặt Hàng", 160);
            lstv.Columns.Add("Đơn vị", 140);
            lstv.Columns.Add("Số Lượng", 130);
            lstv.Columns.Add("Thành Tiền", 130);
        }

        /** 
        * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> dsMatHang)
        {
            lstv.Items.Clear();
            ListViewItem itemMH;
            for (int i = 0; i < dsMatHang.Count(); ++i)
            {
                itemMH = TaoDanhSachItem(dsMatHang.ElementAt(i), i + 1);
                lstv.Items.Add(itemMH);
            }
        }
        ListViewItem TaoDanhSachItem(dynamic itemMH, int id)
        {
            ListViewItem dsItem;
            dsItem = new ListViewItem(id.ToString());
            dsItem.SubItems.Add(mh.TimTheoMa(itemMH.MaMH).TenMh);
            dsItem.SubItems.Add(mh.TimTheoMa(itemMH.MaMH).DonVi);
            dsItem.SubItems.Add(itemMH.SoLuong.ToString());
            dsItem.SubItems.Add(itemMH.ThanhTien.ToString("##,## VNĐ"));

            dsItem.Tag = itemMH;
            return dsItem;
        }

        /** 
        * Load dữ liệu ngược lại từ ListView sang các textbox và combobox.
        */
        private void lstvDanhSachMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dsMH = null;
            if (lstvDanhSachMatHang.SelectedItems.Count > 0)
            {
                dsMH = (dynamic)lstvDanhSachMatHang.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsMH);
            }
        }

        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsMH)
        {
            cboMatHang.Text = mh.TimTheoMa(dsMH.MaMH).TenMh;
            txtSoLuong.Text = dsMH.SoLuong.ToString();
        }


        /** 
        * Xử lý hiện lên form Đổi phòng để đổi phòng cho khách hàng. 
        */
        private void btnDoiPhong_Click(object sender, EventArgs e)
        {
            frmDanhSachPhong frm = new frmDanhSachPhong();
            frm.Show();
        }

        /** 
        * Xử lý hiện lên form Hóa đơn để tạo hóa đơn cho khách hàng. 
        */
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = HoaDon.LayHoaDon(maHoaDon);
            DateTime dt = DateTime.Now;
            TimeSpan tp = (TimeSpan)dt.TimeOfDay;
            hoaDon.GioRa = tp;
            HoaDon.CapNhapHoaDon(hoaDon);

            frmHoaDon frm = new frmHoaDon(this.maHoaDon, true);
            frm.Show();
        }

        /** 
        * Thêm một mặt hàng vào danh sách mặt hàng.
        */
        /*private double ThemMatHangCungTen(string maMatHang, int soLuong)
        {
            var gia = (double)mh.TimMatHangTheoMaDeLayGia(maMatHang).First().Gia;
            double thanhTien = Convert.ToInt32(soLuong+txtSoLuong.Text) * gia;
            return thanhTien;
        }*/
        private void btnThem_Click(object sender, EventArgs e)
        {
            ChiTietHoaDon chiTietHoaDon = TaoChiTietHoaDon();
            cthd.ThemChiTietHoaDon(chiTietHoaDon);

            dsMatHang = HoaDon.LayChiTietHoaDon(maHoaDon);
            TaiDuLieuLenListView(lstvDanhSachMatHang, dsMatHang);
        }

        /** 
        * Tạo mặt hàng
        */
        ChiTietHoaDon TaoChiTietHoaDon()
        {
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.MaHD = this.maHoaDon;
            chiTietHoaDon.MaMH = mh.TimMaTheoTen(cboMatHang.Text).MaMH;
            chiTietHoaDon.SoLuong = Convert.ToInt32(txtSoLuong.Text);
            chiTietHoaDon.ThanhTien = 0;

            return chiTietHoaDon;
        }

        /** 
        * Cập nhật thành tiền
        */
        private double CapNhatThanhTien(string maMatHang, int soLuong)
        {
            var gia = (double)mh.TimTheoMa(maMatHang).Gia;
            double thanhTien = soLuong * gia;
            return thanhTien;
        }

        /** 
        * Sửa thông tin mặt hàng (Số lượng).
        */
        ChiTietHoaDon SuaSoLuongMatHang()
        {
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.MaHD = this.maHoaDon;
            chiTietHoaDon.MaMH = mh.TimMaTheoTen(cboMatHang.Text).MaMH;
            chiTietHoaDon.SoLuong = Convert.ToInt32(txtSoLuong.Text);
            chiTietHoaDon.ThanhTien = Convert.ToDecimal(CapNhatThanhTien(chiTietHoaDon.MaMH, chiTietHoaDon.SoLuong));

            return chiTietHoaDon;
        }

        //Lỗi không load được chỗ listView
        private void btnSua_Click(object sender, EventArgs e)
        {
            ChiTietHoaDon suaSoLuong = SuaSoLuongMatHang();
            cthd.SuaThongTinMatHang(suaSoLuong);

            dsMatHang = HoaDon.LayChiTietHoaHoaTaiLenListView(maHoaDon);
            TaiDuLieuLenListView(lstvDanhSachMatHang, dsMatHang);
        }

        /** 
        * Xoá thông tin trong danh sách Mặt hàng
        */
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult hoiXoa;
            ChiTietHoaDon chiTietHoaDon;

            if (lstvDanhSachMatHang.SelectedItems.Count > 0)
            {
                hoiXoa = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (hoiXoa == DialogResult.Yes)
                {
                    
                    chiTietHoaDon = cthd.TimChiTietHoaDon(mh.TimMaTheoTen(cboMatHang.Text).MaMH, this.maHoaDon).First();
                    cthd.XoaChiTietHoaDon(chiTietHoaDon);

                    dsMatHang = HoaDon.LayChiTietHoaDon(maHoaDon);
                    TaiDuLieuLenListView(lstvDanhSachMatHang, dsMatHang);
                }
            }
        }
    }
}

