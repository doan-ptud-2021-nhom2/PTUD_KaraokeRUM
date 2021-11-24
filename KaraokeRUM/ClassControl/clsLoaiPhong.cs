using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsLoaiPhong:clsKetNoi
    {

        qlKaraokeDataContext dt;

        public clsLoaiPhong()
        {
            dt = LayData();
        }

        /**
        * Lấy thông tin loại phòng - truyền vào mã loại phòng
        */
        public LoaiPhong LayLoaiPhong(string maLoaiPhong)
        {
            var q = from d in dt.LoaiPhongs
                    where d.MaLoaiPhong.Equals(maLoaiPhong)
                    select d;
            return q.FirstOrDefault();
        }

        /**
        * Lấy tất cả các loại phòng
        */
        public IEnumerable<LoaiPhong> LayTatCaLoaiPhong()
        {
            IEnumerable<LoaiPhong> q = from n in dt.LoaiPhongs
                                       select n;
            return q;
        }

        /**
        * Tìm kiếm loại phòng - truyền vào tên loại phòng
        */
        public IQueryable<LoaiPhong> TimLoaiPhong(string tenLoaiPhong)
        {
            IQueryable<LoaiPhong> q = (from n in dt.LoaiPhongs
                                       where n.TenLoaiPhong.Equals(tenLoaiPhong)
                                       select n);
            return q;
        }

        /**
        * Cập nhật giá loại phòng - truyền vào một đối tượng loại phòng
        */
        public bool CapNhatGiaLoaiPhong(LoaiPhong lp)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<LoaiPhong> tam = (from n in dt.LoaiPhongs
                                                 where n.MaLoaiPhong == lp.MaLoaiPhong
                                                 select n);
                    tam.First().Gia = lp.Gia;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi không thể sửa giá Phòng này!" + ex.Message);
                }
            }
            
        }

    }
}
