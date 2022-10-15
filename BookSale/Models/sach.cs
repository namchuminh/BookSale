using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Models
{
    internal class sach
    {
        private string _MaSach;
        private string _TenSach;
        private string _NhaXuatBan;
        private string _TacGia;
        private string _MaChuyenMuc;
        private decimal _GiaBan;
        private int _SoLuong;
        private string _TrangThai;

        public string MaSach { get => _MaSach; set => _MaSach = value; }
        public string TenSach { get => _TenSach; set => _TenSach = value; }
        public string NhaXuatBan { get => _NhaXuatBan; set => _NhaXuatBan = value; }
        public string TacGia { get => _TacGia; set => _TacGia = value; }
        public string MaChuyenMuc { get => _MaChuyenMuc; set => _MaChuyenMuc = value; }
        public decimal GiaBan { get => _GiaBan; set => _GiaBan = value; }
        public int SoLuong { get => _SoLuong; set => _SoLuong = value; }
        public string TrangThai { get => _TrangThai; set => _TrangThai = value; }
    }
}
