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
    public partial class frmQuanLyPhong : Form
    {
        public frmQuanLyPhong()
        {
            InitializeComponent();
        }

        /** 
        * Các biến toàn cục.
        * p: class Phòng.
        * lp: class Loại Phòng.
        * hl: class Hỗn Loạn.
        */
        private clsPhong p;
        private clsLoaiPhong lp;
        private clsHonLoan hl;

        private void frmQuanLyPhong_Load(object sender, EventArgs e)
        {
            // Gán thuộc tính cho combobox
            cboTrangThai.Items.Add("On");
            cboTrangThai.Items.Add("Off");
            cboLoaiPhong.Items.Add("VIP");
            cboLoaiPhong2.Items.Add("VIP");
            cboLoaiPhong.Items.Add("THƯỜNG");
            cboLoaiPhong2.Items.Add("THƯỜNG");

            TaoTieuDeCot(lstvDanhSachPhong);
            p = new clsPhong();
            lp = new clsLoaiPhong();
            hl = new clsHonLoan();
            IEnumerable<dynamic> dsP = hl.LayPhongVaLoaiPhong();
            TaiDuLieuLenListView(lstvDanhSachPhong, dsP);

            txtTimKiemThongTinPhong.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemThongTinPhong.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /** 
         * Xử lý combobox LoaiPhong
         * Xử lý combobox LoaiPhong2
        */
        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            XuLyGiaPhuHopVoiLoaiPhong();
        }
        private void cboLoaiPhong2_SelectedIndexChanged(object sender, EventArgs e)
        {
            XuLyGiaPhuHopVoiLoaiPhong2();
        }

        void XuLyGiaPhuHopVoiLoaiPhong()
        {
            if (cboLoaiPhong.SelectedIndex == -1)
            {
                txtGiaPhong.Text = "";
            }
            else if (cboLoaiPhong.SelectedIndex == 0)
            {
                txtGiaPhong.Text = "250000.00";
            }
            else if (cboLoaiPhong.SelectedIndex == 1)
            {
                txtGiaPhong.Text = "150000.00";
            }
        }
        void XuLyGiaPhuHopVoiLoaiPhong2()
        {
            if (cboLoaiPhong2.SelectedIndex == -1)
            {
                txtGiaPhongCu.Text = "";
            }
            else if (cboLoaiPhong2.SelectedIndex == 0)
            {
                txtGiaPhongCu.Text = "250000.00";
            }
            else if (cboLoaiPhong2.SelectedIndex == 1)
            {
                txtGiaPhongCu.Text = "150000.00";
            }
        }

        /** 
         * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lstv)
        {
            lstv.Columns.Add("Số Phòng", 100);
            lstv.Columns.Add("Tên Phòng", 120);
            lstv.Columns.Add("Trạng thái", 100);
            lstv.Columns.Add("Loại Phòng", 120);
            lstv.Columns.Add("Giá Phòng", 230);

            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
        }

        /** 
         * Load dữ liệu lên ListView
        */
        void TaiDuLieuLenListView(ListView lstv, IEnumerable<dynamic> dsPhong)
        {
            lstv.Items.Clear();
            ListViewItem itemPhong;
            foreach (dynamic ds in hl.LayPhongVaLoaiPhong())
            {
                itemPhong = TaoItem(ds);
                lstv.Items.Add(itemPhong);
            }
        }
        ListViewItem TaoItem(dynamic itemP)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(itemP.MaPhong);
            lstvItem.SubItems.Add(itemP.TenPhong);
            lstvItem.SubItems.Add(itemP.TrangThaiPhong);
            lstvItem.SubItems.Add(itemP.TenLoaiPhong);
            lstvItem.SubItems.Add(itemP.Gia.ToString());

            lstvItem.Tag = itemP;
            return lstvItem;
        }

        /** 
        * Load dữ liệu ngược lại từ ListView sang các textbox và combobox.
        */
        private void lstvDanhSachPhong_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dynamic dsP = null;
            if (lstvDanhSachPhong.SelectedItems.Count > 0)
            {
                dsP = (dynamic)lstvDanhSachPhong.SelectedItems[0].Tag;
                TaiDuLieuTuLstvDenTxtCbo(dsP);
                TaiDuLieuTuLstvDenTxtCbo2(dsP);
            }
        }
        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsP)
        {
            txtSoPhong.Text = dsP.MaPhong;
            cboTrangThai.Text = dsP.TrangThaiPhong;
            cboLoaiPhong.Text = dsP.TenLoaiPhong;
            txtGiaPhong.Text = dsP.Gia.ToString();
        }
        void TaiDuLieuTuLstvDenTxtCbo2(dynamic dsP)
        {
            cboLoaiPhong2.Text = dsP.TenLoaiPhong;
            txtGiaPhongCu.Text = dsP.Gia.ToString();
        }

        /** 
        * Thêm thông tin phòng
        */
        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            Phong phong = TaoPhong();
            p.ThemPhong(phong);
            TaiDuLieuLenListView(lstvDanhSachPhong, p.LayTatCaPhong());
        }
        /** 
        * Tạo mã phòng tự tăng
        */
        private string TaoMaPhong()
        {
            string maPhong = "";
            int dem = p.LayTatCaPhong().Count() + 1;
            if(dem < 10)
            {
                maPhong += "P00" + dem;
            }
            else
            {
                maPhong += "P0" + dem;
            }

            return maPhong;
        }
        /** 
        * Tạo mã phòng (số phòng là tên của phòng)
        */
        dynamic TaoPhong()
        {
            Phong phong = new Phong();
            
            phong.MaPhong = TaoMaPhong();
            phong.TenPhong = txtSoPhong.Text; 
            phong.TrangThaiPhong = cboTrangThai.Text;
            phong.MaLoaiPhong = lp.TimLoaiPhong(cboLoaiPhong.Text).First().MaLoaiPhong;
            phong.MaQL = "NV002";

            return phong;
        }

        /** 
        * Sửa giá của phòng 
        */
        LoaiPhong SuaGiaLoaiPhong()
        {
            LoaiPhong loaiPhong = new LoaiPhong();
            //lp.MaPhong = txtSoPhong.Text;
            //lp.TrangThaiPhong = cboTrangThai.Text;
            //TimPhong();
            //lp.TenLoaiPhong = cboLoaiPhong.Text;
            loaiPhong.Gia = Convert.ToDecimal(txtGiaPhongMoi.Text);

            return loaiPhong;
        }

        /** 
        * Cập nhật giá phòng cũ thành giá phòng mới
        */
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoaiPhong suaLP = SuaGiaLoaiPhong();
            lp.CapNhatGiaLoaiPhong(suaLP);
            IEnumerable<dynamic> layDS = hl.LayPhongVaLoaiPhong();
            TaiDuLieuLenListView(lstvDanhSachPhong, layDS);
        }


        /** 
        * Xóa Phòng
        */
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dynamic phong;
            int viTriThuc;
            IEnumerable<dynamic> dsPhongHL = hl.LayPhongVaLoaiPhong();

            if (lstvDanhSachPhong.SelectedItems.Count > 0)
            {
                dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if(dr == DialogResult.Yes)
                {
                    for (int i = 0; i < lstvDanhSachPhong.SelectedItems.Count; i++)
                    {
                        viTriThuc = lstvDanhSachPhong.SelectedIndices[i];
                        phong = (dynamic)lstvDanhSachPhong.Items[viTriThuc].Tag;
                        p.XoaPhong(phong);
                    }

                    TaiDuLieuLenListView(lstvDanhSachPhong, dsPhongHL);
                }
            }
        }


        /** 
        * Tim kiếm
        */
        private void btnTimKiemThongTinPhong_Click(object sender, EventArgs e)
        {
            // string strInfo = txtTimKiemThongTinPhong.Text;
            int viTriTim = TimKiem();
            int viTriChon;
            if(viTriTim >= 0)
            {
                if (lstvDanhSachPhong.SelectedItems.Count > 0)
                {
                    viTriChon = lstvDanhSachPhong.SelectedIndices[0];
                    lstvDanhSachPhong.Items[viTriChon].Selected = false;
                }
                lstvDanhSachPhong.Items[viTriTim].Selected = true;
                lstvDanhSachPhong.Focus();
            }
        }
        int TimKiem()
        {
            dynamic lp;
            for(int i = 0; i < lstvDanhSachPhong.Items.Count; i++)
            {
                lp = lstvDanhSachPhong.Items[i].Tag;
                if(rdoVIP.Checked)
                {
                    if(lp.TenLoaiPhong.Equals("VIP"))
                        return i;
                }
                else
                {
                    if (lp.TenLoaiPhong.Equals("THƯỜNG"))
                        return i;
                }
            }
            return -1;
        }

        private void rdoVIP_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoVIP.Checked)
             {
                 txtTimKiemThongTinPhong.Text = "VIP";
             }
             else
             {
                 txtTimKiemThongTinPhong.Text = "THƯỜNG";
             }
            /*if (rdoVIP.Checked)
            {
                txtTimKiemThongTinPhong.AutoCompleteCustomSource.Clear();
                txtTimKiemThongTinPhong.Text = "VIP";
                foreach (LoaiPhong i in dsLoaiPhong)
                {
                    txtTimKiemThongTinPhong.AutoCompleteCustomSource.Add(i.TenLoaiPhong);
                }
            }*/
        }
        private void rdoTHUONG_CheckedChanged(object sender, EventArgs e)
        {
            /*if (rdoTHUONG.Checked)
            {
                txtTimKiemThongTinPhong.AutoCompleteCustomSource.Clear();
                txtTimKiemThongTinPhong.Text = "THƯỜNG";
                foreach (LoaiPhong i in dsLoaiPhong)
                {
                    txtTimKiemThongTinPhong.AutoCompleteCustomSource.Add(i.TenLoaiPhong);
                }
            }*/
        }

    }
}
