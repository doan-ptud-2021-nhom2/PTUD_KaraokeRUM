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
        public IEnumerable<NhanVien> LayDSNV(string MANVQL)
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                       where !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                                       select n;
            return nv;

        }
        public IEnumerable<NhanVien> LayDSNVFULL(string MANVQL)
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                       where !n.MaNV.Contains(MANVQL)
                                       select n;
            return nv;

        }
        /**
        * Thêm các thông tin Nhân Viên
        * 
        */
        public int ThemNhanVien(NhanVien nhanVien)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
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

        /**
       * Tìm kiếm Nhân Viên
       */
        public IEnumerable<NhanVien> TimNhanVien(string cmnd , string sdt)
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                   where n.CMND.Equals(cmnd) || n.SDT.Equals(sdt)
                                       select n;
            return nv;
        }
        /**
         * Sửa thông tin nhân viên 
         */
        public bool SuaNhanVien(NhanVien nhanVien)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<NhanVien> tam = (from n in dt.NhanViens
                                         where n.MaNV == nhanVien.MaNV
                                         select n);
                tam.First().GioiTinh = nhanVien.GioiTinh;
                tam.First().DiaChi = nhanVien.DiaChi;
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
        /**
         * thay đổi trạng thái nhân viên thành đã nghỉ.
         */
        public bool XoaNhanVien(NhanVien nhanVien)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
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
        /**
          * join 2 bảng: NhanVien với LoaiNhanVien
          * Lấy dữ liệu ở Nhân viên và Loại Nhân Viên
          */
        public IEnumerable<dynamic> LayNhanVienVaLoaiNhanVien(string MANVQL)
        {
            //Đã nghỉ
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV };

            return nv;
        }
        /**
         * join 2 bảng: NhanVien với LoaiNhanVien
         * Lấy dữ liệu ở Nhân viên và Loại Nhân Viên theo loại
         */
        public IEnumerable<dynamic> LayNhanVienVaLoaiNhanVienTheoLoai(string loaiNV, string MANVQL)
        {
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL) && x.TenLNV.Equals(loaiNV) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV };
            return nv;
        }
        /*
         * join 2 bảng: NhanVien với LoaiNhanVien
         * tìm kiếm nhân viên
         */
        public IEnumerable<dynamic> TimNhanVienVaLoaiNhanVien(string timKiem, string MANVQL)
        {
            IEnumerable<dynamic> nv = from n in dt.NhanViens
                                      join x in dt.LoaiNhanViens
                                      on n.MaLNV equals x.MaLNV
                                      where n.TenNV.Contains(timKiem) || n.MaNV.Contains(timKiem) && !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                                      select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV };
            return nv;
        }

    }
}
