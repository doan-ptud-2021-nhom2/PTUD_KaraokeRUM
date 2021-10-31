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
        public IEnumerable<HoaDon> LayToanBoHoaDon()
        {
            IEnumerable<HoaDon> tb = from n in dt.HoaDons select n;
            return tb;
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
        public int ThemHoaDon(HoaDon hoaDon)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
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
}
