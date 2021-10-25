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
    public partial class frmQuanLyNhanVien : Form
    {
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            var dbnv = new clsKetNoi().LayData();
            var data = from nv in dbnv.NhanViens
                       select new
                       {
                           tenNV =nv.TenNV,
                           maNV = nv.MaNV,
                           gioiTinh = nv.GioiTinh,
                           cMND = nv.CMND,
                           sDT = nv.SDT,
                           diaChi = nv.DiaChi,
                           trangThai = nv.TrangThai,
                           maLNV = nv.MaLNV,
                       };
            foreach (var nv in data)
            {
                this.lvwDSNV.View = View.Details;
                lvwDSNV.BeginUpdate();
                string[] row = {nv.tenNV, nv.maNV, nv.gioiTinh, nv.cMND, nv.sDT, nv.diaChi, nv.trangThai, nv.maLNV};
                var listViewItem = new ListViewItem(row);
                this.lvwDSNV.Items.Add(listViewItem);
                lvwDSNV.EndUpdate();
            }
            //lvwDSNV.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);// tự động căng theo chiều rộng nội dung trong column
        }
    }
}
