
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace quanlydiem
{
    public partial class FrmSuaTaiKhoan : Form
    {
        private readonly string _tenDangNhap;

        private readonly string chuoiKetNoi =
            ConfigurationManager.ConnectionStrings["QuanLyDiem"]?.ConnectionString;

        private TextBox txtHoTen;
        private TextBox txtTenDangNhap;
        private TextBox txtEmail;
        private TextBox txtSoDienThoai;

        public FrmSuaTaiKhoan(string tenDangNhap)
        {
            _tenDangNhap = tenDangNhap;

            TaoGiaoDien();
            TaiThongTin();
        }

        private void TaoGiaoDien()
        {
            Text = "SỬA THÔNG TIN TÀI KHOẢN";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(580, 530);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.FromArgb(247, 237, 244);
            Font = new Font("Segoe UI", 10);

            Label lblTieuDe = new Label
            {
                Text = "SỬA THÔNG TIN TÀI KHOẢN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(171, 51, 133),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 20),
                Size = new Size(520, 45)
            };
            Controls.Add(lblTieuDe);

            TaoTruong("Họ và tên:", 100, out txtHoTen);
            TaoTruong("Tên đăng nhập:", 165, out txtTenDangNhap);
            TaoTruong("Email:", 230, out txtEmail);
            TaoTruong("Số điện thoại:", 295, out txtSoDienThoai);

            txtTenDangNhap.ReadOnly = true;
            txtTenDangNhap.BackColor = Color.Gainsboro;

            Button btnLuu = new Button
            {
                Text = "Lưu",
                Location = new Point(150, 390),
                Size = new Size(120, 42),
                BackColor = Color.FromArgb(171, 51, 133),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;
            Controls.Add(btnLuu);

            Button btnHuy = new Button
            {
                Text = "Hủy",
                Location = new Point(300, 390),
                Size = new Size(120, 42),
                BackColor = Color.FromArgb(77, 18, 63),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => Close();
            Controls.Add(btnHuy);
        }

        private void TaoTruong(string tenNhan, int y, out TextBox txt)
        {
            Label lbl = new Label
            {
                Text = tenNhan,
                Location = new Point(40, y + 4),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(77, 18, 63)
            };
            Controls.Add(lbl);

            txt = new TextBox
            {
                Location = new Point(195, y),
                Size = new Size(320, 30),
                Font = new Font("Segoe UI", 10)
            };
            Controls.Add(txt);
        }

        private void TaiThongTin()
        {
            if (string.IsNullOrWhiteSpace(chuoiKetNoi))
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối QuanLyDiem trong App.config.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT tendangnhap, hoten, email, sodienthoai
                      FROM taikhoan
                      WHERE tendangnhap = @ten", conn))
                {
                    cmd.Parameters.AddWithValue("@ten", _tenDangNhap);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTenDangNhap.Text = reader["tendangnhap"].ToString();
                            txtHoTen.Text = reader["hoten"] == DBNull.Value
                                ? "" : reader["hoten"].ToString();
                            txtEmail.Text = reader["email"] == DBNull.Value
                                ? "" : reader["email"].ToString();
                            txtSoDienThoai.Text = reader["sodienthoai"] == DBNull.Value
                                ? "" : reader["sodienthoai"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message);
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.");
                txtHoTen.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(chuoiKetNoi))
            {
                MessageBox.Show("Chưa cấu hình chuỗi kết nối SQL Server.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
                using (SqlCommand cmd = new SqlCommand(
                    @"UPDATE taikhoan
                      SET hoten = @hoten,
                          email = @email,
                          sodienthoai = @sdt
                      WHERE tendangnhap = @ten", conn))
                {
                    cmd.Parameters.AddWithValue("@hoten", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@email",
                        string.IsNullOrWhiteSpace(txtEmail.Text)
                            ? (object)DBNull.Value : txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@sdt",
                        string.IsNullOrWhiteSpace(txtSoDienThoai.Text)
                            ? (object)DBNull.Value : txtSoDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", _tenDangNhap);

                    conn.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Cập nhật tài khoản thành công!");
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản để cập nhật.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu thông tin: " + ex.Message);
            }
        }
    }
}