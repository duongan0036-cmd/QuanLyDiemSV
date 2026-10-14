using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CodeLogic;
using Database;
using Guna.UI2.WinForms;

namespace UI.UserControls
{
    public class UcHeThong : UserControl
    {
        private Guna2TextBox txtMkCu;
        private Guna2TextBox txtMkMoi;
        private Guna2TextBox txtMkXacNhan;
        private CheckBox chkHienMk;
        private Label lblThongBao;
        private Guna2Button btnDoiMatKhau;
        private Guna2Button btnDangXuat;

        private static readonly Color MauChinh = Color.FromArgb(94, 22, 110);
        private static readonly Color MauNen = Color.FromArgb(250, 243, 253);
        private static readonly Color MauChu = Color.FromArgb(60, 20, 70);

        public UcHeThong()
        {
            XayDungGiaoDien();
        }
        private static Label TaoNhan(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = MauChu,
                BackColor = Color.White,
                Margin = new Padding(0, 12, 0, 4)
            };
        }

        private static Guna2TextBox TaoOMatKhau()
        {
            return new Guna2TextBox
            {
                Height = 40,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                BorderRadius = 8,
                BorderColor = Color.FromArgb(225, 205, 235),
                PasswordChar = '●',
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(0)
            };
        }

        private void XayDungGiaoDien()
        {
            SuspendLayout();
            BackColor = MauNen;
            Padding = new Padding(24, 16, 24, 20);
            Font = new Font("Segoe UI", 9.5f);

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };

            // ====== CARD: ĐỔI MẬT KHẨU ======
            var cardDoiMk = new Guna2Panel
            {
                Width = 520,
                Height = 470,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(28, 24, 28, 24),
                Margin = new Padding(0, 0, 0, 16)
            };

