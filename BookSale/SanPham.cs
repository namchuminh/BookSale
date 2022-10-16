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
    public partial class SanPham : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        public SanPham()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            enabledButton();
            getData();
        }
        void enabledButton()
        {
            btnLamMoiSach.Enabled = true;
            btnThemSach.Enabled = true;
            btnSuaSach.Enabled = false;
            btnXoaSach.Enabled = false;
            textBox1.Enabled = true;
        }
        void disableButton()
        {
            btnLamMoiSach.Enabled = true;
            btnThemSach.Enabled = false;
            btnSuaSach.Enabled = true;
            btnXoaSach.Enabled = true;
            textBox1.Enabled = false;
        }
        void getData()
        {
            try
            {
                List<sach> lstSach = new List<sach>();
                conn.Open();
                query = "SELECT * FROM Sach";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read()) 
                {
                    sach book = new sach();
                    book.MaSach = (string)data["MaSach"];
                    book.TenSach = (string)data["TenSach"];
                    book.NhaXuatBan = (string)data["NhaXuatBan"];
                    book.TacGia = (string)data["TacGia"];
                    book.MaChuyenMuc = (string)data["MaChuyenMuc"];
                    book.GiaBan = (decimal)data["GiaBan"];
                    book.SoLuong = (int)data["SoLuong"];
                    book.TrangThai = (string)data["TrangThai"];
                    lstSach.Add(book);
                }
                dgvSanPham.DataSource = lstSach;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"INSERT INTO sach(MaSach,TenSach,NhaXuatBan,TacGia,MaChuyenMuc,GiaBan,SoLuong,TrangThai) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox6.Text}', '{textBox7.Text}', '{textBox8.Text}', '{textBox4.Text}', '{textBox5.Text}')";
                cmd = new SqlCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                if(result == 1)
                {
                    MessageBox.Show("Thêm Sách Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                }
                else
                {
                    MessageBox.Show("Thêm Sách Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoiSach_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
            enabledButton();
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvSanPham.CurrentCell.RowIndex;
            textBox1.Text = dgvSanPham.Rows[rowindex].Cells[0].Value.ToString();
            textBox2.Text = dgvSanPham.Rows[rowindex].Cells[1].Value.ToString();
            textBox3.Text = dgvSanPham.Rows[rowindex].Cells[2].Value.ToString();
            textBox4.Text = dgvSanPham.Rows[rowindex].Cells[6].Value.ToString();
            textBox5.Text = dgvSanPham.Rows[rowindex].Cells[7].Value.ToString();
            textBox6.Text = dgvSanPham.Rows[rowindex].Cells[3].Value.ToString();
            textBox7.Text = dgvSanPham.Rows[rowindex].Cells[4].Value.ToString();
            textBox8.Text = dgvSanPham.Rows[rowindex].Cells[5].Value.ToString().Replace(',','.');

            disableButton();
        }

        private void btnSuaSach_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"UPDATE sach SET TenSach='{textBox2.Text}',NhaXuatBan='{textBox3.Text}',TacGia='{textBox6.Text}',MaChuyenMuc='{textBox7.Text}',GiaBan={textBox8.Text},SoLuong={textBox4.Text},TrangThai='{textBox5.Text}' WHERE MaSach = '{textBox1.Text}';";
                cmd = new SqlCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Cập Nhật Sách Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                }
                else
                {
                    MessageBox.Show("Cập Nhật Sách Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Bạn Thực Sự Muốn Xóa Sách Này?", "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    query = $"DELETE sach WHERE MaSach = '{textBox1.Text}';";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Sách Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        void searchData(string TenSach)
        {
            try
            {
                List<sach> lstSach = new List<sach>();
                conn.Open();
                query = $"SELECT * FROM Sach WHERE TenSach LIKE N'%{TenSach}%' ";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    sach book = new sach();
                    book.MaSach = (string)data["MaSach"];
                    book.TenSach = (string)data["TenSach"];
                    book.NhaXuatBan = (string)data["NhaXuatBan"];
                    book.TacGia = (string)data["TacGia"];
                    book.MaChuyenMuc = (string)data["MaChuyenMuc"];
                    book.GiaBan = (decimal)data["GiaBan"];
                    book.SoLuong = (int)data["SoLuong"];
                    book.TrangThai = (string)data["TrangThai"];
                    lstSach.Add(book);
                }
                dgvSanPham.DataSource = lstSach;
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
