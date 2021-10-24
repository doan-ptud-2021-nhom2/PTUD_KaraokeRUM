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
        private string maQL;
        
        public frmTrangChu(string maQL)
        {
            InitializeComponent();
            this.maQL = maQL;
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
            OpenFormInPanel(new frmHome());
            Console.WriteLine(maQL);
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmPhong());
        }

        private void btnQLP_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyPhong());
        }

        private void btnQLTB_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyThietBi());
        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new frmQuanLyMatHang());
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
            OpenFormInPanel(new frmDoiMatKhau());
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
