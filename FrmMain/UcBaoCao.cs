using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Database;
using Guna.UI2.WinForms;

namespace UI.UserControls
{
    public class UcBaoCao : UserControl
    {
        // ===== Màu chủ đạo =====
        private static readonly Color MauChinh = Color.FromArgb(94, 22, 110);
        private static readonly Color MauNhat = Color.FromArgb(243, 229, 250);
        private static readonly Color MauNen = Color.FromArgb(250, 243, 253);
        private static readonly Color MauChu = Color.FromArgb(60, 20, 70);

        // ===== Các control trên màn hình =====
        private ComboBox cboLopHP;
        private Label lblTieuDeBaoCao;
        private DataGridView dgv;
        private Guna2Button btnBangDiem, btnThongKe, btnTinhDiem, btnXuat;

        private Panel pnlBieuDo;                           // vùng chứa 2 biểu đồ (chỉ hiện khi xem thống kê)
        private ScottPlot.WinForms.FormsPlot plotCot;      // biểu đồ cột
        private ScottPlot.WinForms.FormsPlot plotTron;     // biểu đồ tròn
        private Label lblTieuDeTron;

        // Mặc định mở ra là xem thống kê (có biểu đồ)
        // false = bảng điểm, true = thống kê
        private bool xemThongKe = true;

        // Một lớp học phần trong combobox
        private class LopHP
        {
            public string Ma;
            public string TenMon;
            public string GiangVien;

            public override string ToString()
            {
                return Ma + " - " + TenMon + " (" + GiangVien + ")";
            }
        }

        // =====================================================================
        // 1. KHỞI TẠO GIAO DIỆN
        // =====================================================================
        public UcBaoCao()
        {
            TaoGiaoDien();
            Load += UcBaoCao_Load;

            // Vẽ lại khi control được hiển thị thật sự (đảm bảo biểu đồ có kích thước đúng)
            VisibleChanged += (s, e) =>
            {
                if (Visible && xemThongKe && LopDangChon() != null)
                    LamMoi();
            };
        }

        private void UcBaoCao_Load(object sender, EventArgs e)
        {
            NapDanhSachLop();
        }

