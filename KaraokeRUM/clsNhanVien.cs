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
        * Sửa thông tin phòng (Trạng thái, Loại phòng).
  
        public bool SuaPhong(Phong phong)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<Phong> tam = (from n in dt.Phongs
                                         where n.MaPhong == phong.MaPhong
                                         select n);
                tam.First().TrangThaiPhong = phong.TrangThaiPhong;
                //truy vào khóa ngoại của bảng Phòng để đổi trạng thái (VIP, THƯỜNG) bên bảng Loại Phòng.
                tam.First().MaLoaiPhong = phong.MaLoaiPhong;
                dt.SubmitChanges();
                dt.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                dt.Transaction.Rollback();
                throw new Exception("Loi không sửa được!" + ex.Message);

            }
      
    }
                */
    }
}
