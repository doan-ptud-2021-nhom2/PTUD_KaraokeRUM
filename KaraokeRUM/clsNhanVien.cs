﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsNhanVien : clsKetNoi
    {

        qlKaraokeDataContext dt;
        public clsNhanVien()
        {
            dt = LayData();
        }
        public IEnumerable<NhanVien> LayDSNV()
        {
            IEnumerable<NhanVien> nv = from n in dt.NhanViens
                                        select n;
            return nv;

        }
        /**
       * Thêm các thông tin Phòng
      
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
         */
        /**
        * Sửa thông tin phòng (Trạng thái, Loại phòng).
  
        public bool SuaPhong(Phong phong)
        {
            System.Data.Common.DbTransaction myTran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = myTran;
                IQueryable<Phong> tam = (from n in dt.Phongs
                                         where n.MaPhong == phong.MaPhong
                                         select n);
                tam.First().TrangThaiPhong = phong.TrangThaiPhong;
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
                */
    }
}