﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsMatHang : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsMatHang()
        {
            dt = LayData();
        }

        /**
        * Lấy tất cả Mặt hàng
        */
        public IEnumerable<MatHang> LayTatCaMatHang()
        {
            IEnumerable<MatHang> q = from n in dt.MatHangs
                                   select n;
            return q;
        }

        /**
        * Lấy loại mặt hàng
        */

        public MatHang LayDonViMatHang(string tenMatHang)
        {
            var loaiMatHang = from n in dt.MatHangs
                                 where n.TenMh.Equals(tenMatHang)
                                 select n;
            return loaiMatHang.First();
        }
    }
}
