using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsKetNoi
    {
        qlKaraokeDataContext qlKaraoke;
        public qlKaraokeDataContext LayData()
        {
            string strKetNoi = @"Data Source=DESKTOP-AUH5LCA\SQLEXPRESS;Initial Catalog=db_karaoke;Integrated Security=True";
            qlKaraoke = new qlKaraokeDataContext(strKetNoi);
            qlKaraoke.Connection.Open();
            return qlKaraoke;
        }
    }
}
