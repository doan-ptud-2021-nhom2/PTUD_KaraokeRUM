using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsKhachHang : clsKetNoi
    {
        qlKaraokeDataContext dt;
        
        public clsKhachHang()
        {
            dt = LayData();
        }
        //Hàm lấy thông tin khách hàng
        public List<KhachHang> LayDSKH()
        {

            var dataKH = from kh in dt.KhachHangs
                         select new
                         {
                             ten = kh.TenKhach,
                             makh = kh.MaKH,
                             sld = kh.SoLanDen,
                             sdt = kh.SDT
                         };
            List<KhachHang> lKH = new List<KhachHang>();
            foreach(var data in dataKH)
            {
                KhachHang kh = new KhachHang()
                {
                    TenKhach = data.ten,
                    MaKH = data.makh,
                    SDT = data.sdt,
                    SoLanDen = data.sld
                }; 
                lKH.Add(kh);
            }
            return lKH;
        }
    }
}
