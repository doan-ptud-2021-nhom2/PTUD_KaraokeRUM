using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeRUM
{
    class clsHonLoan : clsKetNoi
    {
        qlKaraokeDataContext dt;

        public clsHonLoan()
        {
            dt = LayData();
        }

        /**
        * join 2 bảng: Phòng với Loaị Phòng
        * Lấy dữ liệu ở bảng Phòng và Loại Phòng 
        */
        public IEnumerable<dynamic> LayPhongVaLoaiPhong()
        {
            var q = from n in dt.Phongs
                    join x in dt.LoaiPhongs
                    on n.MaLoaiPhong equals x.MaLoaiPhong
                    select new { n.MaPhong, n.TenPhong , n.TrangThaiPhong, x.TenLoaiPhong, x.Gia };
            return q;
        }

        /**
        * join các bảng: Mặt hàng - Chi tiết hóa đơn - Hóa đơn - Phòng.
        * Lấy dữ liệu ở bảng Mặt hàng và Phòng.
        */
        public IEnumerable<dynamic> LayMatHangVaPhong()
        {
            var q = from n in dt.MatHangs
                    join x in dt.ChiTietHoaDons on n.MaMH equals x.MaMH
                    join y in dt.HoaDons on x.MaHD equals y.MaHD
                    select new { n.MaMH, y.MaHD, x.SoLuong, x.ThanhTien };
            return q;
        }

        /**
       * join 2 bảng: KhachHang với LoaiKhachHang
       * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
       */
        public IEnumerable<dynamic> KhachHangVaLoaiKhachHang()
        {
            var kh = from n in dt.KhachHangs
                     join x in dt.LoaiKhachHangs
                     on n.MaLoaiKH equals x.MaLoaiKH
                     select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen, x.TenLoaiKH, x.ChietKhau };
            return kh;
        }
        /**
        * join 2 bảng: KhachHang với LoaiKhachHang
        * Lấy dữ liệu ở Khách Hàng và Loại Khách Hàng
        * Có điều kiện
        */
        public IEnumerable<dynamic> LayKhachHangVaLoaiKhachHangTheoLoai(string loaiKH)
        {
            var kh = from n in dt.KhachHangs
                     join x in dt.LoaiKhachHangs
                     on n.MaLoaiKH equals x.MaLoaiKH
                     where x.TenLoaiKH.Equals(loaiKH)
                     select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen, x.TenLoaiKH, x.ChietKhau };
            return kh;

        }
        /*
         * tìm kiếm khách hàng
         */
        public IEnumerable<dynamic> TimKhach(string timKiem)
        {
            IEnumerable<dynamic> kh = from n in dt.KhachHangs
                                      join x in dt.LoaiKhachHangs
                                      on n.MaLoaiKH equals x.MaLoaiKH
                                      where n.TenKhach.Contains(timKiem) || n.MaKH.Contains(timKiem)
                                      select new { n.MaKH, n.TenKhach, n.SDT, n.SoLanDen, x.TenLoaiKH, x.ChietKhau };
            return kh;
        }
        /**
          * join 2 bảng: NhanVien với LoaiNhanVien
          * Lấy dữ liệu ở Nhân viên và Loại Nhân Viên
          */
        public IEnumerable<dynamic> LayNhanVienVaLoaiNhanVien(string MANVQL)
        {
            //Đã nghỉ
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi ,n.TrangThai, x.TenLNV};
                     
            return nv;
        }
        /**
         * join 2 bảng: NhanVien với LoaiNhanVien
         * Lấy dữ liệu ở Nhân viên và Loại Nhân Viên theo loại
         */
        public IEnumerable<dynamic> LayNhanVienVaLoaiNhanVienTheoLoai(string loaiNV ,string MANVQL)
        {
            var nv = from n in dt.NhanViens
                     join x in dt.LoaiNhanViens
                     on n.MaLNV equals x.MaLNV
                     where !n.MaNV.Contains(MANVQL) && x.TenLNV.Equals(loaiNV) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                     select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV };
            return nv;
        }
        /*
         * tìm kiếm nhân viên
         */
        public IEnumerable<dynamic> TimNhanVien(string timKiem, string MANVQL)
        {
            IEnumerable<dynamic> nv = from n in dt.NhanViens
                                      join x in dt.LoaiNhanViens
                                      on n.MaLNV equals x.MaLNV 
                                      where n.TenNV.Contains(timKiem) || n.MaNV.Contains(timKiem) && !n.MaNV.Contains(MANVQL) && !n.TrangThai.ToLower().Contains("đã nghỉ")
                                      select new { n.MaNV, n.TenNV, n.GioiTinh, n.CMND, n.SDT, n.DiaChi, n.TrangThai, x.TenLNV };
            return nv;
        }
    }
} 
