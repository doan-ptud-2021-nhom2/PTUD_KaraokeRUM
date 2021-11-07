﻿using System;
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
        
        public IEnumerable<KhachHang> LayDSKH()
        {
            IEnumerable<KhachHang> kh = from n in dt.KhachHangs
                                        select n;
            return kh;
           
        }
        public IQueryable<KhachHang> TimTenKhachHang(string sdt)
        {
            IQueryable<KhachHang> q = (from n in dt.KhachHangs
                                       where n.SDT.Equals(sdt)
                                       select n);
            return q;
        }
      /*  public KhachHang TimKhachHangCu(string sdt)
        {
            KhachHang i = from kh in dt.KhachHangs
                    where kh.SDT.Equals(sdt)
                    select kh;
            return i;
        }*/
        public KhachHang LayThongTinKhach(string maKH)
        {
            var in4_kh = from kh in dt.KhachHangs
                     where kh.MaKH.Equals(maKH)
                     select kh;
            return in4_kh.FirstOrDefault();
        }
        public KhachHang TimKhachHang(string sdt)
        {
            KhachHang k = (from n in dt.KhachHangs where n.SDT == sdt select n).FirstOrDefault();
            return k;
        }
        /**
       * Thêm các thông tin Khách Hàng
       */
        public int ThemKhachHang(KhachHang khach)
        {
            System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction();
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
        /**
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
        /**
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
        public IEnumerable<dynamic> KhachHangVaLoaiKhachHangDanhSachDen()
        {
            var kh = from n in dt.KhachHangs
                     where n.GhiChu != null
                     select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen,n.GhiChu};
            return kh;
        }
        /**
        * join 2 bảng: KhachHang với LoaiKhachHang
        * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
        * Có điều kiện
        */
        /*
         * tìm kiếm khách hàng
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
       * tìm kiếm khách hàng theo mã
       */
        public IQueryable<KhachHang> TimKhachHangTheoMa(string maKH)
        {
            IQueryable<KhachHang> q = (from n in dt.KhachHangs
                                       where n.MaKH.Equals(maKH)
                                       select n);
            return q;
        }

        public int SuaSoLanDen(KhachHang kh)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
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

        /*
         * Cập nhật tổng tiền cho khách hàng
         */
        public int CapNhatTongTienChoKhach(KhachHang khachHang)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
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
        /*cập nhập ghi chú của khách hàng*/
        public bool CapNhatGhiChu(KhachHang kh)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<KhachHang> tam = (from n in dt.KhachHangs
                                                 where n.MaKH == kh.MaKH
                                                 select n);
                tam.First().GhiChu = kh.GhiChu;
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
