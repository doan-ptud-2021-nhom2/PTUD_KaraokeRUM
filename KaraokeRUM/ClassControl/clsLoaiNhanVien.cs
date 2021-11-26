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

        /*Lấy danh sách loại nhân viên theo tên nhân viên - truyền vào tên loại nhân viên*/
        public IEnumerable<LoaiNhanVien> LayLoaiNhanVien(string tenLoaiNhanVien)
        {
            var lnv = from n in dt.LoaiNhanViens
                     where n.TenLNV.Equals(tenLoaiNhanVien)
                     select n;
            return lnv;
        }

        /*Lấy thông tin loại nhân viên - truyền vào mã loại nhân viên*/
        public IEnumerable<LoaiNhanVien> LayLoaiNhanVienTheoMa(string maLoaiNhanVien)
        {
            var lnv = from n in dt.LoaiNhanViens
                      where n.MaLNV.Equals(maLoaiNhanVien)
                      select n;
            return lnv;
        }
        
        /*Tìm loại nhân viên - truyền vào mã loại (Tuấn)*/
        public LoaiNhanVien TimLoaiNVTheoMa(string maLoai)
        {
            var loaiNV = from lnv in dt.LoaiNhanViens
                         where lnv.MaLNV.Equals(maLoai)
                         select lnv;
            return loaiNV.FirstOrDefault();
        }

        /*Sửa mức lương của loại nhân viên - truyền vào loại nhân viên*/
        public bool SuaMucLuongLoaiNhanVien(LoaiNhanVien loaiNhanVien)
        {
           using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
           {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<LoaiNhanVien> tam = (from n in dt.LoaiNhanViens
                                                    where n.MaLNV == loaiNhanVien.MaLNV
                                                    select n);
                    tam.First().MucLuong = loaiNhanVien.MucLuong;

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
    }
}
