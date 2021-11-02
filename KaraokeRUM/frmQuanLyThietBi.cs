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
    public partial class frmQuanLyThietBi : Form
    {
        public frmQuanLyThietBi()
        {
            InitializeComponent();
        }
        private clsHonLoan hl;
        private clsPhong phong;
        private IEnumerable<Phong> dsachPhong;
        private clsThietBi thietBi;
        private IEnumerable<TrangThietBi> danhSachThietBi;
        private clsPhongTrangThietBi phongTTB;
        private IEnumerable<Phong_TrangThietBi> dSachPhongTTB;
        private void frmQuanLyThietBi_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnSuaTP.Enabled = false;
            btnXoa.Enabled = false;
            btnXoaTP.Enabled = false;
            cboDonVi.Items.Add("Cặp");
            cboDonVi.Items.Add("Cái");
            TaoListView(lwvThietBi);
            thietBi = new clsThietBi();
            danhSachThietBi = thietBi.LayToanBoTrangThietBis();
            foreach (TrangThietBi i in danhSachThietBi)
            {
                cboTenTTB.Items.Add(i.TenTTB);
            }
            TaiDuLieuLenLWV(lwvThietBi, danhSachThietBi);
            TaoListView2(lwvThietBiTrongPhong);
            phongTTB = new clsPhongTrangThietBi();
            phong = new clsPhong();
            hl = new clsHonLoan();
            dsachPhong = phong.LayTatCaPhong();
            foreach (Phong i in dsachPhong)
            {
                cboSoPhong.Items.Add(i.TenPhong);
            }
            dSachPhongTTB = phongTTB.TraDuLieu();
            TaiDuLieuLenLWV2(lwvThietBiTrongPhong, dSachPhongTTB);
            txtTimKiem.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiem.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        #region ListView
        private void TaoListView(ListView lvw)
        {
            lvw.Columns.Clear();
            lvw.View = View.Details;
            lvw.GridLines = true;
            lvw.FullRowSelect = true;
            lvw.Columns.Add("MaTTB", 130);
            lvw.Columns.Add("TenTTB", 350);
            lvw.Columns.Add("So Luong Ton", 150);
            lvw.Columns.Add("Don Vi", 130);
            lvw.Columns.Add("Gia", 150);
            lvw.Columns.Add("MaQL", 130);
        }
        private void TaiDuLieuLenLWV(ListView lwv, IEnumerable<TrangThietBi> dSach)
        {
            lwv.Items.Clear();
            ListViewItem item;
            foreach (TrangThietBi i in dSach)
            {
                item = TaoItemTTB(i);
                lwv.Items.Add(item);
            }
        }
        private void TaiDuLieuLenLWV21(ListView lwv, IEnumerable<TrangThietBi> dSach)
        {
            lwv.Items.Clear();
            ListViewItem item;
            foreach (TrangThietBi i in dSach)
            {
                item = TaoItemTTB(i);
                lwv.Items.Add(item);
            }
        }
        private ListViewItem TaoItemTTB(TrangThietBi tb)
        {
            ListViewItem lvwItem;
            lvwItem = new ListViewItem(tb.MaTTB);
            lvwItem.SubItems.Add(tb.TenTTB);
            lvwItem.SubItems.Add(tb.SoLuongTon.ToString());
            lvwItem.SubItems.Add(tb.DonVi);
            lvwItem.SubItems.Add(tb.Gia.ToString("#,###,000 VNĐ"));
            lvwItem.SubItems.Add(tb.MaQL);
            lvwItem.Tag = tb;
            lvwItem.ImageIndex = 0;
            return lvwItem;

        }
        private void TaoListView2(ListView lvw)
        {
            lvw.Columns.Clear();
            lvw.View = View.Details;
            lvw.GridLines = true;
            lvw.FullRowSelect = true;
            lvw.Columns.Add("So Phong", 350);
            lvw.Columns.Add("Ten TTB", 150);
            lvw.Columns.Add("So luong", 130);
        }
        private ListViewItem TaoItemPTTB(dynamic pTTB)
        {
            ListViewItem lvwItem;
            lvwItem = new ListViewItem(phong.TimMotPhongTheoMa(pTTB.MaPhong).TenPhong);
            lvwItem.SubItems.Add(thietBi.TimTTBTheoMa(pTTB.MaTTB).TenTTB);
            lvwItem.SubItems.Add(pTTB.SoLuong.ToString());
            lvwItem.Tag = pTTB;
            lvwItem.ImageIndex = 0;
            return lvwItem;
        }
        private void TaiDuLieuLenLWV2(ListView lwv, IEnumerable<dynamic> dSach)
        {
            lwv.Items.Clear();
            ListViewItem item;
            /*dSach = hl.LayThongTinPhongTrangThietBi();*/
            foreach (dynamic i in dSach)
            {
                item = TaoItemPTTB(i);
                lwv.Items.Add(item);
            }
        }
        #endregion
        #region DuLieulenText
        private void DuLieuLenTextBox(TrangThietBi tb)
        {
            txtTen.Text = tb.TenTTB;
            txtSoLuongTon.Text = tb.SoLuongTon.ToString();
            cboDonVi.Text = tb.DonVi;
            txtDonGia.Text = tb.Gia.ToString("#,###,000 VNĐ");

        }
        private void lwvThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic tb = null;
            if (lwvThietBi.SelectedItems.Count > 0)
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                tb = lwvThietBi.SelectedItems[0].Tag;
                DuLieuLenTextBox(tb);
            }
        }
        private void DuLieuLenTextBox2(dynamic ptb)
        {
            cboSoPhong.Text = ptb.TenPhong;
            cboTenTTB.Text = ptb.TenTTB;
            txtSoLuongTP.Text = ptb.SoLuong.ToString();
        }
        private void lwvThietBiTrongPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic ptb = null;
            if (lwvThietBiTrongPhong.SelectedItems.Count > 0)
            {
                ptb = lwvThietBiTrongPhong.SelectedItems[0].Tag;
                DuLieuLenTextBox2(ptb);
            }
            btnSuaTP.Enabled = true;
            btnXoaTP.Enabled = true;
        }
        #endregion
        #region ThietBi
        #region ThemTTb
        private string taoMaTTB()
        {
            string maTTB = "";
            int dem = thietBi.LayToanBoTrangThietBis().Count() + 1;
            if (dem < 10)
            {
                maTTB += "TB00" + dem;
            }
            else
            {
                maTTB += "TB0" + dem;
            }
            return maTTB;
        }
        private TrangThietBi TaoTTB()
        {
            TrangThietBi ttb = new TrangThietBi();
            ttb.MaTTB = taoMaTTB();
            ttb.TenTTB = txtTen.Text;
            ttb.SoLuongTon = (int)Convert.ToDecimal(txtSoLuongTon.Text);
            ttb.DonVi = cboDonVi.Text;
            ttb.Gia = Convert.ToDecimal(txtDonGia.Text);
            ttb.MaQL = "NV002";
            return ttb;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            TrangThietBi ttb = TaoTTB();
            thietBi.Them(ttb);
            TaiDuLieuLenLWV(lwvThietBi, thietBi.LayToanBoTrangThietBis());
        }
        #endregion
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            IEnumerable<TrangThietBi> danhSach;
            danhSach = thietBi.LayToanBoTrangThietBis();
            lwvThietBi.Clear();
            TaoListView(lwvThietBi);
            TaiDuLieuLenLWV21(lwvThietBi, danhSachThietBi);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult yn;
            TrangThietBi ttb;
            int index;
            if (lwvThietBi.SelectedItems.Count > 0)
            {
                yn = MessageBox.Show("Bạn có chắc muốn xóa?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    for (int i = 0; i < lwvThietBi.SelectedItems.Count; ++i)
                    {
                        index = lwvThietBi.SelectedIndices[i];
                        ttb = (TrangThietBi)lwvThietBi.Items[index].Tag;
                        thietBi.Xoa(ttb);
                    }
                    TaiDuLieuLenLWV(lwvThietBi, danhSachThietBi);
                    btnXoa.Enabled = false;
                    btnSua.Enabled = false;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            TrangThietBi tb = taoTBSua();
            thietBi.SuaTrangThietBi(tb);
            TaiDuLieuLenLWV(lwvThietBi, danhSachThietBi);
        }
        TrangThietBi taoTBSua()
        {
            TrangThietBi tb = new TrangThietBi();
            tb.MaTTB = thietBi.TimTenTB(txtTen.Text).First().MaTTB;
            tb.TenTTB = txtTen.Text;
            tb.SoLuongTon = (int)Convert.ToDecimal(txtSoLuongTon.Text);
            tb.DonVi = cboDonVi.Text;
            tb.Gia = Convert.ToDecimal(txtDonGia.Text);
            tb.MaQL = "NV002";
            return tb;
        }
        #endregion     
        dynamic TaoPTTB()
        {
            Phong_TrangThietBi pTTB = new Phong_TrangThietBi();
            pTTB.SoLuong = (int)Convert.ToDecimal(txtSoLuongTP.Text);
            pTTB.MaPhong = phong.TimTenPhong(cboSoPhong.Text).First().MaPhong;
            pTTB.MaTTB = thietBi.TimTenTB(cboTenTTB.Text).First().MaTTB;
            return pTTB;
        }
        private void btnThemTP_Click(object sender, EventArgs e)
        {
            Phong_TrangThietBi pTTB = TaoPTTB();
            phongTTB.Them(pTTB);
            TaiDuLieuLenLWV2(lwvThietBiTrongPhong, phongTTB.TraDuLieu());
        }

        private void btnSuaTP_Click(object sender, EventArgs e)
        {
            Phong_TrangThietBi pTTB = TaoPTTB();
            phongTTB.SuaTrangThietBi(pTTB);
            TaiDuLieuLenLWV2(lwvThietBiTrongPhong, phongTTB.TraDuLieu());
            btnSuaTP.Enabled = false;
            btnXoaTP.Enabled = false;
        }
        private void btnXoaTP_Click(object sender, EventArgs e)
        {
            DialogResult yn;
            Phong_TrangThietBi ttb;
            string maTTB, maPhong;
            if (lwvThietBiTrongPhong.SelectedItems.Count > 0)
            {
                yn = MessageBox.Show("Bạn có chắc muốn xóa?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    maPhong = phong.TimTenPhong(cboSoPhong.Text).First().MaPhong;
                    maTTB = thietBi.TimTenTB(cboTenTTB.Text).First().MaTTB;
                    ttb = phongTTB.TimPhongTTB(maPhong, maTTB).First();
                    phongTTB.Xoa(ttb);
                }
                TaiDuLieuLenLWV2(lwvThietBiTrongPhong, phongTTB.TraDuLieu());
                TaiDuLieuLenLWV(lwvThietBi, thietBi.LayToanBoTrangThietBis());
            }
            btnSuaTP.Enabled = false;
            btnXoaTP.Enabled = false;
        }

        private void btnTaiLaiTP_Click(object sender, EventArgs e)
        {
            lwvThietBiTrongPhong.Clear();
            dSachPhongTTB = phongTTB.TraDuLieu();
            TaiDuLieuLenLWV2(lwvThietBiTrongPhong, dSachPhongTTB);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            danhSachThietBi = thietBi.TimTTB(txtTimKiem.Text);
            TaiDuLieuLenLWV(lwvThietBi, danhSachThietBi);
            txtTimKiem.Clear();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            txtTimKiem.AutoCompleteCustomSource.Clear();
            foreach (TrangThietBi i in danhSachThietBi)
            {
                txtTimKiem.AutoCompleteCustomSource.Add(i.TenTTB);
                txtTimKiem.AutoCompleteCustomSource.Add(i.MaTTB);
            }
        }

        private void cboSoPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string soPhong = cboSoPhong.Text;
            string maPhong = phong.TimTenPhong(soPhong).First().MaPhong;
            dSachPhongTTB = phongTTB.TimPhongTTB(maPhong);           
            TaiDuLieuLenLWV2(lwvThietBiTrongPhong, dSachPhongTTB);
        }
    }
}
