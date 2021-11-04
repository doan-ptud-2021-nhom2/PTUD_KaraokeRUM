using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsPhongTrangThietBi : clsKetNoi
    {
        qlKaraokeDataContext dt;
        public clsPhongTrangThietBi()
        {
            dt = LayData();
        }
        /*Lấy toàn bộ dữ liệu thiết bị trong phòng*/
        public IEnumerable<Phong_TrangThietBi> TraTatCaDuLieu()
        {
            var q = from d in dt.Phong_TrangThietBis
                    select d;
            return q;
        }    
        /*Chức năng thêm thiết bị vào phòng*/
        public int Them(dynamic ttb)
        {
            System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = tran;
                dt.Phong_TrangThietBis.InsertOnSubmit(ttb);
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
       /* Chức năng sửa thông tin thiết bị trong phòng*/
        public int SuaTrangThietBi(Phong_TrangThietBi tb)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<Phong_TrangThietBi> temp = (from n in dt.Phong_TrangThietBis
                                                       where n.MaTTB == tb.MaTTB && n.MaPhong == tb.MaPhong
                                                       select n);
                temp.First().SoLuong = tb.SoLuong;
                dt.SubmitChanges();
                dt.Transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                dt.Transaction.Rollback();
                throw new Exception("Loi không sửa được!" + ex.Message);

            }
        }
        /*Tìm thiết bị trong phòng theo tên thiết bị và mã phòng*/
        public IEnumerable<Phong_TrangThietBi> TimTTBtrongPhongTheoTenVaMaTTB(string maPhong, string maTTB)
        {
            IEnumerable<Phong_TrangThietBi> q = from n in dt.Phong_TrangThietBis
                                                where n.MaPhong.Equals(maPhong) && n.MaTTB.Equals(maTTB)
                                                select n;
            return q;
        }
        /*Tìm thiết bị trong phòng theo mã phòng*/
        public IEnumerable<Phong_TrangThietBi> TimPhongTTB(string maPhong)
        {
            IEnumerable<Phong_TrangThietBi> q = from n in dt.Phong_TrangThietBis
                                                where n.MaPhong.Equals(maPhong) 
                                                select n;
            return q;
        }
        /*Chức năng xóa thiết bị ra khỏi phòng*/
        public int Xoa(Phong_TrangThietBi ttb)
        {
            System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = tran;
                dt.Phong_TrangThietBis.DeleteOnSubmit(ttb);
                dt.SubmitChanges();
                dt.Transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                dt.Transaction.Rollback();
                throw new Exception("Loi" + ex.Message);
            }
        }
    }
}
