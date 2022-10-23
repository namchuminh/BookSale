using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Models
{
    internal class hoadon
    {
        private string _MaHD;
        private string _TenSach;
        private string _TenNV;
        private string _TenKH;
        private DateTime _ThoiGian;
        private Decimal _TongTien;

        public string MaHD { get => _MaHD; set => _MaHD = value; }
        public string TenSach { get => _TenSach; set => _TenSach = value; }
        public string TenNV { get => _TenNV; set => _TenNV = value; }
        public string TenKH { get => _TenKH; set => _TenKH = value; }
        public DateTime ThoiGian { get => _ThoiGian; set => _ThoiGian = value; }
        public decimal TongTien { get => _TongTien; set => _TongTien = value; }
    }
}