            var tbl = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            var lblHeaderDoiMk = new Label
            {
                Text = "ĐỔI MẬT KHẨU ĐĂNG NHẬP",
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 12f),
                ForeColor = MauChinh,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 6)
            };

            txtMkCu = TaoOMatKhau();
            txtMkMoi = TaoOMatKhau();
            txtMkXacNhan = TaoOMatKhau();

            chkHienMk = new CheckBox
            {
                Text = "Hiển thị mật khẩu",
                AutoSize = true,
                ForeColor = Color.FromArgb(120, 100, 130),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9f),
                Margin = new Padding(0, 12, 0, 0)
            };
            chkHienMk.CheckedChanged += (s, e) =>
            {
                char c = chkHienMk.Checked ? '\0' : '●';
                txtMkCu.PasswordChar = c;
                txtMkMoi.PasswordChar = c;
                txtMkXacNhan.PasswordChar = c;
            };

            lblThongBao = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(440, 0),
                ForeColor = Color.FromArgb(200, 40, 50),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9f),
                Margin = new Padding(0, 10, 0, 0)
            };

            btnDoiMatKhau = new Guna2Button
            {
                Text = "Cập nhật mật khẩu",
                Size = new Size(190, 42),
                BorderRadius = 8,
                FillColor = MauChinh,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9.5f),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 14, 0, 0)
            };
            btnDoiMatKhau.Click += BtnDoiMatKhau_Click;

            tbl.Controls.Add(lblHeaderDoiMk);
            tbl.Controls.Add(TaoNhan("Mật khẩu hiện tại"));
            tbl.Controls.Add(txtMkCu);
            tbl.Controls.Add(TaoNhan("Mật khẩu mới (ít nhất 6 ký tự)"));
            tbl.Controls.Add(txtMkMoi);
            tbl.Controls.Add(TaoNhan("Xác nhận mật khẩu mới"));
            tbl.Controls.Add(txtMkXacNhan);
            tbl.Controls.Add(chkHienMk);
            tbl.Controls.Add(lblThongBao);
            tbl.Controls.Add(btnDoiMatKhau);

            cardDoiMk.Controls.Add(tbl);

            // ====== CARD: ĐĂNG XUẤT ======
            var cardLogout = new Guna2Panel
            {
                Width = 520,
                Height = 100,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(28),
                Margin = new Padding(0, 0, 0, 16)
            };

            btnDangXuat = new Guna2Button
            {
                Text = "Đăng xuất khỏi hệ thống",
                Location = new Point(28, 28),
                Size = new Size(230, 42),
                BorderRadius = 8,
                FillColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9.5f),
                Cursor = Cursors.Hand
            };
            btnDangXuat.Click += (s, e) =>
            {
                var f = FindForm();
                if (f is FrmMain main)
                {
                    var btnDx = main.Controls.Find("btnDangXuat", true);
                    if (btnDx.Length > 0 && btnDx[0] is Button b)
                        b.PerformClick();
                    else
                        f.Close();
                }
            };
            cardLogout.Controls.Add(btnDangXuat);

            flow.Controls.Add(cardDoiMk);
            flow.Controls.Add(cardLogout);

            // Căn giữa các card theo chiều ngang
            flow.Resize += (s, e) =>
            {
                int left = Math.Max(0, (flow.ClientSize.Width - cardDoiMk.Width) / 2);
                flow.Padding = new Padding(left, 8, 0, 0);
            };

            Controls.Add(flow);

            ResumeLayout(false);
        }
    
        private void BtnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string cu = txtMkCu.Text ?? "";
            string moi = txtMkMoi.Text ?? "";
            string xacNhan = txtMkXacNhan.Text ?? "";

            if (string.IsNullOrEmpty(cu) || string.IsNullOrEmpty(moi) || string.IsNullOrEmpty(xacNhan))
            {
                lblThongBao.ForeColor = Color.FromArgb(200, 40, 50);
                lblThongBao.Text = "Vui lòng nhập đầy đủ các ô mật khẩu.";
                return;
            }

            if (moi.Length < 6)
            {
                lblThongBao.ForeColor = Color.FromArgb(200, 40, 50);
                lblThongBao.Text = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return;
            }

            if (moi != xacNhan)
            {
                lblThongBao.ForeColor = Color.FromArgb(200, 40, 50);
                lblThongBao.Text = "Mật khẩu xác nhận không khớp.";
                return;
            }

            string tenDangNhap = PhienLamViec.NguoiDung != null ? PhienLamViec.NguoiDung.TenDangNhap : "admin";

            try
            {
                string hashCu = FrmDangNhap.BamSha256(cu);
                string hashMoi = FrmDangNhap.BamSha256(moi);

                using (var conn = new SqlConnection(DbHelper.ConnStr))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM taikhoan WHERE tendangnhap = @t AND matkhau = @m", conn))
                    {
                        cmd.Parameters.Add("@t", SqlDbType.VarChar, 100).Value = tenDangNhap;
                        cmd.Parameters.Add("@m", SqlDbType.VarChar, 100).Value = hashCu;

                        int dung = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        if (dung == 0)
                        {
                            lblThongBao.ForeColor = Color.FromArgb(200, 40, 50);
                            lblThongBao.Text = "Mật khẩu hiện tại không chính xác.";
                            return;
                        }
                    }

                    using (var cmd = new SqlCommand("UPDATE taikhoan SET matkhau = @m WHERE tendangnhap = @t", conn))
                    {
                        cmd.Parameters.Add("@m", SqlDbType.VarChar, 100).Value = hashMoi;
                        cmd.Parameters.Add("@t", SqlDbType.VarChar, 100).Value = tenDangNhap;
                        cmd.ExecuteNonQuery();
                    }
                }

                txtMkCu.Clear();
                txtMkMoi.Clear();
                txtMkXacNhan.Clear();
                lblThongBao.ForeColor = Color.FromArgb(39, 174, 96);
                lblThongBao.Text = "Đổi mật khẩu thành công!";
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblThongBao.ForeColor = Color.FromArgb(200, 40, 50);
                lblThongBao.Text = "Lỗi đổi mật khẩu: " + ex.Message;
            }
        }
    }
}
