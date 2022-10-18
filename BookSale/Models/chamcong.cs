using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Models
{
    internal class chamcong
    {
        private int _MaCC;
        private string _MaNV;
        private DateTime _ThoiGian;

        public int MaCC { get => _MaCC; set => _MaCC = value; }
        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public DateTime ThoiGian { get => _ThoiGian; set => _ThoiGian = value; }
    }
}
