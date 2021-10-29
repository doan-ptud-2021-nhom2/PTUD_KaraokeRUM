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
        /**
         * Các biến được sử dụng trong chương trình
         */
        private clsThongKe ThongKe;
        private clsMatHang MatHang;
        private IEnumerable<dynamic> DSHoaDon;

        public frmThongKe()
        {
            InitializeComponent();
        }

        /**
         * Sự kiện load form
         */
        private void frmThongKe_Load(object sender, EventArgs e)
        {
            ThongKe = new clsThongKe();
            MatHang = new clsMatHang();

            rdoHomNay.Checked = true;
            TaoComboBox();
        }

        /**
         * Gán giá trị cho 2 comboBox
         */
        private void TaoComboBox()
        {
            //Thêm dữ liệu cboThang
            cboThang.Items.Add("1");
            cboThang.Items.Add("2");
            cboThang.Items.Add("3");
            cboThang.Items.Add("4");
            cboThang.Items.Add("5");
            cboThang.Items.Add("6");
            cboThang.Items.Add("7");
            cboThang.Items.Add("8");
            cboThang.Items.Add("9");
            cboThang.Items.Add("10");
            cboThang.Items.Add("11");
            cboThang.Items.Add("12");

            //Thêm dữ liệu cboNam
            cboNam.Items.Add("2021");
            cboNam.Items.Add("2022");
            cboNam.Items.Add("2023");
        }

        /**
         * Khởi tạo listView tạo các cột
         */
        private void TaoListView(ListView lstv)
        {
            lstv.Columns.Add("Mã HD", 100);
            lstv.Columns.Add("Số phòng", 100);
            lstv.Columns.Add("Tên khách hàng", 200);
            lstv.Columns.Add("Tổng tiền", 200);
            lstv.Columns.Add("Ngày lập", 150);

            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /**
         * Tạo các ItemListView để thêm vào listView
         */
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

        /**
         * Hàm hỗ trợ tải dữ liệu lên listview
         */
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

        /**
         * Các hàm hỗ trợ cho việc vẽ biểu đồ gồm có: tải theo ngày hôm nay và tải theo tháng & năm
         */
        #region Các hàm hỗ trợ cho việc load Biểu đồ
        private void TaiBieuDoHomNay(string homNay)
        {
            //Xóa biểu đồ để vẽ lại
            chrThongKeDoanhThu.Series["DoanhThu"].Points.Clear();
            //Load dữ liệu vào data
            dynamic data = ThongKe.LaySoLieuThongKeHomNay(homNay);
            //Hiển thị thông số của trường dữ liệu
            chrThongKeDoanhThu.Series["DoanhThu"].IsValueShownAsLabel = true;
            
            //Chạy vòng lặp để show dữ liệu
            foreach (dynamic item in data)
            {
                //Đối với các loại nước có đơn vị là lon thì / 24 để tính bằng thùng
                if(MatHang.LayDonViMatHang(item.MatHang).DonVi.Equals("Lon"))
                    chrThongKeDoanhThu.Series["DoanhThu"].Points.AddXY(item.MatHang, item.SoLuong / 24);
                else
                    chrThongKeDoanhThu.Series["DoanhThu"].Points.AddXY(item.MatHang, item.SoLuong);
            }
        }

        private void TaiBieuTheoThang(string thang, string nam)
        {
            chrThongKeDoanhThu.Series["DoanhThu"].Points.Clear();
            dynamic data = ThongKe.LaySoLieuThongKeTheoThang(thang, nam);
            chrThongKeDoanhThu.Series["DoanhThu"].IsValueShownAsLabel = true;
            foreach (dynamic item in data)
            {
                if (MatHang.LayDonViMatHang(item.MatHang).DonVi.Equals("Lon"))
                    chrThongKeDoanhThu.Series["DoanhThu"].Points.AddXY(item.MatHang, (item.SoLuong / 24));
                else
                    chrThongKeDoanhThu.Series["DoanhThu"].Points.AddXY(item.MatHang, item.SoLuong);
            }
        }

        #endregion
        /**
         * Sử lý sự kiện check vào radio HomNay
         */
        private void rdoHomNay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHomNay.Checked)
            {
                btnTKDoanhThu.Enabled = false;
                cboThang.Text = "";
                cboNam.Text = "";
                //Thay thế biến bằng date.ToString format theo sql
                //string homNay = "2021-10-28";
                string homNay = DateTime.Now.ToString("yyyy-MM-dd");
                //Load biểu đồ
                TaiBieuDoHomNay(homNay);

                //Load danh sách
                lstvDSHoaDon.Clear();
                DSHoaDon = ThongKe.LayDanhSachHoaDonHomNay(homNay);
                TaoListView(lstvDSHoaDon);
                TaiDuLieuLenListView(lstvDSHoaDon, DSHoaDon);

                //Load thông tin lên các text box
                txtSKH.Text = ThongKe.LayTongSoKhachHangHomNay(homNay).ToString();
                txtSMH.Text = ThongKe.LayTongSanPhamBanHomNay(homNay).ToString();
                txtTDT.Text = ThongKe.LayTongTienHomNay(homNay).ToString("#,### VNĐ");
            }
        }

        /**
         * Sử lý sự kiện khi click vào btn Thống Kê
         */
        private void btnTKDoanhThu_Click(object sender, EventArgs e)
        {
            //Xử lý khi chọn vào combo rồi click Thống kê
            string thang = cboThang.Text;
            string nam = cboNam.Text;
            if(!thang.Equals("") && nam.Equals(""))
            {
                MessageBox.Show("Phải chọn đẩy đủ thông tin để thống kê", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNam.Text = nam = DateTime.Now.Year.ToString();
            }
            //LoadBieuDo
            TaiBieuTheoThang(thang, nam);

            //Load danh sách
            lstvDSHoaDon.Clear();
            DSHoaDon = ThongKe.LayDanhSachHoaDonTheoThangNam(thang, nam);
            TaoListView(lstvDSHoaDon);
            TaiDuLieuLenListView(lstvDSHoaDon, DSHoaDon);

            //Load thông tin lên các text box
            txtSKH.Text = ThongKe.LayTongSoKhachHangTheoThangNam(thang, nam).ToString();
            txtSMH.Text = ThongKe.LayTongSanPhamBanTheoThangNam(thang, nam).ToString();
            txtTDT.Text = ThongKe.LayTongTienTheoThangNam(thang, nam).ToString("#,### VNĐ");

        }

        /**
         * Sử lý sự kiện chọn comboBox Tháng => Chọn tháng
         */
        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoHomNay.Checked = false;
            btnTKDoanhThu.Enabled = true;
        }

        /**
         * Sử lý sự kiện chọn comboBox Năm => Chọn năm
         */
        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoHomNay.Checked = false;
            btnTKDoanhThu.Enabled = true;
            if(Convert.ToInt32(cboNam.Text) > Convert.ToInt32(DateTime.Now.Year.ToString()))
            {
                MessageBox.Show("Không được chọn năm lớn hơn năm hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNam.Text = DateTime.Now.Year.ToString();
            }
        }

        private void lstvDSHoaDon_DoubleClick(object sender, EventArgs e)
        {
            var item = lstvDSHoaDon.SelectedItems[0];
            string maHD = item.SubItems[0].Text;
            MessageBox.Show(maHD);
        }
    }
}
