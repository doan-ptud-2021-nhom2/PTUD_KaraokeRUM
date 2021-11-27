using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsChiTietHoaDon : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsChiTietHoaDon()
        {
            dt = LayData();
        }

        /**
        * Tìm kiếm chi danh sách chi tiết hóa đơn thông qua mã mặt hàng và mã hóa đơn
        */
        public IEnumerable<ChiTietHoaDon> TimChiTietHoaDon(string maMatHang, string maHoaDon)
        {
            IEnumerable<ChiTietHoaDon> q = (from n in dt.ChiTietHoaDons
                                            where n.MaMH.Equals(maMatHang) && n.MaHD.Equals(maHoaDon)
                                            select n);
            return q;
        }

        /**
        * Thêm một chi tiết hóa đơn vào hóa đơn
        */
        public int ThemChiTietHoaDon(ChiTietHoaDon chiTietHoaDon)
        {
            using(System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    dt.ChiTietHoaDons.InsertOnSubmit(chiTietHoaDon);
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

        /**
        * Sửa thông tin Mặt hàng (số lượng) trong chi tiết hóa đơn.
        */
        public bool SuaThongTinMatHang(ChiTietHoaDon chiTietHD)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<ChiTietHoaDon> tam = (from n in dt.ChiTietHoaDons
                                                     where n.MaMH.Equals(chiTietHD.MaMH) && n.MaHD.Equals(chiTietHD.MaHD)
                                                     select n);
                    dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, tam);
                    tam.First().SoLuong = chiTietHD.SoLuong;
                    tam.First().ThanhTien = chiTietHD.ThanhTien;
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

        /**
        * Kiểm tra chi tiết hóa đơn này đã tồn tại hay chưa.
        * Dùng để tránh việc trùng key sinh ra bug.
        */
        public ChiTietHoaDon KiemTraMa(string maMatHang, string maHoaDon)
        {
            var q = (from x in dt.ChiTietHoaDons
                     where x.MaMH.Equals(maMatHang) && x.MaHD.Equals(maHoaDon)
                     select x).FirstOrDefault();
            return q;
        }

        /**
        * Xóa chi tiết hóa đơn trong hóa đơn.
        */
        public int XoaChiTietHoaDon(ChiTietHoaDon chiTietHoaDon)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction()) 
            { 
                try
                {
                    dt.Transaction = myTran;
                    if (KiemTraMa(chiTietHoaDon.MaMH, chiTietHoaDon.MaHD) != null)
                    {
                        dt.ChiTietHoaDons.DeleteOnSubmit(chiTietHoaDon);
                        dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, chiTietHoaDon);
                        dt.SubmitChanges();
                        dt.Transaction.Commit();
                        return 1;
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Lỗi xóa chi tiết hóa đơn!!" + ex.Message);
                }
            }
            
        }

        /**
        * Tìm chi tiết hoá đơn theo mã mặt hàng
        * Tránh bug nếu lữo xoá 1 mặt hàng đang được đặt
        */
        public  IEnumerable<ChiTietHoaDon> TimChiTietHoaDonTheoMaMH(string maMatHang)
        {
            var q = (from x in dt.ChiTietHoaDons
                     where x.MaMH.Equals(maMatHang)
                     select x);
            return q;
        }

    }
}
