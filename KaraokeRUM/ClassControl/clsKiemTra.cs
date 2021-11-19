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
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?!.*\s).{8,}$");
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
            Regex regex = new Regex(@"^((09(\d){8})|(08(\d){8})|(03(\d){8})|(07(\d){8})|(05(\d){8}))$");
            Match match = regex.Match(sdt);
            return match.Success;
        }

        /*Kiểm tra tính hợp lệ của CMND - Không Nhập trùng lặp quá 8 lần*/
        public static bool KiemTraCMNDHopLe(string cmnd)
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
            if (count > 7)
            {
                return false;
            }    
                
            else
            {
                return true;
            }    
               
        }
        /*Kiểm tra độ dài hợp lệ của CMND*/
        public static bool KiemTraDoDaiCMND(this string cmnd)
        {
            Regex regex = new Regex(@"^((\d){9})$");
            Match match = regex.Match(cmnd);
            return match.Success;
        }

        /*
         * Kiểm tra số lượng mặt hàng trong phòng (phải là số)
         */
        public static bool KiemTraSoLuongMatHangTrongPhong(this string soLuong)
        {
            Regex regex = new Regex("^[0-9]+$");
            Match match = regex.Match(soLuong);
            return match.Success;
        }

        /*
         * Kiểm tra Tên phòng theo định dạng: TXXX hoặc VXXX (X là số từ 0-9)
         */
        public static bool KiemTraTenPhong(this string tenPhong)
        {
            Regex regex = new Regex(@"^[T||V]\d{3}$");
            Match match = regex.Match(tenPhong);
            return match.Success;
        }

    }
}
