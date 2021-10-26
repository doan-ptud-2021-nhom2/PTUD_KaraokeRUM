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
       */
        private clsKhachHang kH;
        private clsLoaiKhach lK;
        private clsHonLoan hL;
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            cboLoaiKhachHang.Items.Add("VIP");
            cboLoaiKhachHang.Items.Add("TX");
            cboLoaiKhachHang.Items.Add("THUONG");

            cboLocTheoLoai.Items.Add("VIP");
            cboLocTheoLoai.Items.Add("TX");
            cboLocTheoLoai.Items.Add("THUONG");
            cboLocTheoLoai.Items.Add("All");

            cboSapXep.Items.Add("Mã khách hàng");
            cboSapXep.Items.Add("Tên khách hàng");
            cboSapXep.Items.Add("Số lần đến");
            cboSapXep.Items.Add("Chiết khấu");

            TaoTieuDeCot(lvwDSKH);
            kH = new clsKhachHang();
            lK = new clsLoaiKhach();
            hL = new clsHonLoan();
            IEnumerable<dynamic> dsKH = hL.KhachHangVaLoaiKhachHang();
            TaiDuLieuLenListView(lvwDSKH, dsKH);


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
            IEnumerable<dynamic> layDS = hL.KhachHangVaLoaiKhachHang();
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lvwDSKH, layDS);
        }
        LoaiKhachHang SuaChietKhauLoaiKhach(){
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
            cboSapXep.Text = "";
        }

        private void cboLocTheoLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cboLocTheoLoai.GetItemText(this.cboLocTheoLoai.SelectedItem);
            lvwDSKH.Clear();
            TaoTieuDeCot(lvwDSKH);
            hL = new clsHonLoan();
            IEnumerable<dynamic> dsKH = hL.LayKhachHangVaLoaiKhachHangTheoLoai(selected);
            lvwDSKH.Items.Clear();
            TaoTieuDeCot(lvwDSKH);
            TaiDuLieuLenListView(lvwDSKH, dsKH);
        }

    }
 }
        
 
