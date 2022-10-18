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
    public partial class NhanVien : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        public NhanVien()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            enabledButton();
            getData();
        }
        void enabledButton()
        {
            btnLamMoiNV.Enabled = true;
            btnThemNV.Enabled = true;
            btnSuaNV.Enabled = false;
            btnXoaNV.Enabled = false;
            btnChamCongNV.Enabled = false;
            textBox1.Enabled = true;
        }
        void disableButton()
        {
            btnLamMoiNV.Enabled = true;
            btnThemNV.Enabled = false;
            btnSuaNV.Enabled = true;
            btnXoaNV.Enabled = true;
            btnChamCongNV.Enabled = true;
            textBox1.Enabled = false;
        }

        void getData()
        {
            try
            {
                List<nhanvien> lstNV = new List<nhanvien>();
                conn.Open();
                query = "SELECT * FROM NhanVien";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    nhanvien objNV = new nhanvien();
                    objNV.MaNV = (string)data["MaNV"];
                    objNV.TenNV = (string)data["TenNV"];
                    objNV.TaiKhoan = (string)data["TaiKhoan"];
                    objNV.MatKhau = (string)data["MatKhau"];
                    objNV.ChucVu = (string)data["ChucVu"];
                    objNV.SoDienThoai = (string)data["SoDienThoai"];
                    lstNV.Add(objNV);
                }
                dgvNhanVien.DataSource = lstNV;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    string ChucVu = textBox5.Text == "Nhân Viên" ? "NV" : "QL";
                    conn.Open();
                    query = $"INSERT INTO NhanVien(MaNV,TenNV,TaiKhoan,MatKhau,ChucVu,SoDienThoai) VALUES ('{textBox1.Text}', N'{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{ChucVu}', '{textBox6.Text}')";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Thêm Nhân Viên Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Thêm Nhân Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Chức Vụ!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox9.Text = "";
            enabledButton();
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvNhanVien.CurrentCell.RowIndex;
            textBox1.Text = dgvNhanVien.Rows[rowindex].Cells[0].Value.ToString();
            textBox2.Text = dgvNhanVien.Rows[rowindex].Cells[1].Value.ToString();
            textBox3.Text = dgvNhanVien.Rows[rowindex].Cells[2].Value.ToString();
            textBox4.Text = dgvNhanVien.Rows[rowindex].Cells[3].Value.ToString();
            textBox5.Text = dgvNhanVien.Rows[rowindex].Cells[4].Value.ToString();
            textBox6.Text = dgvNhanVien.Rows[rowindex].Cells[5].Value.ToString();
            disableButton();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    string ChucVu = textBox5.Text == "Nhân Viên" ? "NV" : "QL";
                    conn.Open();
                    query = $"UPDATE NhanVien SET TenNV=N'{textBox2.Text}',TaiKhoan='{textBox3.Text}',MatKhau='{textBox4.Text}', ChucVu='{ChucVu}', SoDienThoai='{textBox6.Text}'  WHERE MaNV = '{textBox1.Text}';";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Cập Nhật Nhân Viên Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Cập Nhật Nhân Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui Lòng Chọn Chức Vụ!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Bạn Thực Sự Muốn Xóa Nhân Viên Này?", "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    query = $"DELETE NhanVien WHERE MaNV = '{textBox1.Text}';";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Nhân Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        void searchData(string TenNV)
        {
            try
            {
                List<nhanvien> lstNV = new List<nhanvien>();
                conn.Open();
                query = $"SELECT * FROM NhanVien WHERE TenNV LIKE N'%{TenNV}%' ";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    nhanvien objNV = new nhanvien();
                    objNV.MaNV = (string)data["MaNV"];
                    objNV.TenNV = (string)data["TenNV"];
                    objNV.TaiKhoan = (string)data["TaiKhoan"];
                    objNV.MatKhau = (string)data["MatKhau"];
                    objNV.ChucVu = (string)data["ChucVu"];
                    objNV.SoDienThoai = (string)data["SoDienThoai"];
                    lstNV.Add(objNV);
                }
                dgvNhanVien.DataSource = lstNV;
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

        private void btnChamCongNV_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                string ThoiGian = dateTime.ToString("yyyy-MM-dd");
                conn.Open();
                query = $"SELECT COUNT(*) FROM ChamCong WHERE MaNV = '{textBox1.Text}' AND ThoiGian = '{ThoiGian}'";
                cmd = new SqlCommand(query, conn);
                int daChamCong = (int)cmd.ExecuteScalar();
                if(daChamCong != 1) {
                    query = $"INSERT INTO ChamCong(MaNV,ThoiGian) VALUES ('{textBox1.Text}','{ThoiGian}')";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    if(result == 1)
                    {
                        MessageBox.Show($"Thành Công! Đã Chấm Công Cho Nhân Viên {textBox1.Text}", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Có Lỗi Khi Chấm Công {textBox1.Text}! Vui Lòng Thử Lại!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Nhân Viên {textBox1.Text} Đã Chấm Công Cho Hôm Nay!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
