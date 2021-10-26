using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    static class clsKiemTra
    {
        /**
         * Class KiemTra tạo những hàm dùng để kiểm tra input.
         */
        public static bool KiemTraMatKhau(this string matKhau)
        {
            Regex regex = new Regex(@"^(.{7,20}|[^0-9]*|[^A-Z])$");
            Match match = regex.Match(matKhau);
            return match.Success;
        }

        /**
         * Class KiemTra tạo những hàm dùng để kiểm tra input.
         */
        public static bool KiemTraSoTien(this string soTien)
        {
            Regex regex = new Regex(@"^[1-9]{1}[0-9]*$");
            Match match = regex.Match(soTien);
            return match.Success;
        }
    }
}
