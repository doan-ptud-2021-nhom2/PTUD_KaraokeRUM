using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsHonLoan : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsHonLoan()
        {
            dt = LayData();
        }

        /**
       * join 2 bảng: Phong với LoaiPhong
       * Lấy dữ liệu ở Phòng và Loại Phòng 
       */
        public IEnumerable<dynamic> LayPhongVaLoaiPhong()
        {
            var q = from n in dt.Phongs
                    join x in dt.LoaiPhongs
                    on n.MaLoaiPhong equals x.MaLoaiPhong
                    select new { n.MaPhong, n.TenPhong , n.TrangThaiPhong, x.TenLoaiPhong, x.Gia };
            return q;
        }

        /**
        * Cập nhật giá loại phòng
       */
        /* public bool capNhatGiaLoaiPhong(dynamic lp)
         {
             System.Data.Common.DbTransaction myTran = qlKaraoke.Connection.BeginTransaction();
             try
             {
                 qlKaraoke.Transaction = myTran;
                 IEnumerable<dynamic> tam = (from n in qlKaraoke.Phongs
                                             join x in qlKaraoke.LoaiPhongs
                                             on n.MaLoaiPhong equals x.MaLoaiPhong
                                             select new { n.MaPhong, n.TrangThaiPhong, x.TenLoaiPhong, x.Gia });
                 tam.First().Gia = lp.Gia;
                 qlKaraoke.SubmitChanges();
                 qlKaraoke.Transaction.Commit();
                 return true;

             }
             catch (Exception ex)
             {
                 qlKaraoke.Transaction.Rollback();
                 throw new Exception("Lỗi không thể sửa giá Phòng này!");
             }
         }*/


    }
}
