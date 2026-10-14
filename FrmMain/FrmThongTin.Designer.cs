using System.Drawing;
using System.Windows.Forms;

namespace quanlydiem
{
    partial class FrmThongTinTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblTieuDe = new Label();
            this.lblAvatar = new Label();
            this.lblHoTen = new Label();
            this.txtHoTen = new TextBox();
            this.lblTenDangNhap = new Label();
            this.txtTenDangNhap = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblSoDienThoai = new Label();
            this.txtSoDienThoai = new TextBox();
            this.lblVaiTro = new Label();
            this.txtVaiTro = new TextBox();
            this.btnSua = new Button();
            this.btnLuu = new Button();

            var tim = Color.FromArgb(77, 18, 63);
            var hong = Color.FromArgb(171, 51, 133);

            this.SuspendLayout();

            // ===== lblTieuDe =====
            this.lblTieuDe.Text = "THÔNG TIN TÀI KHOẢN";
            this.lblTieuDe.Font = new Font("Segoe UI", 17f, FontStyle.Bold);
            this.lblTieuDe.ForeColor = hong;
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTieuDe.Location = new Point(20, 20);
            this.lblTieuDe.Size = new Size(520, 40);

            // ===== lblAvatar =====
            this.lblAvatar.Text = "\U0001F464";
            this.lblAvatar.Font = new Font("Segoe UI Emoji", 34f);
            this.lblAvatar.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAvatar.BackColor = Color.White;
            this.lblAvatar.ForeColor = hong;
            this.lblAvatar.Location = new Point(225, 75);
            this.lblAvatar.Size = new Size(100, 75);

            // ===== Họ và tên =====
            ThietLapNhan(this.lblHoTen, "Họ và tên:", 175);
            ThietLapOText(this.txtHoTen, 175, 0);

            // ===== Tên đăng nhập (chỉ xem) =====
            ThietLapNhan(this.lblTenDangNhap, "Tên đăng nhập:", 223);
            ThietLapOText(this.txtTenDangNhap, 223, 1);
            this.txtTenDangNhap.ReadOnly = true;
            this.txtTenDangNhap.BackColor = Color.FromArgb(235, 235, 235);

            // ===== Email =====
            ThietLapNhan(this.lblEmail, "Email:", 271);
            ThietLapOText(this.txtEmail, 271, 2);

            // ===== Số điện thoại =====
            ThietLapNhan(this.lblSoDienThoai, "Số điện thoại:", 319);
            ThietLapOText(this.txtSoDienThoai, 319, 3);

            // ===== Vai trò (chỉ xem) =====
            ThietLapNhan(this.lblVaiTro, "Vai trò:", 367);
            ThietLapOText(this.txtVaiTro, 367, 4);
            this.txtVaiTro.ReadOnly = true;
            this.txtVaiTro.BackColor = Color.FromArgb(235, 235, 235);

            // ===== btnSua =====
            this.btnSua.Text = "Sửa";
            this.btnSua.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnSua.ForeColor = Color.White;
            this.btnSua.BackColor = tim;
            this.btnSua.FlatStyle = FlatStyle.Flat;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Size = new Size(120, 40);
            this.btnSua.Location = new Point(135, 440);
            this.btnSua.Cursor = Cursors.Hand;
            this.btnSua.TabIndex = 5;

            // ===== btnLuu =====
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            this.btnLuu.ForeColor = Color.White;
            this.btnLuu.BackColor = hong;
            this.btnLuu.FlatStyle = FlatStyle.Flat;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Size = new Size(120, 40);
            this.btnLuu.Location = new Point(285, 440);
            this.btnLuu.Cursor = Cursors.Hand;
            this.btnLuu.TabIndex = 6;

            // ===== FrmThongTinTaiKhoan =====
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(560, 530);
            this.BackColor = Color.FromArgb(247, 237, 244);
            this.Font = new Font("Segoe UI", 10f);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "FrmThongTinTaiKhoan";
            this.Text = "THÔNG TIN TÀI KHOẢN";
            this.AcceptButton = this.btnLuu;

            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblAvatar);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblTenDangNhap);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblVaiTro);
            this.Controls.Add(this.txtVaiTro);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnLuu);

            this.ResumeLayout(false);
        }

        #endregion

        // Nhãn bên trái mỗi dòng
        private static void ThietLapNhan(Label lbl, string noiDung, int y)
        {
            lbl.Text = noiDung;
            lbl.Location = new Point(45, y + 3);
            lbl.Size = new Size(150, 28);
            lbl.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(77, 18, 63);
        }

        // Ô nhập bên phải mỗi dòng
        private static void ThietLapOText(TextBox txt, int y, int tabIndex)
        {
            txt.Location = new Point(205, y);
            txt.Size = new Size(300, 30);
            txt.Font = new Font("Segoe UI", 10f);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = Color.White;
            txt.TabIndex = tabIndex;
        }

        private Label lblTieuDe;
        private Label lblAvatar;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblVaiTro;
        private TextBox txtVaiTro;
        private Button btnSua;
        private Button btnLuu;
    }
}