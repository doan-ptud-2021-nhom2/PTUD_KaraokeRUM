using Microsoft.Office.Interop.Excel;
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
    public partial class frmThongKeQL : Form
    {
        /**
         * Các biến được sử dụng trong chương trình
         */
        private clsThongKe THONGKE;
        private clsMatHang MATHANG;
        private IEnumerable<dynamic> DSHOADON;

        public frmThongKeQL()
        {
            InitializeComponent();
        }

        /**
         * Sự kiện load form
         */
        private void frmThongKeQL_Load(object sender, EventArgs e)
        {
            THONGKE = new clsThongKe();
            MATHANG = new clsMatHang();

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
            lstv.Columns.Add("Mã HD", 130);
            lstv.Columns.Add("Tên phòng", 100);
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
            foreach (dynamic item in dsHD)
            {
                itemHD = TaoItem(item);
                lstv.Items.Add(itemHD);
            }
        }

        /**
         * Các hàm hỗ trợ cho việc vẽ biểu đồ gồm có: tải theo ngày hôm nay và tải theo tháng & năm
         */
        #region Các hàm hỗ trợ cho việc load Biểu đồ
        /**
         * Tải biểu đồ thống kê theo mặt hàng
         */
        private void TaiBieuDoHomNay(string homNay)
        {
            //Xóa biểu đồ để vẽ lại
            chrThongKeMatHang.Series["MatHang"].Points.Clear();
            //Load dữ liệu vào data
            dynamic data = THONGKE.LaySoLieuThongKeHomNay(homNay);
            //Hiển thị thông số của trường dữ liệu
            chrThongKeMatHang.Series["MatHang"].IsValueShownAsLabel = true;

            //Chạy vòng lặp để show dữ liệu
            foreach (dynamic item in data)
            {
                chrThongKeMatHang.Series["MatHang"].Points.AddXY(item.MatHang, item.SoLuong);
            }
        }

        private void TaiBieuDoTheoThang(string thang, string nam)
        {
            chrThongKeMatHang.Series["MatHang"].Points.Clear();
            dynamic data = THONGKE.LaySoLieuThongKeTheoThang(thang, nam);
            chrThongKeMatHang.Series["MatHang"].IsValueShownAsLabel = true;
            foreach (dynamic item in data)
            {
                chrThongKeMatHang.Series["MatHang"].Points.AddXY(item.MatHang, item.SoLuong);
            }
        }

        /**
         * Tải biểu đồ thống kê theo phong
         */
        private void TaiBieuDoPhongHomNay(string homNay)
        {
            //Xóa biểu đồ để vẽ lại
            chrPhong.Series["Phong"].Points.Clear();
            //Load dữ liệu vào data
            dynamic data = THONGKE.LaySoLieuThongKePhongHomNay(homNay);
            //Hiển thị thông số của trường dữ liệu
            chrPhong.Series["Phong"].IsValueShownAsLabel = true;

            //Chạy vòng lặp để show dữ liệu
            foreach (dynamic item in data)
            {
                chrPhong.Series["Phong"].Points.AddXY(item.TenPhong, item.SoLanSD);
            }
        }

        private void TaiBieuDoPhongTheoThang(string thang, string nam)
        {
            chrPhong.Series["Phong"].Points.Clear();
            dynamic data = THONGKE.LaySoLieuThongKePhongTheoThang(thang, nam);
            chrPhong.Series["Phong"].IsValueShownAsLabel = true;
            foreach (dynamic item in data)
            {
                chrPhong.Series["Phong"].Points.AddXY(item.TenPhong, item.SoLanSD);
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
                btnThongKe.Enabled = false;
                cboThang.Text = "";
                cboNam.Text = "";
                //Thay thế biến bằng date.ToString format theo sql
                //string homNay = "2021-10-28";
                string homNay = DateTime.Now.ToString("yyyy-MM-dd");
                //Load biểu đồ
                TaiBieuDoHomNay(homNay);
                TaiBieuDoPhongHomNay(homNay);
                //Load danh sách
                lstvDSHoaDon.Clear();
                DSHOADON = THONGKE.LayDanhSachHoaDonHomNay(homNay);
                TaoListView(lstvDSHoaDon);
                TaiDuLieuLenListView(lstvDSHoaDon, DSHOADON);

                //Load thông tin lên các text box
                txtSKH.Text = THONGKE.LayTongSoKhachHangHomNay(homNay).ToString();
                txtSMH.Text = THONGKE.LayTongSanPhamBanHomNay(homNay).ToString();
                txtTDT.Text = THONGKE.LayTongTienHomNay(homNay).ToString("#,### VNĐ");
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
            if (!thang.Equals("") && nam.Equals(""))
            {
                MessageBox.Show("Phải chọn đẩy đủ thông tin để thống kê", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNam.Text = nam = DateTime.Now.Year.ToString();
            }
            //LoadBieuDo
            TaiBieuDoTheoThang(thang, nam);
            TaiBieuDoPhongTheoThang(thang, nam);
            //Load danh sách
            lstvDSHoaDon.Clear();
            DSHOADON = THONGKE.LayDanhSachHoaDonTheoThangNam(thang, nam);
            TaoListView(lstvDSHoaDon);
            TaiDuLieuLenListView(lstvDSHoaDon, DSHOADON);

            //Load thông tin lên các text box
            txtSKH.Text = THONGKE.LayTongSoKhachHangTheoThangNam(thang, nam).ToString();
            txtSMH.Text = THONGKE.LayTongSanPhamBanTheoThangNam(thang, nam).ToString();
            txtTDT.Text = THONGKE.LayTongTienTheoThangNam(thang, nam).ToString("#,### VNĐ");
        }

        /**
         * Sử lý sự kiện chọn comboBox Tháng => Chọn tháng
         */
        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoHomNay.Checked = false;
            btnThongKe.Enabled = true;
        }

        /**
         * Sử lý sự kiện chọn comboBox Năm => Chọn năm
         */
        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoHomNay.Checked = false;
            btnThongKe.Enabled = true;
            if (Convert.ToInt32(cboNam.Text) > Convert.ToInt32(DateTime.Now.Year.ToString()))
            {
                MessageBox.Show("Không được chọn năm lớn hơn năm hiện tại", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNam.Text = DateTime.Now.Year.ToString();
            }
        }

        private void lstvDSHoaDon_DoubleClick(object sender, EventArgs e)
        {
            var item = lstvDSHoaDon.SelectedItems[0];
            string maHD = item.SubItems[0].Text;
            bool statusBtn = false;
            //MessageBox.Show(maHD);
            frmHoaDon frm = new frmHoaDon(maHD, statusBtn, null);
            frm.Show();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            frmTraCuu frm = new frmTraCuu();
            frm.Show();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (lstvDSHoaDon.Items.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "Excel work|*.xls", ValidateNames = true })
                {
                    if (sdf.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)app.ActiveSheet;
                        app.Visible = false;
                        ws.Cells[1, 1] = "Mã hóa đơn";
                        ws.Cells[1, 2] = "Tên phòng";
                        ws.Cells[1, 3] = "Tên khách hàng";
                        ws.Cells[1, 4] = "Tổng tiền";
                        ws.Cells[1, 5] = "Ngày lập";

                        int i = 2;
                        foreach (ListViewItem item in lstvDSHoaDon.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[0].Text;
                            ws.Cells[i, 2] = item.SubItems[1].Text;
                            ws.Cells[i, 3] = item.SubItems[2].Text;
                            ws.Cells[i, 4] = item.SubItems[3].Text;
                            ws.Cells[i, 5] = item.SubItems[4].Text;

                            i++;
                        }

                        wb.SaveAs(sdf.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false,
                                  XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing,
                                  Type.Missing);
                        app.Quit();
                        MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
