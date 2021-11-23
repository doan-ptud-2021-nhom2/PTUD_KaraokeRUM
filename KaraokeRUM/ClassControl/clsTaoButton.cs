using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsTaoButton : clsKetNoi
    {
        qlKaraokeDataContext dt;
        public clsTaoButton()
        {
            dt = LayData();
        }
        /*public IEnumerable<dynamic> TraSoPhong()
        {
            var q = from n in dt.DonDatPhongs
                    join m in dt.Phongs
                    on n.MaPhong equals m.MaPhong
                    select new { n.MaPhong, m.TenPhong };
            return q;
        }*/
        /*Lấy trạng thái phòng*/
        public IEnumerable<Phong> LayTrangThaiPhong(string maLoaiPhong)
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   where n.MaLoaiPhong.Equals(maLoaiPhong)
                                   select n;
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                dt.Transaction = br;
                dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, q);
                return q;
            }
        }
    }
}
