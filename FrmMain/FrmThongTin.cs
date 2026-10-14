using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Database;

namespace quanlydiem
{
    public partial class FrmThongTinTaiKhoan : Form
    {
        private readonly string _tenDangNhap;

        // Constructor không tham số để Visual Studio Designer mở được form
        public FrmThongTinTaiKhoan() : this("")
        {
        }

        public FrmThongTinTaiKhoan(string tenDangNhap)
        {
            _tenDangNhap = tenDangNhap;

            InitializeComponent();

            // Gắn sự kiện
            btnSua.Click += BtnSua_Click;
            btnLuu.Click += BtnLuu_Click;

            // Chỉ tải dữ liệu khi chạy thật, không chạy trong Designer
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                HienThiThongTin();
        }

        // ================== TRUY VẤN DỮ LIỆU ==================

        // Lấy dữ liệu từ SQL Server
        private void HienThiThongTin()
        {
            if (string.IsNullOrWhiteSpace(_tenDangNhap))
                return;

            try
            {
                const string sql = @"
                    SELECT tendangnhap, hoten, email, sodienthoai
                    FROM taikhoan
                    WHERE tendangnhap = @tendangnhap";

                DataTable dt = DbHelper.ExecuteQuery(
                    sql,
                    new SqlParameter("@tendangnhap", _tenDangNhap));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy tài khoản " + _tenDangNhap + ".",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DataRow r = dt.Rows[0];

                txtTenDangNhap.Text = DocChuoi(r, "tendangnhap");
                txtHoTen.Text = DocChuoi(r, "hoten");
                txtEmail.Text = DocChuoi(r, "email");
                txtSoDienThoai.Text = DocChuoi(r, "sodienthoai");

                // Bảng taikhoan chưa có cột vai trò
                txtVaiTro.Text = "Quản lý";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được thông tin tài khoản.\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static string DocChuoi(DataRow r, string cot)
        {
            object v = r[cot];
            return v == DBNull.Value ? string.Empty : Convert.ToString(v);
        }

        // ================== SỰ KIỆN ==================

        // Bấm Sửa
        private void BtnSua_Click(object sender, EventArgs e)
        {
            txtHoTen.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtSoDienThoai.ReadOnly = false;

            txtHoTen.Focus();
        }

        // Bấm Lưu
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!");
                txtHoTen.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!");
                txtEmail.Focus();
                return;
            }

            try
            {
                const string sql = @"
                    UPDATE taikhoan
                    SET hoten = @hoten,
                        email = @email,
                        sodienthoai = @sodienthoai
                    WHERE tendangnhap = @tendangnhap";

                int soDong = DbHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@hoten", txtHoTen.Text.Trim()),
                    new SqlParameter("@email",
                        string.IsNullOrWhiteSpace(txtEmail.Text)
                            ? (object)DBNull.Value
                            : txtEmail.Text.Trim()),
                    new SqlParameter("@sodienthoai",
                        string.IsNullOrWhiteSpace(txtSoDienThoai.Text)
                            ? (object)DBNull.Value
                            : txtSoDienThoai.Text.Trim()),
                    new SqlParameter("@tendangnhap", _tenDangNhap));

                if (soDong > 0)
                {
                    MessageBox.Show(
                        "Lưu thông tin tài khoản thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    HienThiThongTin();
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy tài khoản để cập nhật.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lưu thông tin thất bại.\nChi tiết: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}