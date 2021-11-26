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
        * join 3 bảng: Khách hàng và Đơn đặt phòng và Phòng
        * Lấy dữ liệu ở Khách hàng và Đơn đặt phòng và Phòng      
        **/
        public IEnumerable<dynamic> LayThongTinDonDatPhong()
        {
            var ddp = from n in dt.KhachHangs
                      join x in dt.DonDatPhongs on n.MaKH equals x.MaKH
                      join e in dt.Phongs on x.MaPhong equals e.MaPhong
                      select new { n.TenKhach, n.SDT, e.TenPhong, x.NgayDat, x.GioDat, x.NgayNhan };
            return ddp.OrderBy(x => x.NgayNhan.Date).ThenBy(x => x.GioDat.Hours);
        }

        /*Hàm lấy thông tin đơn đặt phòng theo mã phòng - truyền vào mã phòng*/
        public IEnumerable<dynamic> LayThongTinDonDatPhongTheoMaPhong(string maPhong)
        {
            var ddp = from n in dt.KhachHangs
                      join x in dt.DonDatPhongs on n.MaKH equals x.MaKH
                      join e in dt.Phongs on x.MaPhong equals e.MaPhong
                      where x.MaPhong.Equals(maPhong)
                      select new { n.TenKhach, n.SDT, e.TenPhong, x.NgayDat, x.GioDat, x.NgayNhan };
            return ddp;
        }

        /*Hàm lấy thông tin đơn đặt phòng theo ngày hôn này - truyền vào ngày hôm nay*/
        public IEnumerable<dynamic> LayThongTinDonDatPhongTheoNgay(string homNay)
        {
            var ddp = from n in dt.KhachHangs
                      join x in dt.DonDatPhongs on n.MaKH equals x.MaKH
                      join e in dt.Phongs on x.MaPhong equals e.MaPhong
                      where x.NgayNhan.ToString().Equals(homNay)
                      select new { n.TenKhach, n.SDT, e.TenPhong, x.NgayDat, x.GioDat, x.NgayNhan };
            return ddp;
        }
    }
} 
