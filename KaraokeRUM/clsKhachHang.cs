using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsKhachHang : clsKetNoi
    {
        qlKaraokeDataContext dt;
        
        public clsKhachHang()
        {
            dt = LayData();
        }
        
        public IEnumerable<KhachHang> LayDSKH()
        {
            IEnumerable<KhachHang> kh = from n in dt.KhachHangs
                                        select n;
            return kh;
           
        }
        public KhachHang LayThongTinKhach(string maKH)
        {
            var in4_kh = from kh in dt.KhachHangs
                     where kh.MaKH.Equals(maKH)
                     select kh;
            return in4_kh.First();
        }

        /**
       * Thêm các thông tin Khách Hàng
       */
        public int ThemKhachHang(KhachHang khach)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = br;
                dt.KhachHangs.InsertOnSubmit(khach);
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
}
