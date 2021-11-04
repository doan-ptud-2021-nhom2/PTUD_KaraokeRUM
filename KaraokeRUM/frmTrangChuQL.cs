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
    public partial class frmTrangChuQL : Form
    {
        protected static string MAQL;
        private clsNhanVien NHANVIEN;
        private clsLoaiNhanVien LOAINV;

        public frmTrangChuQL(string maQL)
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

        private void frmTrangChuCQ_Load(object sender, EventArgs e)
        {
            //Khai báo các biến
            NHANVIEN = new clsNhanVien();
            LOAINV = new clsLoaiNhanVien();

            //Tự động mở form Home
            OpenFormInPanel(new frmHome());

            //Lấy thông tin nhân viên.
            NhanVien quanLy = NHANVIEN.TimNhanVienTheoMa(MAQL);
            string tenNhanVien = quanLy.TenNV;
            string chucVu = LOAINV.TimLoaiNVTheoMa(quanLy.MaLNV).TenLNV;

            //Cập nhật thông tin vào label
            lblChucVu.Text = chucVu;
            lblTenQL.Text = tenNhanVien;
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyNhanVien(MAQL));
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmHome());
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyKhachHang());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Thông báo",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (luaChon == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmThongKeQL());
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmDoiMatKhau());
        }
    }
}
