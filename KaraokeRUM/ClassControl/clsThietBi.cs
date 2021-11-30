using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsThietBi:clsKetNoi
    {
        qlKaraokeDataContext dt;
        public clsThietBi()
        {
            dt = LayData();
        }

        /*Lấy tất cả trang thiết bị*/
        public IEnumerable<TrangThietBi> LayToanBoTrangThietBis()
        {
            IEnumerable<TrangThietBi> tb = from n in dt.TrangThietBis 
                                           where n.TrangThai.Equals("DSD")
                                           select n;
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                dt.Transaction = br;
                dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, tb);
                return tb;
            }
        }

        /*Lấy danh sách thiết bị để hỗ trợ cho việc đếm để tạo mã thiết bị*/
        public IEnumerable<TrangThietBi> DemDanhSachThietBi()
        {
            IEnumerable<TrangThietBi> tb = from n in dt.TrangThietBis
                                           select n;
            return tb;
        }

        /*Kiểm tra thiết bị để hỗ trợ cho việc thêm - truyền vào mã thiết bị*/
        public TrangThietBi KiemTra(string id)
        {
            TrangThietBi temp = (from n in dt.TrangThietBis
                                 where n.MaTTB.Equals(id)
                              select n).FirstOrDefault();
            return temp;
        }

        /*Thêm trang thiết bị - truyền vào một đối tượng trang thiết bị*/
        public int Them(TrangThietBi tb)
        {
            using(System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = tran;
                    dt.TrangThietBis.InsertOnSubmit(tb);
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

        /* Sửa trang thiết bị - truyền vào một đối tượng trang thiết bị*/
        public int SuaTrangThietBi(TrangThietBi tb)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<TrangThietBi> temp = (from n in dt.TrangThietBis
                                                     where n.MaTTB == tb.MaTTB
                                                     select n);
                    temp.First().TenTTB = tb.TenTTB;
                    temp.First().SoLuongTon = tb.SoLuongTon;
                    temp.First().DonVi = tb.DonVi;
                    temp.First().Gia = tb.Gia;
                    temp.First().TrangThai = tb.TrangThai;
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

        /*Hàm tìm danh sách thiết bị theo mã hoặc tên - truyền vào mã thiết bị hoặc tên thiết bị*/
        public IEnumerable<TrangThietBi> TimDSachTTBTheoMa(string timKiem)
        {
            IEnumerable<TrangThietBi> q = from n in dt.TrangThietBis
                                          where (n.MaTTB.Equals(timKiem) || n.TenTTB.Equals(timKiem)) && n.TrangThai.Equals("DSD")
                                          select n;
            return q;
        }

        /*Tìm thiết bị theo mã - truyền vào mã thiết bị*/ 
        public TrangThietBi TimTTBTheoMa(string maTTB)
        {
            TrangThietBi q = (from n in dt.TrangThietBis
                              where n.MaTTB.Equals(maTTB)
                              select n).First();
            return q;
        }

        /* Tìm thiết bị theo tên - truyền vào tên thiết bị*/
        public IQueryable<TrangThietBi> TimThietBiTheoTen(string tenTB)
        {
            IQueryable<TrangThietBi> q = (from n in dt.TrangThietBis
                                          where n.TenTTB.Equals(tenTB) && n.TrangThai.Equals("DSD")
                                          select n);
            return q;
        }
    }
}
