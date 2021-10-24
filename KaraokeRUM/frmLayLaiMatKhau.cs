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
    public partial class frmLayLaiMatKhau : Form
    {
        /**
         * Khai báo các biến toàn cục sử dụng trong class
         */
        private clsTaiKhoan qlTaiKhoan;

        public frmLayLaiMatKhau()
        {
            InitializeComponent();
            qlTaiKhoan = new clsTaiKhoan();
        }

        /**
         * Sự kiện sử lý khi click vào label quay vềđăng nhập
         */
        private void lblDangNhap_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        /**
         * Sự kiện sử lý Lấy lại mật khẩu.
         */
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTaiKhoan.Text;
            string sdt = txtSDT.Text;
            if (tenDangNhap.Equals("") || sdt.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin để lấy lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string matKhau = qlTaiKhoan.TimMatKhau(tenDangNhap.Trim(), sdt.Trim());
                if (!matKhau.Equals(""))
                {
                    string thongTin = "Thông tin tài khoản của bạn:\nTên đăng nhập: " +
                                        tenDangNhap + "\n" + "Mật khẩu: " + matKhau;
                    MessageBox.Show(thongTin, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thông tin nhập vào sai hoặc tài khoản người dùng này không tồn tại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /**
         * Sự kiện sử lý chức năng thoát
         */
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (luaChon == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
            }
           
        }

        private void frmLayLaiMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
