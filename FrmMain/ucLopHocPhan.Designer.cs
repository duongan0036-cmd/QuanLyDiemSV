
using System;
using System.Drawing;
using System.Windows.Forms;

namespace quanlydiem
{
    partial class ucLopHocPhan
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlBanner;
        private Panel pnlLogo;
        private Label lblTieuDe;
        private Label lblMoTa;
        private Label lblTagHeThong;

        private Panel pnlCardLhp;
        private Panel pnlBadgeLhp;
        private Label lblNhomLhp;
        private Label lblTagLhp;
        private TextBox txttim;
        private Button btntim;
        private Button btnlammoi;
        private DataGridView dglhp;

        private Panel pnlCardSv;
        private Panel pnlBadgeSv;
        private Label lblNhomSv;
        private Label lblTagSv;
        private Label lblchon;
        private TextBox txttimsv;
        private Button btntimsv;
        private DataGridView dgsv;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlBanner = new Panel();
            pnlLogo = new Panel();
            lblIcon = new Label();
            lblTieuDe = new Label();
            lblMoTa = new Label();
            lblTagHeThong = new Label();
            pnlCardLhp = new Panel();
            pnlBadgeLhp = new Panel();
            lblIconLhp = new Label();
            lblNhomLhp = new Label();
            lblTagLhp = new Label();
            txttim = new TextBox();
            btntim = new Button();
            btnlammoi = new Button();
            dglhp = new DataGridView();
            pnlCardSv = new Panel();
            pnlBadgeSv = new Panel();
            lblIconSv = new Label();
            lblNhomSv = new Label();
            lblTagSv = new Label();
            lblchon = new Label();
            txttimsv = new TextBox();
            btntimsv = new Button();
            dgsv = new DataGridView();
            pnlBanner.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlCardLhp.SuspendLayout();
            pnlBadgeLhp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dglhp).BeginInit();
            pnlCardSv.SuspendLayout();
            pnlBadgeSv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgsv).BeginInit();
            SuspendLayout();
            // 
            // pnlBanner
            // 
            pnlBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBanner.BackColor = Color.FromArgb(247, 237, 244);
            pnlBanner.Controls.Add(pnlLogo);
            pnlBanner.Controls.Add(lblTieuDe);
            pnlBanner.Controls.Add(lblMoTa);
            pnlBanner.Controls.Add(lblTagHeThong);
            pnlBanner.Location = new Point(20, 20);
            pnlBanner.Name = "pnlBanner";
            pnlBanner.Size = new Size(1314, 100);
            pnlBanner.TabIndex = 0;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = Color.FromArgb(77, 18, 63);
            pnlLogo.Controls.Add(lblIcon);
            pnlLogo.Location = new Point(18, 20);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(56, 56);
            pnlLogo.TabIndex = 0;
            // 
            // lblIcon
            // 
            lblIcon.Dock = DockStyle.Fill;
            lblIcon.Font = new Font("Segoe UI Symbol", 24F, FontStyle.Bold);
            lblIcon.ForeColor = Color.White;
            lblIcon.Location = new Point(0, 0);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(56, 56);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "✦";
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTieuDe
            // 
            lblTieuDe.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(171, 51, 133);
            lblTieuDe.Location = new Point(90, 4);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(600, 53);
            lblTieuDe.TabIndex = 1;
            lblTieuDe.Text = "QUẢN LÝ LỚP HỌC PHẦN";
            // 
            // lblMoTa
            // 
            lblMoTa.ForeColor = Color.FromArgb(77, 18, 63);
            lblMoTa.Location = new Point(92, 57);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(650, 25);
            lblMoTa.TabIndex = 2;
            lblMoTa.Text = "Xem danh sách lớp học phần và sinh viên trong từng lớp";
            // 
            // lblTagHeThong
            // 
            lblTagHeThong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTagHeThong.BackColor = Color.White;
            lblTagHeThong.BorderStyle = BorderStyle.FixedSingle;
            lblTagHeThong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTagHeThong.ForeColor = Color.FromArgb(171, 51, 133);
            lblTagHeThong.Location = new Point(1080, 32);
            lblTagHeThong.Name = "lblTagHeThong";
            lblTagHeThong.Size = new Size(210, 32);
            lblTagHeThong.TabIndex = 3;
            lblTagHeThong.Text = "✦  QUẢN LÝ ĐÀO TẠO";
            lblTagHeThong.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlCardLhp
            // 
            pnlCardLhp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCardLhp.BackColor = Color.White;
            pnlCardLhp.Controls.Add(pnlBadgeLhp);
            pnlCardLhp.Controls.Add(lblNhomLhp);
            pnlCardLhp.Controls.Add(lblTagLhp);
            pnlCardLhp.Controls.Add(txttim);
            pnlCardLhp.Controls.Add(btntim);
            pnlCardLhp.Controls.Add(btnlammoi);
            pnlCardLhp.Controls.Add(dglhp);
            pnlCardLhp.Location = new Point(20, 136);
            pnlCardLhp.Name = "pnlCardLhp";
            pnlCardLhp.Size = new Size(1314, 350);
            pnlCardLhp.TabIndex = 1;
            // 
            // pnlBadgeLhp
            // 
            pnlBadgeLhp.BackColor = Color.FromArgb(247, 237, 244);
            pnlBadgeLhp.Controls.Add(lblIconLhp);
            pnlBadgeLhp.Location = new Point(18, 16);
            pnlBadgeLhp.Name = "pnlBadgeLhp";
            pnlBadgeLhp.Size = new Size(28, 28);
            pnlBadgeLhp.TabIndex = 0;
            // 
            // lblIconLhp
            // 
            lblIconLhp.Dock = DockStyle.Fill;
            lblIconLhp.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
            lblIconLhp.ForeColor = Color.FromArgb(171, 51, 133);
            lblIconLhp.Location = new Point(0, 0);
            lblIconLhp.Name = "lblIconLhp";
            lblIconLhp.Size = new Size(28, 28);
            lblIconLhp.TabIndex = 0;
            lblIconLhp.Text = "▦";
            lblIconLhp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNhomLhp
            // 
            lblNhomLhp.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNhomLhp.ForeColor = Color.FromArgb(171, 51, 133);
            lblNhomLhp.Location = new Point(54, 18);
            lblNhomLhp.Name = "lblNhomLhp";
            lblNhomLhp.Size = new Size(328, 25);
            lblNhomLhp.TabIndex = 1;
            lblNhomLhp.Text = "DANH SÁCH LỚP HỌC PHẦN";
            // 
            // lblTagLhp
            // 
            lblTagLhp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTagLhp.BackColor = Color.FromArgb(247, 237, 244);
            lblTagLhp.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTagLhp.ForeColor = Color.FromArgb(77, 18, 63);
            lblTagLhp.Location = new Point(1080, 17);
            lblTagLhp.Name = "lblTagLhp";
            lblTagLhp.Size = new Size(227, 27);
            lblTagLhp.TabIndex = 2;
            lblTagLhp.Text = "⌕  TÌM KIẾM NHANH";
            lblTagLhp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txttim
            // 
            txttim.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txttim.BackColor = Color.FromArgb(247, 237, 244);
            txttim.BorderStyle = BorderStyle.FixedSingle;
            txttim.Font = new Font("Segoe UI", 10F);
            txttim.ForeColor = Color.FromArgb(77, 18, 63);
            txttim.Location = new Point(18, 56);
            txttim.Name = "txttim";
            txttim.PlaceholderText = "Nhập mã lớp học phần, mã môn, tên môn hoặc giảng viên";
            txttim.Size = new Size(950, 30);
            txttim.TabIndex = 2;
            // 
            // btntim
            // 
            btntim.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btntim.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 31, 97);
            btntim.Location = new Point(985, 52);
            btntim.Name = "btntim";
            btntim.Size = new Size(140, 36);
            btntim.TabIndex = 3;
            btntim.Text = "⌕  Tìm kiếm";
            btntim.UseVisualStyleBackColor = false;
            // 
            // btnlammoi
            // 
            btnlammoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnlammoi.BackColor = Color.FromArgb(247, 237, 244);
            btnlammoi.Cursor = Cursors.Hand;
            btnlammoi.FlatAppearance.BorderColor = Color.FromArgb(171, 51, 133);
            btnlammoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 215, 232);
            btnlammoi.FlatStyle = FlatStyle.Flat;
            btnlammoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnlammoi.ForeColor = Color.FromArgb(77, 18, 63);
            btnlammoi.Location = new Point(1145, 52);
            btnlammoi.Name = "btnlammoi";
            btnlammoi.Size = new Size(151, 36);
            btnlammoi.TabIndex = 4;
            btnlammoi.Text = "↻  Làm mới";
            btnlammoi.UseVisualStyleBackColor = false;
            // 
            // dglhp
            // 
            dglhp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dglhp.BackgroundColor = Color.White;
            dglhp.ColumnHeadersHeightSizeMode =
     DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dglhp.ColumnHeadersHeight = 55;

            dglhp.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.True;

            dglhp.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dglhp.EnableHeadersVisualStyles = false;
            dglhp.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F7EDF4");
            dglhp.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#4D123F");
            dglhp.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new Padding(6, 2, 6, 2);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dglhp.DefaultCellStyle = dataGridViewCellStyle1;
            dglhp.Location = new Point(18, 104);
            dglhp.Name = "dglhp";
            dglhp.RowHeadersVisible = false;
            dglhp.RowHeadersWidth = 51;
            dglhp.Size = new Size(1278, 225);
            dglhp.TabIndex = 5;
            dglhp.DataBindingComplete += dglhp_DataBindingComplete;
            // 
            // pnlCardSv
            // 
            pnlCardSv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCardSv.BackColor = Color.White;
            pnlCardSv.Controls.Add(pnlBadgeSv);
            pnlCardSv.Controls.Add(lblNhomSv);
            pnlCardSv.Controls.Add(lblTagSv);
            pnlCardSv.Controls.Add(lblchon);
            pnlCardSv.Controls.Add(txttimsv);
            pnlCardSv.Controls.Add(btntimsv);
            pnlCardSv.Controls.Add(dgsv);
            pnlCardSv.Location = new Point(20, 502);
            pnlCardSv.Name = "pnlCardSv";
            pnlCardSv.Size = new Size(1314, 390);
            pnlCardSv.TabIndex = 2;
            // 
            // pnlBadgeSv
            // 
            pnlBadgeSv.BackColor = Color.FromArgb(247, 237, 244);
            pnlBadgeSv.Controls.Add(lblIconSv);
            pnlBadgeSv.Location = new Point(18, 16);
            pnlBadgeSv.Name = "pnlBadgeSv";
            pnlBadgeSv.Size = new Size(28, 28);
            pnlBadgeSv.TabIndex = 0;
            // 
            // lblIconSv
            // 
            lblIconSv.Dock = DockStyle.Fill;
            lblIconSv.Font = new Font("Segoe UI Symbol", 15F, FontStyle.Bold);
            lblIconSv.ForeColor = Color.FromArgb(171, 51, 133);
            lblIconSv.Location = new Point(0, 0);
            lblIconSv.Name = "lblIconSv";
            lblIconSv.Size = new Size(28, 28);
            lblIconSv.TabIndex = 0;
            lblIconSv.Text = "♙";
            lblIconSv.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNhomSv
            // 
            lblNhomSv.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNhomSv.ForeColor = Color.FromArgb(171, 51, 133);
            lblNhomSv.Location = new Point(54, 18);
            lblNhomSv.Name = "lblNhomSv";
            lblNhomSv.Size = new Size(450, 25);
            lblNhomSv.TabIndex = 1;
            lblNhomSv.Text = "DANH SÁCH SINH VIÊN";
            // 
            // lblTagSv
            // 
            lblTagSv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTagSv.BackColor = Color.FromArgb(247, 237, 244);
            lblTagSv.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTagSv.ForeColor = Color.FromArgb(77, 18, 63);
            lblTagSv.Location = new Point(1030, 17);
            lblTagSv.Name = "lblTagSv";
            lblTagSv.Size = new Size(277, 27);
            lblTagSv.TabIndex = 2;
            lblTagSv.Text = "♙  SINH VIÊN THEO LỚP";
            lblTagSv.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblchon
            // 
            lblchon.ForeColor = Color.FromArgb(77, 18, 63);
            lblchon.Location = new Point(18, 48);
            lblchon.Name = "lblchon";
            lblchon.Size = new Size(800, 25);
            lblchon.TabIndex = 2;
            lblchon.Text = "Chọn một lớp học phần ở bảng trên để xem sinh viên.";
            // 
            // txttimsv
            // 
            txttimsv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txttimsv.BackColor = Color.FromArgb(247, 237, 244);
            txttimsv.BorderStyle = BorderStyle.FixedSingle;
            txttimsv.ForeColor = Color.FromArgb(77, 18, 63);
            txttimsv.Location = new Point(18, 80);
            txttimsv.Name = "txttimsv";
            txttimsv.PlaceholderText = "Nhập mã hoặc họ tên sinh viên trong lớp học phần";
            txttimsv.Size = new Size(1000, 30);
            txttimsv.TabIndex = 3;
            // 
            // btntimsv
            // 
            btntimsv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btntimsv.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 31, 97);
            btntimsv.Location = new Point(1035, 78);
            btntimsv.Name = "btntimsv";
            btntimsv.Size = new Size(261, 36);
            btntimsv.TabIndex = 4;
            btntimsv.Text = "⌕  Tìm sinh viên";
            btntimsv.UseVisualStyleBackColor = false;
            // 
            // dgsv
            // 
            dgsv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgsv.BackgroundColor = Color.White;
            dgsv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dgsv.ColumnHeadersHeight = 55;
            dgsv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgsv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgsv.EnableHeadersVisualStyles = false;
            dgsv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F7EDF4");
            dgsv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#4D123F");
            dgsv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new Padding(6, 2, 6, 2);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgsv.DefaultCellStyle = dataGridViewCellStyle2;
            dgsv.Location = new Point(18, 128);
            dgsv.Name = "dgsv";
            dgsv.RowHeadersVisible = false;
            dgsv.RowHeadersWidth = 51;
            dgsv.Size = new Size(1278, 240);
            dgsv.TabIndex = 5;
            dgsv.DataBindingComplete += dgsv_DataBindingComplete;
            // 
            // ucLopHocPhan
            // 
            AutoScroll = true;
            BackColor = Color.FromArgb(247, 237, 244);
            Controls.Add(pnlBanner);
            Controls.Add(pnlCardLhp);
            Controls.Add(pnlCardSv);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(700, 600);
            Name = "ucLopHocPhan";
            Size = new Size(1354, 1068);
            pnlBanner.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlCardLhp.ResumeLayout(false);
            pnlCardLhp.PerformLayout();
            pnlBadgeLhp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dglhp).EndInit();
            pnlCardSv.ResumeLayout(false);
            pnlCardSv.PerformLayout();
            pnlBadgeSv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgsv).EndInit();
            ResumeLayout(false);
        }

        private static void TaoNut(Button nut, string nen, Color chu)
        {
            nut.FlatStyle = FlatStyle.Flat;
            nut.FlatAppearance.BorderSize = 0;
            nut.BackColor = ColorTranslator.FromHtml(nen);
            nut.ForeColor = chu;
            nut.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            nut.Cursor = Cursors.Hand;
        }

        private static void DatMauBang(DataGridView bang)
        {
            // Ép toàn bộ nền của DataGridView về trắng, kể cả vùng chưa có dòng.
            bang.BackgroundColor = Color.White;
            bang.BackColor = Color.White;
            bang.BorderStyle = BorderStyle.None;
            bang.EnableHeadersVisualStyles = false;
            bang.ColumnHeadersVisible = true;
            bang.ColumnHeadersHeight = 40;
            bang.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            bang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            bang.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#F7EDF4");
            bang.ColumnHeadersDefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#4D123F");
            bang.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9.5F, FontStyle.Bold);
            bang.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                ColorTranslator.FromHtml("#F7EDF4");

            bang.DefaultCellStyle.BackColor = Color.White;
            bang.DefaultCellStyle.ForeColor = Color.FromArgb(51, 48, 58);
            bang.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            bang.DefaultCellStyle.SelectionBackColor =
                ColorTranslator.FromHtml("#F0D7E8");
            bang.DefaultCellStyle.SelectionForeColor =
                ColorTranslator.FromHtml("#4D123F");
            bang.RowsDefaultCellStyle.BackColor = Color.White;
            bang.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#FCF8FB");
            bang.GridColor = ColorTranslator.FromHtml("#E8D8E4");
            bang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            bang.RowHeadersVisible = false;
            bang.AllowUserToAddRows = false;
            bang.AllowUserToResizeRows = false;
            bang.RowTemplate.Height = 36;
        }

        private static void TaoBang(DataGridView bang)
        {
            DatMauBang(bang);
            bang.ReadOnly = true;
            bang.AllowUserToDeleteRows = false;
            bang.MultiSelect = false;
            bang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Không ép AutoGenerateColumns ở đây để không phá cách nạp dữ liệu
            // nếu code xử lý phía sau đang tự tạo hoặc tự cấu hình các cột.
        }


        private static void ThemCot(
            DataGridView bang, string ten, string tieuDe)
        {
            bang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ten,
                DataPropertyName = ten,
                HeaderText = tieuDe,
                MinimumWidth = 55,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        // Đổi tiêu đề sau khi DataGridView đã tạo/nạp các cột.
        // Tìm theo Name hoặc DataPropertyName để dùng được cả cột tự sinh lẫn cột tạo thủ công.
        private static void DatTieuDeCot(DataGridView bang, string tenCot, string tieuDe)
        {
            foreach (DataGridViewColumn cot in bang.Columns)
            {
                if (string.Equals(cot.Name, tenCot, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(cot.DataPropertyName, tenCot, StringComparison.OrdinalIgnoreCase))
                {
                    cot.HeaderText = tieuDe;
                    return;
                }
            }
        }

        private void dglhp_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DatTieuDeCot(dglhp, "malophp", "Mã lớp học phần");
            DatTieuDeCot(dglhp, "mamon", "Mã môn");
            DatTieuDeCot(dglhp, "tenmon", "Tên môn");
            DatTieuDeCot(dglhp, "sotin", "Số tín chỉ");
            DatTieuDeCot(dglhp, "giangvien", "Giảng viên");
            DatTieuDeCot(dglhp, "siso", "Sĩ số");
            DatTieuDeCot(dglhp, "dakhoa", "Đã khóa");
            DatTieuDeCot(dglhp, "trangthai", "Trạng thái");
        }

        private void dgsv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DatTieuDeCot(dgsv, "stt", "STT");
            DatTieuDeCot(dgsv, "masv", "Mã sinh viên");
            DatTieuDeCot(dgsv, "tensv", "Họ và tên");
            DatTieuDeCot(dgsv, "ngaysinh", "Ngày sinh");
            DatTieuDeCot(dgsv, "malop", "Mã lớp");
            DatTieuDeCot(dgsv, "diemcc", "Điểm chuyên cần");
            DatTieuDeCot(dgsv, "diemgk", "Điểm giữa kỳ");
            DatTieuDeCot(dgsv, "diemck", "Điểm cuối kỳ");
            DatTieuDeCot(dgsv, "dakhoa", "Đã khóa");
            DatTieuDeCot(dgsv, "diemtongket", "Điểm tổng kết");
            DatTieuDeCot(dgsv, "tongket", "Điểm tổng kết");
            DatTieuDeCot(dgsv, "xeploai", "Xếp loại");
            DatTieuDeCot(dgsv, "ketqua", "Kết quả");
        }

        private Label lblIcon;
        private Label lblIconLhp;
        private Label lblIconSv;
    }
}



