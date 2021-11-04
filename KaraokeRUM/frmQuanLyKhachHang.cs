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
       * sortColumn dùng để sắp xếp
       */
        private clsKhachHang KH;
        private clsLoaiKhach LK;
        private IEnumerable<dynamic> DSKH;
        private int sortColumn = -1;
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            
            TaiCombobox();
            TaoTieuDeCot(lstvDSKH);
            TaoTieuDeCotDanhSachDen(lstvDanhSachDen);
            KH = new clsKhachHang();
            LK = new clsLoaiKhach();
            IEnumerable<dynamic> dsKH = KH.KhachHangVaLoaiKhachHang();
            IEnumerable<dynamic> dsKHD = KH.KhachHangVaLoaiKhachHangDanhSachDen();
            TaiDuLieuLenListView(lstvDSKH, dsKH);
            TaiDuLieuLenListView(lstvDanhSachDen, dsKHD);
            txtTimKiemKhachHang.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemKhachHang.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
            lsw.Columns.Add("GhiChú", 150);
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
            //cboLocTheoLoai.Text = DSKH.TenLoaiKH;
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtCKM.Text.Trim()))
            {
                LoaiKhachHang suaLk = SuaChietKhauLoaiKhach();
                LK.CapNhatChietKhau(suaLk);
                IEnumerable<dynamic> layDS = KH.KhachHangVaLoaiKhachHang();
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvDSKH, layDS);
            }    
            else
            {
                MessageBox.Show("Lỗi! Không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
           
        }
        LoaiKhachHang SuaChietKhauLoaiKhach()
        {
            LoaiKhachHang loaiKhachHang = new LoaiKhachHang();
            loaiKhachHang.MaLoaiKH = LK.TimLoaiKhachHang(cboLoaiKhachHang.Text).First().MaLoaiKH;
            loaiKhachHang.ChietKhau = Convert.ToInt32(txtCKM.Text);

            return loaiKhachHang;
        }
        /** 
       * Xóa trắng các ô textbox, combobox.
       */
        void XoaCacTxtCbo()
        {
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
            string selected = this.cboLocTheoLoai.GetItemText(this.cboLocTheoLoai.SelectedItem);
            if (selected.Equals("All") == true)
            {
               // no rename may cái instance ak, k phai rename theo kieu notepad dau, k an loz dau               lvwDSKH.Items.Clear();
                TaoTieuDeCot(lstvDSKH);
                IEnumerable<dynamic> dsKHAll = KH.KhachHangVaLoaiKhachHang();
                TaiDuLieuLenListView(lstvDSKH, dsKHAll);
            }
            else
            {
                
                IEnumerable<dynamic> dsKH = KH.LayKhachHangVaLoaiKhachHangTheoLoai(selected);
                lstvDSKH.Items.Clear();
                TaoTieuDeCot(lstvDSKH);
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
            TaoTieuDeCot(lstvDSKH);
            TaiDuLieuLenListView(lstvDSKH, DSKH);
        }
        /*
         * auto complete tự động tải danh sách
         */
        private void txtTimKiemKhachHang_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (KhachHang i in KH.LayDSKH())
            {
                collection.Add(i.MaKH);
                collection.Add(i.TenKhach);
            }
            txtTimKiemKhachHang.AutoCompleteCustomSource = collection;

        }

       
    }




    
}




