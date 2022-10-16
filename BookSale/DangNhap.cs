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
    public partial class DangNhap : Form
    {
        SqlConnection conn;
        string query;
        SqlCommand cmd = null;
        public DangNhap()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            label3.BackColor = Color.Transparent;
            label3.Parent = pictureBox1;

            label2.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = '{txtTaiKhoan.Text}' AND MatKhau = '{txtMatKhau.Text}'";
                cmd = new SqlCommand(query, conn);
                int kq = (int)cmd.ExecuteScalar();
                if (kq == 1)
                {
                    this.Hide();
                    HeThong heThong = new HeThong(txtTaiKhoan.Text);
                    heThong.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Đăng Nhập Thất Bại! Kiểm Tra Lại Tài Khoản Mật Khẩu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
