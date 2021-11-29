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

        /*Chức năng thêm thiết bị vào phòng - truyền vào một đối tượng phong_trangthietbi*/
        public int Them(dynamic ttb)
        {
            using (System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction())
            {
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
        }

       /* Chức năng sửa thông tin thiết bị trong phòng - truyền vào một đối tượng phong_trangthietbi*/
        public int SuaTrangThietBi(Phong_TrangThietBi tb)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
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
        }

        /*Tìm thiết bị trong phòng theo tên thiết bị và mã phòng - truyền vào mã phòng và mã trang thiết bị*/
        public IEnumerable<Phong_TrangThietBi> TimTTBtrongPhongTheoTenVaMaTTB(string maPhong, string maTTB)
        {
            IEnumerable<Phong_TrangThietBi> q = from n in dt.Phong_TrangThietBis
                                                where n.MaPhong.Equals(maPhong) && n.MaTTB.Equals(maTTB)
                                                select n;
            return q;
        }
        /*Tìm thiết bị trong phòng theo tên thiết bị và mã phòng - truyền vào mã phòng và mã trang thiết bị*/
        public Phong_TrangThietBi TimTTBtrongPhongTheoTenVaMaTTB2(string maPhong, string maTTB)
        {
            Phong_TrangThietBi q = (from n in dt.Phong_TrangThietBis
                                    where n.MaPhong.Equals(maPhong) && n.MaTTB.Equals(maTTB)
                                    select n).FirstOrDefault();
            return q;
        }
        /*Tìm thiết bị trong phòng theo mã phòng - truyền vào mã phòng*/
        public IEnumerable<Phong_TrangThietBi> TimPhongTTB(string maPhong)
        {
            IEnumerable<Phong_TrangThietBi> q = from n in dt.Phong_TrangThietBis
                                                where n.MaPhong.Equals(maPhong) 
                                                select n;
            return q;
        }

        /*Chức năng xóa thiết bị ra khỏi phòng - truyền vào một đối tượng phong_trangthietbi*/
        public int Xoa(Phong_TrangThietBi ttb)
        {
            using(System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction())
            {
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

        /*Lấy tất cả trang thiết bị trong phòng*/
        public IEnumerable<Phong_TrangThietBi> LayToanBoTrangThietBiTrongPhong(string maPhong, string maTTB)
        {
            IEnumerable<Phong_TrangThietBi> tbp = from n in dt.Phong_TrangThietBis
                                           where n.MaPhong.Equals(maPhong) && n.MaTTB.Equals(maTTB)
                                           select n;
            return tbp;
        }
    }
}
