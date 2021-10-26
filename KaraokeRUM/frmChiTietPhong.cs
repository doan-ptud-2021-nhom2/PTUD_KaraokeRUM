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
    public partial class frmChiTietPhong : Form
    {
        public frmChiTietPhong()
        {
            InitializeComponent();
        }

        /** 
        * Các biến toàn cục.
        * hl: class Hỗn Loạn.
        * mh: class Mặt Hàng.
        */
        private clsMatHang mh;
        private clsHonLoan hl;
        //private IEnumerable<MatHang> dsMatHang;

        private void frmChiTietPhong_Load(object sender, EventArgs e)
        {
            // Gán thuộc tính cho combobox
            cboMatHang.Items.Add("Khô bò");
            cboMatHang.Items.Add("Bia");
            cboMatHang.Items.Add("Trái cây");
            cboMatHang.Items.Add("Nước lọc");
            cboTrangThaiPhong.Items.Add("Mở");
            cboTrangThaiPhong.Items.Add("Đặt");
            cboTrangThaiPhong.Items.Add("Đóng");
            cboLoaiPhong.Items.Add("VIP");
            cboLoaiPhong.Items.Add("THƯỜNG");

            TaoTieuDeCot(lstvDanhSachMatHang);
            mh = new clsMatHang();
            hl = new clsHonLoan();
            //dsMatHang = mh.LayTatCaMatHang();

            IEnumerable<dynamic> dsMH = hl.LayMatHangVaPhong();
            TaiDuLieuLenListView(lstvDanhSachMatHang, dsMH);

        }

        /** 
        * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lstv)
        {
            //lstv.Columns.Add("STT", 100);
            lstv.Columns.Add("Mã Mặt Hàng", 140);
            lstv.Columns.Add("Mã Hóa Đơn", 140);
            lstv.Columns.Add("Số Lượng", 130);
            lstv.Columns.Add("Thành Tiền", 130);


            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /** 
        * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> dsMH)
        {
            lstv.Items.Clear();
            ListViewItem lstvItem;
            dsMH = hl.LayMatHangVaPhong();

            foreach(dynamic ds in dsMH)
            {
                lstvItem = TaoItem(ds);
                lstv.Items.Add(lstvItem);
            }
        }
        ListViewItem TaoItem(dynamic itemMH)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemMH.MaMH);
            lstvItem.SubItems.Add(itemMH.MaHD);
            lstvItem.SubItems.Add(itemMH.SoLuong.ToString());
            lstvItem.SubItems.Add(itemMH.ThanhTien.ToString("##,## VNĐ"));

            lstvItem.Tag = itemMH;
            return lstvItem;
        }

        /** 
        * Xử lý hiện lên form Đổi phòng để đổi phòng cho khách hàng. 
        */
        private void btnDoiPhong_Click(object sender, EventArgs e)
        {
            frmDanhSachPhong frm = new frmDanhSachPhong();
            frm.Show();
        }

        /** 
        * Xử lý hiện lên form Hóa đơn để tạo hóa đơn cho khách hàng. 
        */
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            frm.Show();
        }

    }
}
