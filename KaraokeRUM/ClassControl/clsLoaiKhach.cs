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

        /*Hàm lấy loại khách hàng - truyền vào mã loại khách hàng*/
        public LoaiKhachHang LayLoaiKhach(string maLoaiKhach)
        {
            var lk = from n in dt.LoaiKhachHangs
                    where n.MaLoaiKH == maLoaiKhach
                    select n;
            return lk.FirstOrDefault();
        }

        /*Hàm lấy loại khách hàng - truyền vào mã loại khách hàng*/
        public IQueryable<LoaiKhachHang> TimLoaiKhachHangTheoMaLoai(string maLoai)
        {
            IQueryable<LoaiKhachHang> q = (from n in dt.LoaiKhachHangs
                                           where n.MaLoaiKH.Equals(maLoai)
                                           select n);
            return q;
        }

        /*Hàm cập nhật chiết khấu cho loại khách hàng - truyền vào đối tượng loại khách hàng*/
        public bool CapNhatChietKhau(LoaiKhachHang lk)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<LoaiKhachHang> tam = (from n in dt.LoaiKhachHangs
                                                     where n.MaLoaiKH == lk.MaLoaiKH
                                                     select n);
                    tam.First().ChietKhau = lk.ChietKhau;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi không thể sửa chiết khấu Khách hàng này!" + ex.Message);
                }
            }
            
        }
    }
}
