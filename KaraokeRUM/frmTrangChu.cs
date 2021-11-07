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
    public partial class frmTrangChu : Form
    {
        private string MAQL;
        private clsNhanVien NHANVIEN;
        private clsLoaiNhanVien LOAINV;

        public frmTrangChu(string maQL)
        {
            InitializeComponent();
            MAQL = maQL;
        }

        private void OpenFormInPanel(object Formhijo)
        {
            if (this.panel_workarea.Controls.Count > 0)
            {
                this.panel_workarea.Controls.RemoveAt(0);
            }
            Form fm = Formhijo as Form;
            fm.TopLevel = false;
            fm.Dock = DockStyle.Fill;
            fm.WindowState = FormWindowState.Normal;
            this.panel_workarea.Controls.Add(fm);
            this.panel_workarea.Tag = fm;
            fm.Show();
        }
        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            //Khai báo các biến
            NHANVIEN = new clsNhanVien();
            LOAINV = new clsLoaiNhanVien();

            //Tự động mở form Home
            OpenFormInPanel(new frmHome());

            //Lấy thông tin nhân viên.
            NhanVien thuNgan = NHANVIEN.TimNhanVienTheoMa(MAQL);
            string tenNhanVien = thuNgan.TenNV;
            string chucVu = LOAINV.TimLoaiNVTheoMa(thuNgan.MaLNV).TenLNV;

            //Cập nhật thông tin vào label
            lblChucVu.Text = chucVu;
            lblTenNV.Text = tenNhanVien;
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmPhong(MAQL));
        }

        private void btnQLP_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyPhong(MAQL));
        }

        private void btnQLTB_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyThietBi(MAQL));
        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyMatHang(MAQL));
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmThongKe());
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmHome());
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmDoiMatKhau(MAQL));
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Thông báo",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(luaChon == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.No;
            }
        }
    }
}