        private void TaoGiaoDien()
        {
            BackColor = MauNen;
            Padding = new Padding(24, 16, 24, 20);
            Font = new Font("Segoe UI", 9.5f);

            // Tiêu đề trang
            var lblTieuDe = new Label();
            lblTieuDe.Text = "Tổng hợp & Báo cáo";
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 44;
            lblTieuDe.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
            lblTieuDe.ForeColor = MauChinh;
            lblTieuDe.TextAlign = ContentAlignment.MiddleLeft;

            // Thẻ trắng bo góc chứa mọi thứ
            var card = new Guna2Panel();
            card.Dock = DockStyle.Fill;
            card.FillColor = Color.White;
            card.BorderRadius = 14;
            card.Padding = new Padding(16);

            // Hàng trên: chọn lớp + các nút
            var pnlTren = new FlowLayoutPanel();
            pnlTren.Dock = DockStyle.Top;
            pnlTren.Height = 52;
            pnlTren.WrapContents = false;
            pnlTren.BackColor = Color.White;

            var lblLop = new Label();
            lblLop.Text = "Lớp học phần:";
            lblLop.Size = new Size(105, 40);
            lblLop.TextAlign = ContentAlignment.MiddleLeft;
            lblLop.ForeColor = MauChu;

            cboLopHP = new ComboBox();
            cboLopHP.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLopHP.Width = 330;
            cboLopHP.Margin = new Padding(3, 8, 12, 3);

            btnBangDiem = TaoNut("Bảng điểm", 105, MauChinh, Color.White);
            btnThongKe = TaoNut("Thống kê", 105, MauNhat, MauChinh);
            btnTinhDiem = TaoNut("Xếp loại", 160, Color.FromArgb(52, 73, 170), Color.White);
            btnXuat = TaoNut("Xuất CSV", 105, Color.FromArgb(39, 174, 96), Color.White);

            btnBangDiem.Click += (s, e) => { xemThongKe = false; DoiMauNut(); LamMoi(); };
            btnThongKe.Click += (s, e) => { xemThongKe = true; DoiMauNut(); LamMoi(); };
            btnTinhDiem.Click += BtnTinhDiem_Click;
            btnXuat.Click += BtnXuat_Click;

            pnlTren.Controls.Add(lblLop);
            pnlTren.Controls.Add(cboLopHP);
            pnlTren.Controls.Add(btnThongKe);
            pnlTren.Controls.Add(btnBangDiem);
            
            pnlTren.Controls.Add(btnTinhDiem);
            pnlTren.Controls.Add(btnXuat);

            // Tiêu đề của báo cáo đang xem
            lblTieuDeBaoCao = new Label();
            lblTieuDeBaoCao.Dock = DockStyle.Top;
            lblTieuDeBaoCao.Height = 38;
            lblTieuDeBaoCao.Font = new Font("Segoe UI Semibold", 12f);
            lblTieuDeBaoCao.ForeColor = MauChinh;
            lblTieuDeBaoCao.TextAlign = ContentAlignment.MiddleLeft;
            lblTieuDeBaoCao.BackColor = Color.White;

            // Vùng nội dung: bảng ở giữa, biểu đồ bên phải
            var pnlNoiDung = new Panel();
            pnlNoiDung.Dock = DockStyle.Fill;
            pnlNoiDung.BackColor = Color.White;

            dgv = TaoBang();
            pnlBieuDo = TaoVungBieuDo();

            pnlNoiDung.Controls.Add(dgv);         // Fill: thêm trước
            pnlNoiDung.Controls.Add(pnlBieuDo);   // Right: thêm sau

            // Thứ tự thêm: Fill trước, các Top thêm sau (cái nằm trên cùng thêm cuối)
            card.Controls.Add(pnlNoiDung);
            card.Controls.Add(lblTieuDeBaoCao);
            card.Controls.Add(pnlTren);

            Controls.Add(card);
            Controls.Add(lblTieuDe);
        }

        private Guna2Button TaoNut(string chu, int rong, Color nen, Color mauChu)
        {
            var b = new Guna2Button();
            b.Text = chu;
            b.Size = new Size(rong, 40);
            b.BorderRadius = 8;
            b.FillColor = nen;
            b.ForeColor = mauChu;
            b.Font = new Font("Segoe UI Semibold", 9f);
            b.Cursor = Cursors.Hand;
            b.Margin = new Padding(0, 6, 8, 0);
            return b;
        }

        // Nút đang xem thì tô đậm
        private void DoiMauNut()
        {
            btnBangDiem.FillColor = xemThongKe ? MauNhat : MauChinh;
            btnBangDiem.ForeColor = xemThongKe ? MauChinh : Color.White;
            btnThongKe.FillColor = xemThongKe ? MauChinh : MauNhat;
            btnThongKe.ForeColor = xemThongKe ? Color.White : MauChinh;
        }

