using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsLoaiNhanVien : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsLoaiNhanVien()
        {
            dt = LayData();
        }

        /**
        * Lấy thông tin loại nhân viên
        */
        public IEnumerable<LoaiNhanVien> LayLoaiNhanVien(string tenLoaiNhanVien)
        {
            var lnv = from n in dt.LoaiNhanViens
                     where n.TenLNV.Equals(tenLoaiNhanVien)
                     select n;
            return lnv;
        }

        /**
        * Lấy tất cả các loại nhân viên
        */
        public IEnumerable<LoaiNhanVien> LayTatCaLoaiNhanVien()
        {
            IEnumerable<LoaiNhanVien> lnv = from n in dt.LoaiNhanViens
                                            select n;
            return lnv;
        }

        /**
         * Tìm loại nhân viên theo mã
         * - Tuấn
         */
        public LoaiNhanVien TimLoaiNVTheoMa(string maLoai)
        {
            var loaiNV = from lnv in dt.LoaiNhanViens
                         where lnv.MaLNV.Equals(maLoai)
                         select lnv;
            return loaiNV.FirstOrDefault();
        }
    }
}
