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
    public partial class frmDangNhap : Form
    {
        public TaiKhoan tk;
        public static string maQL;//Lấy mã từ username để các form khác sử dụng.
        private clsTaiKhoan qlTaiKhoan;
        //biến đếm dùng để đếm số lần đăng nhập sai, sai 3 lần thì gợi ý lấy lại mật khẩu.
        private int dem = 0;

        public frmDangNhap()
        {
            InitializeComponent();
            qlTaiKhoan = new clsTaiKhoan();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            maQL = txtUsername.Text;
            dem++;
            tk = new TaiKhoan()
            {
                UserName = txtUsername.Text,
                PassWord = txtPassword.Text
            };

            frmTrangChuQL frmQL = new frmTrangChuQL();
            frmTrangChu frmNV = new frmTrangChu();
            frmLayLaiMatKhau frmLLMK = new frmLayLaiMatKhau();

            if (qlTaiKhoan.KiemTraTaiKhoan(tk))
            {
                
                if (qlTaiKhoan.LayLoaiTaiKhoan(tk).Equals("LNV01"))
                {
                    
                    frmQL.Show();
                    this.Hide();
                }

                else if(qlTaiKhoan.LayLoaiTaiKhoan(tk).Equals("LNV02"))
                {
                    frmNV.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Tài khoản không được phân quyền đễ thực hiện chức năng này!!", 
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (txtUsername.Text.Trim().Equals("") || txtPassword.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không được để trống!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Xử lý đăng nhập sai quá 3 lần
            if(dem == 3)
            {
                DialogResult luaChon = MessageBox.Show("Số lần đăng nhập đã hết. Bạn có muốn lấy lại mật khẩu?"
                    , "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(luaChon == DialogResult.Yes)
                {
                    frmLLMK.Show();
                    this.Hide();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void lblLayLaiMatKhau_Click(object sender, EventArgs e)
        {
            using (frmLayLaiMatKhau frm = new frmLayLaiMatKhau())
            {
                this.Hide(); // an form dang nhap hien tai
                frm.ShowDialog(); // hien form lay lai mat khau va pause o dong code nay toi khi form laylaimk bi close
                this.Show(); // khi form lay lai mk bi close thi no chay toi dong nay va hien form dang nhap len lai
            }
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(luaChon == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
