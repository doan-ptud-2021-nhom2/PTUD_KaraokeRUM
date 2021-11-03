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

        #region Lấy số liệu để vẽ biểu đồ
        public IEnumerable<dynamic> LaySoLieuThongKeHomNay(string homNay)
        {
            var sltk = from cthd in dt.ChiTietHoaDons
                       join mh in dt.MatHangs on cthd.MaMH equals mh.MaMH
                       join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                       where hd.NgayLap.ToString().Equals(homNay) && !hd.TongTien.Equals(null)
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
            IEnumerable<dynamic> sltk;
            if (thang.Equals("") && !nam.Equals(""))
            {
                sltk = from cthd in dt.ChiTietHoaDons
                           join mh in dt.MatHangs on cthd.MaMH equals mh.MaMH
                           join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                           where hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                           group cthd.SoLuong by mh.TenMh into g
                           select new
                           {
                               MatHang = g.Key,
                               SoLuong = g.Sum(),
                           };
            }
            else
            {
                sltk = from cthd in dt.ChiTietHoaDons
                       join mh in dt.MatHangs on cthd.MaMH equals mh.MaMH
                       join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                       where hd.NgayLap.Month.Equals(thang) && hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                       group cthd.SoLuong by mh.TenMh into g
                       select new
                       {
                           MatHang = g.Key,
                           SoLuong = g.Sum(),
                       };
            }

            return sltk;
        }

        #endregion
        public IEnumerable<dynamic> LayDanhSachHoaDonHomNay(string homNay)
        {
            var dsHoaDon = from hd in dt.HoaDons
                           join kh in dt.KhachHangs on hd.MaKH equals kh.MaKH
                           join p in dt.Phongs on hd.MaPhong equals p.MaPhong
                           where hd.NgayLap.ToString().Equals(homNay) && !hd.TongTien.Equals(null)
                           select new { hd.MaHD, p.TenPhong, kh.TenKhach, hd.TongTien, hd.NgayLap };
            return dsHoaDon;
        }
        public IEnumerable<dynamic> LayDanhSachHoaDonTheoThangNam(string thang, string nam)
        {
            IEnumerable<dynamic> dsHoaDon;
            if (thang.Equals("") && !nam.Equals(""))
            {
                dsHoaDon = from hd in dt.HoaDons
                               join kh in dt.KhachHangs on hd.MaKH equals kh.MaKH
                               join p in dt.Phongs on hd.MaPhong equals p.MaPhong
                               where hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                               select new { hd.MaHD, p.TenPhong, kh.TenKhach, hd.TongTien, hd.NgayLap };
            }
            else
            {
                dsHoaDon = from hd in dt.HoaDons
                           join kh in dt.KhachHangs on hd.MaKH equals kh.MaKH
                           join p in dt.Phongs on hd.MaPhong equals p.MaPhong
                           where hd.NgayLap.Month.Equals(thang) && hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                           select new { hd.MaHD, p.TenPhong, kh.TenKhach, hd.TongTien, hd.NgayLap };
            }
            return dsHoaDon;
        }


        public int LayTongSoKhachHangHomNay(string homNay)
        {
            var dsDem = from hd in dt.HoaDons
                        where hd.NgayLap.ToString().Equals(homNay) && !hd.TongTien.Equals(null)
                        group hd by hd.MaKH into g
                        select new
                        {
                            MaKH = g.Key,
                            Dem = g.Count()
                        };
            return dsDem.Count();
        }

        public int LayTongSoKhachHangTheoThangNam(string thang, string nam)
        {
            IQueryable<dynamic> dsDem;
            if (thang.Equals("") && !nam.Equals(""))
            {
                  dsDem = from hd in dt.HoaDons
                  where hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                  group hd by hd.MaKH into g
                  select new
                  {
                      MaKH = g.Key,
                      Dem = g.Count()
                  };
            }
            else
            {
                dsDem = from hd in dt.HoaDons
                        where hd.NgayLap.Month.Equals(thang) && hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                        group hd by hd.MaKH into g
                        select new
                        {
                            MaKH = g.Key,
                            Dem = g.Count()
                        };
            }
            return dsDem.Count();
        }

        public int LayTongSanPhamBanHomNay(string homNay)
        {
            var dsCTHD = from cthd in dt.ChiTietHoaDons join hd in dt.HoaDons
                         on cthd.MaHD equals hd.MaHD
                         where hd.NgayLap.ToString().Equals(homNay) && !hd.TongTien.Equals(null)
                         select new { SoLuong = cthd.SoLuong };
            if (!dsCTHD.GetEnumerator().MoveNext())
                return 0;
            return dsCTHD.Sum(a => a.SoLuong);
        }


        public int LayTongSanPhamBanTheoThangNam(string thang, string nam)
        {
            IEnumerable<dynamic> dsCTHD;
            if (thang.Equals("") && !nam.Equals(""))
            {
                dsCTHD = from cthd in dt.ChiTietHoaDons
                join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                where hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                select new { SoLuong = cthd.SoLuong };
            }
            else
            {
                dsCTHD = from cthd in dt.ChiTietHoaDons
                         join hd in dt.HoaDons on cthd.MaHD equals hd.MaHD
                         where hd.NgayLap.Month.Equals(thang) && hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                         select new { SoLuong = cthd.SoLuong };
            }
            if (!dsCTHD.GetEnumerator().MoveNext())
                return 0;
            return dsCTHD.Sum(a => a.SoLuong);
        }

        public double LayTongTienHomNay(string homNay)
        {
            var dsHD = from hd in dt.HoaDons
                       where hd.NgayLap.ToString().Equals(homNay) && !hd.TongTien.Equals(null)
                       select new { TongTien = hd.TongTien };
            return Convert.ToDouble(dsHD.Sum(a => a.TongTien));
        }

        public long LayTongTienTheoThangNam(string thang, string nam)
        {
            IEnumerable<dynamic> dsHD;
            if (thang.Equals("") && !nam.Equals(""))
            {
                dsHD = from hd in dt.HoaDons
                           where hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                       select new { TongTien = hd.TongTien };
               
            }
            else
            {
                dsHD = from hd in dt.HoaDons
                       where hd.NgayLap.Month.Equals(thang) && hd.NgayLap.Year.Equals(nam) && !hd.TongTien.Equals(null)
                       select new { TongTien = hd.TongTien };
            }
            return dsHD.Sum(a => (long)a.TongTien);
        }

    }
}
