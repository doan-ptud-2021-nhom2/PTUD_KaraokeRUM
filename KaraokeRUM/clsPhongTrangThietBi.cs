using System;
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
    }
}
