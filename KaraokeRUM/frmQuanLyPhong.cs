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
    public partial class frmQuanLyPhong : Form
    {

        /** 
         * Các biến toàn cục.
         */
        private clsPhong PHONG;
        private clsLoaiPhong LOAIPHONG;
        private clsHoaDon HOADON;
        private IEnumerable<Phong> DANHSACHPHONG;
        private IEnumerable<LoaiPhong> DANHSACHLOAIPHONG;
        private string MAQL;

        /** 
         * Constructor
         */
        public frmQuanLyPhong(string maQL)
        {
            InitializeComponent();
            this.MAQL = maQL;
        }

        private void frmQuanLyPhong_Load(object sender, EventArgs e)
        {
            //Gán thuộc tính cho combobox
            cboTrangThai.Text = "Đóng";
            cboLoaiPhong.Items.Add("VIP");
            cboLoaiPhong2.Items.Add("VIP");
            cboLoaiPhong.Items.Add("THƯỜNG");
            cboLoaiPhong2.Items.Add("THƯỜNG");

            TaoTieuDeCot(lstvDanhSachPhong);
            PHONG = new clsPhong();
            LOAIPHONG = new clsLoaiPhong();
            HOADON = new clsHoaDon();

            DANHSACHPHONG = PHONG.LayTatCaPhong();
            DANHSACHLOAIPHONG = LOAIPHONG.LayTatCaLoaiPhong();

            IEnumerable<dynamic> dsP = PHONG.LayTatCaPhongDong();
            TaiDuLieuLenListView(lstvDanhSachPhong, dsP);

            txtTimKiemThongTinPhong.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemThongTinPhong.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TaiDuLieuVaoBoxTimKiem();
        }

        /**
         * Hàm hỗ trợ tải tên phòng vào txtTimKiem để thực hiện chức năng tìm kiếm.
         */
        private void TaiDuLieuVaoBoxTimKiem()
        {
            DANHSACHPHONG = PHONG.LayTatCaPhong();
            txtTimKiemThongTinPhong.AutoCompleteCustomSource.Clear();
            foreach (Phong i in DANHSACHPHONG)
            {
                txtTimKiemThongTinPhong.AutoCompleteCustomSource.Add(i.TenPhong);
            }
        }
        /** 
         * Xử lý combobox LoaiPhong
         * Xử lý combobox LoaiPhong2
        */
        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            XuLyGiaPhuHopVoiLoaiPhong();
        }
        private void cboLoaiPhong2_SelectedIndexChanged(object sender, EventArgs e)
        {
            XuLyGiaPhuHopVoiLoaiPhong2();
        }

        void XuLyGiaPhuHopVoiLoaiPhong()
        {
            string loaiPhong = cboLoaiPhong.Text;
            string giaPhong = LOAIPHONG.TimLoaiPhong(loaiPhong).First().Gia.ToString("##,## VNĐ");

            txtGiaPhong.Text = giaPhong;
        }
        void XuLyGiaPhuHopVoiLoaiPhong2()
        {
            string loaiPhong = cboLoaiPhong2.Text;
            string giaPhong = LOAIPHONG.TimLoaiPhong(loaiPhong).First().Gia.ToString("##,## VNĐ");

            txtGiaPhongCu.Text = giaPhong;
        }

        /** 
         * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lstv)
        {
            lstv.Columns.Add("Mã Phòng", 100);
            lstv.Columns.Add("Tên Phòng", 110);
            lstv.Columns.Add("Trạng thái", 100);
            lstv.Columns.Add("Loại Phòng", 120);
            lstv.Columns.Add("Giá Phòng", 150);

            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> _dsPhong)
        {
            lstv.Items.Clear();
            ListViewItem itemPhong;
            foreach (dynamic ds in _dsPhong)
            {
                itemPhong = TaoItem(ds);
                lstv.Items.Add(itemPhong);
            }
        }
        ListViewItem TaoItem(dynamic itemP)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemP.MaPhong);
            lstvItem.SubItems.Add(itemP.TenPhong.Trim());
            lstvItem.SubItems.Add(itemP.TrangThaiPhong);
            lstvItem.SubItems.Add(LOAIPHONG.LayLoaiPhong(itemP.MaLoaiPhong).TenLoaiPhong);
            lstvItem.SubItems.Add(LOAIPHONG.LayLoaiPhong(itemP.MaLoaiPhong).Gia.ToString("#,### VNĐ"));

            lstvItem.Tag = itemP;
            return lstvItem;
        }

        /** 
        * Load dữ liệu ngược lại từ ListView sang các textbox và combobox.
        */
        private void lstvDanhSachPhong_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dynamic dsP = null;
            if (lstvDanhSachPhong.SelectedItems.Count > 0)
            {
                dsP = (dynamic)lstvDanhSachPhong.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsP);
                TaiDuLieuTuLstvDenTxtCbo2(dsP);
            }
        }
        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsP)
        {
            txtSoPhong.Text = dsP.TenPhong;
            cboTrangThai.Text = dsP.TrangThaiPhong;
            cboLoaiPhong.Text = LOAIPHONG.LayLoaiPhong(dsP.MaLoaiPhong).TenLoaiPhong;
            txtGiaPhong.Text = LOAIPHONG.LayLoaiPhong(dsP.MaLoaiPhong).Gia.ToString("##,## VNĐ");
        }
        void TaiDuLieuTuLstvDenTxtCbo2(dynamic dsP)
        {
            cboLoaiPhong2.Text = LOAIPHONG.LayLoaiPhong(dsP.MaLoaiPhong).TenLoaiPhong;
            txtGiaPhongCu.Text = LOAIPHONG.LayLoaiPhong(dsP.MaLoaiPhong).Gia.ToString("##,## VNĐ");
        }

        /** 
        * Xóa trắng các ô textbox, combobox.
        */
        void XoaCacTxtCbo()
        {
            txtSoPhong.Text = "";
            cboLoaiPhong.Text = "";
            txtGiaPhong.Text = "";
            cboLoaiPhong2.Text = "";
            txtGiaPhongCu.Text = "";
            txtGiaPhongMoi.Text = "";
            txtTimKiemThongTinPhong.Text = "";
        }

        /** 
        * Thêm thông tin phòng
        */
        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            if(TaoPhong() == null)
            {
                MessageBox.Show("Bạn cần nhập, chọn đầy đủ các thông tin để thực hiện chức năng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Phong phong = TaoPhong();
                if (PHONG.TimPhong(phong.TenPhong).Count() > 0)
                {
                    MessageBox.Show("Tên phòng đã tồn tại. Vui lòng nhập tên phòng khác để thực hiện chức năng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    PHONG.ThemPhong(phong);
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lstvDanhSachPhong, PHONG.LayTatCaPhongDong());
                }
            }
        }
        /** 
        * Tạo mã phòng tự tăng
        */
        private string TaoMaPhong()
        {
            string maPhong = "";
            if(PHONG.LayTatCaPhong().Count() == 0)
            {
                maPhong = "P001";
            }
            else
            {
                string maPhongTam = PHONG.LayTatCaPhong().Last().MaPhong.ToString();
                int dem = Convert.ToInt32(maPhongTam.Split('P')[1]) + 1;
                if (dem < 10)
                {
                    maPhong += "P00" + dem;
                }
                else if (dem >= 10 && dem < 100)
                {
                    maPhong += "P0" + dem;
                }
                else
                {
                    maPhong += "P" + dem;
                }
            }

            return maPhong;
        }
        /** 
        * Tạo phòng (số phòng là tên của phòng)
        */
        dynamic TaoPhong()
        {
            if(txtSoPhong.Text != "" && cboLoaiPhong.SelectedIndex >= 0 && cboTrangThai.Text != "" && KiemTraPhong())
            {
                Phong phong = new Phong();

                phong.MaPhong = TaoMaPhong();
                phong.TenPhong = txtSoPhong.Text;
                phong.TrangThaiPhong = cboTrangThai.Text;
                phong.MaLoaiPhong = LOAIPHONG.TimLoaiPhong(cboLoaiPhong.Text).First().MaLoaiPhong;
                phong.MaQL = MAQL;

                return phong;
            }

            return null;
        }

        /** 
        * Sửa thông tin của phòng (Tên, Loại phòng)
        */
        dynamic SuaTenVaLoaiPhong()
        {
            if(lstvDanhSachPhong.SelectedItems.Count > 0 && (cboLoaiPhong.Text == "VIP" || cboLoaiPhong.Text == "THƯỜNG") && txtSoPhong.Text != "" && KiemTraPhong())
            {
                ListViewItem item = lstvDanhSachPhong.SelectedItems[0];
                Phong phong = new Phong();
                phong.MaPhong = item.Text;
                phong.TenPhong = txtSoPhong.Text;
                phong.TrangThaiPhong = cboTrangThai.Text;
                phong.MaLoaiPhong = LOAIPHONG.TimLoaiPhong(cboLoaiPhong.Text).First().MaLoaiPhong;

                return phong;
            }

            return null;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if(SuaTenVaLoaiPhong() == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng để chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Phong suaPhong = SuaTenVaLoaiPhong();
                if (PHONG.TimPhong(suaPhong.TenPhong).Count() > 0)
                {
                    MessageBox.Show("Tên phòng đã tồn tại. Vui lòng nhập tên phòng khác để thực hiện chức năng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    PHONG.SuaPhong(suaPhong);
                    XoaCacTxtCbo();
                    DANHSACHPHONG = PHONG.LayTatCaPhongDong();
                    TaiDuLieuLenListView(lstvDanhSachPhong, DANHSACHPHONG);
                }
            }
            
        }

        /** 
        * Sửa giá của phòng 
        */
        LoaiPhong SuaGiaLoaiPhong()
        {
            if(cboLoaiPhong2.SelectedIndex >= 0 && txtGiaPhongMoi.Text != "" && txtGiaPhongCu.Text != "")
            {
                LoaiPhong loaiPhong = new LoaiPhong();
                loaiPhong.MaLoaiPhong = LOAIPHONG.TimLoaiPhong(cboLoaiPhong2.Text).First().MaLoaiPhong;
                loaiPhong.Gia = Convert.ToDecimal(txtGiaPhongMoi.Text);

                return loaiPhong;
            }

            return null;
        }
        /** 
        * Cập nhật giá phòng cũ thành giá phòng mới
        */
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if(SuaGiaLoaiPhong() == null) 
            {
                MessageBox.Show("Bạn phải chọn loại phòng và nhập giá mới!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoaiPhong suaLP = SuaGiaLoaiPhong();
                if(suaLP.Gia > 100000)
                {
                    LOAIPHONG.CapNhatGiaLoaiPhong(suaLP);
                    DANHSACHPHONG = PHONG.LayTatCaPhongDong();
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lstvDanhSachPhong, DANHSACHPHONG);
                }
                else
                {
                    MessageBox.Show("Giá phòng mới phải lớn hơn 100.000 VNĐ!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }


        /** 
         * Xóa Phòng
         */
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult hoiXoa;
            Phong phong;

            if (lstvDanhSachPhong.SelectedItems.Count > 0 && (cboLoaiPhong.Text == "VIP" || cboLoaiPhong.Text == "THƯỜNG") && txtSoPhong.Text != "")
            {
                hoiXoa = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if(hoiXoa == DialogResult.Yes)
                {

                    if (PHONG.TimPhong(txtSoPhong.Text).First().TrangThaiPhong == "Mở" || PHONG.TimPhong(txtSoPhong.Text).First().TrangThaiPhong == "Đặt")
                    {
                        MessageBox.Show("Phòng đang được sử dụng không được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                    else if (HOADON.LayDanhSachHoaDonTheoMaPhong(PHONG.TimPhongTheoTen(txtSoPhong.Text).FirstOrDefault().MaPhong).Count() > 0)
                    {
                        MessageBox.Show("Phòng đã được tạo hóa đơn không được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        phong = PHONG.TimPhong(txtSoPhong.Text).First();
                        PHONG.XoaPhong(phong);

                        DANHSACHPHONG = PHONG.LayTatCaPhongDong();
                        XoaCacTxtCbo();
                        TaiDuLieuLenListView(lstvDanhSachPhong, DANHSACHPHONG);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /** 
        * Tải lại danh sách ban đầu.
        */
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            DANHSACHPHONG = PHONG.LayTatCaPhongDong();
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lstvDanhSachPhong, DANHSACHPHONG);
        }


        /** 
        * Tim kiếm
        */
        private void btnTimKiemThongTinPhong_Click(object sender, EventArgs e)
        {
            DANHSACHPHONG = PHONG.TimPhong(txtTimKiemThongTinPhong.Text);
            TaiDuLieuLenListView(lstvDanhSachPhong, DANHSACHPHONG);
        }


        /**
         * Xử lý các hủy focus trong danh sách Phòng
         */
        private void frmQuanLyPhong_Click(object sender, EventArgs e)
        {
            
            XoaCacTxtCbo();
            lstvDanhSachPhong.SelectedItems.Clear();
            txtSoPhong.Enabled = true;
        }

        

        /**
        * Xử lý và kiểm tra tên Phòng (số phòng)
        */
        private void txtSoPhong_Validating(object sender, CancelEventArgs e)
        {
            string txtTenPhong = txtSoPhong.Text;
            if(!clsKiemTra.KiemTraTenPhong(txtTenPhong))
            {
                e.Cancel = true;
                txtSoPhong.Focus();
                errorProvider2.SetError(txtSoPhong, "Nhập đúng định dạng: TXXX hoặc VXXX (X: là số từ 0-9)!");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(txtSoPhong, null);
            }
        }

        /*
         * Kiếm tra tên Phòng đã đúng định dạng hay chưa
         */
        public bool KiemTraPhong()
        {
            string txtTenPhong = txtSoPhong.Text;
            if(!clsKiemTra.KiemTraTenPhong(txtTenPhong))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtGiaPhongMoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
