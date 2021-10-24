using System;
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
        public IEnumerable<TrangThietBi> GetTrangThietBis()
        {
            IEnumerable<TrangThietBi> tb = from n in dt.TrangThietBis select n;
            return tb;
        }
        public TrangThietBi KiemTra(string id)
        {
            TrangThietBi temp = (from n in dt.TrangThietBis
                                 where n.MaTTB.Equals(id)
                              select n).FirstOrDefault();
            return temp;
        }
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
    }
}
