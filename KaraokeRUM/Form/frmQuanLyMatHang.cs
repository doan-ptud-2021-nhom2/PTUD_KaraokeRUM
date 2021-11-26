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
        private clsMatHang MH;
        private int SORTCOLUMN = -1;

        private void frmQuanLyMatHang_Load(object sender, EventArgs e)
        {
            loadCombobox();
            TaoTieuDeCot(lstvMatHang);
            TaiDuLieu();
            TaiDuLieuVaoBoxTimKiem();
        }

        /**
         * Hàm hỗ trợ tải tên và mã mặt hàng vào txtTimKiem để thực hiện chức năng tìm kiếm.
         */
        private void TaiDuLieuVaoBoxTimKiem()
        {
            txtTimKiemMatHang.AutoCompleteCustomSource.Clear();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (MatHang i in MH.LayTatCaMatHang())
            {
                collection.Add(i.MaMH);
                collection.Add(i.TenMh);
            }
            txtTimKiemMatHang.AutoCompleteCustomSource = collection;
        }

        private void TaiDuLieu()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            MH = new clsMatHang();

            IEnumerable<MatHang> dsMH = MH.LayTatCaMatHang();
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
            lstv.Columns.Add("Tên Mặt Hàng", 200);
            lstv.Columns.Add("Số lượng", 100);
            lstv.Columns.Add("Đơn vị", 100);
            lstv.Columns.Add("Giá",160);
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
            txtTenMH.Enabled = false;
            cboLMH.Enabled = false;
            txtSoLuongTon.Enabled = false;
            cboDonVi.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
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
            txtGia.Text = dsMH.Gia.ToString("##,##");

        }

        /*
        * sự kiện click vào cột để sắp xếp
        */
        private void lstvMatHang_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != SORTCOLUMN)
            {
                SORTCOLUMN = e.Column;
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
            dsMHTim = MH.TimMatHang(txtTimKiemMatHang.Text);
            lstvMatHang.Items.Clear();
            txtTimKiemMatHang.Clear();
            TaiDuLieuLenListView(lstvMatHang, dsMHTim);
        }

        void XoaCacTxtCbo()
        {
            txtTenMH.Text = "";
            txtSoLuongTon.Text = "";
            txtGia.Text = "";
            txtTimKiemMatHang.Text = "";
            cboDonVi.SelectedIndex = -1;
            cboLMH.SelectedIndex = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MatHang matHang = ThemMatHang();
            if (MH.TimMatHang(matHang.TenMh).Count() > 0)
            {
                MessageBox.Show("Lỗi! đã tồn tại mặt hàng này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MH.ThemMatHang(matHang);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvMatHang, MH.LayTatCaMatHang());
            }
        }

        /** 
        * Tạo mã mặt hàng tự tăng
        */
        private string TaoMaMatHang()
        {
            string maMatHang = "";
            string maMatHangTam = MH.LayTatCaMatHangTonTai().Last().MaMH.ToString();

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
                MH.SuaMatHang(matHang);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvMatHang, MH.LayTatCaMatHang());
 
        }

        dynamic SuaMatHang()
        {
            MatHang matHang = new MatHang();
            matHang.MaMH = MH.TimMatHang(txtTenMH.Text).First().MaMH;
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
            matHang.MaMH = MH.TimMatHang(txtTenMH.Text).First().MaMH;
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
                    MH.XoaMatHang(xoaMatHang);
                    XoaCacTxtCbo();
                    TaiDuLieuLenListView(lstvMatHang, MH.LayTatCaMatHang());
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
            MH = new clsMatHang();
            IEnumerable<MatHang> dsMH = MH.LayTatCaMatHang();
            TaiDuLieuLenListView(lstvMatHang, dsMH);
        }

        /*Kiểm tra xem các thành phần trong text box có rỗng k*/
        private void KiemTraTxtCbo()
        {
            foreach (Control c in brbThongTinMatHang.Controls)
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
                if (c is ComboBox)
                {
                    var a = c as ComboBox;
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
           
        }

        private void txtTenMH_TextChanged(object sender, EventArgs e)
        {
            KiemTraTxtCbo();
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {
            KiemTraTxtCbo();
            if(txtTenMH.Enabled==false)
            {
                btnThem.Enabled = false;
            }    
            else
            {
                btnThem.Enabled = true;
            }    
        }

        private void txtSoLuongTon_TextChanged(object sender, EventArgs e)
        {
            KiemTraTxtCbo();
        }
        private void cboLMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            KiemTraTxtCbo();
        }
        private void cboDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            KiemTraTxtCbo();
        }

        /*Chặn không cho nhập chữ */
        private void txtSoLuongTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmQuanLyMatHang_Click(object sender, EventArgs e)
        {
            if (lstvMatHang.SelectedItems.Count > 0)
            {
                lstvMatHang.SelectedItems.Clear();
                XoaCacTxtCbo();
                txtTenMH.Enabled = true;
                cboLMH.Enabled = true;
                txtSoLuongTon.Enabled = true;
                cboDonVi.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                
            }
        } 
    }
}



