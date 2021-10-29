using System;
using System.Collections;
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
    public partial class frmQuanLyNhanVien : Form
    {
        /** 
         * Các biến toàn cục.
         * nV: class Nhân Viên.
         * lNV: class Loại Nhân Viên.
         * hl: class Hỗn Loạn.
         * dsNV danh sách nhân viên lấy từ 2 bảng
         * sortColumn dùng để sắp xếp
         * TextWasChanged dùng để kiểm tra xem text box có đổi hay không
         */
        private clsNhanVien nV;
        private clsLoaiNhanVien lNV;
        private clsHonLoan hL;
        private clsTaiKhoan tK;
        private IEnumerable<dynamic> dsNV;
        private int sortColumn = -1;
        private string MANVQL;
        public frmQuanLyNhanVien(string maNVQL)
        {
            InitializeComponent();
            MANVQL = maNVQL;
        }


        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadCombobox();
            TaoTieuDeCot(lvwDSNV);
            nV = new clsNhanVien();
            lNV = new clsLoaiNhanVien();
            hL = new clsHonLoan();
            tK = new clsTaiKhoan();
            IEnumerable<dynamic> dsNV = nV.LayNhanVienVaLoaiNhanVien(MANVQL);
            TaiDuLieuLenListView(lvwDSNV, dsNV);
            txtTimKiemNhanVien.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemNhanVien.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        /** 
         * Tạo tiêu đề cột
         */
        void TaoTieuDeCot(ListView lsw)
        {
            lsw.Columns.Add("Mã NV", 100);
            lsw.Columns.Add("Tên Nhân viên", 170);
            lsw.Columns.Add("Giới tính", 100);
            lsw.Columns.Add("CMND", 130);
            lsw.Columns.Add("SDT", 130);
            lsw.Columns.Add("Địa chỉ", 400);
            lsw.Columns.Add("Trạng thái", 150);
            lsw.Columns.Add("Chức vụ", 130);
            lsw.View = View.Details;
            lsw.GridLines = true;
            lsw.FullRowSelect = true;
        }

        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lsw, IEnumerable<dynamic> dsNV)
        {
            lsw.Items.Clear();
            ListViewItem itemNhanVien;
            foreach (dynamic ds in dsNV)
            {
                itemNhanVien = TaoItem(ds);
                lsw.Items.Add(itemNhanVien);
            }

        }
        ListViewItem TaoItem(dynamic itemNV)
        {
            ListViewItem lswItem;
            lswItem = new ListViewItem(itemNV.MaNV);
            lswItem.SubItems.Add(itemNV.TenNV);
            lswItem.SubItems.Add(itemNV.GioiTinh);
            lswItem.SubItems.Add(itemNV.CMND);
            lswItem.SubItems.Add(itemNV.SDT);
            lswItem.SubItems.Add(itemNV.DiaChi);
            lswItem.SubItems.Add(itemNV.TrangThai);
            lswItem.SubItems.Add(itemNV.TenLNV);
            lswItem.Tag = itemNV;
            return lswItem;
        }
        /*
         * load combobox 
         */
        void loadCombobox()
        {
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");

            cboLoaiNV.Items.Add("Quản lý");
            cboLoaiNV.Items.Add("Thu ngân");
            cboLoaiNV.Items.Add("Lễ tân");
            cboLoaiNV.Items.Add("Phục vụ");
            cboLoaiNV.Items.Add("Bảo vệ");

            cboLocTheoLoai.Items.Add("Quản lý");
            cboLocTheoLoai.Items.Add("Thu ngân");
            cboLocTheoLoai.Items.Add("Lễ tân");
            cboLocTheoLoai.Items.Add("Phục vụ");
            cboLocTheoLoai.Items.Add("Bảo vệ");
            cboLocTheoLoai.Items.Add("All");
        }
        private void lvwDSNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dsNV = null;
            if (lvwDSNV.SelectedItems.Count > 0)
            {
                dsNV = (dynamic)lvwDSNV.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsNV);
            }
        }
        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsNV)
        {
            txtTen.Text = dsNV.TenNV;
            cboGioiTinh.Text = dsNV.GioiTinh;
            txtCMND.Text = dsNV.CMND;
            txtSDT.Text = dsNV.SDT;
            txtDiaChi.Text = dsNV.DiaChi;
            cboTrangThai.Text = dsNV.TrangThai;
            cboLoaiNV.Text = dsNV.TenLNV;
        }

        private void cboLocTheoLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cboLocTheoLoai.GetItemText(this.cboLocTheoLoai.SelectedItem);
            if (selected.Equals("All") == true)
            {
                hL = new clsHonLoan();
                lvwDSNV.Items.Clear();
                TaoTieuDeCot(lvwDSNV);
                IEnumerable<dynamic> dsNVALL = nV.LayNhanVienVaLoaiNhanVien(MANVQL);
                TaiDuLieuLenListView(lvwDSNV, dsNVALL);
            }
            else
            {
                hL = new clsHonLoan();
                IEnumerable<dynamic> dsKH = nV.LayNhanVienVaLoaiNhanVienTheoLoai(selected, MANVQL);
                lvwDSNV.Items.Clear();
                TaoTieuDeCot(lvwDSNV);
                TaiDuLieuLenListView(lvwDSNV, dsKH);
            }
        }
        /*
       * sự kiện click vào cột để sắp xếp
       */
        private void lvwDSNV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                lvwDSNV.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvwDSNV.Sorting == SortOrder.Ascending)
                    lvwDSNV.Sorting = SortOrder.Descending;
                else
                    lvwDSNV.Sorting = SortOrder.Ascending;
            }
            lvwDSNV.Sort();
            this.lvwDSNV.ListViewItemSorter = new clsListViewItemComparer(e.Column,lvwDSNV.Sorting);
        }
        /*
         * Tim kiem
         */
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            IEnumerable<dynamic> dsNVTim;
            dsNVTim = nV.TimNhanVienVaLoaiNhanVien(txtTimKiemNhanVien.Text, MANVQL);
            lvwDSNV.Items.Clear();
            txtTimKiemNhanVien.Clear();
            TaoTieuDeCot(lvwDSNV);
            TaiDuLieuLenListView(lvwDSNV, dsNVTim);
        }
        /*
        * auto complete 
        */
        private void txtTimKiemNhanVien_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (NhanVien i in nV.LayDSNV(MANVQL))
            {
                collection.Add(i.MaNV);
                collection.Add(i.TenNV);
            }
            txtTimKiemNhanVien.AutoCompleteCustomSource = collection;
        }
        void XoaCacTxtCbo()
        {
            txtTen.Text = "";
            cboGioiTinh.Text = "";
            txtCMND.Text = "";
            cboTrangThai.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            cboLoaiNV.Text = "";
            txtTimKiemNhanVien.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = ThemNhanVien();
            if (nV.TimNhanVien(nhanVien.CMND, nhanVien.SDT).Count() > 0 )
            {
                MessageBox.Show("Lỗi! đã tồn tại nhân viên này rồi", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                nV.ThemNhanVien(nhanVien);
                if (nhanVien.MaLNV.ToLower().Equals("lnv01") || nhanVien.MaLNV.ToLower().Equals("lnv02"))
                {
                    TaiKhoan taiKhoan = TaoTaiKhoan(nhanVien);
                    tK.ThemTaiKhoan(taiKhoan);
                }
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lvwDSNV, nV.LayNhanVienVaLoaiNhanVien(MANVQL));
            }
            
        }
   
        /** 
        * Tạo mã nhân viên tự tăng
        */
        private string TaoMaNhanVien()
        {
            string maNhanVien = "";
            string maNhanVienTam = nV.LayDSNVFULL(MANVQL).Last().MaNV.ToString();
            int dem = Convert.ToInt32(maNhanVienTam.Split('N','V')[2]) + 1;
            if (dem < 10)
            {
                maNhanVien += "NV00" + dem;
            }
            else if (dem >= 10 && dem < 100)
            {
                maNhanVien += "NV0" + dem;
            }
            else
            {
                maNhanVien += "NV" + dem;
            }

            return maNhanVien;
        }
        /** 
        * Tạo mã nhân viên
        */
        dynamic ThemNhanVien()
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.MaNV = TaoMaNhanVien();
            nhanVien.TenNV = txtTen.Text;
            nhanVien.GioiTinh = cboGioiTinh.Text;
            nhanVien.MaLNV = lNV.LayLoaiNhanVien(cboLoaiNV.Text).First().MaLNV;
            nhanVien.CMND = txtSDT.Text;
            nhanVien.DiaChi = txtDiaChi.Text;
            nhanVien.TrangThai = cboTrangThai.Text;
            nhanVien.SDT = txtSDT.Text;
            return nhanVien;
        }
        TaiKhoan TaoTaiKhoan(NhanVien nhanVien)
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.UserName = nhanVien.MaNV;
            taiKhoan.PassWord = "123456";
            return taiKhoan;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            NhanVien suaNhanVien = SuaThongTinNhanVien();
                    nV.SuaNhanVien(suaNhanVien);
                    IEnumerable<dynamic> dsNV = nV.LayNhanVienVaLoaiNhanVien(MANVQL);
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lvwDSNV, dsNV);
          


        }

        /** 
        * Sửa chức vụ, địa chỉ, giới tính của nhân viên
        */
        NhanVien SuaThongTinNhanVien()
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.MaNV = nV.TimNhanVien(txtCMND.Text, txtSDT.Text).First().MaNV;
            nhanVien.MaLNV = lNV.LayLoaiNhanVien(cboLoaiNV.Text).First().MaLNV;
            nhanVien.GioiTinh = cboGioiTinh.Text;
            nhanVien.DiaChi = txtDiaChi.Text;

            return nhanVien;
        }
        /** 
        * Xoá nhân viên (Thay đổi trạng thái nhân viên)
        */
        NhanVien XoaNhanVien()
        {
            
            NhanVien nhanVien = new NhanVien();
            nhanVien.MaNV = nV.TimNhanVien(txtCMND.Text, txtSDT.Text).First().MaNV;
            nhanVien.TrangThai = "Đã nghỉ";
            return nhanVien;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult hoiXoa;
            hoiXoa = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (hoiXoa == DialogResult.Yes)
            {
                NhanVien xoaNhanVien = XoaNhanVien();
                nV.XoaNhanVien(xoaNhanVien);
                IEnumerable<dynamic> dsNV = nV.LayNhanVienVaLoaiNhanVien(MANVQL);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lvwDSNV, dsNV);
            }
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            IEnumerable<dynamic> dsNV = nV.LayNhanVienVaLoaiNhanVien(MANVQL);
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lvwDSNV, dsNV);
        }
        /*
         * chỉ nhập số
         */
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
    }
  
} 






