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
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;


namespace BookSale
{
    public partial class HoaDon : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        string TaiKhoan;
        DataGridView dgvExportHoaDon = new DataGridView();
        public HoaDon(string TaiKhoan)
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            this.TaiKhoan = TaiKhoan;
            getDataKH();
            getDataHD();
            button5.Enabled = false;
        }
        void getDataKH()
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

        void getDataHD()
        {
            try
            {
                List<hoadon> lstHD = new List<hoadon>();
                conn.Open();
                query = "SELECT HoaDon.MaHD, HoaDon.ThoiGian, HoaDon.TongTien, Sach.TenSach, NhanVien.TenNV, KhachHang.TenKH FROM HoaDon, Sach, NhanVien, KhachHang WHERE HoaDon.MaSach = Sach.MaSach AND HoaDon.MaNV = NhanVien.MaNV AND HoaDon.MaKH = KhachHang.MaKH";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    hoadon hoadon = new hoadon();
                    hoadon.MaHD = (string)data["MaHD"];
                    hoadon.TenSach = (string)data["TenSach"];
                    hoadon.TenNV = (string)data["TenNV"];
                    hoadon.TenKH = (string)data["TenKH"];
                    hoadon.ThoiGian = (DateTime)data["ThoiGian"];
                    hoadon.TongTien = (Decimal)data["TongTien"];
                    lstHD.Add(hoadon);
                }
                dgvHoaDon.DataSource = lstHD;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        string getMaNV(string TaiKhoan)
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
            return objNV.MaNV;
        }
        string getMaHoaDonMoi(string maHoaDonCuoi)
        {
            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(maHoaDonCuoi);
            string alphaPart = result.Groups[1].Value;
            int numberPart = int.Parse(result.Groups[2].Value);
            numberPart = numberPart + 1;

            string newNumberPart = null;
            if (numberPart.ToString().Length == 1)
            {
                newNumberPart = "HD0" + numberPart.ToString();
                return newNumberPart;
            }
            else
            {
                newNumberPart = "HD"+numberPart.ToString();
                return newNumberPart;
            }
        }
        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Ẩn nút thanh toán, hiện nút thanh toán
            button5.Enabled = false;
            button6.Enabled = true;

            int rowindex = dgvKhachHang.CurrentCell.RowIndex;
            Int32 lastindex = dgvHoaDon.Rows.Count - 1;
            string maHoaDonCuoi = dgvHoaDon.Rows[lastindex].Cells[0].Value.ToString();
            textBox1.Text = getMaHoaDonMoi(maHoaDonCuoi);
            textBox4.Text = dgvKhachHang.Rows[rowindex].Cells[0].Value.ToString();
            textBox3.Text = getMaNV(this.TaiKhoan);
            textBox6.Text = DateTime.Now.ToString("yyyy-MM-dd h:mm");
        }
        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file
                oDoc.SaveAs2(filename);

                //NASSIM LOUCHANI
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = textBox1.Text+".docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Export_Data_To_Word(dgvExportHoaDon, sfd.FileName);
            }
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Ẩn nút thanh toán, hiện nút thanh toán
                button5.Enabled = true;
                button6.Enabled = false;

                //Khai báo số lượng cột có trong dgvExportHoaDon
                dgvExportHoaDon.ColumnCount = 6;
                //Thêm header vào dgvExportHoaDon
                dgvExportHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
                dgvExportHoaDon.Columns[1].HeaderText = "Tên Sách";
                dgvExportHoaDon.Columns[2].HeaderText = "Tên Nhân Viên";
                dgvExportHoaDon.Columns[3].HeaderText = "Tên Khách Hàng";
                dgvExportHoaDon.Columns[4].HeaderText = "Thời Gian";
                dgvExportHoaDon.Columns[5].HeaderText = "Tổng Tiền";

                //Lấy thông tin của dòng được click
                int rowindex = dgvHoaDon.CurrentCell.RowIndex;
                string MaHD = dgvHoaDon.Rows[rowindex].Cells[0].Value.ToString(); //Mã Hóa Đơn
                string TenSach = dgvHoaDon.Rows[rowindex].Cells[1].Value.ToString();
                string TenNV = dgvHoaDon.Rows[rowindex].Cells[2].Value.ToString();
                string TenKH = dgvHoaDon.Rows[rowindex].Cells[3].Value.ToString();
                string ThoiGian = dgvHoaDon.Rows[rowindex].Cells[4].Value.ToString();
                string TongTien = dgvHoaDon.Rows[rowindex].Cells[5].Value.ToString();

                //Xóa tất cả các dòng cũ có trong dgvExportHoaDon
                dgvExportHoaDon.Rows.Clear();
                //Thêm dòng mới vào dgvExportHoaDon với thông tin của dòng được click
                dgvExportHoaDon.Rows.Add(MaHD, TenSach, TenNV, TenKH, ThoiGian, TongTien);
                //Xóa bớt dòng trống bên cuối dgvExportHoaDon
                dgvExportHoaDon.AllowUserToAddRows = false;
                dgvExportHoaDon.AllowUserToDeleteRows = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        void clearTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        void exportHoaDonVuaThanhToan(string MaHoaDonThanhToan)
        {
            //Khai báo số lượng cột có trong dgvExportHoaDon
            dgvExportHoaDon.ColumnCount = 6;
            //Thêm header vào dgvExportHoaDon
            dgvExportHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
            dgvExportHoaDon.Columns[1].HeaderText = "Tên Sách";
            dgvExportHoaDon.Columns[2].HeaderText = "Tên Nhân Viên";
            dgvExportHoaDon.Columns[3].HeaderText = "Tên Khách Hàng";
            dgvExportHoaDon.Columns[4].HeaderText = "Thời Gian";
            dgvExportHoaDon.Columns[5].HeaderText = "Tổng Tiền";

            conn.Open();
            query = $"SELECT HoaDon.MaHD, HoaDon.ThoiGian, HoaDon.TongTien, Sach.TenSach, NhanVien.TenNV, KhachHang.TenKH FROM HoaDon, Sach, NhanVien, KhachHang WHERE HoaDon.MaSach = Sach.MaSach AND HoaDon.MaNV = NhanVien.MaNV AND HoaDon.MaKH = KhachHang.MaKH AND MaHD = '{MaHoaDonThanhToan}'";
            cmd = new SqlCommand(query, conn);
            data = cmd.ExecuteReader();
            hoadon hoadon = new hoadon();
            while (data.Read())
            {
                hoadon.MaHD = (string)data["MaHD"];
                hoadon.TenSach = (string)data["TenSach"];
                hoadon.TenNV = (string)data["TenNV"];
                hoadon.TenKH = (string)data["TenKH"];
                hoadon.ThoiGian = (DateTime)data["ThoiGian"];
                hoadon.TongTien = (Decimal)data["TongTien"];
            }
            conn.Close();

            //Lấy thông tin của dòng được click
            string MaHD = hoadon.MaHD; //Mã Hóa Đơn
            string TenSach = hoadon.TenSach;
            string TenNV = hoadon.TenNV;
            string TenKH = hoadon.TenKH;
            string ThoiGian = hoadon.ThoiGian.ToString();
            string TongTien = hoadon.TongTien.ToString();

            //Xóa tất cả các dòng cũ có trong dgvExportHoaDon
            dgvExportHoaDon.Rows.Clear();
            //Thêm dòng mới vào dgvExportHoaDon với thông tin của dòng được click
            dgvExportHoaDon.Rows.Add(MaHD, TenSach, TenNV, TenKH, ThoiGian, TongTien);
            //Xóa bớt dòng trống bên cuối dgvExportHoaDon
            dgvExportHoaDon.AllowUserToAddRows = false;
            dgvExportHoaDon.AllowUserToDeleteRows = false;

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = textBox1.Text + ".docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Export_Data_To_Word(dgvExportHoaDon, sfd.FileName);
            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Mã Sách!");
            }else if (textBox6.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Tổng Tiền!");
            }
            else
            {
                try
                {
                    conn.Open();
                    query = $"INSERT INTO HoaDon(MaHD,MaSach,MaNV,MaKH,ThoiGian,TongTien) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox6.Text}', '{textBox5.Text}')";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        DialogResult delete = MessageBox.Show("Thêm Hóa Đơn Thành Công! \nBạn Muốn Xuất Hóa Đơn Không?", "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (delete == DialogResult.Yes)
                        {
                            exportHoaDonVuaThanhToan(textBox1.Text);
                        }
                        clearTextBox();
                        getDataHD();
                    }
                    else
                    {
                        MessageBox.Show("Thêm Hóa Đơn Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

        }

        void searchData(string MaHD)
        {
            try
            {
                List<hoadon> lstHD = new List<hoadon>();
                conn.Open();
                query = $"SELECT HoaDon.MaHD, HoaDon.ThoiGian, HoaDon.TongTien, Sach.TenSach, NhanVien.TenNV, KhachHang.TenKH FROM HoaDon, Sach, NhanVien, KhachHang WHERE HoaDon.MaSach = Sach.MaSach AND HoaDon.MaNV = NhanVien.MaNV AND HoaDon.MaKH = KhachHang.MaKH AND MaHD LIKE '%{MaHD}%'";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    hoadon hoadon = new hoadon();
                    hoadon.MaHD = (string)data["MaHD"];
                    hoadon.TenSach = (string)data["TenSach"];
                    hoadon.TenNV = (string)data["TenNV"];
                    hoadon.TenKH = (string)data["TenKH"];
                    hoadon.ThoiGian = (DateTime)data["ThoiGian"];
                    hoadon.TongTien = (Decimal)data["TongTien"];
                    lstHD.Add(hoadon);
                }
                dgvHoaDon.DataSource = lstHD;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            searchData(textBox7.Text);
        }
    }
}
