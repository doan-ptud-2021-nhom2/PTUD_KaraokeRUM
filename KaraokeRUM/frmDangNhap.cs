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
        /**
         * Khai báo các biến trong class
         * maQL: Lấy mã từ username để các form khác sử dụng.
         * dem: dùng để đếm số lần đăng nhập sai, sai 3 lần thì gợi ý lấy lại mật khẩu.
         */
        private TaiKhoan tk;
        public static string maQL;
        private clsTaiKhoan qlTaiKhoan;
        private int dem = 0;

        public frmDangNhap()
        {
            InitializeComponent();
            qlTaiKhoan = new clsTaiKhoan();
        }


        /**
         * Sự kiện sử lý đăng nhập
         */
        private void btnDN_Click(object sender, EventArgs e)
        {
            maQL = txtUsername.Text;
            dem++;
            tk = new TaiKhoan()
            {
                UserName = txtUsername.Text,
                PassWord = txtPassword.Text
            };
 
            frmLayLaiMatKhau frmLLMK = new frmLayLaiMatKhau();
            frmTrangChu frmNV = new frmTrangChu(maQL);
            frmTrangChuQL frmQL = new frmTrangChuQL(maQL);
            if (qlTaiKhoan.KiemTraTaiKhoan(tk))
            {
                
                if (qlTaiKhoan.LayLoaiTaiKhoan(tk).Equals("LNV01"))
                {
                    this.Hide();
                    
                    
                    if (frmQL.ShowDialog() == DialogResult.Yes)
                        this.Close();
                    else
                    {
                        this.Show();
                        dem = 0;
                    }    
                }

                else if(qlTaiKhoan.LayLoaiTaiKhoan(tk).Equals("LNV02"))
                {
                    this.Hide();
                    
                    if (frmNV.ShowDialog() == DialogResult.Yes)
                        this.Close();
                    else
                    {
                        this.Show();
                        dem = 0;
                    }
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
                    frmLayLaiMatKhau frm = new frmLayLaiMatKhau();
                    DongForm(frm);
                }
                else
                {
                    this.Close();
                }
            }
        }

        /**
         * Sự kiện sử lý khi click vào label lấy lại mật khẩu.
         */

        private void lblLayLaiMatKhau_Click(object sender, EventArgs e)
        {
            frmLayLaiMatKhau frm = new frmLayLaiMatKhau();
            DongForm(frm);
            
        }

        /**
         * Hàm hỗ trợ cho việc đóng form
         */
        private void DongForm(Form frm)
        {
            this.Hide();//this->FormDangNhap
            DialogResult chonDong = frm.ShowDialog();
            if (chonDong == DialogResult.Yes)
                this.Close();//this->frm
            else
                this.Show();
        }

        /**
         * Sự kiện sử lý chức năng thoát.
         */
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (luaChon == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
