﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsPhong:clsKetNoi
    {

        qlKaraokeDataContext dt;
        clsLoaiPhong lp;

        public clsPhong()
        {
            dt = LayData();
            lp = new clsLoaiPhong();
        }        

        /*Lấy tất cả các phòng*/
        public IEnumerable<Phong> LayTatCaPhong()
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   select n;
            return q;
        }

        /*Tìm một phòng theo mã*/
        public Phong TimMotPhongTheoMa(string maPhong)
        {
            foreach (Phong i in dt.Phongs)
            {
                if (i.MaPhong == maPhong)
                    return i;
            }
            return null;
        }

        /**
        * Lấy tất cả các phòng
        * fix load lên ListView (Trạng thái phòng đóng)
        */
        public IEnumerable<Phong> LayTatCaPhongDong()
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   where n.TrangThaiPhong.Equals("Đóng")
                                   select n;
            return q;
        }

        /*Tìm kiếm theo mã phòng (trấn thêm)*/
        public Phong LayThongTinPhong(string maPhong)
        {
            var phong = (from p in dt.Phongs
                          where p.MaPhong.Equals(maPhong)
                          select p).First();
            return phong;
        }
        /*Lấy các phòng theo loại (VIP, THUONG)*/
      /*  public IEnumerable<Phong> LayTatCaTheoLoai(string maLoaiPhong)
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   where n.MaLoaiPhong.Equals(lp.TimLoaiPhong(maLoaiPhong).First().MaLoaiPhong)
                                   select n;
            return q;
        }*/
        /*Lấy danh sách phòng theo loại*/
        public IEnumerable<Phong> LayDSPhongTheoLoai(string maLoaiPhong)
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   where n.MaLoaiPhong.Equals(maLoaiPhong)
                                   select n;
            return q;
        }

        /*Thêm các thông tin Phòng*/
        public int ThemPhong(Phong phong)
        {
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
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
            
        }

        /*Sửa thông tin phòng (Trạng thái, Loại phòng)*/
        public bool SuaPhong(Phong phong)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<Phong> tam = (from n in dt.Phongs
                                             where n.MaPhong == phong.MaPhong
                                             select n);
                    tam.First().TenPhong = phong.TenPhong;
                    //truy vào khóa ngoại của bảng Phòng để đổi trạng thái (VIP, THƯỜNG) bên bảng Loại Phòng.
                    tam.First().MaLoaiPhong = phong.MaLoaiPhong;
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dt.Transaction.Rollback();
                    throw new Exception("Loi không sửa được!" + ex.Message);

                }
            }
        }


        /*Tìm kiếm phòng */
        public IEnumerable<Phong> TimPhong(string tenPhong)
        {
            IEnumerable<Phong> q = from n in dt.Phongs
                                   where n.TenPhong.Equals(tenPhong)
                                   select n;
            return q;
        }

        public Phong TimMaPhong(string tenPhong)
        {
            Phong q = (from n in dt.Phongs
                       where n.TenPhong.Equals(tenPhong)
                       select n).FirstOrDefault();
            return q;
        }

        public Phong TimMotPhongTheoTen(string tenPhong)
        {
            Phong q = (from n in dt.Phongs
                       where n.TenPhong.Equals(tenPhong)
                       select n).FirstOrDefault();
            return q;
        }
        public Phong TimMotPhongTheoTen_XemPhong(string tenPhong)
        {
            Phong q = (from n in dt.Phongs
                       where n.TenPhong.Equals(tenPhong)
                       select n).FirstOrDefault();
            using (System.Data.Common.DbTransaction br = dt.Connection.BeginTransaction())
            {
                dt.Transaction = br;
                dt.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, q);
                return q;
            }
        }

        /**
       * Tim ten phòng
       */
        public IQueryable<Phong> TimPhongTheoTen(string tenPhong)
        {
            IQueryable<Phong> q = (from n in dt.Phongs
                                   where n.TenPhong.Equals(tenPhong)
                                   select n);
            return q;
        }

        /*Hàm tìm phòng theo mã*/
        public IQueryable<Phong> TimPhongTheoMa(string maPhong)
        {
            IQueryable<Phong> q = (from n in dt.Phongs
                                   where n.MaPhong.Equals(maPhong)
                                   select n);
            return q;
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
        * Hàm xóa phòng
        * Hàm xóa phòng là cập nhật lại trạng thái phòng = null.
        */
        public int XoaPhong(Phong p)
        {
            using(System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
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
            
        }

        /*Hàm sửa trạng thái phòng*/
        public int SuaTrangThaiPhong(Phong phong)
        {
            using (System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction())
            {
                try
                {
                    dt.Transaction = myTran;
                    IQueryable<Phong> temp = (from n in dt.Phongs
                                              where n.MaPhong == phong.MaPhong
                                              select n);
                    temp.First().TrangThaiPhong = phong.TrangThaiPhong;
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


    }
}
