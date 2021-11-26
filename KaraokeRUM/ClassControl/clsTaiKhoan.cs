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

        /*Hàm lấy tài khoản, truyền vào tham số username: String -Trả về một tài khoản tồn tại.*/
        public TaiKhoan LayTaiKhoan(string tenDangNhap)
        {
            var taiKhoan = (from tk in dt.TaiKhoans
                            where tk.UserName == tenDangNhap
                            select tk).FirstOrDefault();
            if (taiKhoan == null)
                return taiKhoan;
            else
            {
                dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, taiKhoan);
                return taiKhoan;
            }
        }

        /*Hàm kiểm tra tài khoản, truyền vào tham số là một taiKhoan: TaiKhoan
        Trả về true nếu cả tài khoản và mật khẩu đều khớp với dữ liệu trong database.*/
        public bool KiemTraTaiKhoan(TaiKhoan taiKhoan)
        {
            TaiKhoan tk = LayTaiKhoan(taiKhoan.UserName);
            if(tk == null)
            {
                return false;
            }

            if(taiKhoan.UserName.Equals(tk.UserName) && taiKhoan.PassWord.Equals(tk.PassWord.Trim()))
            {
                return true;
            }
            return false;
        }

        /*Hàm lấy loại tài khoản, truyền vào tham số là một taiKhoan: TaiKhoan
        Trả về mã của loại nhân viên từ đó so sánh để phân quyền đăng nhập*/
        public String LayLoaiTaiKhoan(TaiKhoan taiKhoan)
        {
            var strMaLoaiTaiKhoan = (from tk in dt.TaiKhoans
                                   join nv in dt.NhanViens on tk.UserName equals nv.MaNV
                                   where tk.UserName.Equals(taiKhoan.UserName)
                                   select nv.MaLNV).FirstOrDefault();
            return strMaLoaiTaiKhoan;
        }

        /*Hàm lấy lại mật khẩu, truyền vào username: String và sdt: String
        Trả về mật khẩu của nhân viên*/
        public String TimMatKhau(String tenDangNhap, String sdt)
        {
            var taiKhoan = (from tk in dt.TaiKhoans
                           join nv in dt.NhanViens on tk.UserName equals nv.MaNV
                           where tk.UserName.Equals(tenDangNhap) && nv.SDT.Equals(sdt)
                           select tk).FirstOrDefault();
            return taiKhoan == null ? "" : taiKhoan.PassWord.Trim();
        }

        /*Hàm tìm mật khẩu theo tên đăng nhập - truyền vào tên đăng nhập*/
        public String TimMatKhau(String tenDangNhap)
        {
            var taiKhoan = (from tk in dt.TaiKhoans
                            where tk.UserName.Equals(tenDangNhap) 
                            select tk).FirstOrDefault();
            return taiKhoan == null ? "" : taiKhoan.PassWord.Trim();
        }

        /*Hàm đổi mật khẩu cho tài khoản - truyền vào đối tượng tài khoản*/
        public int DoiMatKhau(TaiKhoan taiKhoan)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<TaiKhoan> tkTam = (from tk in dt.TaiKhoans
                                                  where tk.UserName == taiKhoan.UserName
                                                  select tk);
                    tkTam.First().PassWord = taiKhoan.PassWord;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi đổi mật khẩu: " + ex.Message);
                }
            }
            
        }

        /*Thêm một tài khoản - truyền vào đối tượng tài khoản*/
        public int ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            using(System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    dt.TaiKhoans.InsertOnSubmit(taiKhoan);
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
           
        }

    }
}
