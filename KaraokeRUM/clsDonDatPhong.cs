using System;
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
    }
}
