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

       
        public IEnumerable<dynamic> LayMatHangVaPhong()
        {
            var q = from n in dt.MatHangs
                    join x in dt.ChiTietHoaDons on n.MaMH equals x.MaMH
                    join y in dt.HoaDons on x.MaHD equals y.MaHD
                    select new { n.MaMH, y.MaHD, x.SoLuong, x.ThanhTien };
            return q;
        }
        public IEnumerable<dynamic> LayThongTinPhongTrangThietBi()
        {
            var pTTB = from n in dt.TrangThietBis
                       join x in dt.Phong_TrangThietBis on n.MaTTB equals x.MaTTB
                       join e in dt.Phongs on x.MaPhong equals e.MaPhong
                       select new { n.TenTTB, x.SoLuong, e.TenPhong };
            return pTTB;
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
        public IEnumerable<dynamic> LayThongTinDonDatPhongTheoMaPhong(string maPhong)
        {
            var ddp = from n in dt.KhachHangs
                      join x in dt.DonDatPhongs on n.MaKH equals x.MaKH
                      join e in dt.Phongs on x.MaPhong equals e.MaPhong
                      where x.MaPhong.Equals(maPhong)
                      select new { n.TenKhach, n.SDT, e.TenPhong, x.NgayDat, x.GioDat, x.NgayNhan };
            return ddp/*.OrderByDescending(x=>x.NgayNhan.Date).ThenBy(x=>x.GioDat.Hours)*/;
        }
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
