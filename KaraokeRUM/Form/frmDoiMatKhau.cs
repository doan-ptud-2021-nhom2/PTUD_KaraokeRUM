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
    public partial class frmDoiMatKhau : Form
    {
        /**
         * Khai báo các biến trong class
         */
        private clsTaiKhoan TAIKHOAN;
        private string MAQL;

        public frmDoiMatKhau(string maQL)
        {
            InitializeComponent();
            this.MAQL = maQL;
        }

        /**
         * Sự kiện form load khi form được khởi chạy
         */
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            TAIKHOAN = new clsTaiKhoan();
        }

      
        /**
         * Hàm tạo tài khoản hỗ trợ cho việc đổi mật khẩu.
         */
        private TaiKhoan TaoTaiKhoan()
        {
            TaiKhoan tk = new TaiKhoan();
            tk.UserName = MAQL;
            tk.PassWord = txtNLMK.Text;
            return tk;
        }

        /**
         * Sự kiện sử lý đổi mật khẩu.
         */
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string txtMatKhauHienTai = txtMKHT.Text.Trim();
            string txtMatKhauMoi = txtMKM.Text.Trim();
            string txtMatKhauNhapLai = txtNLMK.Text.Trim();

            if(String.IsNullOrEmpty(txtMatKhauHienTai) && !String.IsNullOrEmpty(txtMatKhauMoi) && 
               !String.IsNullOrEmpty(txtMatKhauNhapLai))
            {
                MessageBox.Show("Mật khẩu hiện tại không được để trống. Vui lòng nhập đẩy đủ thông tin!","Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!String.IsNullOrEmpty(txtMatKhauHienTai) && String.IsNullOrEmpty(txtMatKhauMoi) && 
                    !String.IsNullOrEmpty(txtMatKhauNhapLai))
            {
                MessageBox.Show("Mật khẩu mới không được để trống. Vui lòng nhập đẩy đủ thông tin!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(!String.IsNullOrEmpty(txtMatKhauHienTai) && !String.IsNullOrEmpty(txtMatKhauMoi) && 
                    String.IsNullOrEmpty(txtMatKhauNhapLai))
            {
                MessageBox.Show("Mật khẩu nhập lại không được để trống. Vui lòng nhập đẩy đủ thông tin!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if(String.IsNullOrEmpty(txtMatKhauHienTai) && String.IsNullOrEmpty(txtMatKhauMoi) && 
                    String.IsNullOrEmpty(txtMatKhauNhapLai))
            {
                MessageBox.Show("Vui lòng nhập đẩy đủ thông tin để thực hiện chức năng!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if(String.IsNullOrEmpty(txtMatKhauHienTai) || String.IsNullOrEmpty(txtMatKhauMoi) || 
                    String.IsNullOrEmpty(txtMatKhauNhapLai))
            {
                MessageBox.Show("Vui lòng nhập đẩy đủ thông tin để thực hiện chức năng!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!TAIKHOAN.TimMatKhau(MAQL).Equals(txtMatKhauHienTai))
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu hiện tại để thực hiện chức năng!", "Thông báo",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (!txtMatKhauMoi.Equals(txtMatKhauNhapLai))
                    {
                        MessageBox.Show("Mật khẩu nhập lại phải đúng với mật khẩu mới!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        TaiKhoan tk = TaoTaiKhoan();
                        if (TAIKHOAN.DoiMatKhau(tk) == 1)
                        {
                            MessageBox.Show("Mật khẩu được đổi thành công!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamMoiInput();
                            Logger.LogWritter.Write("Nhân viên đổi mật khẩu");

                        }
                        else 
                        {
                            MessageBox.Show("Quá trình đổi mật khẩu thất bại!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
        }

        /**
         * Hàm xử lý làm mới input sau khi đổi mật khẩu thành công
         */
        private void LamMoiInput()
        {
            txtMKHT.Clear();
            txtMKM.Clear();
            txtNLMK.Clear();
        }
        /*
         * Sự kiện kiểm tra input "Nhập Mật khẩu Mới" trong khi nhập
         */
        private void txtMKM_Validating(object sender, CancelEventArgs e)
        {
            string txtMatKhauMoi = txtMKM.Text;
            if (!clsKiemTra.KiemTraMatKhau(txtMatKhauMoi))
            {
                e.Cancel = true;
                txtMKM.Focus();
                errorProvider1.SetError(txtMKM, "Mật khẩu tối thiểu 8 ký tự, tối đa 20 ký tự, chứa ít nhất một ký tự in hoa");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMKM, null);
            }
        }
    }
}
