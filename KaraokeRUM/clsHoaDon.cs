using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
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

        public HoaDon LayHoaDon(string maHD)
        {
            var hoaDon = (from hd in dt.HoaDons
                          where hd.MaHD.Equals(maHD)
                          select hd).First();
            return hoaDon;
        }

        /*
         * Thay thế hàm Lấy chi tiết hóa đơn của chức năng Sửa
         */
        public IEnumerable<dynamic> LayChiTietHoaHoaTaiLenListView(string maHD)
        {
            var ds = from n in dt.ChiTietHoaDons
                     join y in dt.HoaDons on n.MaHD equals y.MaHD
                     where y.MaHD.Equals(maHD)
                     select new { n.MaMH, n.SoLuong, n.ThanhTien };
            return ds;
        }

        public IEnumerable<ChiTietHoaDon> LayChiTietHoaDon(string maHD)
        {
            var dsCTHD = from cthd in dt.ChiTietHoaDons
                         where cthd.MaHD.Equals(maHD)
                         select cthd;
            return dsCTHD;
        }

        public int TinhTongTienMatHang(string maHD)
        {
            var dsCTHD = from cthd in dt.ChiTietHoaDons
                         where cthd.MaHD.Equals(maHD)
                         select new { ThanhTien = cthd.ThanhTien };
            if (!dsCTHD.GetEnumerator().MoveNext())
                return 0;
            return (int)dsCTHD.Sum(tt => tt.ThanhTien);
        }

        public int TinhGio(string maHD)
        {
            var gio = (from hd in dt.HoaDons
                       where hd.MaHD.Equals(maHD)
                       select new { GioRa = hd.GioRa, GioVao = hd.GioVao }).First();
            int ts = (int)(gio.GioRa - gio.GioVao).Value.TotalMinutes;

            return ts;
        }
    }
}
