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
     
            TaoTieuDeCot(lvwDSKH);
            kH = new clsKhachHang();
            lK = new clsLoaiKhach();
            hL = new clsHonLoan();
            IEnumerable<dynamic> dsKH = hL.LayPhongVaLoaiPhong();
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
            foreach (dynamic ds in hL.KhachHangVaLoaiKhachHang())
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
    }
        
 }
