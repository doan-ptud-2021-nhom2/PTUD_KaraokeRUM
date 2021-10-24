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
    public partial class frmTrangChuQL : Form
    {
        private string maQL;
        public frmTrangChuQL()
        {
            InitializeComponent();
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
            OpenFormInPanel(new frmHome());
            maQL = frmDangNhap.maQL;
            Console.WriteLine(maQL);
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyNhanVien());
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmHome());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
            OpenFormInPanel(new frmThongKe());
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmDoiMatKhau());
        }
    }
}
