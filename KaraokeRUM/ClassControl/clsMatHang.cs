using System;
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
                                     where !n.TrangThai.Equals("KSD")
                                   select n;
            return q;
        }

        /**
         * Lấy tên mặt hàng - truyền vào mã mặt hàng
         */
        public MatHang TimTheoMa(string maMH)
        {
            var matHang = from mh in dt.MatHangs
                          where mh.MaMH.Equals(maMH)
                          select mh;
            return matHang.First();
        }

        /**
         * Tìm mã mặt hàng - truyền vào tên mặt hàng
         */
        public MatHang TimMaTheoTen(string tenMH)
        {
            var matHang = from mh in dt.MatHangs
                          where mh.TenMh.Equals(tenMH) && !mh.TrangThai.Equals("KSD")
                          select mh;
            return matHang.First();
        }

        /*Tìm mặt hàng - truyền vào tên mặt hàng hoặc mã mặt hàng*/
        public IEnumerable<MatHang> TimMatHang(string timKiem)
        {
            IEnumerable<MatHang> nv = from n in dt.MatHangs
                                      where (n.MaMH.Contains(timKiem) || n.TenMh.Contains(timKiem) ) && n.TrangThai.Equals("DSD")
                                      select n;
            return nv;
        }

        /*Tìm mặt hàng - truyền vào tên mặt hàng hoặc mã mặt hàng để thực hiện chức năng thêm*/
        public IEnumerable<MatHang> TimMatHangTenChinhXac(string timKiem)
        {
            IEnumerable<MatHang> nv = from n in dt.MatHangs
                                      where n.TenMh.Equals(timKiem) && n.TrangThai.Equals("DSD")
                                      select n;
            return nv;
        }

        /*Thêm một mặt hàng - truyền vào một đối tượng mặt hàng*/
        public int ThemMatHang(MatHang matHang)
        {
            using(System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    dt.MatHangs.InsertOnSubmit(matHang);
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

        /*Sửa thông tin mặt hàng - truyền vào một đối tượng mặt hàng.*/
        public bool SuaMatHang(MatHang matHang)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<MatHang> tam = (from n in dt.MatHangs
                                               where n.MaMH == matHang.MaMH
                                               select n);
                    tam.First().TenMh = matHang.TenMh;
                    tam.First().Loai = matHang.Loai;
                    tam.First().SoLuongTon = matHang.SoLuongTon;
                    tam.First().DonVi = matHang.DonVi;
                    tam.First().Gia = matHang.Gia;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi không sửa được!" + ex.Message);

                }
            }
        }


        /*Xóa một mặt hàng(Cập nhật lại trạng thái) - truyền vào một đối tượng mặt hàng*/
        public bool XoaMatHang(MatHang matHang)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<MatHang> tam = (from n in dt.MatHangs
                                               where n.MaMH == matHang.MaMH
                                               select n);
                    tam.First().TrangThai = matHang.TrangThai;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi không sửa được!" + ex.Message);

                }
            }  
        }
        
        /*Hàm lấy tắt cả mặt hàng*/
        public IEnumerable<MatHang> LayTatCaMatHangTonTai()
        {
            IEnumerable<MatHang> q = from n in dt.MatHangs
                                        select n;
            return q;
        }
    }
}
