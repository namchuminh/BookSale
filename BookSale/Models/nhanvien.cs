using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Models
{
    internal class nhanvien
    {
        private string _MaNV;
        private string _TenNV;
        private string _TaiKhoan;
        private string _MatKhau;
        private string _ChucVu;
        private string _SoDienThoai;
        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string TenNV { get => _TenNV; set => _TenNV = value; }
        public string TaiKhoan { get => _TaiKhoan; set => _TaiKhoan = value; }
        public string MatKhau { get => _MatKhau; set => _MatKhau = value; }
        public string ChucVu { get => _ChucVu; set => _ChucVu = value; }
        public string SoDienThoai { get => _SoDienThoai; set => _SoDienThoai = value; }
    }
}
