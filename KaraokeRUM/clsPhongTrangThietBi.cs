﻿using System;
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
        public IEnumerable<Phong_TrangThietBi> TraDuLieu()
        {
            var q = from d in dt.Phong_TrangThietBis
                    select d;
            return q;
        }    
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
        public IEnumerable<Phong_TrangThietBi> TimPhongTTB(string maPhong, string maTTB)
        {
            IEnumerable<Phong_TrangThietBi> q = from n in dt.Phong_TrangThietBis
                                                where n.MaPhong.Equals(maPhong) && n.MaTTB.Equals(maTTB)
                                                select n;
            return q;
        }
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
