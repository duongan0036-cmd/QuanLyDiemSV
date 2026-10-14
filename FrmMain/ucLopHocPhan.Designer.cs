using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace UI.UserControls
{
    partial class UcLopHocPhan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Bảng màu dùng chung với các màn hình khác
            var mauNen = Color.FromArgb(250, 243, 253);
            var mauChinh = Color.FromArgb(94, 22, 110);
            var mauNhat = Color.FromArgb(243, 229, 250);
            var mauChu = Color.FromArgb(60, 20, 70);
            var mauVien = Color.FromArgb(225, 205, 235);

            this.card = new Guna2Panel();
            this.pnlTim = new Panel();
            this.txttim = new Guna2TextBox();
            this.btntim = new Guna2Button();
            this.btnlammoi = new Guna2Button();
            this.pnlLop = new Panel();
            this.dglhp = new DataGridView();
            this.pnlSvHeader = new Panel();
            this.lblchon = new Label();
            this.pnlTimSv = new Panel();
            this.txttimsv = new Guna2TextBox();
            this.btntimsv = new Guna2Button();
            this.dgsv = new DataGridView();

            var spacer1 = new Panel();
            var spacer2 = new Panel();
            var spacer3 = new Panel();

            this.card.SuspendLayout();
            this.pnlTim.SuspendLayout();
            this.pnlLop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dglhp).BeginInit();
            this.pnlSvHeader.SuspendLayout();
            this.pnlTimSv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgsv).BeginInit();
            this.SuspendLayout();

            // ===== card: thẻ trắng bo góc chứa toàn bộ nội dung =====
            this.card.Dock = DockStyle.Fill;
            this.card.FillColor = Color.White;
            this.card.BorderRadius = 16;
            this.card.Padding = new Padding(16);
            // Fill thêm trước, các Top thêm sau: thêm sau thì nằm trên cùng
            this.card.Controls.Add(this.dgsv);
            this.card.Controls.Add(this.pnlSvHeader);
            this.card.Controls.Add(this.pnlLop);
            this.card.Controls.Add(this.pnlTim);

            // ===== pnlTim: ô tìm lớp học phần + nút Tìm + Làm mới =====
            this.pnlTim.BackColor = Color.White;
            this.pnlTim.Dock = DockStyle.Top;
            this.pnlTim.Height = 48;
            this.pnlTim.Padding = new Padding(0, 0, 0, 10);
            // Fill thêm trước, các control Right thêm sau (cái thêm sau nằm ngoài cùng bên phải)
            this.pnlTim.Controls.Add(this.txttim);
            this.pnlTim.Controls.Add(spacer1);
            this.pnlTim.Controls.Add(this.btntim);
            this.pnlTim.Controls.Add(spacer2);
            this.pnlTim.Controls.Add(this.btnlammoi);

            var iconTim = IconTimKiem();

            this.txttim.Dock = DockStyle.Fill;
            this.txttim.PlaceholderText = "Tìm lớp học phần...";
            this.txttim.BorderRadius = 8;
            this.txttim.BorderColor = mauVien;
            this.txttim.FillColor = Color.White;
            this.txttim.ForeColor = mauChu;
            this.txttim.Font = new Font("Segoe UI", 9.5f);
            this.txttim.IconLeft = iconTim;
            this.txttim.IconLeftSize = iconTim.Size;
            this.txttim.TabIndex = 0;

            spacer1.Dock = DockStyle.Right;
            spacer1.Width = 10;
            spacer1.BackColor = Color.White;

            TaoKieuNut(this.btntim, "Tìm", 110, true, mauChinh, mauNhat);
            this.btntim.Dock = DockStyle.Right;
            this.btntim.TabIndex = 1;

            spacer2.Dock = DockStyle.Right;
            spacer2.Width = 10;
            spacer2.BackColor = Color.White;

            TaoKieuNut(this.btnlammoi, "Làm mới", 120, false, mauChinh, mauNhat);
            this.btnlammoi.Dock = DockStyle.Right;
            this.btnlammoi.TabIndex = 2;

            // ===== pnlLop: bảng lớp học phần =====
            this.pnlLop.BackColor = Color.White;
            this.pnlLop.Dock = DockStyle.Top;
            this.pnlLop.Height = 250;
            this.pnlLop.Padding = new Padding(0, 0, 0, 10);
            this.pnlLop.Controls.Add(this.dglhp);

            ApDungKieuBang(this.dglhp);
            this.dglhp.Dock = DockStyle.Fill;
            this.dglhp.Columns.AddRange(new DataGridViewColumn[] {
                Cot("malophp",   "Mã lớp HP",   100),
                Cot("mamon",     "Mã môn",       90),
                Cot("tenmon",    "Tên môn học", 220),
                Cot("sotin",     "Số tín chỉ",   80),
                Cot("giangvien", "Giảng viên",  180),
                Cot("siso",      "Sĩ số",        80)
            });
            this.dglhp.TabIndex = 3;

            // ===== pnlSvHeader: tiêu đề danh sách sinh viên + ô tìm sinh viên =====
            this.pnlSvHeader.BackColor = Color.White;
            this.pnlSvHeader.Dock = DockStyle.Top;
            this.pnlSvHeader.Height = 56;
            this.pnlSvHeader.Controls.Add(this.lblchon);     // Fill (thêm trước)
            this.pnlSvHeader.Controls.Add(this.pnlTimSv);    // Right (thêm sau)

            this.lblchon.Dock = DockStyle.Fill;
            this.lblchon.AutoSize = false;
            this.lblchon.BackColor = Color.White;
            this.lblchon.Font = new Font("Segoe UI Semibold", 11.5f);
            this.lblchon.ForeColor = mauChinh;
            this.lblchon.TextAlign = ContentAlignment.MiddleLeft;
            this.lblchon.UseMnemonic = false;
            this.lblchon.Text = "Chọn một lớp học phần ở bảng trên để xem sinh viên.";

            this.pnlTimSv.BackColor = Color.White;
            this.pnlTimSv.Dock = DockStyle.Right;
            this.pnlTimSv.Width = 440;
            this.pnlTimSv.Padding = new Padding(0, 9, 0, 9);
            this.pnlTimSv.Controls.Add(this.txttimsv);
            this.pnlTimSv.Controls.Add(spacer3);
            this.pnlTimSv.Controls.Add(this.btntimsv);

            var iconTimSv = IconTimKiem();

            this.txttimsv.Dock = DockStyle.Fill;
            this.txttimsv.PlaceholderText = "Tìm sinh viên...";
            this.txttimsv.BorderRadius = 8;
            this.txttimsv.BorderColor = mauVien;
            this.txttimsv.FillColor = Color.White;
            this.txttimsv.ForeColor = mauChu;
            this.txttimsv.Font = new Font("Segoe UI", 9.5f);
            this.txttimsv.IconLeft = iconTimSv;
            this.txttimsv.IconLeftSize = iconTimSv.Size;
            this.txttimsv.TabIndex = 4;

            spacer3.Dock = DockStyle.Right;
            spacer3.Width = 10;
            spacer3.BackColor = Color.White;

            TaoKieuNut(this.btntimsv, "Tìm SV", 110, true, mauChinh, mauNhat);
            this.btntimsv.Dock = DockStyle.Right;
            this.btntimsv.TabIndex = 5;

            // ===== dgsv: bảng sinh viên =====
            ApDungKieuBang(this.dgsv);
            this.dgsv.Dock = DockStyle.Fill;
            this.dgsv.Columns.AddRange(new DataGridViewColumn[] {
                Cot("stt",      "STT",        50),
                Cot("masv",     "Mã SV",      90),
                Cot("tensv",    "Họ tên",    200),
                Cot("ngaysinh", "Ngày sinh", 100, "dd/MM/yyyy"),
                Cot("malp",     "Lớp",        90),
                Cot("diemcc",   "Điểm CC",    80, "0.00"),
                Cot("diemgk",   "Điểm GK",    80, "0.00"),
                Cot("diemck",   "Điểm CK",    80, "0.00"),
                CotKhoa()
            });
            this.dgsv.TabIndex = 6;

            // ===== UcLopHocPhan =====
            this.BackColor = mauNen;
            this.Font = new Font("Segoe UI", 9.5f);
            this.Name = "UcLopHocPhan";
            this.Padding = new Padding(24, 16, 24, 24);
            this.Size = new Size(1000, 600);
            this.Controls.Add(this.card);

            this.card.ResumeLayout(false);
            this.pnlTim.ResumeLayout(false);
            this.pnlLop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dglhp).EndInit();
            this.pnlTimSv.ResumeLayout(false);
            this.pnlSvHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgsv).EndInit();
            this.ResumeLayout(false);
        }

        // Nút Guna cùng kiểu với các màn hình khác: chính = tím đậm, phụ = tím nhạt
        private static void TaoKieuNut(
            Guna2Button b, string chu, int rong, bool chinh, Color mauChinh, Color mauNhat)
        {
            b.Text = chu;
            b.Width = rong;
            b.BorderRadius = 8;
            b.FillColor = chinh ? mauChinh : mauNhat;
            b.ForeColor = chinh ? Color.White : mauChinh;
            b.Font = new Font("Segoe UI Semibold", 9.5f);
            b.Cursor = Cursors.Hand;
            b.Margin = Padding.Empty;
            b.HoverState.FillColor = chinh ? Color.FromArgb(120, 40, 140) : Color.FromArgb(232, 210, 244);
        }

        // Kiểu bảng giống màn hình Quản lý điểm
        private static void ApDungKieuBang(DataGridView g)
        {
            var mauChinh = Color.FromArgb(94, 22, 110);
            var mauChu = Color.FromArgb(60, 20, 70);

            g.AutoGenerateColumns = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.ReadOnly = true;
            g.MultiSelect = false;
            g.RowHeadersVisible = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            g.GridColor = Color.FromArgb(236, 222, 243);
            g.RowTemplate.Height = 42;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tiêu đề cột
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 40;
            var hs = g.ColumnHeadersDefaultCellStyle;
            hs.BackColor = mauChinh;
            hs.ForeColor = Color.White;
            hs.SelectionBackColor = mauChinh;
            hs.SelectionForeColor = Color.White;
            hs.Font = new Font("Segoe UI Semibold", 9f);
            hs.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ô dữ liệu
            var cs = g.DefaultCellStyle;
            cs.BackColor = Color.White;
            cs.ForeColor = mauChu;
            cs.Font = new Font("Segoe UI", 9f);
            cs.SelectionBackColor = Color.FromArgb(236, 214, 247);
            cs.SelectionForeColor = mauChu;
            cs.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cs.NullValue = "—";

            // Dòng xen kẽ
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 242, 252);
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = cs.SelectionBackColor;
            g.AlternatingRowsDefaultCellStyle.SelectionForeColor = cs.SelectionForeColor;
        }

        // Tạo cột text gắn với tên cột của DataTable (Name = DataPropertyName)
        private static DataGridViewTextBoxColumn Cot(
            string ten, string tieuDe, float trongSo, string dinhDang = null)
        {
            var c = new DataGridViewTextBoxColumn();
            c.Name = ten;
            c.DataPropertyName = ten;
            c.HeaderText = tieuDe;
            c.FillWeight = trongSo;
            c.MinimumWidth = 50;
            c.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (dinhDang != null)
                c.DefaultCellStyle.Format = dinhDang;
            return c;
        }

        private static DataGridViewCheckBoxColumn CotKhoa()
        {
            var c = new DataGridViewCheckBoxColumn();
            c.Name = "dakhoa";
            c.DataPropertyName = "dakhoa";
            c.HeaderText = "Đã khóa";
            c.FillWeight = 70;
            c.MinimumWidth = 60;
            c.SortMode = DataGridViewColumnSortMode.NotSortable;
            return c;
        }

        // Icon kính lúp vẽ từ font Segoe MDL2 Assets, kích thước tự theo DPI màn hình
        private static Image IconTimKiem()
        {
            float k;
            using (var g0 = Graphics.FromHwnd(IntPtr.Zero))
                k = g0.DpiX / 96f;
            int size = (int)Math.Round(16 * k);

            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            using (var f = new Font("Segoe MDL2 Assets", size * 0.8f, FontStyle.Regular, GraphicsUnit.Pixel))
            using (var br = new SolidBrush(Color.FromArgb(130, 100, 150)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.DrawString("\uE721", f, br, new RectangleF(0, 0, size, size), sf);
            }
            return bmp;
        }

        private Guna2Panel card;
        private Panel pnlTim;
        private Guna2TextBox txttim;
        private Guna2Button btntim;
        private Guna2Button btnlammoi;
        private Panel pnlLop;
        private DataGridView dglhp;
        private Panel pnlSvHeader;
        private Label lblchon;
        private Panel pnlTimSv;
        private Guna2TextBox txttimsv;
        private Guna2Button btntimsv;
        private DataGridView dgsv;
    }
}