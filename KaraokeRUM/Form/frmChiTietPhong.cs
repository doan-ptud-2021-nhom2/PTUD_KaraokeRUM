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
        private clsMatHang MATHANG;
        private clsChiTietHoaDon CHITIETHOADON;
        private clsHoaDon HOADON;
        private clsPhong PHONG;
        private clsLoaiPhong LOAIPHONG;
        private clsKhachHang KHACHHANG;
        private IEnumerable<dynamic> DANHSACHMATHANG;
        private IEnumerable<MatHang> DANHSACHTENMATHANG;
        private string MAHOADON;

        /**
         * Tạo constructor có:
         * 1. Tên phòng.
         * 2. Mã hóa đơn.
        */
        public frmChiTietPhong(string maHoaDon)
        {
            InitializeComponent();
            this.MAHOADON = maHoaDon;
        }

        /**
         * Form chính
        */
        private void frmChiTietPhong_Load(object sender, EventArgs e)
        {
            MATHANG = new clsMatHang();
            CHITIETHOADON = new clsChiTietHoaDon();
            HOADON = new clsHoaDon();
            PHONG = new clsPhong();
            LOAIPHONG = new clsLoaiPhong();
            KHACHHANG = new clsKhachHang();

            //Khởi tạo
            HoaDon hoaDon = HOADON.LayHoaDon(MAHOADON);
            Phong phong = PHONG.LayThongTinPhong(hoaDon.MaPhong);
            txtTenKhachHang.Text = KHACHHANG.LayThongTinKhach(hoaDon.MaKH).TenKhach;
            txtSoDienThoai.Text = KHACHHANG.LayThongTinKhach(hoaDon.MaKH).SDT;
            txtGioVao.Text = hoaDon.GioVao.ToString(@"hh\:mm\:ss");
            txtTenPhong.Text = PHONG.TimMotPhongTheoMa(hoaDon.MaPhong).TenPhong;
            txtLoaiPhong.Text = LOAIPHONG.LayLoaiPhong(phong.MaLoaiPhong).TenLoaiPhong;
            txtTTP.Text = PHONG.TimMotPhongTheoMa(hoaDon.MaPhong).TrangThaiPhong;

            //Lấy tên phòng
            lblTenPhong.Text = "Phòng - " + PHONG.TimMotPhongTheoMa(hoaDon.MaPhong).TenPhong;

            //Tải tên mặt hàng lên combobox.
            TaiMatHangLenComboBox();

            TaoTieuDeCot(lstvDanhSachMatHang);
            DANHSACHMATHANG = HOADON.LayChiTietHoaDon(MAHOADON);
            TaiDuLieuLenListView(lstvDanhSachMatHang, DANHSACHMATHANG);

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

        }

        /** 
         * Xử lý tải tên của tất cả Mặt hàng lên combobox.
        */
        private void TaiMatHangLenComboBox()
        {
            DANHSACHTENMATHANG = MATHANG.LayTatCaMatHang();
            foreach (MatHang mh in DANHSACHTENMATHANG)
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
            dsItem.SubItems.Add(MATHANG.TimTheoMa(itemMH.MaMH).TenMh);
            dsItem.SubItems.Add(MATHANG.TimTheoMa(itemMH.MaMH).DonVi);
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
                cboMatHang.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            } else
            {
                cboMatHang.Enabled = true;
            }
        }

        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsMH)
        {
            cboMatHang.Text = MATHANG.TimTheoMa(dsMH.MaMH).TenMh;
            txtSoLuong.Text = dsMH.SoLuong.ToString();
        }


        /** 
        * Xử lý hiện lên form Đổi phòng để đổi phòng cho khách hàng. 
        */
        private void btnDoiPhong_Click(object sender, EventArgs e)
        {
            
            //Cập nhật phòng cũ
            Phong phongCu;
            phongCu = PHONG.TimPhong(txtTenPhong.Text).First();
            frmDanhSachPhong frm = new frmDanhSachPhong(MAHOADON, phongCu, this);
            frm.Show();
        }

        /** 
        * Xử lý hiện lên form Hóa đơn để tạo hóa đơn cho khách hàng. 
        */
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = HOADON.LayHoaDon(MAHOADON);
            DateTime dt = DateTime.Now;
            TimeSpan tp = (TimeSpan)dt.TimeOfDay;
            hoaDon.GioRa = tp;
            HOADON.CapNhapHoaDon(hoaDon);

            frmHoaDon frm = new frmHoaDon(this.MAHOADON, true, this);
            frm.Show();
        }

        /** 
        * Thêm một mặt hàng vào danh sách mặt hàng.
        */
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboMatHang.Text == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập, chọn đầy đủ thông tin để thực hiện chức năng!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Convert.ToInt32(txtSoLuong.Text) < 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ChiTietHoaDon chiTietHoaDon = TaoChiTietHoaDon();
                int soLuongTon = MATHANG.TimMaTheoTen(cboMatHang.Text).SoLuongTon;

                if (CHITIETHOADON.TimChiTietHoaDon(MATHANG.TimMaTheoTen(cboMatHang.Text).MaMH, this.MAHOADON).Count() > 0)
                {
                    MessageBox.Show("Mặt hàng đã tồn tại!. Bạn có thể dùng chức năng sửa.", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboMatHang.Text = "";
                    txtSoLuong.Text = "";
                }
                else if (Convert.ToInt32(txtSoLuong.Text) > soLuongTon)
                {
                    string thongBao = "Số lượng tồn của mặt hàng không đủ. Số lượng hiện tại: " + 
                                       soLuongTon + ". Vui lòng nhập số lượng phù hợp!";
                    MessageBox.Show(thongBao, "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    CHITIETHOADON.ThemChiTietHoaDon(chiTietHoaDon);
                    DANHSACHMATHANG = HOADON.LayChiTietHoaDon(MAHOADON);
                    TaiDuLieuLenListView(lstvDanhSachMatHang, DANHSACHMATHANG);

                    cboMatHang.Text = "";
                    txtSoLuong.Text = "";
                }
            }

            cboMatHang.Enabled = true;
        }

        /** 
        * Tạo mặt hàng
        */
        ChiTietHoaDon TaoChiTietHoaDon()
        {
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.MaHD = this.MAHOADON;
            chiTietHoaDon.MaMH = MATHANG.TimMaTheoTen(cboMatHang.Text).MaMH;
            chiTietHoaDon.SoLuong = Convert.ToInt32(txtSoLuong.Text);
            chiTietHoaDon.ThanhTien = 0;

            return chiTietHoaDon;
        }

        /** 
        * Cập nhật thành tiền
        */
        private double CapNhatThanhTien(string maMatHang, int soLuong)
        {
            var gia = (double)MATHANG.TimTheoMa(maMatHang).Gia;
            double thanhTien = soLuong * gia;
            return thanhTien;
        }

        /** 
        * Sửa thông tin mặt hàng (Số lượng).
        */
        ChiTietHoaDon SuaSoLuongMatHang()
        {
            if(lstvDanhSachMatHang.SelectedItems.Count > 0 && cboMatHang.SelectedIndex >= 0 && txtSoLuong.Text != "")
            {
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                chiTietHoaDon.MaHD = this.MAHOADON;
                chiTietHoaDon.MaMH = MATHANG.TimMaTheoTen(cboMatHang.Text).MaMH;
                chiTietHoaDon.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                chiTietHoaDon.ThanhTien = Convert.ToDecimal(CapNhatThanhTien(chiTietHoaDon.MaMH, chiTietHoaDon.SoLuong));

                return chiTietHoaDon;
            }

            return null;
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            int soLuongTon = MATHANG.TimMaTheoTen(cboMatHang.Text).SoLuongTon;
            if (SuaSoLuongMatHang() == null)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng để chỉnh sửa!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(Convert.ToInt32(txtSoLuong.Text) > soLuongTon)
            {
                string thongBao = "Số lượng tồn của mặt hàng không đủ. Số lượng hiện tại: " + 
                                    soLuongTon + ". Vui lòng nhập số lượng phù hợp!";
                MessageBox.Show(thongBao, "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ChiTietHoaDon suaSoLuong = SuaSoLuongMatHang();
                CHITIETHOADON.SuaThongTinMatHang(suaSoLuong);
                DANHSACHMATHANG = HOADON.LayChiTietHoaDon(MAHOADON);
                TaiDuLieuLenListView(lstvDanhSachMatHang, DANHSACHMATHANG);
                cboMatHang.Text = "";
                txtSoLuong.Text = "";
            }

            cboMatHang.Enabled = true;
        }

        /** 
        * Xoá thông tin trong danh sách Mặt hàng
        */
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult hoiXoa;
            ChiTietHoaDon chiTietHoaDon;

            if (lstvDanhSachMatHang.SelectedItems.Count > 0 && cboMatHang.Text != "" && txtSoLuong.Text != "")
            {
                hoiXoa = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (hoiXoa == DialogResult.Yes)
                {
                    chiTietHoaDon = CHITIETHOADON.TimChiTietHoaDon(MATHANG.TimMaTheoTen(cboMatHang.Text).MaMH, this.MAHOADON).First();
                    CHITIETHOADON.XoaChiTietHoaDon(chiTietHoaDon);
                    DANHSACHMATHANG = HOADON.LayChiTietHoaDon(MAHOADON);
                    TaiDuLieuLenListView(lstvDanhSachMatHang, DANHSACHMATHANG); 

                    cboMatHang.Text = "";
                    txtSoLuong.Text = "";
                    cboMatHang.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng để xóa!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * Hủy Phòng
         */
        private void btnHuyPhong_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = HOADON.LayHoaDon(MAHOADON);
            Phong phong = PHONG.LayThongTinPhong(hoaDon.MaPhong);
            //Phong tam = Phong.TimMotPhongTheoMa(hoaDon.MaPhong);
            DialogResult hoiHuy;
            hoiHuy = MessageBox.Show("Bạn có muốn hủy phòng không?", "Thông báo",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (hoiHuy == DialogResult.Yes)
            {
                if(lstvDanhSachMatHang.Items.Count != 0)
                {
                    MessageBox.Show("Cần phải xóa các mặt hàng trước khi hủy!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtTenPhong.Text == phong.TenPhong)
                {
                    phong.TrangThaiPhong = "Đóng";
                    PHONG.SuaTrangThaiPhong(phong);
                    HOADON.XoaHoaDon(hoaDon);
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
            hoiThoat = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(hoiThoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmChiTietPhong_Click(object sender, EventArgs e)
        {
            if(lstvDanhSachMatHang.SelectedItems.Count > 0)
            {
                lstvDanhSachMatHang.SelectedItems.Clear();
                cboMatHang.Enabled = true;
                txtSoLuong.Clear();
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
    }
}

