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
       * join 2 bảng: Phòng với Loaị Phòng
       * Lấy dữ liệu ở Phòng và Loại Phòng 
       */
        public IEnumerable<dynamic> LayPhongVaLoaiPhong()
        {
            var q = from n in dt.Phongs
                    join x in dt.LoaiPhongs
                    on n.MaLoaiPhong equals x.MaLoaiPhong
                    select new { n.MaPhong, n.TenPhong , n.TrangThaiPhong, x.TenLoaiPhong, x.Gia, n.MaQL };
            return q;
        }

        /**
   * join 2 bảng: KhachHang với LoaiKhachHang
   * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
   */

    }
}
