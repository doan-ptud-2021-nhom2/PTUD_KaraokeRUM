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
    public partial class frmTimKiemKhachHang : Form
    {
        private clsKhachHang KHACHHANG;
        private clsHoaDon HOADON;
        private clsPhong PHONG;
        public frmTimKiemKhachHang()
        {
            InitializeComponent();
        }
        private void frmTimKiemKhachHang_Load(object sender, EventArgs e)
        {
            KHACHHANG = new clsKhachHang();
            HOADON = new clsHoaDon();
            PHONG = new clsPhong();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text;
            KhachHang khachHang = KHACHHANG.TimKhachHang(sdt);
            HoaDon hoaDon = HOADON.TimHoaDonTheoMaKhachHang(khachHang.MaKH);
            Phong phong = PHONG.TimMotPhongTheoMa(hoaDon.MaPhong);
            string tenKhachHang = khachHang.TenKhach;
            string tenPhong = phong.TenPhong;
            string kqua = "Khách hàng:" + tenKhachHang + "\nPhòng sử dụng :"+ tenPhong;
            MessageBox.Show(kqua, "Thông tin tìm kiếm", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
