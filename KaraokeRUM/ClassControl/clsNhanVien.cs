using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsNhanVien : clsKetNoi
    {

        qlKaraokeDataContext dt;
        public clsNhanVien()
        {
            dt = LayData();
        }

        /*Lấy danh sách tất cả nhân viên trừ quản lý và nhân viên đã nghỉ - truyền vào mã quản lý*/
        public IEnumerable<NhanVien> LayDSNV(string MANVQL)
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                       where !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("Đã nghỉ")
                                       select n;
            return nv;
        }

        /*Lấy tất cả danh sách nhân viên trừ quản lý*/
        public IEnumerable<NhanVien> LayDSNVFULL(string MANVQL)
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                       where !n.MaNV.Contains(MANVQL)
                                       select n;
            return nv;

        }

        /*Thêm vào một nhân viên - truyền vào một đối tượng nhân viên*/
        public int ThemNhanVien(NhanVien nhanVien)
        {
            using(System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    dt.NhanViens.InsertOnSubmit(nhanVien);
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            } 
        }

        /*Tìm kiếm nhân viên - truyền vào chứng minh nhân dân và số điện thoại*/
        public IEnumerable<NhanVien> TimNhanVien(string cmnd , string sdt)
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                       where n.CMND.Equals(cmnd) || n.SDT.Equals(sdt)
                                       select n;
            return nv;
        }

        /*Tìm kiếm nhân viên - truyền vào số điện thoại*/
        public NhanVien TimNhanVienTheoSDT(string sdt)
        {
            var nv = from n in dt.NhanViens
                     where n.SDT.Equals(sdt)
                     select n;
            return nv.FirstOrDefault();
        }

        /*Sửa thông tin nhân viên - truyền vào một đối tượng nhân viên*/
        public bool SuaNhanVien(NhanVien nhanVien)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<NhanVien> tam = (from n in dt.NhanViens
                                                where n.MaNV == nhanVien.MaNV
                                                select n);
                    tam.First().TenNV = nhanVien.TenNV;
                    tam.First().GioiTinh = nhanVien.GioiTinh;
                    tam.First().DiaChi = nhanVien.DiaChi;
                    tam.First().SDT = nhanVien.SDT;
                    tam.First().TrangThai = nhanVien.TrangThai;
                    tam.First().MaLNV = nhanVien.MaLNV;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi không sửa được!" + ex.Message);

                }
            }
        }

        /*Xóa một nhân viên(Đổi trạng thái thành đã nghỉ) - truyền vào một đối tượng nhân viên*/
        public bool XoaNhanVien(NhanVien nhanVien)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<NhanVien> tam = (from n in dt.NhanViens
                                                where n.MaNV == nhanVien.MaNV
                                                select n);
                    tam.First().TrangThai = nhanVien.TrangThai;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi không sửa được!" + ex.Message);

                }
            }
        }
        
        /*Hàm lấy toàn bộ danh sách trừ nhân viên quản lý hiện tại - truyền vào mã nhân viên quản lý*/
        public IEnumerable<dynamic> LayToanBoNhanVienVaLoaiNhanVien(string MANVQL)
        {
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL)
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV , x.MucLuong};

            return nv;
        }

        /*Lấy danh sách nhân viên đã nghỉ trừ nhân viên quản lý hiện tại - truyền vào mã nhân viên quản lý*/
        public IEnumerable<dynamic> LayNhanVienVaLoaiNhanVienDaNghi(string MANVQL)
        {
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL) && n.TrangThai.ToLower().Contains("đã nghỉ")
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV , x.MucLuong};

            return nv;
        }

        /*Lấy danh sách nhân viên theo loại và không lấy nhân viên quản lý hiện tại - truyền vào loại nhân viên và mã quản lý*/
        public IEnumerable<dynamic> LayNhanVienVaLoaiNhanVienTheoLoai(string loaiNV, string MANVQL)
        {
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL) && x.TenLNV.Equals(loaiNV) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV, x.MucLuong};
            return nv;
        }

        /*Lấy danh sách nhân viên theo loại và tên hoặc mã nhân viên - truyền vào tên hoặc mã nhân viên và mã nhân viên quản lý*/
        public IEnumerable<dynamic> TimNhanVienVaLoaiNhanVien(string timKiem, string MANVQL)
        {
            IEnumerable<dynamic> nv = from n in dt.NhanViens
                                      join x in dt.LoaiNhanViens
                                      on n.MaLNV equals x.MaLNV
                                      where n.TenNV.Contains(timKiem) || n.MaNV.Contains(timKiem) && !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                                      select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV , x.MucLuong};
            return nv;
        }

        /* Tìm nhân viên theo mã - truyền vào mã nhân viên(Tuấn)*/
        public NhanVien TimNhanVienTheoMa(string maNV)
        {
            var nhanVien = from nv in dt.NhanViens
                           where nv.MaNV.Equals(maNV)
                           select nv;
            return nhanVien.FirstOrDefault();
        }

    }
}
