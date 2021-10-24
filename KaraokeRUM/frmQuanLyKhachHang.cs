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
    public partial class frmQuanLyKhachHang : Form
    {
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            var dbkh = new clsKetNoi().LayData();
            var data = from kh in dbkh.KhachHangs
                       select new
                       {
                           ten = kh.TenKhach,
                           makh = kh.MaKH,
                           sld = kh.SoLanDen,
                           sdt = kh.SDT
                       };
            foreach(var kh in data) {
                this.lvwDSKH.View = View.Details;             
                lvwDSKH.BeginUpdate();
                string[] row = { kh.ten, kh.sdt, kh.makh, kh.sld.ToString() };
                var listViewItem = new ListViewItem(row);
                this.lvwDSKH.Items.Add(listViewItem);
                lvwDSKH.EndUpdate();               
            }
        }
    }
}