        private DataGridView TaoBang()
        {
            var b = new DataGridView();
            b.Dock = DockStyle.Fill;
            b.ReadOnly = true;
            b.AllowUserToAddRows = false;
            b.AllowUserToResizeRows = false;
            b.RowHeadersVisible = false;
            b.MultiSelect = false;
            b.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            b.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            b.BackgroundColor = Color.White;
            b.BorderStyle = BorderStyle.None;
            b.GridColor = Color.FromArgb(236, 222, 243);
            b.RowTemplate.Height = 34;

            // Tiêu đề cột
            b.EnableHeadersVisualStyles = false;
            b.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            b.ColumnHeadersHeight = 36;
            b.ColumnHeadersDefaultCellStyle.BackColor = MauChinh;
            b.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            b.ColumnHeadersDefaultCellStyle.SelectionBackColor = MauChinh;
            b.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ô dữ liệu
            b.DefaultCellStyle.ForeColor = MauChu;
            b.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 214, 247);
            b.DefaultCellStyle.SelectionForeColor = MauChu;
            b.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            b.DefaultCellStyle.NullValue = "—";    // ô trống hiện dấu —
            b.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 242, 252);
            return b;
        }

        // Vùng bên phải gồm 2 biểu đồ xếp chồng: cột (trên), tròn (dưới)
        private Panel TaoVungBieuDo()
        {
            var pnl = new Panel();
            pnl.Dock = DockStyle.Right;
            pnl.Width = 460;
            pnl.Padding = new Padding(12, 0, 0, 0);
            pnl.BackColor = Color.White;
            pnl.Visible = false;

            var bang = new TableLayoutPanel();
            bang.Dock = DockStyle.Fill;
            bang.RowCount = 2;
            bang.ColumnCount = 1;
            bang.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            bang.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            plotCot = new ScottPlot.WinForms.FormsPlot();
            plotTron = new ScottPlot.WinForms.FormsPlot();
            lblTieuDeTron = new Label();

            bang.Controls.Add(TaoTheBieuDo("Thống kê kết quả học tập", plotCot, null), 0, 0);
            bang.Controls.Add(TaoTheBieuDo("Xếp loại học lực", plotTron, lblTieuDeTron), 0, 1);

            pnl.Controls.Add(bang);
            return pnl;
        }

        // Một thẻ biểu đồ: tiêu đề ở trên, biểu đồ bên dưới
        private Guna2Panel TaoTheBieuDo(string tieuDe, Control bieuDo, Label nhan)
        {
            var the = new Guna2Panel();
            the.Dock = DockStyle.Fill;
            the.FillColor = Color.White;
            the.BorderColor = MauNhat;
            the.BorderThickness = 1;
            the.BorderRadius = 10;
            the.Padding = new Padding(12);
            the.Margin = new Padding(0, 0, 0, 8);

            if (nhan == null) nhan = new Label();
            nhan.Text = tieuDe;
            nhan.Dock = DockStyle.Top;
            nhan.Height = 28;
            nhan.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            nhan.ForeColor = MauChu;
            nhan.TextAlign = ContentAlignment.MiddleLeft;

            bieuDo.Dock = DockStyle.Fill;
            the.Controls.Add(bieuDo);   // Fill thêm trước
            the.Controls.Add(nhan);     // Top thêm sau
            return the;
        }

        // =====================================================================
        // 2. NẠP DANH SÁCH LỚP HỌC PHẦN
        // =====================================================================
        private void NapDanhSachLop()
        {
            try
            {
                string sql = @"SELECT l.malophp, m.tenmon, l.giangvien
                               FROM lophocphan l
                               JOIN monhoc m ON m.mamon = l.mamon
                               ORDER BY l.malophp";

                using (var conn = new SqlConnection(DbHelper.ConnStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var lop = new LopHP();
                            lop.Ma = rd["malophp"].ToString();
                            lop.TenMon = rd["tenmon"].ToString();
                            lop.GiangVien = rd["giangvien"].ToString();
                            cboLopHP.Items.Add(lop);
                        }
                    }
                }

                // Gắn sự kiện sau khi nạp xong, rồi chọn lớp đầu tiên
                cboLopHP.SelectedIndexChanged += (s, e) => LamMoi();
                DoiMauNut();                          // đồng bộ màu nút với chế độ mặc định
                if (cboLopHP.Items.Count > 0)
                    cboLopHP.SelectedIndex = 0;       // kích hoạt LamMoi() -> HienThongKe()
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp học phần: " + ex.Message);
            }
        }

        private LopHP LopDangChon()
        {
            return cboLopHP.SelectedItem as LopHP;
        }

        private void LamMoi()
        {
            if (xemThongKe)
                HienThongKe();
            else
                HienBangDiem();
        }

        // Xóa bảng và tạo các cột mới
        private void TaoCot(params string[] tieuDe)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            foreach (string t in tieuDe)
            {
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = t });
            }
        }

        // Định dạng điểm: 2 chữ số thập phân, null nếu không có điểm
        private static string DinhDangDiem(object giaTri)
        {
            if (giaTri == DBNull.Value) return null;
            return Convert.ToDecimal(giaTri).ToString("0.00");
        }

        // Xếp loại theo điểm tổng kết (giống FrmNhapDiem)
        private static string XepLoai(decimal diem)
        {
            if (diem >= 8.5m) return "Giỏi";
            if (diem >= 7m) return "Khá";
            if (diem >= 5m) return "Trung bình";
            return "Yếu";
        }

        // =====================================================================
        // 3. BẢNG ĐIỂM
        // =====================================================================
        private void HienBangDiem()
        {
            var lop = LopDangChon();
            if (lop == null) return;

            pnlBieuDo.Visible = false;
            lblTieuDeBaoCao.Text = "BẢNG ĐIỂM CHI TIẾT: " + lop;
            TaoCot("STT", "Mã SV", "Họ tên", "Lớp SH",
                   "Chuyên cần (10%)", "Giữa kỳ (30%)", "Cuối kỳ (60%)",
                   "Tổng kết", "Xếp loại", "Kết quả");

            try
            {
                string sql = @"SELECT sv.masv, sv.tensv, sv.malp,
                                      d.diemcc, d.diemgk, d.diemck, d.diemtongket, d.xeploai
                               FROM diem d
                               JOIN sinhvien sv ON sv.masv = d.masv
                               WHERE d.malophp = @MaLopHP
                               ORDER BY sv.masv";

                using (var conn = new SqlConnection(DbHelper.ConnStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLopHP", lop.Ma);
                    conn.Open();

                    using (var rd = cmd.ExecuteReader())
                    {
                        int stt = 0;
                        while (rd.Read())
                        {
                            stt++;

                            // Kết quả: Đạt nếu tổng kết >= 5
                            string ketQua;
                            if (rd["diemtongket"] == DBNull.Value)
                                ketQua = "Chưa tổng kết";
                            else if (Convert.ToDecimal(rd["diemtongket"]) >= 5m)
                                ketQua = "Đạt";
                            else
                                ketQua = "Không đạt";

                            dgv.Rows.Add(
                                stt,
                                rd["masv"],
                                rd["tensv"],
                                rd["malp"],
                                DinhDangDiem(rd["diemcc"]),
                                DinhDangDiem(rd["diemgk"]),
                                DinhDangDiem(rd["diemck"]),
                                DinhDangDiem(rd["diemtongket"]),
                                rd["xeploai"] == DBNull.Value ? null : rd["xeploai"],
                                ketQua);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bảng điểm: " + ex.Message);
            }
        }

        // =====================================================================
        // 4. THỐNG KÊ + BIỂU ĐỒ
        // =====================================================================
        private void HienThongKe()
        {
            var lop = LopDangChon();
            if (lop == null) return;

            lblTieuDeBaoCao.Text = "BÁO CÁO THỐNG KÊ: " + lop;
            TaoCot("Chỉ tiêu", "Giá trị", "Tỷ lệ");

            try
            {
                // Các biến đếm
                int tongSV = 0;     // tổng số sinh viên
                int coDiem = 0;     // số sinh viên đã có điểm tổng kết
                int dat = 0;        // số sinh viên đạt (>= 5)
                int gioi = 0, kha = 0, trungBinh = 0, yeu = 0;
                decimal tongDiem = 0, diemMax = 0, diemMin = 10;

                string sql = "SELECT diemtongket FROM diem WHERE malophp = @MaLopHP";
                using (var conn = new SqlConnection(DbHelper.ConnStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLopHP", lop.Ma);
                    conn.Open();

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            tongSV++;
                            if (rd["diemtongket"] == DBNull.Value) continue;   // chưa có điểm

                            decimal diem = Convert.ToDecimal(rd["diemtongket"]);
                            coDiem++;
                            tongDiem += diem;
                            if (diem > diemMax) diemMax = diem;
                            if (diem < diemMin) diemMin = diem;
                            if (diem >= 5m) dat++;

                            string xl = XepLoai(diem);
                            if (xl == "Giỏi") gioi++;
                            else if (xl == "Khá") kha++;
                            else if (xl == "Trung bình") trungBinh++;
                            else yeu++;
                        }
                    }
                }

                // Đổ kết quả vào bảng
                dgv.Rows.Add("Tổng số sinh viên", tongSV, "");
                dgv.Rows.Add("Đã có điểm tổng kết", coDiem, TyLe(coDiem, tongSV));
                dgv.Rows.Add("Chưa có điểm", tongSV - coDiem, TyLe(tongSV - coDiem, tongSV));
                dgv.Rows.Add("Đạt (>= 5.0)", dat, TyLe(dat, coDiem));
                dgv.Rows.Add("Không đạt (< 5.0)", coDiem - dat, TyLe(coDiem - dat, coDiem));
                dgv.Rows.Add("Xếp loại Giỏi (>= 8.5)", gioi, TyLe(gioi, coDiem));
                dgv.Rows.Add("Xếp loại Khá (7.0 - 8.4)", kha, TyLe(kha, coDiem));
                dgv.Rows.Add("Xếp loại Trung bình (5.0 - 6.9)", trungBinh, TyLe(trungBinh, coDiem));
                dgv.Rows.Add("Xếp loại Yếu (< 5.0)", yeu, TyLe(yeu, coDiem));

                if (coDiem > 0)
                {
                    dgv.Rows.Add("Điểm trung bình", Math.Round(tongDiem / coDiem, 2).ToString("0.00"), "");
                    dgv.Rows.Add("Điểm cao nhất", diemMax.ToString("0.00"), "");
                    dgv.Rows.Add("Điểm thấp nhất", diemMin.ToString("0.00"), "");
                }

                // Vẽ biểu đồ từ số liệu xếp loại
                string[] ten = { "Giỏi", "Khá", "Trung bình", "Yếu" };
                int[] soLuong = { gioi, kha, trungBinh, yeu };
                pnlBieuDo.Visible = true;
                VeBieuDo(ten, soLuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê: " + ex.Message);
            }
        }

        private static string TyLe(int n, int tong)
        {
            if (tong == 0) return "";
            return (100.0 * n / tong).ToString("0.0") + "%";
        }

        // Vẽ biểu đồ cột và biểu đồ tròn từ mảng tên + mảng số lượng
        private void VeBieuDo(string[] ten, int[] soLuong)
        {
            string[] mau = { "#B5307F", "#D43F9A", "#8E3A8E", "#5B1257" };   // màu từng xếp loại
            int tong = 0;
            foreach (int n in soLuong) tong += n;

            // ----- Biểu đồ cột -----
            plotCot.Plot.Clear();

            var cot = new ScottPlot.Bar[ten.Length];
            var nhan = new ScottPlot.Tick[ten.Length];
            for (int i = 0; i < ten.Length; i++)
            {
                cot[i] = new ScottPlot.Bar
                {
                    Position = i,
                    Value = soLuong[i],
                    Label = soLuong[i] > 0 ? soLuong[i].ToString() : "",
                    FillColor = ScottPlot.Color.FromHex("#A62A7E"),
                    Size = 0.5
                };
                nhan[i] = new ScottPlot.Tick(i, ten[i]);
            }

            var bars = plotCot.Plot.Add.Bars(cot);
            bars.ValueLabelStyle.Bold = true;
            bars.ValueLabelStyle.FontSize = 14;

            plotCot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(nhan);
            plotCot.Plot.Axes.Bottom.TickLabelStyle.FontSize = 11;
            plotCot.Plot.Grid.XAxisStyle.IsVisible = false;
            plotCot.Plot.Axes.Margins(0.05, 0.05, 0, 0.2);
            plotCot.Plot.Axes.AutoScale();
            plotCot.Refresh();

            // ----- Biểu đồ tròn (donut) -----
            plotTron.Plot.Clear();
            lblTieuDeTron.Text = "Xếp loại học lực (Tổng: " + tong + " SV)";

            var lat = new List<ScottPlot.PieSlice>();
            for (int i = 0; i < ten.Length; i++)
            {
                if (soLuong[i] == 0) continue;   // loại không có sinh viên thì bỏ qua

                lat.Add(new ScottPlot.PieSlice
                {
                    Value = soLuong[i],
                    Label = (100.0 * soLuong[i] / tong).ToString("0.0") + "%",
                    LegendText = ten[i],
                    LabelFontSize = 12,
                    LabelBold = true,
                    LabelFontColor = ScottPlot.Colors.White,
                    FillColor = ScottPlot.Color.FromHex(mau[i])
                });
            }

            if (lat.Count > 0)
            {
                var pie = plotTron.Plot.Add.Pie(lat);
                pie.DonutFraction = 0.6;
                pie.SliceLabelDistance = 0.8;
                plotTron.Plot.Layout.Frameless();
                plotTron.Plot.Axes.Frameless();
                plotTron.Plot.HideGrid();
                plotTron.Plot.ShowLegend(ScottPlot.Alignment.MiddleRight);
            }
            plotTron.Refresh();
        }

        // =====================================================================
        // 5. TÍNH ĐIỂM, XẾP LOẠI - chỉ một câu SQL
        //    Tổng kết = 10% chuyên cần + 30% giữa kỳ + 60% cuối kỳ (làm tròn 2 số)
        //    Chỉ tính cho sinh viên đủ 3 cột điểm và điểm chưa bị khóa
        // =====================================================================
        private void BtnTinhDiem_Click(object sender, EventArgs e)
        {
            var lop = LopDangChon();
            if (lop == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần.");
                return;
            }

            string sql = @"UPDATE diem
                           SET diemtongket = ROUND(diemcc * 0.2 + diemgk * 0.3 + diemck * 0.5, 2),
                               xeploai = CASE
                                   WHEN ROUND(diemcc * 0.2 + diemgk * 0.3 + diemck * 0.5, 2) >= 8.5 THEN N'Xuất Sắc'
                                   WHEN ROUND(diemcc * 0.2 + diemgk * 0.3 + diemck * 0.5, 2) >= 8.5 THEN N'Giỏi'
                                   WHEN ROUND(diemcc * 0.2 + diemgk * 0.3 + diemck * 0.5, 2) >= 7   THEN N'Khá'
                                   WHEN ROUND(diemcc * 0.2 + diemgk * 0.3 + diemck * 0.5, 2) >= 5   THEN N'Trung bình'
                                   ELSE N'Yếu'
                               END
                           WHERE malophp = @MaLopHP
                             AND diemcc IS NOT NULL AND diemgk IS NOT NULL AND diemck IS NOT NULL
                             AND (dakhoa IS NULL OR dakhoa = 0)";

            try
            {
                int soDong;
                using (var conn = new SqlConnection(DbHelper.ConnStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLopHP", lop.Ma);
                    conn.Open();
                    soDong = cmd.ExecuteNonQuery();   // số dòng đã cập nhật
                }

                xemThongKe = false;
                DoiMauNut();
                HienBangDiem();
                MessageBox.Show("Đã tính điểm cho " + soDong + " sinh viên.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính điểm: " + ex.Message);
            }
        }

        // =====================================================================
        // 6. XUẤT CSV (mở được bằng Excel)
        // =====================================================================
        private void BtnXuat_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.");
                return;
            }

            var hop = new SaveFileDialog();
            hop.Filter = "CSV (*.csv)|*.csv";
            hop.FileName = "baocao.csv";
            if (hop.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine(lblTieuDeBaoCao.Text);

                // Dòng tiêu đề cột
                foreach (DataGridViewColumn cot in dgv.Columns)
                    sb.Append(Chuoi(cot.HeaderText) + ",");
                sb.AppendLine();

                // Các dòng dữ liệu
                foreach (DataGridViewRow dong in dgv.Rows)
                {
                    foreach (DataGridViewCell o in dong.Cells)
                        sb.Append(Chuoi(Convert.ToString(o.FormattedValue)) + ",");
                    sb.AppendLine();
                }

                // UTF-8 có BOM để Excel đọc đúng tiếng Việt
                File.WriteAllText(hop.FileName, sb.ToString(), new UTF8Encoding(true));
                MessageBox.Show("Xuất file thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message);
            }
        }

        // Bọc chuỗi trong dấu ngoặc kép cho CSV
        private static string Chuoi(string s)
        {
            return "\"" + (s ?? "").Replace("\"", "\"\"") + "\"";
        }
    }
}