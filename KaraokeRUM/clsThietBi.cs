﻿using System;
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
        /**
        * Lấy tất cả trang thiết bị
        */
        public IEnumerable<TrangThietBi> GetTrangThietBis()
        {
            IEnumerable<TrangThietBi> tb = from n in dt.TrangThietBis select n;
            return tb;
        }
        /**
        * kiểm tra
        */
        public TrangThietBi KiemTra(string id)
        {
            TrangThietBi temp = (from n in dt.TrangThietBis
                                 where n.MaTTB.Equals(id)
                              select n).FirstOrDefault();
            return temp;
        }
        /**
        * Thêm trang thiết bị
        */
        public int Them(TrangThietBi tb)
        {
            System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction();
                try
                {
                    dt.Transaction = tran;
                    dt.TrangThietBis.InsertOnSubmit(tb);
                    dt.SubmitChanges();
                    dt.Transaction.Commit();
                    return 1;
                }
                catch(Exception ex)
                {
                   dt.Transaction.Rollback();
                   throw new Exception(ex.Message);
                }
        }
        /**
        * Xóa trang thiết bị
        */
        public int Xoa(TrangThietBi tb)
        {
            System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = tran;
                if(KiemTra(tb.MaTTB)!=null)
                {
                    dt.TrangThietBis.DeleteOnSubmit(tb);
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
        /**
        * Sửa trang thiết bị
        */
        /**
        public int Sua(TrangThietBi trangTB)
        {
            System.Data.Common.DbTransaction tran = dt.Connection.BeginTransaction();
            try
            {
                dt.Transaction = tran;
                IEnumerable<TrangThietBi> ttb = (from n in dt.TrangThietBis where n.MaTTB == trangTB.MaTTB select n);
                ttb.First().MaTTB = trangTB.MaTTB;
                ttb.First().TenTTB = trangTB.TenTTB;
                ttb.First().SoLuongTon = trangTB.SoLuongTon;
                ttb.First().Gia = trangTB.Gia;
                ttb.First().MaQL = trangTB.MaQL;
                dt.SubmitChanges();
                dt.Transaction.Commit();
                return 1;
            }
            catch(Exception ex)
            {
                dt.Transaction.Rollback();
                throw new Exception("Loi" + ex.Message);
            }
        }
         */
        /**
        * tìm trang thiết bị
        */
        public IEnumerable<TrangThietBi> TimTTB(string maTTB)
        {
            IEnumerable<TrangThietBi> q = from n in dt.TrangThietBis
                                   where n.MaTTB.Equals(maTTB)
                                   select n;
            return q;
        }
    }
}