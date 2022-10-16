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
    public partial class KhachHang : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        public KhachHang()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            enabledButton();
            getData();
        }

        void enabledButton()
        {
            btnLamMoiKH.Enabled = true;
            btnThemKH.Enabled = true;
            btnSuaKH.Enabled = false;
            btnXoaKH.Enabled = false;
            textBox1.Enabled = true;
        }
        void disableButton()
        {
            btnLamMoiKH.Enabled = true;
            btnThemKH.Enabled = false;
            btnSuaKH.Enabled = true;
            btnXoaKH.Enabled = true;
            textBox1.Enabled = false;
        }

        void getData()
        {
            try
            {
                List<khachhang> lstKH = new List<khachhang>();
                conn.Open();
                query = "SELECT * FROM KhachHang";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    khachhang customer = new khachhang();
                    customer.MaKH = (string)data["MaKH"];
                    customer.TenKH = (string)data["TenKH"];
                    customer.SoDienThoai = (string)data["SoDienThoai"];
                    customer.DiaChi = (string)data["DiaChi"];
                    lstKH.Add(customer);
                }
                dgvKhachHang.DataSource = lstKH;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"INSERT INTO KhachHang(MaKH,TenKH,SoDienThoai,DiaChi) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}')";
                cmd = new SqlCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Thêm Khách Hàng Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                }
                else
                {
                    MessageBox.Show("Thêm Khách Hàng Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox9.Text = "";
            enabledButton();
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvKhachHang.CurrentCell.RowIndex;
            textBox1.Text = dgvKhachHang.Rows[rowindex].Cells[0].Value.ToString();
            textBox2.Text = dgvKhachHang.Rows[rowindex].Cells[1].Value.ToString();
            textBox3.Text = dgvKhachHang.Rows[rowindex].Cells[2].Value.ToString();
            textBox4.Text = dgvKhachHang.Rows[rowindex].Cells[3].Value.ToString();
            disableButton();
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"UPDATE KhachHang SET TenKH=N'{textBox2.Text}',SoDienThoai='{textBox3.Text}',DiaChi=N'{textBox4.Text}' WHERE MaKH = '{textBox1.Text}';";
                cmd = new SqlCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Cập Nhật Khách Hàng Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                }
                else
                {
                    MessageBox.Show("Cập Nhật Khách Hàng Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Bạn Thực Sự Muốn Xóa Khách Hàng Này?", "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    query = $"DELETE KhachHang WHERE MaKH = '{textBox1.Text}';";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Khách Hàng Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        void searchData(string TenKH)
        {
            try
            {
                List<khachhang> lstKH = new List<khachhang>();
                conn.Open();
                query = $"SELECT * FROM KhachHang WHERE TenKH LIKE N'%{TenKH}%' ";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    khachhang customer = new khachhang();
                    customer.MaKH = (string)data["MaKH"];
                    customer.TenKH = (string)data["TenKH"];
                    customer.SoDienThoai = (string)data["SoDienThoai"];
                    customer.DiaChi = (string)data["DiaChi"];
                    lstKH.Add(customer);
                }
                dgvKhachHang.DataSource = lstKH;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            searchData(textBox9.Text);
        }
    }
}
