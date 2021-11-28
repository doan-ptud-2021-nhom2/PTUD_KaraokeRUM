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
    public partial class frmTraCuu : Form
    {
        private clsKhachHang KHACHHANG;
        public frmTraCuu()
        {
            InitializeComponent();
        }

        private void frmTraCuu_Load(object sender, EventArgs e)
        {
            KHACHHANG = new clsKhachHang();
            TaoListView(lstvKhachHang);
            btnTraCuu.Enabled = false;
        }

        /**
        * Khởi tạo listView tạo các cột
        */
        private void TaoListView(ListView lstv)
        {
            lstv.Columns.Add("Mã Khách Hàng", 150);
            lstv.Columns.Add("Tên Khách Hàng", 200);
            lstv.Columns.Add("Số Điện Thoại", 150);
            lstv.Columns.Add("Số Lần Đến", 100);
            lstv.Columns.Add("Tổng Tiền", 200);

            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /**
         * Tạo các ItemListView để thêm vào listView
         */
        private ListViewItem TaoItem(KhachHang itemKH)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemKH.MaKH);
            lstvItem.SubItems.Add(itemKH.TenKhach);
            lstvItem.SubItems.Add(itemKH.SDT);
            lstvItem.SubItems.Add(itemKH.SoLanDen.ToString());
            lstvItem.SubItems.Add(Convert.ToDouble(itemKH.TongTien).ToString("#,### VNĐ"));

            lstvItem.Tag = itemKH;
            return lstvItem;
        }

        /**
         * Hàm hỗ trợ tải dữ liệu lên listview
         */
        private void TaiDuLieuLenListView(ListView lstv, IEnumerable<KhachHang> dsKH)
        {
            lstv.Items.Clear();
            ListViewItem itemHD;
            foreach (dynamic item in dsKH)
            {
                itemHD = TaoItem(item);
                lstv.Items.Add(itemHD);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", 
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                this.Close();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text;
            IQueryable<KhachHang> khachHang = KHACHHANG.TimTenKhachHang(sdt);
            if(khachHang.Count() == 0)
            {
                lstvKhachHang.Items.Clear();
                MessageBox.Show("Khách hàng này chưa từng đến cửa hàng. Vui lòng kiểm tra lại SĐT", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TaiDuLieuLenListView(lstvKhachHang, khachHang);
            }
            
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text;
            if (!clsKiemTra.KiemTraSDT(sdt))
            {
                txtSDT.Focus();
                errorProvider1.SetError(txtSDT, "Số điện thoại phải bắt đầu từ đầu số hợp lệ. VD: 09XXXXXXXX");
                btnTraCuu.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(txtSDT, null);
                btnTraCuu.Enabled = true;
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
