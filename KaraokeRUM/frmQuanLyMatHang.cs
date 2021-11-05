using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KaraokeRUM
{
    public partial class frmQuanLyMatHang : Form
    {
        public frmQuanLyMatHang(string maQL)
        {
            InitializeComponent();
            MAQL = maQL;
        }
        private string MAQL;
        private clsMatHang mH;
        private int sortColumn = -1;
        private void frmQuanLyMatHang_Load(object sender, EventArgs e)
        {
            loadCombobox();
            TaoTieuDeCot(lstvMatHang);
            mH = new clsMatHang();

            IEnumerable<MatHang> dsMH = mH.LayTatCaMatHang();
            TaiDuLieuLenListView(lstvMatHang, dsMH);
            txtTimKiemMatHang.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemMatHang.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        /** 
       * Tạo tiêu đề cột
       */
        void TaoTieuDeCot(ListView lstv)
        {
            lstv.Columns.Add("Mã MH", 100);
            lstv.Columns.Add("Tên Mặt Hàng", 230);
            lstv.Columns.Add("Số lượng", 100);
            lstv.Columns.Add("Đơn vị", 100);
            lstv.Columns.Add("Giá", 130);
            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lstv, IEnumerable<MatHang> dsMH)
        {
            lstv.Items.Clear();
            ListViewItem itemMatHang;
            foreach (MatHang ds in dsMH)
            {
                itemMatHang = TaoItem(ds);
                lstv.Items.Add(itemMatHang);
            }

        }
        // ủa m, à tuấn anh
        ListViewItem TaoItem(MatHang itemMH)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemMH.MaMH);
            lstvItem.SubItems.Add(itemMH.TenMh);
            lstvItem.SubItems.Add(itemMH.SoLuongTon.ToString());
            lstvItem.SubItems.Add(itemMH.DonVi);
            lstvItem.SubItems.Add(itemMH.Gia.ToString("##,## VNĐ"));
            lstvItem.Tag = itemMH;
            return lstvItem;
        }
        private void lstvMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            MatHang dsMH = null;
            if (lstvMatHang.SelectedItems.Count > 0)
            {
                dsMH = (MatHang)lstvMatHang.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsMH);
            }
        }
        /*
         * load combobox 
         */
        void loadCombobox()
        {
            cboDonVi.Items.Add("Hộp");
            cboDonVi.Items.Add("Đĩa");
            cboDonVi.Items.Add("Lon");
            cboDonVi.Items.Add("Thùng");
            cboDonVi.Items.Add("Chai");

            cboLMH.Items.Add("Thức ăn");
            cboLMH.Items.Add("Đồ uống");
        }

        void TaiDuLieuTuLstvDenTxtCbo(MatHang dsMH)
        {
            txtTenMH.Text = dsMH.TenMh;
            cboLMH.Text = dsMH.Loai;
            txtSoLuongTon.Text = dsMH.SoLuongTon.ToString();
            cboDonVi.Text = dsMH.DonVi;
            txtGia.Text = dsMH.Gia.ToString("##,## VNĐ");

        }
        /*
       * sự kiện click vào cột để sắp xếp
       */
        private void lstvMatHang_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                lstvMatHang.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lstvMatHang.Sorting == SortOrder.Ascending)
                    lstvMatHang.Sorting = SortOrder.Descending;
                else
                    lstvMatHang.Sorting = SortOrder.Ascending;
            }
            lstvMatHang.Sort();
            this.lstvMatHang.ListViewItemSorter = new clsListViewItemComparer(e.Column,lstvMatHang.Sorting);
         }   
        /*
         * Tim kiem
         */
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            IEnumerable<MatHang> dsMHTim;
            dsMHTim = mH.TimMatHang(txtTimKiemMatHang.Text);
            lstvMatHang.Items.Clear();
            txtTimKiemMatHang.Clear();
            TaiDuLieuLenListView(lstvMatHang, dsMHTim);
        }

        private void txtTimKiemMatHang_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (MatHang i in mH.LayTatCaMatHang())
            {
                collection.Add(i.MaMH);
                collection.Add(i.TenMh);
            }
            txtTimKiemMatHang.AutoCompleteCustomSource = collection;

        }
        void XoaCacTxtCbo()
        {
            txtTenMH.Text = "";
            cboDonVi.Text = "";
            txtSoLuongTon.Text = "";
            cboLMH.Text = "";
            txtGia.Text = "";
            txtTimKiemMatHang.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MatHang matHang = ThemMatHang();
            if (mH.TimMatHang(matHang.TenMh).Count() > 0)
            {
                MessageBox.Show("Lỗi! đã tồn tại mặt hàng này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                mH.ThemMatHang(matHang);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvMatHang, mH.LayTatCaMatHang());
            }
        }
        /** 
       * Tạo mã mặt hàng tự tăng
       */
        private string TaoMaMatHang()
        {
            string maMatHang = "";
            string maMatHangTam = mH.LayTatCaMatHangTonTai().Last().MaMH.ToString();

            int dem = Convert.ToInt32(maMatHangTam.Split('M', 'H')[2]) + 1;
            if (dem < 10)
            {
                maMatHang += "MH00" + dem;
            }
            else if (dem >= 10 && dem < 100)
            {
                maMatHang += "MH0" + dem;
            }
            else
            {
                maMatHang += "MH" + dem;
            }

            return maMatHang;
        }
        /** 
        * Tạo mã nhân viên
        */
        dynamic ThemMatHang()
        {
            MatHang matHang = new MatHang();
            matHang.MaMH = TaoMaMatHang();
            matHang.TenMh = txtTenMH.Text;
            matHang.Loai = cboLMH.Text;
            matHang.SoLuongTon = Convert.ToInt32(txtSoLuongTon.Text);
            matHang.DonVi = cboDonVi.Text;
            string gia = txtGia.Text.Replace("VNĐ", String.Empty).Replace(",", String.Empty);
            gia.Trim();
            matHang.Gia = Convert.ToInt64(gia);
            matHang.MaQL = MAQL;
            matHang.TrangThai = "DSD";
            return matHang;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MatHang matHang = SuaMatHang();
                mH.SuaMatHang(matHang);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvMatHang, mH.LayTatCaMatHang());
 
        }
        dynamic SuaMatHang()
        {
            MatHang matHang = new MatHang();
            matHang.MaMH = mH.TimMatHang(txtTenMH.Text).First().MaMH;
            matHang.Loai = cboLMH.Text;
            matHang.SoLuongTon = Convert.ToInt32(txtSoLuongTon.Text);
            matHang.DonVi = cboDonVi.Text;
            string gia = txtGia.Text.Replace("VNĐ", String.Empty).Replace(",", String.Empty);
            gia.Trim();
            matHang.Gia = Convert.ToInt64(gia);
           
           
            return matHang;
        }
        /** 
        * Xoá mặt hàng (Thay đổi trạng thái mặt hàng)
        */
        MatHang XoaMatHang()
        {

            MatHang matHang = new MatHang();
            matHang.MaMH = mH.TimMatHang(txtTenMH.Text).First().MaMH;
            matHang.TrangThai = "KSD";
            return matHang;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult hoiXoa;
            hoiXoa = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (hoiXoa == DialogResult.Yes)
            {
                if(txtSoLuongTon.Text.Equals("0"))
                {
                    MatHang xoaMatHang = XoaMatHang();
                    mH.XoaMatHang(xoaMatHang);
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lstvMatHang, mH.LayTatCaMatHang());
                }
                else
                {
                    MessageBox.Show("Lỗi! số lượng mặt hàng này vẫn còn, không xoá được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            lstvMatHang.Clear();
            loadCombobox();
            TaoTieuDeCot(lstvMatHang);
            mH = new clsMatHang();
            IEnumerable<MatHang> dsMH = mH.LayTatCaMatHang();
            TaiDuLieuLenListView(lstvMatHang, dsMH);
        }
    }
}



