﻿using System;
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
         * NV: class Nhân Viên.
         * LNV: class Loại Nhân Viên.
         * hl: class Hỗn Loạn.
         * dsNV danh sách nhân viên lấy từ 2 bảng
         * SORTCOLUMN dùng để sắp xếp
         * TextWasChanged dùng để kiểm tra xem text box có đổi hay không
         */
        private clsNhanVien NV;
        private clsLoaiNhanVien LNV;
        private clsTaiKhoan TK;
        private int SORTCOLUMN = -1;
        private string MANVQL;
        public frmQuanLyNhanVien(string maNVQL)
        {
            InitializeComponent();
            MANVQL = maNVQL;
        }


        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadCombobox();
            TaoTieuDeCot(lstvDSNV);
            TaiDuLieu();
        }
        /*Khởi tạo ban đầu - tải dữ liệu lên listview, vô hiệu hoá các nút chức năng*/
        void TaiDuLieu()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            NV = new clsNhanVien();
            LNV = new clsLoaiNhanVien();
            TK = new clsTaiKhoan();
            IEnumerable<dynamic> dsNV = NV.LayNhanVienVaLoaiNhanVienTheoLoai("thu ngân", MANVQL);
            TaiDuLieuLenListView(lstvDSNV, dsNV);
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
            lsw.Columns.Add("Chức vụ", 130);
            lsw.Columns.Add("Trạng thái", 150);
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
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemNV.MaNV);
            lstvItem.SubItems.Add(itemNV.TenNV);
            lstvItem.SubItems.Add(itemNV.GioiTinh);
            lstvItem.SubItems.Add(itemNV.CMND);
            lstvItem.SubItems.Add(itemNV.SDT);
            lstvItem.SubItems.Add(itemNV.DiaChi);
            lstvItem.SubItems.Add(itemNV.TenLNV);
            lstvItem.SubItems.Add(itemNV.TrangThai);
            lstvItem.Tag = itemNV;
            return lstvItem;
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
            cboLocTheoLoai.Items.Add("Đã nghỉ");
            cboLocTheoLoai.Items.Add("Tất cả");
        }
        private void lstvDSNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dsNV = null;
            if (lstvDSNV.SelectedItems.Count > 0)
            {
                dsNV = (dynamic)lstvDSNV.SelectedItems[0].Tag;
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
            if (selected.Equals("Tất cả") == true)
            {
                lstvDSNV.Clear();
                TaoTieuDeCot(lstvDSNV);
                IEnumerable<dynamic> dsNVALL = NV.LayToanBoNhanVienVaLoaiNhanVien(MANVQL);
                TaiDuLieuLenListView(lstvDSNV, dsNVALL);
            }
            else
            {
                if (selected.Equals("Đã nghỉ") == true)
                {

                    lstvDSNV.Clear();
                    TaoTieuDeCot(lstvDSNV);
                    IEnumerable<dynamic> dsNVDaNghi = NV.LayNhanVienVaLoaiNhanVienDaNghi(MANVQL);
                    TaiDuLieuLenListView(lstvDSNV, dsNVDaNghi);
                }
                else
                {

                    IEnumerable<dynamic> dsKH = NV.LayNhanVienVaLoaiNhanVienTheoLoai(selected, MANVQL);
                    lstvDSNV.Clear();
                    TaoTieuDeCot(lstvDSNV);
                    TaiDuLieuLenListView(lstvDSNV, dsKH);
                }
            }
            
        }
        /*
       * sự kiện click vào cột để sắp xếp
       */
        private void lstvDSNV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != SORTCOLUMN)
            {
                SORTCOLUMN = e.Column;
                lstvDSNV.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lstvDSNV.Sorting == SortOrder.Ascending)
                    lstvDSNV.Sorting = SortOrder.Descending;
                else
                    lstvDSNV.Sorting = SortOrder.Ascending;
            }
            lstvDSNV.Sort();
            this.lstvDSNV.ListViewItemSorter = new clsListViewItemComparer(e.Column, lstvDSNV.Sorting);
        }
        /*
         * Tim kiem
         */
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            IEnumerable<dynamic> dsNVTim;
            dsNVTim = NV.TimNhanVienVaLoaiNhanVien(txtTimKiemNhanVien.Text, MANVQL);
            lstvDSNV.Clear();
            txtTimKiemNhanVien.Clear();
            TaoTieuDeCot(lstvDSNV);
            TaiDuLieuLenListView(lstvDSNV, dsNVTim);
        }
        /*
        * auto complete 
        */
        private void txtTimKiemNhanVien_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (NhanVien i in NV.LayDSNV(MANVQL))
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
            if (NV.TimNhanVien(nhanVien.CMND, nhanVien.SDT).Count() > 0 )
            {
                MessageBox.Show("Nhân viên này đã tồn tại. Vui lòng không nhập trùng !! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(clsKiemTra.KiemTraCMNDHopLE(txtCMND.Text))
                {
                    NV.ThemNhanVien(nhanVien);
                    if (nhanVien.MaLNV.ToLower().Equals("lnv01") || nhanVien.MaLNV.ToLower().Equals("lnv02"))
                    {
                        TaiKhoan taiKhoan = TaoTaiKhoan(nhanVien);
                        TK.ThemTaiKhoan(taiKhoan);
                    }
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lstvDSNV, NV.LayNhanVienVaLoaiNhanVien(MANVQL));
                    errorProvider1.SetError(txtSDT, null);
                    errorProvider1.SetError(txtCMND, null);
                } 
                else
                {
                    MessageBox.Show("Chứng minh nhân dân bạn nhập không hợp lệ!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }    
                

            }
            
        }
   
        /** 
        * Tạo mã nhân viên tự tăng
        */
        private string TaoMaNhanVien()
        {

            string maNhanVien = "";
            string maNhanVienTam = NV.LayDSNVFULL(MANVQL).Last().MaNV.ToString();
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
            nhanVien.MaLNV = LNV.LayLoaiNhanVien(cboLoaiNV.Text).First().MaLNV;
            nhanVien.CMND = txtCMND.Text;
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
                    NV.SuaNhanVien(suaNhanVien);
                    IEnumerable<dynamic> dsNV = NV.LayNhanVienVaLoaiNhanVien(MANVQL);
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lstvDSNV, dsNV);
                    errorProvider1.SetError(txtSDT, null);
        }

        /** 
        * Sửa chức vụ, địa chỉ, giới tính của nhân viên
        */
        NhanVien SuaThongTinNhanVien()
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.TenNV = txtTen.Text;
            nhanVien.MaNV = NV.TimNhanVien(txtCMND.Text, txtSDT.Text).First().MaNV;
            nhanVien.MaLNV = LNV.LayLoaiNhanVien(cboLoaiNV.Text).First().MaLNV;
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
            nhanVien.MaNV = NV.TimNhanVien(txtCMND.Text, txtSDT.Text).First().MaNV;
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
                NV.XoaNhanVien(xoaNhanVien);
                IEnumerable<dynamic> dsNV = NV.LayNhanVienVaLoaiNhanVien(MANVQL);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvDSNV, dsNV);
            }
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            IEnumerable<dynamic> dsNV = NV.LayNhanVienVaLoaiNhanVien(MANVQL);
            XoaCacTxtCbo();
            lstvDSNV.Clear();
            TaoTieuDeCot(lstvDSNV);
            TaiDuLieuLenListView(lstvDSNV, dsNV);
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

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text;
            if (!clsKiemTra.KiemTraSDT(sdt))
            {
                txtSDT.Focus();
                errorProvider1.SetError(txtSDT, "Số điện thoại phải bắt đầu từ đầu số hợp lệ. VD: 09XXXXXXXX");
                btnThem.Enabled = false;
                btnSua.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(txtSDT, null);
            }
        }

        private void txtCMND_TextChanged(object sender, EventArgs e)
        {
            string cmnd = txtCMND.Text;
            if (!clsKiemTra.KiemTraDoDaiCMND(cmnd))
            {
                txtCMND.Focus();
                errorProvider1.SetError(txtCMND, "Chứng nhân nhân dân phải đủ 9 số");
                btnThem.Enabled = false;
                btnSua.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(txtCMND, null);
            }
        }
        /*Kiểm tra xem các thành phần trong text box có rỗng k*/
        private void KiemTraTxt()
        {
            foreach (Control c in grbThongTinNhanVien.Controls)
            {
                if (c is TextBox)
                {
                    var a = c as TextBox;
                    if (a.Text == "")
                    {
                        btnThem.Enabled = false;
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        return;
                    }    
                   
                }
            }
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void txtTen_TextChanged(object sender, EventArgs e)
        {
            KiemTraTxt();
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
            KiemTraTxt();
        }

        

       
    }
  
} 






