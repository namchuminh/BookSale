using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Models
{
    internal class khachhang
    {
        private string _MaKH;
        private string _TenKH;
        private string _SoDienThoai;
        private string _DiaChi;

        public string MaKH { get => _MaKH; set => _MaKH = value; }
        public string TenKH { get => _TenKH; set => _TenKH = value; }
        public string SoDienThoai { get => _SoDienThoai; set => _SoDienThoai = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
    }
}
