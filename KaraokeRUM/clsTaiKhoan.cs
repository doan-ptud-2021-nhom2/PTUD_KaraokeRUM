using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsTaiKhoan : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsTaiKhoan()
        {
            dt = LayData();
        }

        //Hàm lấy tài khoản, truyền vào tham số username: String
        //Trả về một tài khoản tồn tại.
        public TaiKhoan LayTaiKhoan(string tenDangNhap)
        {
            var taiKhoan = (from tk in dt.TaiKhoans
                            where tk.UserName == tenDangNhap
                            select tk).FirstOrDefault();
            return taiKhoan;
        }

        //Hàm kiểm tra tài khoản, truyền vào tham số là một taiKhoan: TaiKhoan
        //Trả về true nếu cả tài khoản và mật khẩu đều khớp với dữ liệu trong database.
        public bool KiemTraTaiKhoan(TaiKhoan taiKhoan)
        {
            TaiKhoan tk = LayTaiKhoan(taiKhoan.UserName);
            if(tk == null)
            {
                //throw new Exception("TK null");
                return false;
            }

            if(taiKhoan.UserName.Equals(tk.UserName) && taiKhoan.PassWord.Equals(tk.PassWord.Trim()))
            {
                //throw new Exception("TK yes");
                return true;
            }
            //throw new Exception("TK no");
            return false;
        }

        //Hàm lấy loại tài khoản, truyền vào tham số là một taiKhoan: TaiKhoan
        //Trả về mã của loại nhân viên từ đó so sánh để phân quyền đăng nhập
        public String LayLoaiTaiKhoan(TaiKhoan taiKhoan)
        {
            var strMaLoaiTaiKhoan = (from tk in dt.TaiKhoans
                                   join nv in dt.NhanViens on tk.UserName equals nv.MaNV
                                   where tk.UserName.Equals(taiKhoan.UserName)
                                   select nv.MaLNV).FirstOrDefault();
            return strMaLoaiTaiKhoan;
        }

        //Hàm lấy lại mật khẩu, truyền vào username: String và sdt: String
        //Trả về mật khẩu của nhân viên
        public String TimMatKhau(String tenDangNhap, String sdt)
        {
            var taiKhoan = (from tk in dt.TaiKhoans
                           join nv in dt.NhanViens on tk.UserName equals nv.MaNV
                           where tk.UserName.Equals(tenDangNhap) && nv.SDT.Equals(sdt)
                           select tk).FirstOrDefault();
            return taiKhoan == null ? "" : taiKhoan.PassWord ;
        }
    }
}
