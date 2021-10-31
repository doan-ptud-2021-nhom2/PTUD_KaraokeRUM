using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsChuyenSoThanhChu : clsKetNoi
    {
        qlKaraokeDataContext dt;

        /**
         *Biến cục bộ 
         */
        private string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };

        public clsChuyenSoThanhChu()
        {
            dt = LayData();
        }

        /**
         * Xử lý đọc số thành chữ (Tổng tiền)
         */
        public string DocTienBangChu(long soTien)
        {
            int lan, i;
            long so;
            string ketQua = "", tam = "";
            int[] viTri = new int[6];

            if(soTien < 0)
            {
                return "Số tiền âm !";
            }
            if(soTien == 0)
            {
                return "Không đồng !";
            }
            if(soTien > 0)
            {
                so = soTien;
            }
            else
            {
                so = -soTien;
            }

            //Kiểm tra số quá lớn
            if(soTien > 8999999999999999)
            {
                soTien = 0;
                return "";
            }
            viTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(viTri[5].ToString()) * 1000000000000000;
            viTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(viTri[4].ToString()) * +1000000000000;
            viTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(viTri[3].ToString()) * 1000000000;
            viTri[2] = (int)(so / 1000000);
            viTri[1] = (int)((so % 1000000) / 1000);
            viTri[0] = (int)(so % 1000);
            if(viTri[5] > 0)
            {
                lan = 5;
            }
            else if(viTri[4] > 0)
            {
                lan = 4;
            }
            else if(viTri[3] > 0)
            {
                lan = 3;
            }
            else if(viTri[2] > 0)
            {
                lan = 2;
            }
            else if(viTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }

            for(i = lan; i >= 0; i--)
            {
                tam = DocSoBaChuSo(viTri[i]);
                ketQua += tam;
                if(viTri[i] != 0)
                {
                    ketQua += Tien[i];
                }
            }

            if(ketQua.Substring(ketQua.Length - 1, 1) == ",")
            {
                ketQua = ketQua.Substring(0, ketQua.Length - 1);
            }
            ketQua = ketQua.Trim();

            return ketQua.Substring(0, 1).ToUpper() + ketQua.Substring(1);
        }

        // Hàm đọc số có 3 chữ số
        private string DocSoBaChuSo(int baChuSo)
        {
            int tram, chuc, donVi;
            string ketQua = "";
            tram = (int)(baChuSo / 100);
            chuc = (int)((baChuSo % 100) / 10);
            donVi = baChuSo % 10;

            if((tram == 0) && (chuc == 0) && (donVi == 0))
            {
                return "";
            }
            if(tram != 0)
            {
                ketQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donVi != 0))
                {
                    ketQua += " linh";
                }
            }
            if((chuc != 0) && (chuc != 1))
            {
                ketQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donVi != 0))
                {
                    ketQua = ketQua + " linh";
                }
            }
            if(chuc == 1)
            {
                ketQua += " mười";
            }

            switch(donVi) 
            {
                case 1:
                    if((chuc != 0) && (chuc != 1))
                    {
                        ketQua += " mốt";
                    }
                    else
                    {
                        ketQua += ChuSo[donVi];
                    }
                    break;
                case 5:
                    if(chuc == 0)
                    {
                        ketQua += ChuSo[donVi];
                    }
                    else
                    {
                        ketQua += " lăm";
                    }
                    break;
                default:
                    if(donVi != 0)
                    {
                        ketQua += ChuSo[donVi];
                    }
                    break;
            }

            return ketQua;
        }

    }
}
