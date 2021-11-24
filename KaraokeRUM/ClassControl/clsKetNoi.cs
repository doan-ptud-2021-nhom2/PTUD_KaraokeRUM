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
            string strKetNoi = @"Data Source=.\SQLEXPRESS;Initial Catalog=db_karaoke_rum;Integrated Security=True";
            qlKaraoke = new qlKaraokeDataContext(strKetNoi)
            {
                CommandTimeout = 30000
            };
            qlKaraoke.Connection.Open();
            return qlKaraoke;
        }
    }
}
