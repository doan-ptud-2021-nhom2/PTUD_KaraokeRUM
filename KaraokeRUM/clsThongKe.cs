using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsThongKe:clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsThongKe()
        {
            dt = LayData();
        }

        public IEnumerable<dynamic> LaySoLieuThongKeHomNay(string homNay)
        {
            var sltk = from cthd in dt.ChiTietHoaDons
                       join mh in dt.MatHangs on cthd.MaMH equals mh.MaMH
                       join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                       where hd.NgayLap.ToString().Equals(homNay)
                       group cthd.SoLuong by mh.TenMh into g
                       select new
                       {
                           MatHang = g.Key,
                           SoLuong = g.Sum(),
                       };
            
            return sltk;
        }

        public IEnumerable<dynamic> LaySoLieuThongKeTheoThang(string thang, string nam)
        {
            var sltk = from cthd in dt.ChiTietHoaDons
                       join mh in dt.MatHangs on cthd.MaMH equals mh.MaMH
                       join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                       where hd.NgayLap.Month.Equals(thang) && hd.NgayLap.Year.Equals(nam)
                       group cthd.SoLuong by mh.TenMh into g
                       select new
                       {
                           MatHang = g.Key,
                           SoLuong = g.Sum(),
                       };

            return sltk;
        }
    }
}
