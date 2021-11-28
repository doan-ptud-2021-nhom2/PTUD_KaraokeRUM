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
    public partial class frmDanhSachPhong : Form
    {
        public frmDanhSachPhong()
        {
            InitializeComponent();
        }

        /*
         * Các biến toàn cục 
         */
        private clsPhong PHONG;
        private clsTaoButton TAOBUTTON;
        private clsHoaDon HOADON;
        private IEnumerable<Phong> DANHSACHPHONGVIP;
        private IEnumerable<Phong> DANHSACHPHONGTHUONG;
        private string MAHOADON;
        private frmChiTietPhong frmCTP;
        private Phong PHONGCU;

        /*
         * Constructor
         */
        public frmDanhSachPhong(string maHoaDon, Phong phongCu, frmChiTietPhong _frmCTP)
        {
            InitializeComponent();
            this.MAHOADON = maHoaDon;
            frmCTP = _frmCTP;
            PHONGCU = phongCu;
        }

        /*
         * Form chính 
         */
        private void frmDanhSachPhong_Load(object sender, EventArgs e)
        {
            PHONG = new clsPhong();
            TAOBUTTON = new clsTaoButton();
            HOADON = new clsHoaDon();

            fpnlPhongVip.Controls.Clear();
            fpnlPhongThuong.Controls.Clear();

            DANHSACHPHONGVIP = PHONG.LayDSPhongTheoLoai("LP001");
            DANHSACHPHONGTHUONG = PHONG.LayDSPhongTheoLoai("LP002");
            TaoPhongVip(DANHSACHPHONGVIP);
            TaoPhongThuong(DANHSACHPHONGTHUONG);
        }

        /** 
         * Tạo phòng Vip 
         */
        void TaoPhongVip(IEnumerable<Phong> phongVip)
        {
            Button btn;
            for (int i = 0; i < phongVip.Count(); i++)
            {
                btn = new Button();
                btn.Name = "btnVip" + (i + 1).ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.Text = phongVip.ElementAt(i).TenPhong;
                btn.BackColor = Color.Teal;
                btn.ForeColor = Color.White;
                foreach (dynamic item in TAOBUTTON.LayTrangThaiPhong("LP001"))
                {
                    if (btn.Text.Equals(item.TenPhong))
                    {
                        if (item.TrangThaiPhong == "Mở")
                        {
                            btn.BackColor = Color.Teal;
                        }
                        else if (item.TrangThaiPhong == "Đặt")
                        {
                            btn.BackColor = Color.Orange;
                        }
                        else if (item.TrangThaiPhong == "Đóng")
                        {
                            btn.BackColor = Color.Gray;
                        }
                    }
                }
                btn.Click += new EventHandler(SuKienChonNutTheoMau);
                fpnlPhongVip.Controls.Add(btn);
            }
        }

        /** 
         * Tạo phòng Thường
         */
        void TaoPhongThuong(IEnumerable<Phong> phongThuong)
        {
            Button btn;
            for (int i = 0; i < phongThuong.Count(); i++)
            {
                btn = new Button();
                btn.Name = "btnThuong" + (i + 1).ToString();
                btn.Width = 65;
                btn.Height = 50;
                btn.ForeColor = Color.White;
                btn.Text = phongThuong.ElementAt(i).TenPhong;
                btn.BackColor = Color.Teal;
                foreach (var item in TAOBUTTON.LayTrangThaiPhong("LP002"))
                {
                    if (btn.Text.Equals(item.TenPhong))
                    {
                        if (item.TrangThaiPhong == "Mở")
                        {
                            btn.BackColor = Color.Teal;
                        }
                        else if (item.TrangThaiPhong == "Đặt")
                        {
                            btn.BackColor = Color.Orange;
                        }
                        else if (item.TrangThaiPhong == "Đóng")
                        {
                            btn.BackColor = Color.Gray;
                        }
                    }
                }
                btn.Click += new EventHandler(SuKienChonNutTheoMau);
                fpnlPhongThuong.Controls.Add(btn);
            }
        }

        /** 
         * Xử lý chọn button theo màu
         */
        private void SuKienChonNutTheoMau(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Phong phong;
            HoaDon hoaDon;
            string maPhong;

            if (btn.BackColor == Color.Gray)
            {
                DialogResult hoiChon;
                hoiChon = MessageBox.Show("Bạn có muốn chuyển sang phòng này không?", "Thông báo", 
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(hoiChon == DialogResult.Yes)
                {
                    hoaDon = HOADON.LayHoaDon(MAHOADON);
                    phong = PHONG.TimMaPhong(btn.Text);
                    maPhong = phong.MaPhong;
                    hoaDon.MaPhong = maPhong;

                    btn.BackColor = Color.Teal;
                    phong.TrangThaiPhong = "Mở";
                    HOADON.CapNhatDoiPhong(hoaDon);
                    PHONG.SuaTrangThaiPhong(phong);

                    PHONGCU.TrangThaiPhong = "Đóng";
                    PHONG.SuaTrangThaiPhong(PHONGCU);

                    MessageBox.Show("Chuyển phòng thành công.", "Thông báo", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string LogDetail = string.Format("từ phòng [{0}] dang phòng [{1}]", PHONGCU.MaPhong, phong.MaPhong);
                    Logger.LogWritter.Write("Thu ngân thay đổi phòng cho khách "+LogDetail+"- Phong, HoaDon");
                    //Thực hiển chuyển phòng và tắt 2 from
                    this.Close();
                    frmCTP.Close();
                }
            }
            else if(btn.BackColor == Color.Teal)
            {
                MessageBox.Show("Phòng đang mở, vui lòng chọn phòng khác!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(btn.BackColor == Color.Orange)
            {
                MessageBox.Show("Phòng đã đặt, vui lòng chọn phòng khác!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
