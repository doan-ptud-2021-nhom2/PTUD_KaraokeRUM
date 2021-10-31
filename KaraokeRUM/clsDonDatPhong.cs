﻿using System;
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
        public IEnumerable<DonDatPhong> TraTatCaDDP()
        {
            IEnumerable<DonDatPhong> ddp = from n in dt.DonDatPhongs select n;
            return ddp;
        }
        /**
        * Tim mã đơn đặt phòng
        */
        public DonDatPhong TimDDPhong(string maPhong)
        {

            foreach (DonDatPhong i in dt.DonDatPhongs)
            {
                if (i.MaPhong == maPhong)
                    return i;
            }
            return null;
        }
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
        public DonDatPhong KiemTra(string id)
        {
            DonDatPhong temp = (from n in dt.DonDatPhongs
                                where n.MaDDP.Equals(id)
                                select n).FirstOrDefault();
            return temp;
        }
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