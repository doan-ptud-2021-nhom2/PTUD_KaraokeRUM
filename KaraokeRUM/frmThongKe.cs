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
    public partial class frmThongKe : Form
    {
        private clsThongKe ThongKe;
        private clsHoaDon HoaDon;
        private IEnumerable<dynamic> DSHoaDon;
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            ThongKe = new clsThongKe();
            HoaDon = new clsHoaDon();
            rdoHomNay.Enabled = true;
            DSHoaDon = HoaDon.LayDanhSachHoaDon();
            
            TaiBieuTheoThang("10","2021");
            TaoListView(lstvDSHoaDon);
            TaiDuLieuLenListView(lstvDSHoaDon, DSHoaDon);
        }

        private void TaoListView(ListView lstv)
        {
            lstv.Columns.Add("Mã HD", 100);
            lstv.Columns.Add("Số phòng", 100);
            lstv.Columns.Add("Tên khách hàng", 200);
            lstv.Columns.Add("Tổng tiền", 200);
            lstv.Columns.Add("Ngày lập", 200);

            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        private ListViewItem TaoItem(dynamic itemHD)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemHD.MaHD);
            lstvItem.SubItems.Add(itemHD.TenPhong);
            lstvItem.SubItems.Add(itemHD.TenKhach);
            lstvItem.SubItems.Add(itemHD.TongTien.ToString("#,### VNĐ"));
            lstvItem.SubItems.Add(itemHD.NgayLap.ToString("dd/MM/yyyy"));

            lstvItem.Tag = itemHD;
            return lstvItem;
        }

        private void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> dsHD)
        {
            lstv.Items.Clear();
            ListViewItem itemHD;
            foreach(dynamic item in dsHD)
            {
                itemHD = TaoItem(item);
                lstv.Items.Add(itemHD);
            }
        }
        #region Các phương thức hỗ trợ việc vẽ biểu đồ
        private void TaiBieuDoHomNay(string homNay)
        {
            dynamic data = ThongKe.LaySoLieuThongKeHomNay(homNay);
            chrThongKeDoanhThu.Series["DoanhThu"].IsValueShownAsLabel = true;
            foreach(dynamic item in data)
            {
                chrThongKeDoanhThu.Series["DoanhThu"].Points.AddXY(item.MatHang, item.SoLuong);
            }
        }

        private void TaiBieuTheoThang(string thang, string nam)
        {
            dynamic data = ThongKe.LaySoLieuThongKeTheoThang(thang, nam);
            chrThongKeDoanhThu.Series["DoanhThu"].IsValueShownAsLabel = true;
            foreach (dynamic item in data)
            {
                chrThongKeDoanhThu.Series["DoanhThu"].Points.AddXY(item.MatHang, item.SoLuong);
            }
        }
        #endregion

        private void rdoHomNay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHomNay.Checked)
            {

            }
        }
    }
}
