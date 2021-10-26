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
        * Lấy dữ liệu ở bảng Phòng và Loại Phòng 
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
        * join các bảng: Mặt hàng - Chi tiết hóa đơn - Hóa đơn - Phòng.
        * Lấy dữ liệu ở bảng Mặt hàng và Phòng.
        */
        public IEnumerable<dynamic> LayMatHangVaPhong()
        {
            var q = from n in dt.MatHangs
                    join x in dt.ChiTietHoaDons on n.MaMH equals x.MaMH
                    join y in dt.HoaDons on x.MaHD equals y.MaHD
                    select new { n.MaMH, y.MaHD, x.SoLuong, x.ThanhTien };
            return q;
        }

    }
}
