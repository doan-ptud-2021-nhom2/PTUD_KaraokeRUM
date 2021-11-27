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

        /*Hàm lấy hóa đơn theo mã*/
        public HoaDon LayHoaDon(string maHD)
        {
            var hoaDon = (from hd in dt.HoaDons
                          where hd.MaHD.Equals(maHD)
                          select hd).FirstOrDefault();
            return hoaDon;
        }

        /*Hàm lấy danh sách hóa đơn theo mã phòng*/
        public IEnumerable<HoaDon> LayDanhSachHoaDonTheoMaPhong(string maPhong)
        {
            var hoaDon = (from hd in dt.HoaDons
                          where hd.MaPhong.Equals(maPhong)
                          select hd);
            return hoaDon;
        }

        /*Hàm lấy hóa đơn theo mã phòng*/
        public HoaDon LayMaHoaDonTheoMaPhong(string maPhong)
        {
            var hoaDon = (from hd in dt.HoaDons
                          where hd.MaPhong.Equals(maPhong) && hd.TongTien == null
                          select hd).FirstOrDefault();
            return hoaDon;
        }

        /*Hàm lấy toàn bộ danh sách hóa đơn*/
        public IEnumerable<HoaDon> LayToanBoHoaDon()
        {
            IEnumerable<HoaDon> tb = from n in dt.HoaDons select n;
            return tb;
        }

        /*Hàm lấy chi tiết hóa đơn theo mã hóa đơn*/
        public IEnumerable<ChiTietHoaDon> LayChiTietHoaDon(string maHD)
        {
            var dsCTHD = from cthd in dt.ChiTietHoaDons
                         where cthd.MaHD.Equals(maHD)
                         select cthd;
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                dt.Transaction = br;
                dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, dsCTHD);
                return dsCTHD;
            }
        }

        /*Hàm tính tổng tiền mặt hàng theo mã hóa đơn*/
        public int TinhTongTienMatHang(string maHD)
        {
            var dsCTHD = from cthd in dt.ChiTietHoaDons
                         where cthd.MaHD.Equals(maHD)
                         select new { ThanhTien = cthd.ThanhTien };
            if (!dsCTHD.GetEnumerator().MoveNext())
                return 0;
            return (int)dsCTHD.Sum(tt => tt.ThanhTien);
        }

        /*Hàm tính thời gian sử dụng theo mã hóa đơn*/
        public int TinhGio(string maHD)
        {
            var gio = (from hd in dt.HoaDons
                       where hd.MaHD.Equals(maHD)
                       select new { GioRa = hd.GioRa, GioVao = hd.GioVao }).First();
            int ts = (int)(gio.GioRa - gio.GioVao).Value.TotalMinutes;

            return ts;
        }

        /*Hàm thêm hóa đơn*/
        public int ThemHoaDon(HoaDon hoaDon)
        {
            using(System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    dt.HoaDons.InsertOnSubmit(hoaDon);
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /*Hàm cập nhật giờ ra cho hóa đơn*/
        public bool CapNhapHoaDon(HoaDon hoaDon)
        {
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    IQueryable<HoaDon> tam = (from n in dt.HoaDons
                                              where n.MaHD == hoaDon.MaHD
                                              select n);
                    tam.First().GioRa = hoaDon.GioRa;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /* Cập nhật đổi phòng*/
        public bool CapNhatDoiPhong(HoaDon hoaDon)
        {
            using(System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    IQueryable<HoaDon> tam = (from n in dt.HoaDons
                                              where n.MaHD == hoaDon.MaHD
                                              select n);
                    tam.First().MaPhong = hoaDon.MaPhong;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        } 

        /*Tìm hóa đơn theo mã khách hàng - Huy*/
        public HoaDon TimHoaDonTheoMaKhachHang(string maKH)
        {
            var kh = from n in dt.HoaDons
                     where n.MaKH.Equals(maKH) && n.TongTien.Equals(null)
                     select n;
            return kh.FirstOrDefault();
        }

        /* Kiểm tra hóa đơn theo mã*/
        public HoaDon KiemTraMaHoaDon(string maHoaDon)
        {
            var q = (from hd in dt.HoaDons
                     where hd.MaHD.Equals(maHoaDon)
                     select hd).FirstOrDefault();
            return q;
        }

        public HoaDon KiemTraMaHoaDonHomNay(string maHoaDon, string homNay)
        {
            var q = (from hd in dt.HoaDons
                     where hd.MaHD.Equals(maHoaDon) && hd.NgayLap.Equals(homNay)
                     select hd).FirstOrDefault();
            return q;
        }

        /*Hàm xóa hóa đơn - hỗ trợ cho việc đổi phòng*/
        public int XoaHoaDon(HoaDon hoaDon)
        {
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    if (KiemTraMaHoaDon(hoaDon.MaHD) != null)
                    {
                        dt.HoaDons.DeleteOnSubmit(hoaDon);
                        dt.SubmitChanges();
                        dt.Transaction.Commit();
                        return 1;
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi!!" + ex.Message);
                }
            }
        }

        /*Tìm hóa đơn theo mã phòng - Huy*/
        public HoaDon TimHoaDonTheoMaPhong(string maPhong)
        {
            var kh = from n in dt.HoaDons
                     where n.MaPhong.Equals(maPhong) && n.TongTien.Equals(null)
                     select n;
            return kh.FirstOrDefault();
        }
    }
}
