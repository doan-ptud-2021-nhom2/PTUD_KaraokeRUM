using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsPhong:clsKetNoi
    {

        qlKaraokeDataContext dt;

        public clsPhong()
        {
            dt = LayData();
        }

        /**
        * Lấy thông tin phòng
        */
        public Phong LayPhong(string maPhong)
        {
            var q = from d in dt.Phongs
                    where d.MaPhong == maPhong
                    select d;
            return q.FirstOrDefault();
        }
        /**
        * Lấy tất cả các phòng
        */
        public IEnumerable<Phong> LayTatCaPhong()
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   select n;
            return q;
        }


        /**
        * Thêm các thông tin phòng
        */
        public int ThemPhong(Phong phong)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = br;
                dt.Phongs.InsertOnSubmit(phong);
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

        /**
        * kiểm tra
        */
        public Phong KiemTra(string maPhong)
        {
            var q = (from x in dt.Phongs
                     where x.MaPhong.Equals(maPhong)
                     select x).FirstOrDefault();
            return q;
        }
        /**
        * Xóa phòng hỗn loạn
        */
        public int XoaPhong(Phong p)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                if (KiemTra(p.MaPhong) != null)
                {
                    dt.Phongs.DeleteOnSubmit(p);
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                dt.Transaction.Rollback();
                throw new Exception("Lỗi!!" + ex.Message);
            }
        }

        /**
        * Tìm kiếm phòng 
        */
        public IEnumerable<Phong> TimPhong(string maPhong)
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   where n.MaPhong.Equals(maPhong)
                                   select n;
            return q;
        }



    }
}
