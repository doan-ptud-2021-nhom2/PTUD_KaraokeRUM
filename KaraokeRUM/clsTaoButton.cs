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
        public IEnumerable<dynamic> TraSoPhong()
        {
            var q = from n in dt.DonDatPhongs
                    join m in dt.Phongs
                    on n.MaPhong equals m.MaPhong
                    select new { n.MaPhong, m.TenPhong };
            return q;
        }
    }
}
