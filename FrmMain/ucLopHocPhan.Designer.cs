using System.Drawing;
using System.Windows.Forms;

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
            this.pnlTim = new Panel();
            this.txttim = new TextBox();
            this.btntim = new Button();
            this.btnlammoi = new Button();
            this.pnlLop = new Panel();
            this.dglhp = new DataGridView();
            this.pnlSvHeader = new Panel();
            this.lblchon = new Label();
            this.pnlTimSv = new Panel();
            this.txttimsv = new TextBox();
            this.btntimsv = new Button();
            this.dgsv = new DataGridView();

            var pageColor = Color.FromArgb(252, 246, 251);
            var tim = Color.FromArgb(90, 20, 95);

            this.pnlTim.SuspendLayout();
            this.pnlLop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dglhp).BeginInit();
            this.pnlSvHeader.SuspendLayout();
            this.pnlTimSv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgsv).BeginInit();
            this.SuspendLayout();

            // ===== pnlTim: ô tìm lớp học phần =====
            this.pnlTim.BackColor = pageColor;
            this.pnlTim.Dock = DockStyle.Top;
            this.pnlTim.Height = 56;
            this.pnlTim.Controls.Add(this.btnlammoi);
            this.pnlTim.Controls.Add(this.btntim);
            this.pnlTim.Controls.Add(this.txttim);

            this.txttim.Font = new Font("Segoe UI", 10.5f);
            this.txttim.BorderStyle = BorderStyle.FixedSingle;
            this.txttim.Location = new Point(0, 12);
            this.txttim.Size = new Size(380, 30);
            this.txttim.TabIndex = 0;

            this.btntim.Text = "Tìm";
            this.btntim.Location = new Point(392, 10);
            this.btntim.Size = new Size(100, 34);
            this.btntim.TabIndex = 1;

            this.btnlammoi.Text = "Làm mới";
            this.btnlammoi.Location = new Point(504, 10);
            this.btnlammoi.Size = new Size(110, 34);
            this.btnlammoi.TabIndex = 2;

            // ===== pnlLop: bảng lớp học phần =====
            this.pnlLop.BackColor = pageColor;
            this.pnlLop.Dock = DockStyle.Top;
            this.pnlLop.Height = 240;
            this.pnlLop.Padding = new Padding(0, 0, 0, 12);
            this.pnlLop.Controls.Add(this.dglhp);

            this.dglhp.Dock = DockStyle.Fill;
            this.dglhp.AutoGenerateColumns = false;
            this.dglhp.Columns.AddRange(new DataGridViewColumn[] {
                Cot("malophp",   "Mã lớp HP",   100, false),
                Cot("mamon",     "Mã môn",       90, false),
                Cot("tenmon",    "Tên môn học", 220, false),
                Cot("sotin",     "Số tín chỉ",   80, true),
                Cot("giangvien", "Giảng viên",  180, false),
                Cot("siso",      "Sĩ số",        80, true)
            });
            this.dglhp.TabIndex = 3;

            // ===== pnlSvHeader: tiêu đề + ô tìm sinh viên =====
            this.pnlSvHeader.BackColor = pageColor;
            this.pnlSvHeader.Dock = DockStyle.Top;
            this.pnlSvHeader.Height = 54;
            this.pnlSvHeader.Controls.Add(this.lblchon);     // Fill (thêm trước)
            this.pnlSvHeader.Controls.Add(this.pnlTimSv);    // Right (thêm sau)

            this.lblchon.Dock = DockStyle.Fill;
            this.lblchon.AutoSize = false;
            this.lblchon.Font = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            this.lblchon.ForeColor = tim;
            this.lblchon.TextAlign = ContentAlignment.MiddleLeft;
            this.lblchon.Text = "Chọn một lớp học phần ở bảng trên để xem sinh viên.";

            this.pnlTimSv.BackColor = pageColor;
            this.pnlTimSv.Dock = DockStyle.Right;
            this.pnlTimSv.Width = 420;
            this.pnlTimSv.Controls.Add(this.btntimsv);
            this.pnlTimSv.Controls.Add(this.txttimsv);

            this.txttimsv.Font = new Font("Segoe UI", 10.5f);
            this.txttimsv.BorderStyle = BorderStyle.FixedSingle;
            this.txttimsv.Location = new Point(0, 12);
            this.txttimsv.Size = new Size(290, 30);
            this.txttimsv.TabIndex = 4;

            this.btntimsv.Text = "Tìm SV";
            this.btntimsv.Location = new Point(302, 10);
            this.btntimsv.Size = new Size(100, 34);
            this.btntimsv.TabIndex = 5;

            // ===== dgsv: bảng sinh viên =====
            this.dgsv.Dock = DockStyle.Fill;
            this.dgsv.AutoGenerateColumns = false;
            this.dgsv.Columns.AddRange(new DataGridViewColumn[] {
                Cot("stt",      "STT",       50,  true),
                Cot("masv",     "Mã SV",     90,  false),
                Cot("tensv",    "Họ tên",    200, false),
                Cot("ngaysinh", "Ngày sinh", 100, true, "dd/MM/yyyy"),
                Cot("malp",     "Lớp",       90,  true),
                Cot("diemcc",   "Điểm CC",   80,  true, "0.##"),
                Cot("diemgk",   "Điểm GK",   80,  true, "0.##"),
                Cot("diemck",   "Điểm CK",   80,  true, "0.##"),
                CotKhoa()
            });
            this.dgsv.TabIndex = 6;

            // ===== UcLopHocPhan =====
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = pageColor;
            this.Font = new Font("Segoe UI", 9f);
            this.Name = "UcLopHocPhan";
            this.Padding = new Padding(24, 8, 24, 16);
            this.Size = new Size(1000, 600);
            // Fill thêm trước, các Top thêm sau: thêm sau thì nằm trên cùng
            this.Controls.Add(this.dgsv);
            this.Controls.Add(this.pnlSvHeader);
            this.Controls.Add(this.pnlLop);
            this.Controls.Add(this.pnlTim);

            this.pnlTim.ResumeLayout(false);
            this.pnlTim.PerformLayout();
            this.pnlLop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dglhp).EndInit();
            this.pnlTimSv.ResumeLayout(false);
            this.pnlTimSv.PerformLayout();
            this.pnlSvHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgsv).EndInit();
            this.ResumeLayout(false);
        }

        // Tạo cột text gắn với tên cột của DataTable (Name = DataPropertyName)
        private static DataGridViewTextBoxColumn Cot(
            string ten, string tieuDe, float trongSo, bool canGiua, string dinhDang = null)
        {
            var c = new DataGridViewTextBoxColumn();
            c.Name = ten;
            c.DataPropertyName = ten;
            c.HeaderText = tieuDe;
            c.FillWeight = trongSo;
            c.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (canGiua)
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            c.SortMode = DataGridViewColumnSortMode.NotSortable;
            return c;
        }

        private Panel pnlTim;
        private TextBox txttim;
        private Button btntim;
        private Button btnlammoi;
        private Panel pnlLop;
        private DataGridView dglhp;
        private Panel pnlSvHeader;
        private Label lblchon;
        private Panel pnlTimSv;
        private TextBox txttimsv;
        private Button btntimsv;
        private DataGridView dgsv;
    }
}