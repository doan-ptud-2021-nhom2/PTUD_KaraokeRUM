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
            if (!KiemTraSoDienThoai())
            {
                MessageBox.Show("Yêu cầu nhập đúng số điện thoại khách hàng ", "Thông báo",
                                MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if(txtSDT.Text=="")
            {
                MessageBox.Show("Yêu cầu nhập số điện thoại khách hàng ", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
            else
            {
                string sdt = txtSDT.Text;
                KhachHang khachHang = KHACHHANG.TimKhachHang(sdt);
                if (khachHang == null)
                {
                    MessageBox.Show("Không tìm thấy khách hàng!", "Thông tin tìm kiếm", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HoaDon hoaDon = HOADON.TimHoaDonTheoMaKhachHang(khachHang.MaKH);
                    if(hoaDon == null)
                    {
                        MessageBox.Show("Khách hàng hiện không sử dụng phòng nào!", "Thông tin tìm kiếm",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Phong phong = PHONG.TimMotPhongTheoMa(hoaDon.MaPhong);
                        string tenKhachHang = khachHang.TenKhach;
                        string tenPhong = phong.TenPhong;
                        string kqua = "Khách hàng: " + tenKhachHang + "\nPhòng sử dụng : " + tenPhong;
                        MessageBox.Show(kqua, "Thông tin tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }                         
        }
        private void txtSoDienThoai_Validating(object sender, CancelEventArgs e)
        {
            string soDienThoai = txtSDT.Text;
            if (!clsKiemTra.KiemTraSDT(soDienThoai))
            {
                txtSDT.Focus();
                errSoDienThoai.SetError(txtSDT, "Số điện thoại chưa hợp lý!. Ví dụ: 0879276284");
            }
            else
            {
                errSoDienThoai.SetError(txtSDT, null);
            }
        }
        private bool KiemTraSoDienThoai()
        {
            string soDienThoai = txtSDT.Text;
            if (!clsKiemTra.KiemTraSDT(soDienThoai))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", 
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                errSoDienThoai.SetError(txtSDT, null);
                this.Close();
            }
                
        }
    }
}
