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
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            frmTrangChu frm = new frmTrangChu();
            frm.Show();
            this.Hide();
        }

        private void lblLayLaiMatKhau_Click(object sender, EventArgs e)
        {
            frmLayLaiMatKhau frm = new frmLayLaiMatKhau();
            frm.Show();
            this.Hide();
        }
    }
}
