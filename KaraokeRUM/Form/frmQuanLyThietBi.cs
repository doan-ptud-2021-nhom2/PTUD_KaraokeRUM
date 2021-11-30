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
    public partial class frmQuanLyThietBi : Form
    {

        private clsHonLoan HONLOAN;
        private clsPhong PHONG;
        private IEnumerable<Phong> DANHSACHPHONG;
        private clsThietBi THIETBI;
        private IEnumerable<TrangThietBi> DANHSACHTHIETBI;
        private clsPhongTrangThietBi PHONGTRANGTHIETBI;
        private IEnumerable<Phong_TrangThietBi> DANHSACHPHONGTTB;
        private string MAQL;
        private string TENTHIETBI;

        /*
         * Constructor
         */
        public frmQuanLyThietBi(string maQL)
        {
            InitializeComponent();
            this.MAQL = maQL;
        }

        private void frmQuanLyThietBi_Load(object sender, EventArgs e)
        {
            THIETBI = new clsThietBi();
            PHONGTRANGTHIETBI = new clsPhongTrangThietBi();
            PHONG = new clsPhong();
            HONLOAN = new clsHonLoan();

            btnSua.Enabled = false;
            btnSuaTP.Enabled = false;
            btnXoa.Enabled = false;
            btnXoaTP.Enabled = false;
            btnThem.Enabled = true;
            TaoListViewThietBi(lstvThietBi);
            TaoListViewPhongTrangThietBi(lstvThietBiTrongPhong);

            DANHSACHTHIETBI = THIETBI.LayToanBoTrangThietBis();
            DANHSACHPHONG = PHONG.LayTatCaPhong();

            LoadDuLieuVaoComboBox(DANHSACHPHONG, DANHSACHTHIETBI);
            cboTenPhong.SelectedIndex = 0;

            string tenPhong = cboTenPhong.Text;
            string maPhong = PHONG.TimPhongTheoTen(tenPhong).First().MaPhong;
            DANHSACHPHONGTTB = PHONGTRANGTHIETBI.TimPhongTTB(maPhong);

            TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
            TaiDuLieuLenLstvPhongTrangThietBi(lstvThietBiTrongPhong, DANHSACHPHONGTTB);

            txtTimKiem.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiem.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteSearch();
        }

        private void frmQuanLyThietBi_Click(object sender, EventArgs e)
        {
            lstvThietBi.SelectedItems.Clear();
            lstvThietBiTrongPhong.SelectedItems.Clear();
            txtTen.Enabled = true;
            txtTen.Clear();
            txtSoLuongTon.Enabled = true;
            txtSoLuongTon.Clear();
            txtDonGia.Enabled = true;
            txtDonGia.Clear();
            txtSoLuongTP.Clear();
            btnThemTP.Enabled = true;
            btnSuaTP.Enabled = false;
            btnXoaTP.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            cboDonVi.SelectedIndex = -1;
            cboTenTTB.SelectedIndex = -1;
        }

        /*Hàm hỗ trợ tải dữ liệu vào các combobox*/
        private void LoadDuLieuVaoComboBox(IEnumerable<Phong> dsPhong, IEnumerable<TrangThietBi> dsThietBi)
        {
            cboDonVi.Items.Clear();
            cboTenPhong.Items.Clear();
            cboTenTTB.Items.Clear();
            cboDonVi.Items.Add("Cặp");
            cboDonVi.Items.Add("Cái");

            foreach (Phong i in dsPhong)
            {
                cboTenPhong.Items.Add(i.TenPhong);
            }

            foreach (TrangThietBi i in dsThietBi)
            {
                cboTenTTB.Items.Add(i.TenTTB);
            }
        }

        #region ListView
        /*Tạo lstvThietBi*/
        private void TaoListViewThietBi(ListView lstv)
        {
            lstv.Columns.Clear();
            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
            lstv.Columns.Add("Mã TTB", 130);
            lstv.Columns.Add("Tên Trang Thiết Bị", 350);
            lstv.Columns.Add("Số Lượng Tồn", 150);
            lstv.Columns.Add("Đơn Vị", 130);
            lstv.Columns.Add("Giá", 200);
            lstv.Columns.Add("Trạng thái", 150);
        }

        /*Tải dữ liệu lên lsvtThietBi*/
        private void TaiDuLieuLenLstvThietBi(ListView lstv, IEnumerable<TrangThietBi> dSach)
        {
            lstv.Items.Clear();
            ListViewItem item;
            foreach (TrangThietBi i in dSach)
            {
                item = TaoItemTTB(i);
                lstv.Items.Add(item);
            }
        }

        /*Tải dữ liệu lên lsvtThietBi*/
        private void TaiDuLieuLenLstvThietBi2(ListView lstv, IEnumerable<TrangThietBi> dSach)
        {
            lstv.Items.Clear();
            ListViewItem item;
            foreach (TrangThietBi i in dSach)
            {
                item = TaoItemTTB(i);
                lstv.Items.Add(item);
            }
        }

        /*Tạo item trong lstvThietBi*/
        private ListViewItem TaoItemTTB(TrangThietBi tb)
        {
            ListViewItem lvwItem;
            lvwItem = new ListViewItem(tb.MaTTB);

            lvwItem.SubItems.Add(tb.TenTTB);
            lvwItem.SubItems.Add(tb.SoLuongTon.ToString());
            lvwItem.SubItems.Add(tb.DonVi);
            lvwItem.SubItems.Add(tb.Gia.ToString("#,###,000 VNĐ"));
            lvwItem.SubItems.Add(tb.TrangThai);
            lvwItem.Tag = tb;
            lvwItem.ImageIndex = 0;

            return lvwItem;
        }

        /*Tạo lstv Phòng trang thiết bị*/
        private void TaoListViewPhongTrangThietBi(ListView lstv)
        {
            lstv.Columns.Clear();
            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
            lstv.Columns.Add("Tên Phòng", 150);
            lstv.Columns.Add("Tên Trang Thiết Bị", 350);
            lstv.Columns.Add("Số Lượng", 250);
        }

        /*Tạo item trong lstvPhongTrangThietBi*/
        private ListViewItem TaoItemPTTB(dynamic pTTB)
        {
            ListViewItem lstvItem;
            lstvItem = new ListViewItem(PHONG.TimMotPhongTheoMa(pTTB.MaPhong).TenPhong);
            lstvItem.SubItems.Add(THIETBI.TimTTBTheoMa(pTTB.MaTTB).TenTTB);
            lstvItem.SubItems.Add(pTTB.SoLuong.ToString());
            lstvItem.Tag = pTTB;
            lstvItem.ImageIndex = 0;

            return lstvItem;
        }

        /*Tải dữ liệu lên lstvPhongTrangThietBi*/
        private void TaiDuLieuLenLstvPhongTrangThietBi(ListView lstv, IEnumerable<dynamic> dSach)
        {
            lstv.Items.Clear();
            ListViewItem item;
            /*dSach = hl.LayThongTinPhongTrangThietBi();*/
            foreach (dynamic i in dSach)
            {
                item = TaoItemPTTB(i);
                lstv.Items.Add(item);
            }
        }

        #endregion
        #region DuLieulenTextbox
        /*Gán dữ liệu vào các textbox tương ứng của thiết bị*/
        private void DuLieuLenTextBoxThietBi(TrangThietBi tb)
        {
            txtTen.Text = tb.TenTTB;
            txtSoLuongTon.Text = tb.SoLuongTon.ToString();
            cboDonVi.Text = tb.DonVi;
            txtDonGia.Text = tb.Gia.ToString("#,###,000");

        }

        /*Chức năng tải dữ liệu lên textbox từ lstvThietBi*/
        private void lstvThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic tb = null;
            if (lstvThietBi.SelectedItems.Count > 0)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                tb = lstvThietBi.SelectedItems[0].Tag;
                DuLieuLenTextBoxThietBi(tb);
                TENTHIETBI = txtTen.Text;
            }
        }

        /*Gán dữ liệu vào các textbox tương ứng của thiết bị trong phòng*/
        private void DuLieuLenTextBoxPhongTrangThietBi(dynamic ptb)
        {
            cboTenPhong.Text = PHONG.TimMotPhongTheoMa(ptb.MaPhong).TenPhong;
            cboTenTTB.Text = THIETBI.TimTTBTheoMa(ptb.MaTTB).TenTTB;
            txtSoLuongTP.Text = ptb.SoLuong.ToString();
        }

        /*Chức năng tải dữ liệu lên textbox từ lstvThietBiTrongPhong*/
        private void lstvThietBiTrongPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic ptb = null;
            if (lstvThietBiTrongPhong.SelectedItems.Count > 0)
            {
                ptb = lstvThietBiTrongPhong.SelectedItems[0].Tag;
                DuLieuLenTextBoxPhongTrangThietBi(ptb);
            }
            btnSuaTP.Enabled = true;
            btnXoaTP.Enabled = true;
            btnThemTP.Enabled = false;
        }
        #endregion
        #region ThietBi
        #region ThemTTb
        /*Tạo mã thiết bị mới*/
        private string taoMaTTB()
        {
            string maTTB = "";
            if (THIETBI.DemDanhSachThietBi().Count() == 0)
            {
                maTTB = "TB001";
            }
            else
            {
                string maTTBTam = THIETBI.DemDanhSachThietBi().Last().MaTTB.ToString();
                int dem = Convert.ToInt32(maTTBTam.Split('T','B')[2]) + 1;
                if (dem < 10)
                {
                    maTTB += "TB00" + dem;
                }
                else
                {
                    maTTB += "TB0" + dem;
                }
            }
            return maTTB;
        }

        /*Tạo thiết bị mới*/
        private TrangThietBi TaoTTB()
        {
            TrangThietBi ttb = new TrangThietBi();
            ttb.MaTTB = taoMaTTB();
            ttb.TenTTB = txtTen.Text;
            ttb.SoLuongTon = (int)Convert.ToDecimal(txtSoLuongTon.Text);
            ttb.DonVi = cboDonVi.Text;
            ttb.Gia = Convert.ToDecimal(txtDonGia.Text);
            ttb.MaQL = MAQL;
            ttb.TrangThai = "DSD";
            return ttb;
        }

        /*Kiểm tra xem thiết bị đã tồn tại hay chưa*/
        private bool KiemTraTonTaiTenThietBi(TrangThietBi ttb)
        {
            foreach (TrangThietBi i in THIETBI.LayToanBoTrangThietBis())
            {
                if (ttb.TenTTB.Equals(i.TenTTB))
                {
                    return false;
                }
            }
            return true;
        }

        /*Chức năng thêm thiết bị mới vào kho*/
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtDonGia.Enabled = true;
            txtTen.Enabled = true;
            txtSoLuongTon.Enabled = true;
            cboDonVi.Enabled = true;
           
            if (string.IsNullOrEmpty(txtTen.Text) || string.IsNullOrEmpty(txtDonGia.Text) 
                || string.IsNullOrEmpty(txtSoLuongTon.Text))
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtSoLuongTon.Text.Equals("0"))
            {
                MessageBox.Show("Số lượng tồn không được nhập bằng 0. Vui lòng nhập lớn hơn 0!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtDonGia.Text.Equals("0"))
            {
                MessageBox.Show("Đơn giá không được nhập bằng 0. Vui lòng nhập lớn hơn 0!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string ten = txtTen.Text + " đã tồn tại!";
                TrangThietBi ttb = TaoTTB();
                if (!KiemTraTonTaiTenThietBi(ttb))
                {
                    MessageBox.Show(ten, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                else
                {
                    THIETBI.Them(ttb);
                    TaiDuLieuLenLstvThietBi(lstvThietBi, THIETBI.LayToanBoTrangThietBis());
                    string LogDetail = string.Format(" với tên [{0}]  số lượng tồn [{1}] đơn vị [{2}] đơn giá [{3}]",
                                                    ttb.TenTTB, ttb.SoLuongTon, ttb.DonVi, ttb.Gia);
                    Logger.LogWritter.Write("Thu ngân thêm thiết bị mới " + LogDetail + "- TrangThietBi");
                    XoaDuLieuTextBox();
                    LoadDuLieuVaoComboBox(DANHSACHPHONG, DANHSACHTHIETBI);
                }
            }    
        }

        #endregion   

        /*Chức năng xóa thiết bị*/
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult yn;
            TrangThietBi ttb;
            int index;
            if (lstvThietBi.SelectedItems.Count > 0)
            {
                yn = MessageBox.Show("Bạn có chắc muốn xóa?", "Hỏi xóa", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    for (int i = 0; i < lstvThietBi.SelectedItems.Count; ++i)
                    {
                        index = lstvThietBi.SelectedIndices[i];
                        ttb = (TrangThietBi)lstvThietBi.Items[index].Tag;
                        if(ttb.SoLuongTon > 0)
                        {
                            MessageBox.Show("Số lượng tồn lớn hơn 0. Không được xóa! ", "Thông báo", 
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }     
                        else if(!KiemTraThietBiTrongPhong(ttb))
                        {
                            MessageBox.Show("Thiết bị đang sử dụng trong phòng không được xóa", "Thông báo", 
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }    
                        else if(ttb.SoLuongTon == 0)
                        {
                            ttb.TrangThai = "HET";
                            THIETBI.SuaTrangThietBi(ttb);
                            string LogDetail = string.Format(" với tên [{0}]  ",ttb.TenTTB);
                            Logger.LogWritter.Write("Thu ngân xoá trang thiết bị " + LogDetail + "- TrangThietBi");
                        }   
                    }

                    XoaDuLieuTextBox();
                    TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
                    LoadDuLieuVaoComboBox(DANHSACHPHONG, DANHSACHTHIETBI);
                    btnXoa.Enabled = false;
                    btnSua.Enabled = false;
                }
            }
        }

        /*Kiểm tra thiết bị trong phòng*/
        private bool KiemTraThietBiTrongPhong(TrangThietBi ttb)
        {
            foreach(Phong_TrangThietBi i in DANHSACHPHONGTTB)
            {
                if(i.MaTTB.Equals(ttb.MaTTB))
                {
                    return false;
                }    
            }
            return true;
        }

        /*Chức năng sửa thông tin của thiết bị*/
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoLuongTon.Text))
            {
                MessageBox.Show("Số lượng tồn không được trống!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TrangThietBi tb = ThongTinMoiCuaThietBi();
                if (!tb.TenTTB.Equals(TENTHIETBI))
                {
                    if (THIETBI.TimThietBiTheoTen(tb.TenTTB).Count() > 0)
                    {
                        MessageBox.Show("Tên thiết bị này đã tồn tại. Vui lòng nhập lại!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        THIETBI.SuaTrangThietBi(tb);
                        string LogDetail = string.Format(" với tên [{0}]  số lượng tồn [{1}] đơn vị [{2}] đơn giá [{3}]",
                                                    tb.TenTTB, tb.SoLuongTon, tb.DonVi, tb.Gia);
                        Logger.LogWritter.Write("Thu ngân cập nhập thông tin thiết bị " + LogDetail + "- TrangThietBi");
                        XoaDuLieuTextBox();
                        TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
                        cboDonVi.SelectedIndex = -1;
                    }
                }
                else
                {
                    THIETBI.SuaTrangThietBi(tb);
                    string LogDetail = string.Format(" với tên [{0}]  số lượng tồn [{1}] đơn vị [{2}] đơn giá [{3}]",
                                                    tb.TenTTB, tb.SoLuongTon, tb.DonVi, tb.Gia);
                    Logger.LogWritter.Write("Thu ngân cập nhập thông tin thiết bị " + LogDetail + "- TrangThietBi");
                    XoaDuLieuTextBox();
                    TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
                    cboDonVi.SelectedIndex = -1;
                }
            }    
            
        }

        /*Tạo thiết bị mới để sửa thông tin thiết bị*/
        TrangThietBi ThongTinMoiCuaThietBi()
        {
            TrangThietBi tb = new TrangThietBi();
            tb.MaTTB = THIETBI.TimThietBiTheoTen(txtTen.Text).FirstOrDefault().MaTTB;
            tb.TenTTB = txtTen.Text;
            tb.SoLuongTon = (int)Convert.ToDecimal(txtSoLuongTon.Text);
            tb.DonVi = cboDonVi.Text;
            tb.Gia = Convert.ToDecimal(txtDonGia.Text);
            tb.TrangThai = "DSD";
            tb.MaQL = MAQL;
            return tb;
        }
        #endregion

        #region Thiết bị trong phòng
        /*Tạo trang thiết bị trong phòng*/
        dynamic TaoPTTB()
        {
            Phong_TrangThietBi pTTB = new Phong_TrangThietBi();
            pTTB.SoLuong = (int)Convert.ToDecimal(txtSoLuongTP.Text);
            pTTB.MaPhong = PHONG.TimPhongTheoTen(cboTenPhong.Text).First().MaPhong;
            pTTB.MaTTB = THIETBI.TimThietBiTheoTen(cboTenTTB.Text).First().MaTTB;
            return pTTB;
        }

        /*kiểm tra tên thiết bị trong phòng*/
        private bool KiemTraTonTaiTenThietBiTrongPhong(string maTTB , string maPhong)
        {
            foreach (Phong_TrangThietBi i in PHONGTRANGTHIETBI.LayToanBoTrangThietBiTrongPhong(maPhong, maTTB))
            {
                if (maTTB.Equals(i.MaTTB))
                {
                    return false;
                }
            }
            return true;
        }

        /* Chức năng thêm thiết bị vào phòng*/
        private void btnThemTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboTenPhong.Text) || string.IsNullOrEmpty(cboTenTTB.Text) 
                || string.IsNullOrEmpty(txtSoLuongTP.Text))
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtSoLuongTP.Text.Equals("0"))
            {
                MessageBox.Show("Số lượng không được nhập bằng 0. Vui lòng nhập lớn hơn 0!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Phong_TrangThietBi pTTBTam = new Phong_TrangThietBi(); ;
                pTTBTam.MaPhong = PHONG.TimPhongTheoTen(cboTenPhong.Text).First().MaPhong;
                pTTBTam.MaTTB = THIETBI.TimThietBiTheoTen(cboTenTTB.Text).First().MaTTB;
                Phong_TrangThietBi pTTB = TaoPTTB();
                btnThemTP.Enabled = true;
                if(pTTB.SoLuong > THIETBI.TimThietBiTheoTen(cboTenTTB.Text).First().SoLuongTon)
                {
                    MessageBox.Show("Số lượng vượt quá số lượng tồn. Không thể thêm!", "Thông báo", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else if (!KiemTraTonTaiTenThietBiTrongPhong(pTTBTam.MaTTB, pTTBTam.MaPhong))
                {
                    MessageBox.Show("Thiết bị này đã tồn tại trong phòng, vui lòng thực hiện chức năng sửa !", "Thông báo",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    PHONGTRANGTHIETBI.Them(pTTB);
                    DANHSACHTHIETBI = THIETBI.LayToanBoTrangThietBis();
                    string LogDetail = string.Format(" với tên [{0}] vào phòng [{1}] với số lượng [{2}]", cboTenTTB.Text, cboTenPhong.Text, pTTB.SoLuong);
                    Logger.LogWritter.Write("Thu ngân thêm thiết bị " + LogDetail + "- Phong_TrangThietBi");
                    XoaDuLieuTextBox();
                    TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
                    TaiDuLieuLenLstvPhongTrangThietBi(lstvThietBiTrongPhong, 
                    PHONGTRANGTHIETBI.TimPhongTTB(PHONG.TimMotPhongTheoTen(cboTenPhong.Text).MaPhong));
                }                   
            }               
        }

        /*Chức năng sửa thông tin thiết bị trong phòng*/
        private void btnSuaTP_Click(object sender, EventArgs e)
        {
            Phong_TrangThietBi pTTB = TaoPTTB();
            string maPhong = PHONG.TimMotPhongTheoTen(cboTenPhong.Text).MaPhong;
            string maTTB = THIETBI.TimThietBiTheoTen(cboTenTTB.Text).First().MaTTB;
            Phong_TrangThietBi ttbTam = PHONGTRANGTHIETBI.TimTTBtrongPhongTheoTenVaMaTTB2(maPhong, maTTB);
            if (pTTB.SoLuong > (THIETBI.TimThietBiTheoTen(cboTenTTB.Text).First().SoLuongTon + ttbTam.SoLuong))
            {
                MessageBox.Show("Số lượng vượt quá số lượng tồn. Không thể sửa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtSoLuongTP.Text.Equals("0"))
            {
                MessageBox.Show("Số lượng tồn không được nhập bằng 0. Vui lòng nhập lớn hơn 0!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                PHONGTRANGTHIETBI.SuaTrangThietBi(pTTB);
                TaiDuLieuLenLstvPhongTrangThietBi(lstvThietBiTrongPhong,
                PHONGTRANGTHIETBI.TimPhongTTB(PHONG.TimMotPhongTheoTen(cboTenPhong.Text).MaPhong));
                DANHSACHTHIETBI = THIETBI.LayToanBoTrangThietBis();
                TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
                string LogDetail = string.Format(" với tên [{0}] ở phòng [{1}] với số lượng [{2}]", cboTenTTB.Text, cboTenPhong.Text, pTTB.SoLuong);
                Logger.LogWritter.Write("Thu ngân cập nhập thiết bị " + LogDetail + "- Phong_TrangThietBi");
                XoaDuLieuTextBox();
                btnSuaTP.Enabled = false;
                btnXoaTP.Enabled = false;
            }                
        }

        /*Chức năng xóa thiết bị ra khỏi phòng*/
        private void btnXoaTP_Click(object sender, EventArgs e)
        {
            DialogResult yn;
            Phong_TrangThietBi pttb;
            string maTTB, maPhong;
            if (lstvThietBiTrongPhong.SelectedItems.Count > 0)
            {
                yn = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yn == DialogResult.Yes)
                {
                    maPhong = PHONG.TimPhongTheoTen(cboTenPhong.Text).First().MaPhong;
                    maTTB = THIETBI.TimThietBiTheoTen(cboTenTTB.Text).First().MaTTB;
                    TrangThietBi ttb = THIETBI.TimTTBTheoMa(maTTB);
                    ttb.TrangThai = "DSD";
                    THIETBI.SuaTrangThietBi(ttb);
                    pttb = PHONGTRANGTHIETBI.TimTTBtrongPhongTheoTenVaMaTTB(maPhong, maTTB).First();
                    PHONGTRANGTHIETBI.Xoa(pttb);
                }

                TaiDuLieuLenLstvPhongTrangThietBi(lstvThietBiTrongPhong,
                                                      PHONGTRANGTHIETBI.TimPhongTTB(PHONG.TimMotPhongTheoTen(cboTenPhong.Text).MaPhong));
                DANHSACHTHIETBI = THIETBI.LayToanBoTrangThietBis();
                string LogDetail = string.Format(" với tên [{0}] ra khỏi phòng [{1}]", cboTenTTB.Text, cboTenPhong.Text);
                Logger.LogWritter.Write("Thu ngân xoá thiết bị " + LogDetail + "- Phong_TrangThietBi");
                XoaDuLieuTextBox();
                TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
            }
            btnSuaTP.Enabled = false;
            btnXoaTP.Enabled = false;

        }

        /*Chức năng tìm thiết bị (theo mã hoặc theo tên)*/
        private void btnTim_Click(object sender, EventArgs e)
        {
            DANHSACHTHIETBI = THIETBI.TimDSachTTBTheoMa(txtTimKiem.Text);
            TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
            txtTimKiem.Clear();
        }

        private void AutoCompleteSearch()
        {
            txtTimKiem.AutoCompleteCustomSource.Clear();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (TrangThietBi i in THIETBI.LayToanBoTrangThietBis())
            {
                collection.Add(i.MaTTB);
                collection.Add(i.TenTTB);
            }
            txtTimKiem.AutoCompleteCustomSource = collection;
        }

        /*Chức năng duyệt danh sách thiết bị trong phòng theo tên phòng ở cboTenPhong*/
        private void cboTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenPhong = cboTenPhong.Text;
            string maPhong = PHONG.TimPhongTheoTen(tenPhong).First().MaPhong;
            DANHSACHPHONGTTB = PHONGTRANGTHIETBI.TimPhongTTB(maPhong);
            TaiDuLieuLenLstvPhongTrangThietBi(lstvThietBiTrongPhong, DANHSACHPHONGTTB);
        }

        /*Xóa dữ liệu các ô input*/
        private void XoaDuLieuTextBox()
        {
            txtTen.Text = "";
            txtSoLuongTon.Text = "";
            txtDonGia.Text = "";
            cboTenTTB.Text = "";
            cboTenPhong.Text = "";
            txtSoLuongTP.Text = "";
        }
        #endregion

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            DANHSACHTHIETBI = THIETBI.LayToanBoTrangThietBis();
            TaiDuLieuLenLstvThietBi(lstvThietBi, DANHSACHTHIETBI);
            XoaDuLieuTextBox();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSoLuongTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSoLuongTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
