using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsKhachHang : clsKetNoi
    {
        qlKaraokeDataContext dt;
        
        public clsKhachHang()
        {
            dt = LayData();
        }
        
        public IEnumerable<KhachHang> LayDSKH()
        {
            IEnumerable<KhachHang> kh = from n in dt.KhachHangs
                                        select n;
            return kh;
           
        }
    }
}
