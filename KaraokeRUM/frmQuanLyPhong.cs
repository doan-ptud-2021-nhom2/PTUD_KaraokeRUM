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
        private IEnumerable<Phong> dsPhong;
        private IEnumerable<LoaiPhong> dsLoaiPhong;

        private void frmQuanLyPhong_Load(object sender, EventArgs e)
        {
            // Gán thuộc tính cho combobox
            cboTrangThai.Items.Add("Mở");
            cboTrangThai.Items.Add("Đặt");
            cboTrangThai.Items.Add("Đóng");
            cboLoaiPhong.Items.Add("VIP");
            cboLoaiPhong2.Items.Add("VIP");
            cboLoaiPhong.Items.Add("THƯỜNG");
            cboLoaiPhong2.Items.Add("THƯỜNG");

            TaoTieuDeCot(lstvDanhSachPhong);
            p = new clsPhong();
            lp = new clsLoaiPhong();
            hl = new clsHonLoan();

            dsPhong = p.LayTatCaPhong();
            dsLoaiPhong = lp.LayTatCaLoaiPhong();

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
            string loaiPhong = cboLoaiPhong.Text;
            string giaPhong = lp.TimLoaiPhong(loaiPhong).First().Gia.ToString("##,## VNĐ");
            //string.Format("{ 0:0,0 vnđ}", lp.TimLoaiPhong(loaiPhong).First().Gia.ToString());

            txtGiaPhong.Text = giaPhong;
        }
        void XuLyGiaPhuHopVoiLoaiPhong2()
        {
            string loaiPhong = cboLoaiPhong2.Text;
            string giaPhong = lp.TimLoaiPhong(loaiPhong).First().Gia.ToString("##,## VNĐ");

            txtGiaPhongCu.Text = giaPhong;
        }

        /** 
         * Tạo tiêu đề cột
        */
        void TaoTieuDeCot(ListView lstv)
        {
            lstv.Columns.Add("Mã Phòng", 100);
            lstv.Columns.Add("Số Phòng", 120);
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
            lstvItem.SubItems.Add(itemP.TenPhong.Trim());
            lstvItem.SubItems.Add(itemP.TrangThaiPhong);
            lstvItem.SubItems.Add(itemP.TenLoaiPhong);
            lstvItem.SubItems.Add(itemP.Gia.ToString("##,## VNĐ"));

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
            txtSoPhong.Enabled = false;
        }
        void TaiDuLieuTuLstvDenTxtCbo(dynamic dsP)
        {
            txtSoPhong.Text = dsP.TenPhong;
            cboTrangThai.Text = dsP.TrangThaiPhong;
            cboLoaiPhong.Text = dsP.TenLoaiPhong;
            txtGiaPhong.Text = dsP.Gia.ToString("##,## VNĐ");
        }
        void TaiDuLieuTuLstvDenTxtCbo2(dynamic dsP)
        {
            cboLoaiPhong2.Text = dsP.TenLoaiPhong;
            txtGiaPhongCu.Text = dsP.Gia.ToString("##,## VNĐ");
        }

        /** 
        * Xóa trắng các ô textbox, combobox.
        */
        void XoaCacTxtCbo()
        {
            txtSoPhong.Text = "";
            cboTrangThai.Text = "";
            cboLoaiPhong.Text = "";
            txtGiaPhong.Text = "";
            cboLoaiPhong2.Text = "";
            txtGiaPhongCu.Text = "";
            txtGiaPhongMoi.Text = "";
            txtTimKiemThongTinPhong.Text = "";
        }

        /** 
        * Thêm thông tin phòng
        */
        private void btnThemPhong_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(TaoMaPhong());
            Phong phong = TaoPhong();

            if (p.TimPhong(phong.TenPhong).Count() > 0)
            {
                MessageBox.Show("Lỗi!");
            }
            else
            {
                p.ThemPhong(phong);
                XoaCacTxtCbo();
                TaiDuLieuLenListView(lstvDanhSachPhong, p.LayTatCaPhong());
            }
        }
        /** 
        * Tạo mã phòng tự tăng
        */
        private string TaoMaPhong()
        {
            string maPhong = "";
            string maPhongTam  = p.LayTatCaPhong().Last().MaPhong.ToString();
            int dem = Convert.ToInt32(maPhongTam.Split('P')[1]) + 1;
            if(dem < 10)
            {
                maPhong += "P00" + dem;
            }
            else if(dem >= 10 && dem < 100)
            {
                maPhong += "P0" + dem;
            }
            else
            {
                maPhong += "P" + dem;
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
        * Sửa thông tin của phòng (Trạng thái, Loại phòng)
        */
        dynamic SuaTrangThaiVaLoaiPhong()
        {
            Phong phong = new Phong();

            phong.TrangThaiPhong = cboTrangThai.Text;
            phong.MaPhong = p.TimPhong(txtSoPhong.Text).First().MaPhong;
            phong.MaLoaiPhong = lp.TimLoaiPhong(cboLoaiPhong.Text).First().MaLoaiPhong;

            return phong;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            Phong suaPhong = SuaTrangThaiVaLoaiPhong();
            p.SuaPhong(suaPhong);
            IEnumerable<dynamic> layDS = hl.LayPhongVaLoaiPhong();
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lstvDanhSachPhong, layDS);
        }

        /** 
        * Sửa giá của phòng 
        */
        LoaiPhong SuaGiaLoaiPhong()
        {
            LoaiPhong loaiPhong = new LoaiPhong();
            loaiPhong.MaLoaiPhong = lp.TimLoaiPhong(cboLoaiPhong.Text).First().MaLoaiPhong;
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
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lstvDanhSachPhong, layDS);
        }


        /** 
        * Xóa Phòng
        * Chỉ được quyền xóa phần từ cuối cùng trong danh sách Phòng, còn lại
        * bắt bược phải cập nhật (Số phòng, Trạng thái, Loại phòng).
        */
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult hoiXoa;
            Phong phong;

            if (lstvDanhSachPhong.SelectedItems.Count > 0)
            {
                hoiXoa = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if(hoiXoa == DialogResult.Yes)
                {
                    
                    phong = p.TimPhong(txtSoPhong.Text).First();
                    p.XoaPhong(phong);

                    IEnumerable<dynamic> layDS = hl.LayPhongVaLoaiPhong();
                    TaiDuLieuLenListView(lstvDanhSachPhong, layDS);
                }
            }
        }

        /** 
        * Tải lại danh sách ban đầu.
        */
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            IEnumerable<dynamic> layDS = hl.LayPhongVaLoaiPhong();
            XoaCacTxtCbo();
            TaiDuLieuLenListView(lstvDanhSachPhong, layDS);
        }


        /** 
        * Tim kiếm
        */
        private void btnTimKiemThongTinPhong_Click(object sender, EventArgs e)
        {
            int viTriTim = TimKiem();
            int viTriChon;
            if (viTriTim >= 0)
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
            dynamic phong;
            for(int i = 0; i < lstvDanhSachPhong.Items.Count; i++)
            {
                phong = (dynamic)lstvDanhSachPhong.Items[i].Tag;
                if(rdoVIP.Checked)
                {
                    if(phong.TenPhong.Equals(txtTimKiemThongTinPhong.Text))
                        return i;
                }
                else
                {
                    if (phong.TenPhong.Equals(txtTimKiemThongTinPhong.Text))
                        return i;
                }
            }
            return -1;
        }

        /**
        * Xử lý tìm kiếm theo Phòng Vip
        */
        private void rdoVIP_CheckedChanged(object sender, EventArgs e)
        {
            dsPhong = p.LayTatCaTheoLoai("VIP");

            if (rdoVIP.Checked)
            {
                txtTimKiemThongTinPhong.AutoCompleteCustomSource.Clear();
                foreach (Phong i in dsPhong)
                {
                    txtTimKiemThongTinPhong.AutoCompleteCustomSource.Add(i.TenPhong);
                }
            }
        }
        /**
        * Xử lý tìm kiếm theo Phòng thường 
        */
        private void rdoTHUONG_CheckedChanged(object sender, EventArgs e)
        {
            dsPhong = p.LayTatCaTheoLoai("THƯỜNG");
            {
                txtTimKiemThongTinPhong.AutoCompleteCustomSource.Clear();
                foreach (Phong i in dsPhong)
                {
                    txtTimKiemThongTinPhong.AutoCompleteCustomSource.Add(i.TenPhong);
                }
            }
        }

        /**
         * Xử lý các hủy focus trong danh sách Phòng
         */
        private void frmQuanLyPhong_Click(object sender, EventArgs e)
        {
            
            XoaCacTxtCbo();
            lstvDanhSachPhong.SelectedItems.Clear();
            txtSoPhong.Enabled = true;
        }

        /**
        * Xử lý và kiểm tra giá Phòng
        */
        private void txtGiaPhongMoi_Validating(object sender, CancelEventArgs e)
        {
            string txtGiaPhong = txtGiaPhongMoi.Text;
            if (!clsKiemTra.KiemTraSoTien(txtGiaPhong) || Convert.ToInt32(txtGiaPhong) < 100000)
            {
                e.Cancel = true;
                txtGiaPhongMoi.Focus();
                errorProvider1.SetError(txtGiaPhongMoi, "Phải là số và lớn hơn 100000!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtGiaPhongMoi, null);
            }
        }
    }
}
