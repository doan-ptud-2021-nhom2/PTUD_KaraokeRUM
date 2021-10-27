using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsHoaDon : clsKetNoi 
    {
        qlKaraokeDataContext dt;
        public clsHoaDon()
        {
            dt = LayData();
        }

        public IEnumerable<dynamic> LayDanhSachHoaDon()
        {
            var dsHoaDon = from hd in dt.HoaDons
                           join kh in dt.KhachHangs on hd.MaKH equals kh.MaKH
                           join p in dt.Phongs on hd.MaPhong equals p.MaPhong
                           select new { hd.MaHD, p.TenPhong, kh.TenKhach, hd.TongTien, hd.NgayLap };
            return dsHoaDon;
        }
    }
}
