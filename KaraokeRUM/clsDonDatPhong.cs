using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsDonDatPhong : clsKetNoi
    {
        qlKaraokeDataContext dt;
        public clsDonDatPhong()
        {
            dt = LayData();
        }
        /*Lấy tất cả danh sách đơn đặt phòng*/
        public IEnumerable<DonDatPhong> TraTatCaDDP()
        {
            IEnumerable<DonDatPhong> ddp = from n in dt.DonDatPhongs select n;
            return ddp;
        }
        /*Tim đơn đặt phòng theo mã*/
        public DonDatPhong TimDDPhong(string maPhong)
        {

            foreach (DonDatPhong i in dt.DonDatPhongs)
            {
                if (i.MaPhong == maPhong)
                    return i;
            }
            return null;
        }
        /*Chức năng thêm đơn đặt phòng*/
        public int ThemDonDatPhong(DonDatPhong donDatPhong)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = br;
                dt.DonDatPhongs.InsertOnSubmit(donDatPhong);
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
        /*Kiểm tra mã đơn đặt phòng có trùng hay không*/
        public DonDatPhong KiemTra(string id)
        {
            DonDatPhong temp = (from n in dt.DonDatPhongs
                                where n.MaDDP.Equals(id)
                                select n).FirstOrDefault();
            return temp;
        }
        /*Tìm đơn đặt phòng theo ngày*/
        public IEnumerable<DonDatPhong> TimDonDatPhongTheoNgay(string ngay)
        {
            var temp = (from n in dt.DonDatPhongs
                                where n.NgayNhan.ToString().Equals(ngay)
                                select n);
            return temp;
        }
        /*Chức năng xóa đơn đặt phòng*/
        public int Xoa(DonDatPhong ddp)
        {
            System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = tran;
                if (KiemTra(ddp.MaDDP) != null)
                {
                    dt.DonDatPhongs.DeleteOnSubmit(ddp);
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                dt.Transaction.Rollback();
                throw new Exception("Loi" + ex.Message);
            }
        }
    }
}
