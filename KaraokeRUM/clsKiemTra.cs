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
            Regex regex = new Regex(@"^(.{8,20}|[^0-9]*|[^A-Z])$");
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

        /**
         * Class KiemTra tạo những hàm dùng để kiểm tra số điện thoại.
         */
        public static bool KiemTraSDT(this string sdt)
        {
            Regex regex = new Regex(@"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9})|(03(\d){8})|(07(\d){8})|(05(\d){8}))$");
            Match match = regex.Match(sdt);
            return match.Success;
        }

    }
}
