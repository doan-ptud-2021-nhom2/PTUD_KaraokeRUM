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
       * kH: class khách hàng.
       * lK: class Loại khách.
       * hl: class Hỗn Loạn.
       * dsKh danh sách khách hàng lấy từ 2 bảng
       * sortColumn dùng để sắp xếp
       */
        private clsKhachHang kH;
        private clsLoaiKhach lK;
        private clsHonLoan hL;
        private IEnumerable<dynamic> dsKH;
        private int sortColumn = -1;
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            cboLoaiKhachHang.Items.Add("VIP");
            cboLoaiKhachHang.Items.Add("TX");
            cboLoaiKhachHang.Items.Add("THUONG");

            cboLocTheoLoai.Items.Add("VIP");
            cboLocTheoLoai.Items.Add("TX");
            cboLocTheoLoai.Items.Add("THUONG");
            cboLocTheoLoai.Items.Add("All");

            TaoTieuDeCot(lvwDSKH);
            kH = new clsKhachHang();
            lK = new clsLoaiKhach();
            hL = new clsHonLoan();
            IEnumerable<dynamic> dsKH = kH.KhachHangVaLoaiKhachHang();
            TaiDuLieuLenListView(lvwDSKH, dsKH);

            txtTimKiemKhachHang.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemKhachHang.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }
        /** 
         * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lsw)
        {
            lsw.Columns.Add("Mã khách", 100);
            lsw.Columns.Add("Tên khách hàng", 150);
            lsw.Columns.Add("SDT", 130);
            lsw.Columns.Add("Số lần đến", 110);
            lsw.Columns.Add("Tên loại khách", 150);
            lsw.Columns.Add("Chiết khấu", 120);


            lsw.View = View.Details;
            lsw.GridLines = true;
            lsw.FullRowSelect = true;
        }

        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lsw, IEnumerable<dynamic> dsKhach)
        {
            lsw.Items.Clear();
            ListViewItem itemKhach;
            foreach (dynamic ds in dsKhach)
            {
                itemKhach = TaoItem(ds);
                lsw.Items.Add(itemKhach);
            }

        }
        ListViewItem TaoItem(dynamic itemKH)
        {
            ListViewItem lswItem;
            lswItem = new ListViewItem(itemKH.MaKH);
            lswItem.SubItems.Add(itemKH.TenKhach);
            lswItem.SubItems.Add(itemKH.SDT);
            lswItem.SubItems.Add(itemKH.SoLanDen.ToString());
            lswItem.SubItems.Add(itemKH.TenLoaiKH);
            lswItem.SubItems.Add(itemKH.ChietKhau.ToString());
            lswItem.Tag = itemKH;
            return lswItem;
        }
        private void lvwDSKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic dsKH = null;
            if (lvwDSKH.SelectedItems.Count > 0)
            {
                dsKH = (dynamic)lvwDSKH.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsKH);

            }
        }
        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsKH)
        {
            cboLoaiKhachHang.Text = dsKH.TenLoaiKH;
            txtCKC.Text = dsKH.ChietKhau.ToString();
            //cboLocTheoLoai.Text = dsKH.TenLoaiKH;
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            LoaiKhachHang suaLk = SuaChietKhauLoaiKhach();
            lK.CapNhatChietKhau(suaLk);
            IEnumerable<dynamic> layDS = kH.KhachHangVaLoaiKhachHang();
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lvwDSKH, layDS);
        }
        LoaiKhachHang SuaChietKhauLoaiKhach()
        {
            LoaiKhachHang loaiKhachHang = new LoaiKhachHang();
            loaiKhachHang.MaLoaiKH = lK.TimLoaiKhachHang(cboLoaiKhachHang.Text).First().MaLoaiKH;
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
                hL = new clsHonLoan();
                lvwDSKH.Items.Clear();
                TaoTieuDeCot(lvwDSKH);
                IEnumerable<dynamic> dsKHAll = kH.KhachHangVaLoaiKhachHang();
                TaiDuLieuLenListView(lvwDSKH, dsKHAll);
            }
            else
            {
                hL = new clsHonLoan();
                IEnumerable<dynamic> dsKH = kH.LayKhachHangVaLoaiKhachHangTheoLoai(selected);
                lvwDSKH.Items.Clear();
                TaoTieuDeCot(lvwDSKH);
                TaiDuLieuLenListView(lvwDSKH, dsKH);
            }

        }
        /*
        * sự kiện click vào cột để sắp xếp
        */
        private void lvwDSKH_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                lvwDSKH.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvwDSKH.Sorting == SortOrder.Ascending)
                    lvwDSKH.Sorting = SortOrder.Descending;
                else
                    lvwDSKH.Sorting = SortOrder.Ascending;
            }
            lvwDSKH.Sort();
            this.lvwDSKH.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              lvwDSKH.Sorting);
        }
        /*
         * Tim kiem
         */
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dsKH = kH.TimKhach(txtTimKiemKhachHang.Text);
            lvwDSKH.Items.Clear();
            TaoTieuDeCot(lvwDSKH);
            TaiDuLieuLenListView(lvwDSKH, dsKH);
        }
        /*
         * auto complete tự động tải danh sách
         */
        private void txtTimKiemKhachHang_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (KhachHang i in kH.LayDSKH())
            {
                collection.Add(i.MaKH);
                collection.Add(i.TenKhach);
            }
            txtTimKiemKhachHang.AutoCompleteCustomSource = collection;

        }
    }



    /*
     * tạo 1 lớp sắp xếp kế thừa từ IComparer
     */
    public class ListViewItemComparer : IComparer
    {

        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                            ((ListViewItem)y).SubItems[col].Text);
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }


    }
}




