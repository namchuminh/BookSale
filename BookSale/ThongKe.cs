using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BookSale.Models;

namespace BookSale
{
    public partial class ThongKe : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader data;
        string query;
        public ThongKe()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();


        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            ThongKeTong();

        }
        void bgBtn()
        {
            btnTong.BackColor = SystemColors.Window;
            btn30NgayQua.BackColor = SystemColors.Window;
            btn7NgayQua.BackColor = SystemColors.Window;
            btnHomNay.BackColor = SystemColors.Window;
            btnThangNay.BackColor = SystemColors.Window;

            btnTong.ForeColor = SystemColors.ControlText;
            btn30NgayQua.ForeColor = SystemColors.ControlText;
            btn7NgayQua.ForeColor = SystemColors.ControlText;
            btnHomNay.ForeColor = SystemColors.ControlText;
            btnThangNay.ForeColor = SystemColors.ControlText;
        }
        void ThongKeTong()
        {
            bgBtn();
            btnTong.BackColor = SystemColors.WindowFrame;
            btnTong.ForeColor = SystemColors.Window;

            conn.Open();
            query = "SELECT COUNT(HoaDon.MaHD) AS SoLuongHoaDon FROM HoaDon";
            cmd = new SqlCommand(query, conn);
            int soLuongHoaDon = (int)cmd.ExecuteScalar();
            lblSoHoaDon.Text = soLuongHoaDon.ToString();

            //Doanh thu
            query = "SELECT SUM(HoaDon.TongTien) AS TongDoanhThu FROM HoaDon";
            cmd = new SqlCommand(query, conn);
            lblTongDoanhThu.Text = cmd.ExecuteScalar().ToString();

            //Khach hang
            query = "SELECT COUNT(KhachHang.MaKH) AS SoKhachHang FROM KhachHang";
            cmd = new SqlCommand(query, conn);
            lblSoKhachHang.Text = cmd.ExecuteScalar().ToString();

            //So sach
            query = "SELECT COUNT(Sach.MaSach) AS SoSanPham FROM Sach";
            cmd = new SqlCommand(query, conn);
            lblSoSanPham.Text = cmd.ExecuteScalar().ToString();

            conn.Close();

            //Tong doanh thu
            SqlDataAdapter ad = new SqlDataAdapter("SELECT ThoiGian, SUM (TongTien) AS TongTien FROM HoaDon GROUP BY ThoiGian ", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Thời gian";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";
            chart1.Series["Series1"].XValueMember = "ThoiGian";
            chart1.Series["Series1"].YValueMembers = "TongTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 HoaDon.MaSach, COUNT(HoaDon.MaSach) AS SoLuong FROM HoaDon GROUP BY MaSach ", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartSPBC.DataSource = dtl;
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";
            chartSPBC.Series["Series1"].XValueMember = "MaSach";
            chartSPBC.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btnThangNay_Click(object sender, EventArgs e)
        {
            bgBtn();
            btnThangNay.BackColor = SystemColors.WindowFrame;
            btnThangNay.ForeColor = SystemColors.Window;

            conn.Open();
            query = "SELECT COUNT(HoaDon.MaHD) AS SoLuongHoaDon FROM HoaDon WHERE MONTH(ThoiGian) = MONTH(GETDATE())";
            cmd = new SqlCommand(query, conn);
            int soLuongHoaDon = (int)cmd.ExecuteScalar();
            lblSoHoaDon.Text = soLuongHoaDon.ToString();

            //Doanh thu
            query = "SELECT SUM(HoaDon.TongTien) AS TongDoanhThu FROM HoaDon WHERE MONTH(ThoiGian) = MONTH(GETDATE())";
            cmd = new SqlCommand(query, conn);
            lblTongDoanhThu.Text = cmd.ExecuteScalar().ToString();

            //Khach hang
            query = "SELECT COUNT(KhachHang.MaKH) AS SoKhachHang FROM KhachHang ";
            cmd = new SqlCommand(query, conn);
            lblSoKhachHang.Text = cmd.ExecuteScalar().ToString();

            //So sach
            query = "SELECT COUNT(Sach.MaSach) AS SoSanPham FROM Sach";
            cmd = new SqlCommand(query, conn);
            lblSoSanPham.Text = cmd.ExecuteScalar().ToString();

            conn.Close();

            //Tong doanh thu
            SqlDataAdapter ad = new SqlDataAdapter("SELECT ThoiGian, SUM (TongTien) AS TongTien FROM HoaDon WHERE MONTH(ThoiGian) = MONTH(GETDATE()) GROUP BY ThoiGian", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Thời gian";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";
            chart1.Series["Series1"].XValueMember = "ThoiGian";
            chart1.Series["Series1"].YValueMembers = "TongTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 HoaDon.MaSach, COUNT(HoaDon.MaSach) AS SoLuong FROM HoaDon WHERE MONTH(ThoiGian) = MONTH(GETDATE()) GROUP BY MaSach ", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartSPBC.DataSource = dtl;
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";
            chartSPBC.Series["Series1"].XValueMember = "MaSach";
            chartSPBC.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btnTong_Click(object sender, EventArgs e)
        {
            ThongKeTong();
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            bgBtn();
            btnHomNay.BackColor = SystemColors.WindowFrame;
            btnHomNay.ForeColor = SystemColors.Window;

            conn.Open();
            query = "SELECT COUNT(HoaDon.MaHD) AS SoLuongHoaDon FROM HoaDon WHERE FORMAT(ThoiGian, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd')";
            cmd = new SqlCommand(query, conn);
            int soLuongHoaDon = (int)cmd.ExecuteScalar();
            lblSoHoaDon.Text = soLuongHoaDon.ToString();

            //Doanh thu
            query = "SELECT SUM(HoaDon.TongTien) AS TongDoanhThu FROM HoaDon WHERE FORMAT(ThoiGian, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd')";
            cmd = new SqlCommand(query, conn);
            lblTongDoanhThu.Text = cmd.ExecuteScalar().ToString();

            //Khach hang
            query = "SELECT COUNT(KhachHang.MaKH) AS SoKhachHang FROM KhachHang ";
            cmd = new SqlCommand(query, conn);
            lblSoKhachHang.Text = cmd.ExecuteScalar().ToString();

            //So sach
            query = "SELECT COUNT(Sach.MaSach) AS SoSanPham FROM Sach";
            cmd = new SqlCommand(query, conn);
            lblSoSanPham.Text = cmd.ExecuteScalar().ToString();

            conn.Close();

            //Tong doanh thu
            SqlDataAdapter ad = new SqlDataAdapter("SELECT ThoiGian, SUM (TongTien) AS TongTien FROM HoaDon WHERE FORMAT(ThoiGian, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd') GROUP BY ThoiGian", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Thời gian";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";
            chart1.Series["Series1"].XValueMember = "ThoiGian";
            chart1.Series["Series1"].YValueMembers = "TongTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 HoaDon.MaSach, COUNT(HoaDon.MaSach) AS SoLuong FROM HoaDon WHERE FORMAT(ThoiGian, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd') GROUP BY MaSach ", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartSPBC.DataSource = dtl;
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";
            chartSPBC.Series["Series1"].XValueMember = "MaSach";
            chartSPBC.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btn7NgayQua_Click(object sender, EventArgs e)
        {
            bgBtn();
            btn7NgayQua.BackColor = SystemColors.WindowFrame;
            btn7NgayQua.ForeColor = SystemColors.Window;

            conn.Open();
            query = "SELECT COUNT(HoaDon.MaHD) AS SoLuongHoaDon FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 8";
            cmd = new SqlCommand(query, conn);
            int soLuongHoaDon = (int)cmd.ExecuteScalar();
            lblSoHoaDon.Text = soLuongHoaDon.ToString();

            //Doanh thu
            query = "SELECT SUM(HoaDon.TongTien) AS TongDoanhThu FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 8";
            cmd = new SqlCommand(query, conn);
            lblTongDoanhThu.Text = cmd.ExecuteScalar().ToString();

            //Khach hang
            query = "SELECT COUNT(KhachHang.MaKH) AS SoKhachHang FROM KhachHang ";
            cmd = new SqlCommand(query, conn);
            lblSoKhachHang.Text = cmd.ExecuteScalar().ToString();

            //So sach
            query = "SELECT COUNT(Sach.MaSach) AS SoSanPham FROM Sach";
            cmd = new SqlCommand(query, conn);
            lblSoSanPham.Text = cmd.ExecuteScalar().ToString();

            conn.Close();

            //Tong doanh thu
            SqlDataAdapter ad = new SqlDataAdapter("SELECT ThoiGian, SUM (TongTien) AS TongTien FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 8 GROUP BY ThoiGian", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Thời gian";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";
            chart1.Series["Series1"].XValueMember = "ThoiGian";
            chart1.Series["Series1"].YValueMembers = "TongTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 HoaDon.MaSach, COUNT(HoaDon.MaSach) AS SoLuong FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 8 GROUP BY MaSach ", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartSPBC.DataSource = dtl;
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";
            chartSPBC.Series["Series1"].XValueMember = "MaSach";
            chartSPBC.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btn30NgayQua_Click(object sender, EventArgs e)
        {
            bgBtn();
            btn30NgayQua.BackColor = SystemColors.WindowFrame;
            btn30NgayQua.ForeColor = SystemColors.Window;

            conn.Open();
            query = "SELECT COUNT(HoaDon.MaHD) AS SoLuongHoaDon FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 31";
            cmd = new SqlCommand(query, conn);
            int soLuongHoaDon = (int)cmd.ExecuteScalar();
            lblSoHoaDon.Text = soLuongHoaDon.ToString();

            //Doanh thu
            query = "SELECT SUM(HoaDon.TongTien) AS TongDoanhThu FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 31";
            cmd = new SqlCommand(query, conn);
            lblTongDoanhThu.Text = cmd.ExecuteScalar().ToString();

            //Khach hang
            query = "SELECT COUNT(KhachHang.MaKH) AS SoKhachHang FROM KhachHang ";
            cmd = new SqlCommand(query, conn);
            lblSoKhachHang.Text = cmd.ExecuteScalar().ToString();

            //So sach
            query = "SELECT COUNT(Sach.MaSach) AS SoSanPham FROM Sach";
            cmd = new SqlCommand(query, conn);
            lblSoSanPham.Text = cmd.ExecuteScalar().ToString();

            conn.Close();

            //Tong doanh thu
            SqlDataAdapter ad = new SqlDataAdapter("SELECT ThoiGian, SUM (TongTien) AS TongTien FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 31 GROUP BY ThoiGian", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Thời gian";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";
            chart1.Series["Series1"].XValueMember = "ThoiGian";
            chart1.Series["Series1"].YValueMembers = "TongTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 HoaDon.MaSach, COUNT(HoaDon.MaSach) AS SoLuong FROM HoaDon WHERE ThoiGian <= GETDATE() AND ThoiGian > GETDATE() - 31 GROUP BY MaSach ", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartSPBC.DataSource = dtl;
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartSPBC.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";
            chartSPBC.Series["Series1"].XValueMember = "MaSach";
            chartSPBC.Series["Series1"].YValueMembers = "SoLuong";
        }
    }
}
