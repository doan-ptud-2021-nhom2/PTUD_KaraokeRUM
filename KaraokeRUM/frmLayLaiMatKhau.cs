﻿using System;
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
        clsTaiKhoan qlTaiKhoan;
        public frmLayLaiMatKhau()
        {
            InitializeComponent();
            qlTaiKhoan = new clsTaiKhoan();
        }

        private void lblDangNhap_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDangNhap frm = new frmDangNhap();
            frm.Show();
        }

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

        private void frmLayLaiMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult luaChon = MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (luaChon == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
