using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsKhachHang : clsKetNoi
    {
        qlKaraokeDataContext dt;
        
        public clsKhachHang()
        {
            dt = LayData();
        }
        
        /*Hàm lấy toàn bộ danh sách khách hàng*/
        public IEnumerable<KhachHang> LayDSKH()
        {
            IEnumerable<KhachHang> kh = from n in dt.KhachHangs
                                        select n;
            return kh;
           
        }

        /*Hàm tìm tên khách hàng - truyền vào chuỗi số điện thoại*/
        public IQueryable<KhachHang> TimTenKhachHang(string sdt)
        {
            IQueryable<KhachHang> q = (from n in dt.KhachHangs
                                       where n.SDT.Equals(sdt)
                                       select n);
            return q;
        }

        /*Hàm lấy thông tin khách hàng - truyền vào mã khách hàng*/
        public KhachHang LayThongTinKhach(string maKH)
        {
            var in4_kh = from kh in dt.KhachHangs
                     where kh.MaKH.Equals(maKH)
                     select kh;
            return in4_kh.FirstOrDefault();
        }

        /*Hàm tìm khách hàng - truyền vào chuỗi số điện thoại*/
        public KhachHang TimKhachHang(string sdt)
        {
            KhachHang k = (from n in dt.KhachHangs where n.SDT == sdt select n).FirstOrDefault();
            return k;
        }

        /*Hàm thêm khách hàng - truyền vào một đối tượng khách hàng*/
        public int ThemKhachHang(KhachHang khach)
        {
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = br;
                    dt.KhachHangs.InsertOnSubmit(khach);
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

        /** Hàm lấy khách hàng và loại khách hàng
          * join 2 bảng: KhachHang với LoaiKhachHang
          * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
          */
        public IEnumerable<dynamic> KhachHangVaLoaiKhachHang()
        {
            var kh = from n in dt.KhachHangs
                     join x in dt.LoaiKhachHangs
                     on n.MaLoaiKH equals x.MaLoaiKH
                     where n.GhiChu == null
                     select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen, x.TenLoaiKH, x.ChietKhau };
            return kh;
        }

        /** Hàm lấy danh sách khách hàng
        * join 2 bảng: KhachHang với LoaiKhachHang
        * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
        * Có điều kiện
        */
        public IEnumerable<dynamic> LayKhachHangVaLoaiKhachHangTheoLoai(string maLoaiKH)
        {
            var kh = from n in dt.LoaiKhachHangs
                     join x in dt.KhachHangs
                     on n.MaLoaiKH equals x.MaLoaiKH
                     where n.MaLoaiKH.Equals(maLoaiKH) && x.GhiChu == null
                     select new { x.MaKH, x.TenKhach, x.SDT, x.SoLanDen, n.TenLoaiKH, n.ChietKhau };
            return kh;
        }

        /*Hàm lấy danh sách khách hàng được thêm vào danh sách đen*/
        public IEnumerable<dynamic> KhachHangVaLoaiKhachHangDanhSachDen()
        {
            var kh = from n in dt.KhachHangs
                     where n.GhiChu != null
                     select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen,n.GhiChu};
            return kh;
        }

        /*
         * Hàm tìm khách hàng bằng tên hoặc mã khách hàng
         */
        public IEnumerable<dynamic> TimKhach(string timKiem)
        {
            IEnumerable<dynamic> kh = from n in dt.KhachHangs
                                      join x in dt.LoaiKhachHangs
                                      on n.MaLoaiKH equals x.MaLoaiKH
                                      where n.TenKhach.Contains(timKiem) || n.MaKH.Contains(timKiem)
                                      select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen, x.TenLoaiKH, x.ChietKhau , n.GhiChu};
            return kh;
        }

        /*
        * Hàm tìm kiếm khách hàng theo mã khách hàng
        */
        public IQueryable<KhachHang> TimKhachHangTheoMa(string maKH)
        {
            IQueryable<KhachHang> q = (from n in dt.KhachHangs
                                       where n.MaKH.Equals(maKH)
                                       select n);
            return q;
        }

        /*Hàm cập nhật số lần đến cho khách hàng*/
        public int SuaSoLanDen(KhachHang kh)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<KhachHang> temp = (from n in dt.KhachHangs
                                                  where n.MaKH == kh.MaKH
                                                  select n);
                    temp.First().SoLanDen = kh.SoLanDen;
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

        /*
         * Cập nhật tổng tiền cho khách hàng
         */
        public int CapNhatTongTienChoKhach(KhachHang khachHang)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<KhachHang> temp = (from n in dt.KhachHangs
                                                  where n.MaKH == khachHang.MaKH
                                                  select n);
                    temp.First().TongTien = khachHang.TongTien;
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

        /*Hàm cập nhập ghi chú của khách hàng*/
        public bool CapNhatGhiChuSDT(KhachHang kh)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<KhachHang> tam = (from n in dt.KhachHangs
                                                 where n.MaKH == kh.MaKH
                                                 select n);
                    tam.First().GhiChu = kh.GhiChu;
                    tam.First().SDT = kh.SDT;
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
