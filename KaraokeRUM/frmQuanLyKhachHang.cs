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
            var dskh = new clsKhachHang().LayDSKH();
            foreach (var kh in dskh) {
                this.lvwDSKH.View = View.Details;             
                lvwDSKH.BeginUpdate();
                string[] row = { kh.TenKhach, kh.SDT, kh.MaKH, kh.SoLanDen.ToString() };
                var listViewItem = new ListViewItem(row);
                this.lvwDSKH.Items.Add(listViewItem);
                lvwDSKH.EndUpdate();               
            }
            lvwDSKH.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);// tự động căng theo chiều rộng nội dung trong column
        }
    }
}
