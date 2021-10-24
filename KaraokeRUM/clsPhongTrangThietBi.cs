using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsPhongTrangThietBi : clsKetNoi
    {
        qlKaraokeDataContext dt;
        public clsPhongTrangThietBi()
        {
            dt = LayData();
        }
        public IEnumerable<dynamic> TraDuLieu()
        {
            var q = from d in dt.TrangThietBis
                    join c in dt.Phong_TrangThietBis on d.MaTTB equals c.MaTTB
                    join s in dt.Phongs on c.MaPhong equals s.MaPhong
                    join z in dt.LoaiPhongs on s.MaLoaiPhong equals z.MaLoaiPhong
                    select new {z.TenLoaiPhong,s.TenPhong,d.TenTTB,c.SoLuong};
            return q;
        }
    }
}
