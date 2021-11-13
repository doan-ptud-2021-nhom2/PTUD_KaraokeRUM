using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KaraokeRUM
{
    public partial class frmQuanLyKhachHang : Form
    {
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
        }
        /** 
       * Các biến toàn cục.
       * KH: class khách hàng.
       * LK: class Loại khách.
       * dsKh danh sách khách hàng lấy từ 2 bảng
       * SORTCOLUMN dùng để sắp xếp
       */
        private clsKhachHang KH;
        private clsLoaiKhach LK;
        private IEnumerable<dynamic> DSKH;
        private int sortColumn = -1;
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            TaiCombobox();
            TaiDuLieu();
            txtTimKiemKhachHang.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemKhachHang.AutoCompleteSource = AutoCompleteSource.CustomSource;

            TaiDuLieuVaoBoxTimKiem();
        }

        /**
         * Hàm hỗ trợ tải tên và mã khách hàng vào txtTimKiem để thực hiện chức năng tìm kiếm.
         */
        private void TaiDuLieuVaoBoxTimKiem()
        {
            txtTimKiemKhachHang.AutoCompleteCustomSource.Clear();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (KhachHang i in KH.LayDSKH())
            {
                collection.Add(i.MaKH);
                collection.Add(i.TenKhach);
            }
            txtTimKiemKhachHang.AutoCompleteCustomSource = collection;
        }
        /*Làm mới lại toàn bộ list view và tải dữ liệu mới lên list view*/
        void TaiDuLieu()
        {
            lstvDSKH.Clear();
            lstvDanhSachDen.Clear();
            TaoTieuDeCot(lstvDSKH);
            TaoTieuDeCotDanhSachDen(lstvDanhSachDen);
            KH = new clsKhachHang();
            LK = new clsLoaiKhach();
            IEnumerable<dynamic> dsKH = KH.LayKhachHangVaLoaiKhachHangTheoLoai("LKH01");
            IEnumerable<dynamic> dsKHD = KH.KhachHangVaLoaiKhachHangDanhSachDen();
            TaiDuLieuLenListView(lstvDSKH, dsKH);
            TaiDuLieuLenListViewDanhSachDen(lstvDanhSachDen, dsKHD);
        }
        /* Tải thông tin vào combobox */
        void TaiCombobox()
        {
            cboLoaiKhachHang.Items.Add("Vip");
            cboLoaiKhachHang.Items.Add("Thường xuyên");
            cboLoaiKhachHang.Items.Add("Thường");

            cboLocTheoLoai.Items.Add("Vip");
            cboLocTheoLoai.Items.Add("Thường xuyên");
            cboLocTheoLoai.Items.Add("Thường");
            cboLocTheoLoai.Items.Add("Tất cả");

            cboGhiChu.Items.Add("Bình thường");
            cboGhiChu.Items.Add("Cảnh báo");
            cboGhiChu.Items.Add("Cấm");
        }
        /** 
         * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lsw)
        {
            lsw.Columns.Add("Mã khách", 100);
            lsw.Columns.Add("Tên khách hàng", 220);
            lsw.Columns.Add("SDT", 130);
            lsw.Columns.Add("Số lần đến", 110);
            lsw.Columns.Add("Tên loại khách", 150);
            lsw.Columns.Add("Chiết khấu", 120);
            lsw.View = View.Details;
            lsw.GridLines = true;
            lsw.FullRowSelect = true;
        }
        void TaoTieuDeCotDanhSachDen(ListView lsw)
        {
            lsw.Columns.Add("Mã khách", 100);
            lsw.Columns.Add("Tên khách hàng", 220);
            lsw.Columns.Add("SDT", 130);
            lsw.Columns.Add("Số lần đến", 110);
            lsw.Columns.Add("Ghi chú", 150);
            lsw.View = View.Details;
            lsw.GridLines = true;
            lsw.FullRowSelect = true;
        }
        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> dsKhach)
        {
            lstv.Items.Clear();
            ListViewItem itemKhach;
            foreach (dynamic ds in dsKhach)
            {
                itemKhach = TaoItem(ds);
                lstv.Items.Add(itemKhach);
            }

        }
        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListViewDanhSachDen(ListView lstv, IEnumerable<dynamic> dsKhach)
        {
            lstv.Items.Clear();
            ListViewItem itemKhach;
            foreach (dynamic ds in dsKhach)
            {
                itemKhach = TaoItemDanhSachDen(ds);
                lstv.Items.Add(itemKhach);
            }

        }
        ListViewItem TaoItem(dynamic itemKH)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemKH.MaKH);
            lstvItem.SubItems.Add(itemKH.TenKhach);
            lstvItem.SubItems.Add(itemKH.SDT);
            lstvItem.SubItems.Add(itemKH.SoLanDen.ToString());
            lstvItem.SubItems.Add(itemKH.TenLoaiKH);
            lstvItem.SubItems.Add(itemKH.ChietKhau.ToString());
            lstvItem.Tag = itemKH;
            return lstvItem;
        }
        ListViewItem TaoItemDanhSachDen(dynamic itemKH)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemKH.MaKH);
            lstvItem.SubItems.Add(itemKH.TenKhach);
            lstvItem.SubItems.Add(itemKH.SDT);
            lstvItem.SubItems.Add(itemKH.SoLanDen.ToString());
            lstvItem.SubItems.Add(itemKH.GhiChu);
            lstvItem.Tag = itemKH;
            return lstvItem;
        }
        private void lstvDSKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dsKH = null;
            if (lstvDSKH.SelectedItems.Count > 0)
            {
                dsKH = (dynamic)lstvDSKH.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsKH);

            }
        }
        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsKH)
        {
            cboLoaiKhachHang.Text = dsKH.TenLoaiKH;
            txtCKC.Text = dsKH.ChietKhau.ToString();
            txtMaKhachHang.Text = dsKH.MaKH;
            txtTenKhachHang.Text = dsKH.TenKhach;
            txtSDT.Text = dsKH.SDT;
            //cboLocTheoLoai.Text = DSKH.TenLoaiKH;
        }
        void TaiDuLieuTuLstvDSKHDenLenTxtCbo(dynamic dsKH)
        {
            
            txtMaKhachHang.Text = dsKH.MaKH;
            txtTenKhachHang.Text = dsKH.TenKhach;
            txtSDT.Text = dsKH.SDT;
            cboGhiChu.Text = dsKH.GhiChu;
            //cboLocTheoLoai.Text = DSKH.TenLoaiKH;
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtCKM.Text.Trim()) && !string.IsNullOrEmpty(cboLoaiKhachHang.Text))
            {
                
                LoaiKhachHang suaLk = SuaChietKhauLoaiKhach();
                LK.CapNhatChietKhau(suaLk);
                TaiDuLieu();
                txtCKC.Text = "";
                txtCKM.Text = "";
            }    
            else
            {
                MessageBox.Show("Lỗi! Không được để trống chiết khấu mới !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
           
        }
        /*Tìm kiếm loại khách hàng sau đó sửa chiết khấu theo mã loại*/
        LoaiKhachHang SuaChietKhauLoaiKhach()
        {
            LoaiKhachHang loaiKhachHang = new LoaiKhachHang();
            string maLoaiKhachHang;
            if (cboLoaiKhachHang.SelectedIndex == 0)
            {
                maLoaiKhachHang = "LKH01";
            }
            else if (cboLoaiKhachHang.SelectedIndex == 1)
            {
                maLoaiKhachHang = "LKH02";
            }
            else if (cboLoaiKhachHang.SelectedIndex == 2)
            {
                maLoaiKhachHang = "LKH03";
            }
            else
            {
                maLoaiKhachHang = null;
            }
            loaiKhachHang.MaLoaiKH = maLoaiKhachHang;
            loaiKhachHang.ChietKhau = Convert.ToInt32(txtCKM.Text);

            return loaiKhachHang;
        }
        /*Tìm kiếm loại khách hàng sau đó sửa chiết khấu theo mã loại*/
        KhachHang SuaGhiChuKhach()
        {
            KhachHang khachHang = new KhachHang();
            khachHang.MaKH = KH.TimKhachHangTheoMa(txtMaKhachHang.Text).First().MaKH;
            if(cboGhiChu.SelectedIndex==0)
            {
                khachHang.GhiChu = null;
            }    
            else
            {
                khachHang.GhiChu = cboGhiChu.Text;
            }    

           

            return khachHang;
        }
        /** 
       * Xóa trắng các ô textbox, combobox.
       */
        void XoaCacTxtCbo()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtSDT.Text = "";
            cboGhiChu.Text = "";
            cboLoaiKhachHang.Text = "";
            txtCKC.Text = "";
            cboLocTheoLoai.Text = "";
            txtCKM.Text = "";
        }
        /*
        * sự kiện 
        */
        private void cboLocTheoLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maLoaiKhachHang;
            if (cboLocTheoLoai.SelectedIndex==0)
            {
                maLoaiKhachHang = "LKH01";
            }    
            else if (cboLocTheoLoai.SelectedIndex == 1 )
            {
                maLoaiKhachHang = "LKH02";
            }    
            else if (cboLocTheoLoai.SelectedIndex==2)
            {
                maLoaiKhachHang = "LKH03";
            }
            else
            {
                maLoaiKhachHang = null;
            }
            if (maLoaiKhachHang == null)
            {
                IEnumerable<dynamic> dsKH = KH.KhachHangVaLoaiKhachHang();
                lstvDSKH.Items.Clear();
                TaiDuLieuLenListView(lstvDSKH, dsKH);
            }
            else
            {
                IEnumerable<dynamic> dsKH = KH.LayKhachHangVaLoaiKhachHangTheoLoai(maLoaiKhachHang);
                lstvDSKH.Items.Clear();
                TaiDuLieuLenListView(lstvDSKH, dsKH);
            }


        }
        /*
        * sự kiện click vào cột để sắp xếp
        */
         private void lstvDSKH_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                lstvDSKH.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lstvDSKH.Sorting == SortOrder.Ascending)
                    lstvDSKH.Sorting = SortOrder.Descending;
                else
                    lstvDSKH.Sorting = SortOrder.Ascending;
            }
            lstvDSKH.Sort();
            this.lstvDSKH.ListViewItemSorter = new clsListViewItemComparer(e.Column,
                                                              lstvDSKH.Sorting);
        }
        /*
         * Tim kiem
         */
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DSKH = KH.TimKhach(txtTimKiemKhachHang.Text);

            lstvDSKH.Items.Clear();
            lstvDanhSachDen.Items.Clear();
            if (DSKH.First().GhiChu == null)
            {
              
                TaiDuLieuLenListView(lstvDSKH, DSKH);
            }    
            else
            {
               
                TaiDuLieuLenListViewDanhSachDen(lstvDanhSachDen, DSKH);
            }    
            
            
        }
        private void lstvDanhSachDen_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dsKH = null;
            if (lstvDanhSachDen.SelectedItems.Count > 0)
            {
                dsKH = (dynamic)lstvDanhSachDen.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDSKHDenLenTxtCbo(dsKH);

            }
        }

        private void btnCapNhapGhiChu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboGhiChu.Text.Trim()))
            {
                KhachHang suaKH = SuaGhiChuKhach();
                KH.CapNhatGhiChu(suaKH);
                TaiDuLieu();
            }
            else
            {
                MessageBox.Show("Lỗi! Bạn chưa chọn ghi chú khách hàng !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cboLoaiKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maLoaiKhachHang;
            if (cboLoaiKhachHang.SelectedIndex == 0)
            {
                maLoaiKhachHang = "LKH01";
            }
            else if (cboLoaiKhachHang.SelectedIndex == 1)
            {
                maLoaiKhachHang = "LKH02";
            }
            else if (cboLoaiKhachHang.SelectedIndex == 2)
            {
                maLoaiKhachHang = "LKH03";
            }
            else
            {
                maLoaiKhachHang = "LKH01";
            }
           
            
                IEnumerable<LoaiKhachHang> kh = LK.TimLoaiKhachHangTheoMaLoai(maLoaiKhachHang);
            txtCKC.Text = kh.First().ChietKhau.ToString();


        }
    }




    
}




