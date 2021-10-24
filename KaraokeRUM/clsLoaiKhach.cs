using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsLoaiKhach : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsLoaiKhach()
        {
            dt = LayData();
        }

        /**
        * Lấy thông tin loại khách
        */
        public LoaiKhachHang LayLoaiKhach(string maLoaiKhach)
        {
            var lk = from n in dt.LoaiKhachHangs
                    where n.MaLoaiKH == maLoaiKhach
                    select n;
            return lk.FirstOrDefault();
        }

        /**
        * Lấy tất cả các loại phòng
        */
        public IEnumerable<LoaiKhachHang> LayTatCaLoaiKhach()
        {
            IEnumerable<LoaiKhachHang> lk = from n in dt.LoaiKhachHangs
                                           select n;
            return lk;
        }
    }
}
