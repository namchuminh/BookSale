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
    public partial class HeThong : Form
    {

        SqlConnection conn;
        string query;
        SqlCommand cmd;
        SqlDataReader data;
        public HeThong(string TaiKhoan)
        {
            InitializeComponent();
            loadform(new TrangChu());
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            getName(TaiKhoan);
        }

        void getName(string TaiKhoan)
        {
            nhanvien objNV = new nhanvien();
            conn.Open();
            query = $"SELECT * FROM NhanVien WHERE TaiKhoan = '{TaiKhoan}'";
            cmd = new SqlCommand(query, conn);
            data = cmd.ExecuteReader();
            while (data.Read())
            {
                objNV.MaNV = (string)data["MaNV"];
                objNV.TenNV = (string)data["TenNV"];
                objNV.TaiKhoan = (string)data["TaiKhoan"];
                objNV.MatKhau = (string)data["MatKhau"];
                objNV.ChucVu = (string)data["ChucVu"];
                objNV.SoDienThoai = (string)data["SoDienThoai"];
            }
            conn.Close();
            label3.Text = objNV.TenNV;

            if (objNV.ChucVu != "QL")
            {
                button5.Hide();
                button6.Hide();
            }


        }

        void loadform(object Form)
        {
            if(this.panel_main.Controls.Count > 0)
            {
                this.panel_main.Controls.RemoveAt(0);
            }
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel_main.Controls.Add(f);
            this.panel_main.Tag = f;
            f.Show(); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new TrangChu());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new SanPham());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadform(new KhachHang());
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            loadform(new KhachHang());
        }
    }
}
