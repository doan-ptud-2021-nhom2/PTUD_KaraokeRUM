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
        private clsThietBi thietBi;
        private IEnumerable<TrangThietBi> danhSachThietBi;
        private clsPhongTrangThietBi phongTTB;
        private IEnumerable<dynamic> dSachPhongTTB;
        private void frmQuanLyThietBi_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnSuaTP.Enabled = false;
            btnXoa.Enabled = false;
            btnXoaTP.Enabled = false;
         
            TaoListView(lwvThietBi);
            thietBi = new clsThietBi();
            danhSachThietBi = thietBi.GetTrangThietBis();
            TaiDuLieuLenLWV(lwvThietBi, danhSachThietBi);
            TaoListView2(lwvThietBiTrongPhong);
            phongTTB = new clsPhongTrangThietBi();
            dSachPhongTTB = phongTTB.TraDuLieu();
            TaiDuLieuLenLWV2(lwvThietBiTrongPhong, dSachPhongTTB);
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
            foreach(TrangThietBi i in dSach)
            {
                item = TaoItem(i);
                lwv.Items.Add(item);
            }    
        }
        private ListViewItem TaoItem(TrangThietBi tb)
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
            lvw.Columns.Add("Loai Phong", 130);
            lvw.Columns.Add("So Phong", 350);
            lvw.Columns.Add("Ten TTB", 150);
            lvw.Columns.Add("So luong", 130);
        }
        private ListViewItem TaoItem2(dynamic tb)
        {
            ListViewItem lvwItem;
            lvwItem = new ListViewItem(tb.MaPhong);
            lvwItem.SubItems.Add(tb.TenPhong);
            lvwItem.SubItems.Add(tb.TenTTB);
            lvwItem.SubItems.Add(tb.DonVi);
            lvwItem.SubItems.Add(tb.Gia.ToString("#,###,000 VNĐ"));
            lvwItem.SubItems.Add(tb.SoLuong);
            lvwItem.Tag = tb;
            lvwItem.ImageIndex = 0;
            return lvwItem;

        }
        private void TaiDuLieuLenLWV2(ListView lwv, IEnumerable<dynamic> dSach)
        {
            lwv.Items.Clear();
            ListViewItem item;
            foreach (dynamic i in dSach)
            {
                item = TaoItem(i);
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
            cboLoaiPhong.Text = ptb.TenLoaiPhong;
            cboSoPhong.Text = ptb.TenPhong;
            cboTenTTB.Text = ptb.TenTTB;
            txtSoLuongTP.Text = ptb.SoLuong;
        }
        private void lwvThietBiTrongPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic ptb = null;
            if (lwvThietBi.SelectedItems.Count > 0)
            {
                btnSuaTP.Enabled = true;
                btnXoaTP.Enabled = true;
                ptb = lwvThietBiTrongPhong.SelectedItems[0].Tag;
                DuLieuLenTextBox2(ptb);
            }
        }
        #endregion
        private void btnTaiLai_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnThemTP_Click(object sender, EventArgs e)
        {

        }

        private void btnSuaTP_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaTP_Click(object sender, EventArgs e)
        {

        }

        private void btnTaiLaiTP_Click(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {

        }      
    }
}
