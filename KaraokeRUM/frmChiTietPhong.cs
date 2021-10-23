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
    public partial class frmChiTietPhong : Form
    {
        public frmChiTietPhong()
        {
            InitializeComponent();
        }

        private void btnDoiPhong_Click(object sender, EventArgs e)
        {
            frmDanhSachPhong frm = new frmDanhSachPhong();
            frm.Show();
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            frm.Show();
        }
    }
}
