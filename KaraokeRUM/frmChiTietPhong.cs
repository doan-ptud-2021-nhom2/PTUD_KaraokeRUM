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
        private clsKhachHang KhachHang;
        private IEnumerable<dynamic> dsMatHang;
        private IEnumerable<MatHang> dsTenMatHang;
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
            KhachHang = new clsKhachHang();

            //Khởi tạo
            HoaDon hoaDon = HoaDon.LayHoaDon(maHoaDon);
            Phong phong = Phong.LayThongTinPhong(hoaDon.MaPhong);
            txtTenKhachHang.Text = KhachHang.LayThongTinKhach(hoaDon.MaKH).TenKhach;
            txtSoDienThoai.Text = KhachHang.LayThongTinKhach(hoaDon.MaKH).SDT;
            txtGioVao.Text = hoaDon.GioVao.ToString(@"hh\:mm\:ss");
            txtTenPhong.Text = Phong.TimMotPhongTheoMa(hoaDon.MaPhong).TenPhong;
            txtLoaiPhong.Text = LoaiPhong.LayLoaiPhong(phong.MaLoaiPhong).TenLoaiPhong;
            txtTTP.Text = Phong.TimMotPhongTheoMa(hoaDon.MaPhong).TrangThaiPhong;

            //Lấy tên phòng
            lblTenPhong.Text = "Phòng - " + Phong.TimMotPhongTheoMa(hoaDon.MaPhong).TenPhong;

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
            frmDanhSachPhong frm = new frmDanhSachPhong(maHoaDon, this);
            frm.Show();
            //Cập nhật phòng cũ
            Phong phong;
            phong = Phong.TimPhong(txtTenPhong.Text).First();
            phong.TrangThaiPhong = "Đóng";
            Phong.SuaTrangThaiPhong(phong);
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

            frmHoaDon frm = new frmHoaDon(this.maHoaDon, true, this);
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
            cboMatHang.Text = "";
            txtSoLuong.Text = "";
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
            cboMatHang.Text = "";
            txtSoLuong.Text = "";
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
                    cboMatHang.Text = "";
                    txtSoLuong.Text = "";
                }
            }
        }

        /*
         * Hủy Phòng
         */
        private void btnHuyPhong_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = HoaDon.LayHoaDon(maHoaDon);
            Phong phong = Phong.LayThongTinPhong(hoaDon.MaPhong);
            //Phong tam = Phong.TimMotPhongTheoMa(hoaDon.MaPhong);
            DialogResult hoiHuy;
            hoiHuy = MessageBox.Show("Bạn có muốn hủy phòng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (hoiHuy == DialogResult.Yes)
            {
                if(lstvDanhSachMatHang.Items.Count != 0)
                {
                    MessageBox.Show("Cần phải xóa các mặt hàng trước khi hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtTenPhong.Text == phong.TenPhong)
                {
                    phong.TrangThaiPhong = "Đóng";
                    Phong.SuaTrangThaiPhong(phong);
                    HoaDon.XoaHoaDon(hoaDon);
                    this.Close();
                }
            }
        }

        /*
         * Thoát: quay lại form Đặt phòng
         */
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat;
            hoiThoat = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(hoiThoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

    }
}

