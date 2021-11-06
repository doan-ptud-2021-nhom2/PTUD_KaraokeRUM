﻿using System;
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

        /**
         * Class KiemTra tạo những hàm dùng để kiểm tra số điện thoại.
         */
        public static bool KiemTraSDT(this string sdt)
        {
            Regex regex = new Regex(@"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9})|(03(\d){8})|(07(\d){8})|(05(\d){8}))$");
            Match match = regex.Match(sdt);
            return match.Success;
        }

        /*Kiểm tra tính hợp lệ của CMND - Không Nhập trùng lặp quá 8 lần*/
        public static bool KiemTraCMNDHopLE(string cmnd)
        {
            char temp = 'x';
            int count = 0;
            foreach (char c in cmnd)
            {
                if (temp == c)
                    count++;
                else
                {
                    temp = c;
                    count = 0;
                }
            }
            if (count > 8)
                return false;
            else
                return true;
        }
        /*Kiểm tra độ dài hợp lệ của CMND*/
        public static bool KiemTraDoDaiCMND(this string cmnd)
        {
            Regex regex = new Regex(@"^((\d){9})$");
            Match match = regex.Match(cmnd);
            return match.Success;
        }


    }
}
