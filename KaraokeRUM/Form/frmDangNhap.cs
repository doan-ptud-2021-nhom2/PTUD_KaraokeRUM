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
        public static string MAQL;
        private clsTaiKhoan TAIKHOAN;
        private clsNhanVien NHANVIEN;
        private int DEM = 0;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            TAIKHOAN = new clsTaiKhoan();
            NHANVIEN = new clsNhanVien();
        }

        /**
         * Sự kiện sử lý đăng nhập
         */
        private void btnDN_Click(object sender, EventArgs e)
        {
            TaiKhoan taiKhoan;
            MAQL = txtUsername.Text;
            DEM++;
            taiKhoan = new TaiKhoan()
            {
                UserName = txtUsername.Text,
                PassWord = txtPassword.Text
            };
 
            frmTrangChu frmNV = new frmTrangChu(MAQL);
            frmTrangChuQL frmQL = new frmTrangChuQL(MAQL);

            if (TAIKHOAN.KiemTraTaiKhoan(taiKhoan))
            {

                Logger.LogWritter.Admin = taiKhoan.UserName;
                if (TAIKHOAN.LayLoaiTaiKhoan(taiKhoan).Equals("LNV01") && 
                    NHANVIEN.TimNhanVienTheoMa(MAQL).TrangThai.Equals("Đang làm"))
                {
                    this.Hide();
                    Logger.LogWritter.Write("Quản lý đăng nhập vào hệ thống");
                    if (frmQL.ShowDialog() == DialogResult.Yes) 
                        this.Close(); 
                    else
                    {
                        this.Show();
                        DEM = 0;
                    }    
                }

                else if(TAIKHOAN.LayLoaiTaiKhoan(taiKhoan).Equals("LNV02") && 
                        NHANVIEN.TimNhanVienTheoMa(MAQL).TrangThai.Equals("Đang làm"))
                {
                    this.Hide();
                    Logger.LogWritter.Write("Thu ngân đăng nhập vào hệ thống");
                    if (frmNV.ShowDialog() == DialogResult.Yes)
                        this.Close();
                    else
                    {
                        this.Show();
                        DEM = 0;
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
                if (txtUsername.Text.Trim().Equals("") && !txtPassword.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Tài khoản không được để trống. Vui lòng nhập đầy đủ thông tin!!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(!txtUsername.Text.Trim().Equals("") && txtPassword.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Mật khẩu không được để trống. Vui lòng nhập đầy đủ thông tin!!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(txtUsername.Text.Trim().Equals("") && txtPassword.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Tài khoản và mật khẩu không được để trống. Vui lòng nhập đầy đủ thông tin!!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!!", 
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Xử lý đăng nhập sai quá 3 lần
            if(DEM == 3)
            {
                DialogResult luaChon = MessageBox.Show("Số lần đăng nhập đã hết. Bạn có muốn lấy lại mật khẩu?",
                                       "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn thoát", 
                                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (luaChon == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
