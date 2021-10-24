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
        public IEnumerable<dynamic> KhachHangVaLoaiKhachHang()
        {
            var kh = from n in dt.KhachHangs
                     join x in dt.LoaiKhachHangs
                     on n.MaLoaiKH equals x.MaLoaiKH
                     select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen, x.TenLoaiKH, x.ChietKhau };
            return kh;
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

        /**
   * join 2 bảng: KhachHang với LoaiKhachHang
   * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
   */

    }
}
