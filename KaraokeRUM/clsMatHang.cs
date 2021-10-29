using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsMatHang : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsMatHang()
        {
            dt = LayData();
        }

        /**
        * Lấy tất cả Mặt hàng
        */
        public IEnumerable<MatHang> LayTatCaMatHang()
        {
            IEnumerable<MatHang> q = from n in dt.MatHangs
                                     where !n.TrangThai.Contains("KSD")
                                   select n;
            return q;
        }

        /**
        * Lấy loại mặt hàng
        */

        public MatHang LayDonViMatHang(string tenMatHang)
        {
            var loaiMatHang = from n in dt.MatHangs
                                 where n.TenMh.Equals(tenMatHang)
                                 select n;
            return loaiMatHang.First();
        }

        /**
         * Lấy tên mặt hàng
         */
        public MatHang TimTheoMa(string maMH)
        {
            var matHang = from mh in dt.MatHangs
                          where mh.MaMH.Equals(maMH)
                          select mh;
            return matHang.First();
        }
        /**
         * Tìm mã mặt hàng
         */
        public MatHang TimMaTheoTen(string tenMH)
        {
            var matHang = from mh in dt.MatHangs
                          where mh.TenMh.Equals(tenMH)
                          select mh;
            return matHang.First();
        }
        public IEnumerable<MatHang> TimMatHang(string timKiem)
        {
            IEnumerable<MatHang> nv = from n in dt.MatHangs
                                      where n.MaMH.Contains(timKiem) || n.TenMh.Contains(timKiem)
                                      select n;
            return nv;
        }
        public int ThemMatHang(MatHang matHang)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = br;
                dt.MatHangs.InsertOnSubmit(matHang);
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
         * Sửa thông tin mặt hàng
         */
        public bool SuaMatHang(MatHang matHang)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<MatHang> tam = (from n in dt.MatHangs
                                            where n.MaMH == matHang.MaMH
                                           select n);
                tam.First().Loai = matHang.Loai;
                tam.First().SoLuongTon = matHang.SoLuongTon;
                tam.First().DonVi = matHang.DonVi;
                tam.First().Gia = matHang.Gia;
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
        public bool XoaMatHang(MatHang matHang)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<MatHang> tam = (from n in dt.MatHangs
                                           where n.MaMH == matHang.MaMH
                                           select n);
                tam.First().TrangThai = matHang.TrangThai;
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
        
         public IEnumerable<MatHang> LayTatCaMatHangTonTai()
        {
            IEnumerable<MatHang> q = from n in dt.MatHangs
                                   
                                     select n;
            return q;
        }
    }
}
