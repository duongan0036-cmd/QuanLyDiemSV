using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Database;

namespace UI.UserControls
{
    /// <summary>
    /// Form khóa điểm: danh sách sinh viên có ô tích chọn để khóa (hoặc mở khóa) điểm.
    /// ShowDialog() trả về DialogResult.OK nếu đã cập nhật thành công.
    /// Phần khai báo form nằm trong FrmKhoaDiem.Designer.cs (partial class).
    /// </summary>
    public partial class FrmKhoaDiem : Form
    {
        // Chế độ khóa: lấy SV chưa khóa và đã có ít nhất 1 cột điểm.
        // Chế độ mở khóa: lấy SV đã khóa.
        private const string Sql = @"
SELECT  CAST(0 AS bit)  AS Chon,
        d.masv          AS MaSV,
        sv.tensv        AS HoTen,
        d.malophp       AS MaLopHP,
        mh.tenmon       AS MonHoc,
        d.diemcc        AS ChuyenCan,
        d.diemgk        AS GiuaKy,
        d.diemck        AS CuoiKy,
        d.xeploai       AS XepLoai
FROM    diem d
JOIN    sinhvien   sv ON sv.masv    = d.masv
JOIN    lophocphan lp ON lp.malophp = d.malophp
JOIN    monhoc     mh ON mh.mamon   = lp.mamon
WHERE   ISNULL(d.dakhoa, 0) = @DaKhoa
  AND  (@DaKhoa = 1 OR d.diemcc IS NOT NULL OR d.diemgk IS NOT NULL OR d.diemck IS NOT NULL)
ORDER BY d.malophp, d.masv";

        // ====== MÀU SẮC ======
        private static readonly Color MauChinh = Color.FromArgb(94, 22, 110);
        private static readonly Color MauNhat = Color.FromArgb(243, 229, 250);
        private static readonly Color MauNen = Color.FromArgb(250, 243, 253);
        private static readonly Color MauChu = Color.FromArgb(60, 20, 70);
        private static readonly Color MauVien = Color.FromArgb(225, 205, 235);
        private static readonly Color MauDo = Color.FromArgb(192, 57, 43);
        private static readonly Color MauXanh = Color.FromArgb(39, 174, 96);

        // ====== CONTROL ======
        private Guna2TextBox txtTim;
        private Guna2ComboBox cboLop;
        private Guna2Button btnCheDoKhoa, btnCheDoMo, btnXacNhan, btnDong;
        private CheckBox chkTatCa;
        private Label lblHuongDan, lblDaChon;
        private DataGridView dgv;
        private DataTable _dt;

        private bool _cheDoKhoa = true;   // true = khóa điểm, false = mở khóa
        private bool _dangNap;            // chặn sự kiện khi đang nạp lại dữ liệu

        public FrmKhoaDiem()
        {
            InitializeComponent();
            XayDungGiaoDien();
            Load += (s, e) =>
            {
                if (DesignMode) return;
                TaiDuLieu();
            };
        }

        // =====================================================================
        //  DỰNG GIAO DIỆN
        // =====================================================================
        private void XayDungGiaoDien()
        {
            SuspendLayout();

            BackColor = MauNen;
            Padding = new Padding(16);
            Font = new Font("Segoe UI", 9.5f);

            var card = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                FillColor = Color.White,
                BorderRadius = 14,
                Padding = new Padding(16)
            };

            // ---- Hàng 1: chọn chế độ khóa / mở khóa ----
            var pnlCheDo = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 48,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.White
            };

            btnCheDoKhoa = TaoNut("Khóa điểm", 120, true);
            btnCheDoMo = TaoNut("Mở khóa", 120, false);
            btnCheDoKhoa.Click += (s, e) => { if (!_cheDoKhoa) DatCheDo(true, true); };
            btnCheDoMo.Click += (s, e) => { if (_cheDoKhoa) DatCheDo(false, true); };

            lblHuongDan = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(120, 100, 130),
                Margin = new Padding(8, 11, 0, 0)
            };

            pnlCheDo.Controls.Add(btnCheDoKhoa);
            pnlCheDo.Controls.Add(btnCheDoMo);
            pnlCheDo.Controls.Add(lblHuongDan);

            // ---- Hàng 2: ô tìm kiếm + lọc theo lớp học phần ----
            var pnlLoc = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(0, 0, 0, 10),
                BackColor = Color.White
            };

            txtTim = new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Tìm mã sinh viên, họ tên, môn học...",
                BorderRadius = 8,
                BorderColor = MauVien,
                FillColor = Color.White,
                ForeColor = MauChu,
                Font = new Font("Segoe UI", 9.5f)
            };
            txtTim.TextChanged += (s, e) => { if (!_dangNap) ApDungLoc(); };

            var spacer = new Panel { Dock = DockStyle.Right, Width = 10, BackColor = Color.White };

            cboLop = new Guna2ComboBox
            {
                Dock = DockStyle.Right,
                Width = 240,
                BorderRadius = 8,
                BorderColor = MauVien,
                FillColor = Color.White,
                ForeColor = MauChu,
                Font = new Font("Segoe UI", 9.5f),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLop.SelectedIndexChanged += (s, e) => { if (!_dangNap) ApDungLoc(); };

            // Fill thêm trước, các control Right thêm sau
            pnlLoc.Controls.Add(txtTim);
            pnlLoc.Controls.Add(spacer);
            pnlLoc.Controls.Add(cboLop);

            // ---- Hàng dưới cùng: chọn tất cả + số đã chọn + nút xác nhận ----
            var pnlDay = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 56,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = Color.White
            };

            var flowTrai = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                BackColor = Color.White
            };

            chkTatCa = new CheckBox
            {
                Text = "Chọn tất cả (đang hiển thị)",
                AutoSize = true,
                ForeColor = MauChu,
                BackColor = Color.White,
                Margin = new Padding(2, 10, 0, 0)
            };
            chkTatCa.CheckedChanged += ChkTatCa_CheckedChanged;

            lblDaChon = new Label
            {
                AutoSize = true,
                ForeColor = MauChinh,
                BackColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9.5f),
                Margin = new Padding(20, 10, 0, 0)
            };

            flowTrai.Controls.Add(chkTatCa);
            flowTrai.Controls.Add(lblDaChon);

            btnDong = TaoNut("Đóng", 110, false);
            btnDong.Dock = DockStyle.Right;
            btnDong.Margin = Padding.Empty;
            btnDong.Click += (s, e) => { DialogResult = DialogResult.Cancel; };

            var spacer2 = new Panel { Dock = DockStyle.Right, Width = 10, BackColor = Color.White };

            btnXacNhan = TaoNut("Khóa điểm đã chọn", 200, true);
            btnXacNhan.Dock = DockStyle.Right;
            btnXacNhan.Margin = Padding.Empty;
            btnXacNhan.Click += BtnXacNhan_Click;

            // Control thêm sau cùng nằm ngoài cùng bên phải
            pnlDay.Controls.Add(flowTrai);
            pnlDay.Controls.Add(btnDong);
            pnlDay.Controls.Add(spacer2);
            pnlDay.Controls.Add(btnXacNhan);

            // ---- Bảng dữ liệu ----
            dgv = TaoGrid();

            // Fill thêm trước, rồi Bottom, rồi các Top (cái thêm sau nằm trên cùng)
            card.Controls.Add(dgv);
            card.Controls.Add(pnlDay);
            card.Controls.Add(pnlLoc);
            card.Controls.Add(pnlCheDo);

            Controls.Add(card);

            DatCheDo(true, false);
            CapNhatDem();

            ResumeLayout(false);
        }

        private Guna2Button TaoNut(string text, int rong, bool chinh)
        {
            var b = new Guna2Button
            {
                Text = text,
                Size = new Size(rong, 38),
                BorderRadius = 8,
                FillColor = chinh ? MauChinh : MauNhat,
                ForeColor = chinh ? Color.White : MauChinh,
                Font = new Font("Segoe UI Semibold", 9.5f),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 8, 0)
            };
            b.DisabledState.FillColor = Color.FromArgb(215, 205, 220);
            b.DisabledState.ForeColor = Color.FromArgb(150, 140, 155);
            return b;
        }

        private DataGridView TaoGrid()
        {
            var g = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = false,                 // cần để tích được ô chọn; các cột khác tự đặt ReadOnly
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                GridColor = Color.FromArgb(236, 222, 243),
                RowTemplate = { Height = 40 },
                EnableHeadersVisualStyles = false,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 40,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var hs = g.ColumnHeadersDefaultCellStyle;
            hs.BackColor = MauChinh;
            hs.ForeColor = Color.White;
            hs.SelectionBackColor = MauChinh;
            hs.SelectionForeColor = Color.White;
            hs.Font = new Font("Segoe UI Semibold", 9f);
            hs.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var cs = g.DefaultCellStyle;
            cs.BackColor = Color.White;
            cs.ForeColor = MauChu;
            cs.Font = new Font("Segoe UI", 9f);
            cs.SelectionBackColor = Color.FromArgb(236, 214, 247);
            cs.SelectionForeColor = MauChu;
            cs.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cs.NullValue = "—";

            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 242, 252);
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = cs.SelectionBackColor;
            g.AlternatingRowsDefaultCellStyle.SelectionForeColor = cs.SelectionForeColor;

            // Cột tích chọn
            g.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colChon",
                DataPropertyName = "Chon",
                HeaderText = "Chọn",
                FillWeight = 45,
                MinimumWidth = 55,
                ReadOnly = false
            });

            g.Columns.Add(Cot("MaSV", "Mã SV", 70));
            g.Columns.Add(Cot("HoTen", "Họ tên", 130));
            g.Columns.Add(Cot("MaLopHP", "Mã lớp HP", 90));
            g.Columns.Add(Cot("MonHoc", "Môn học", 130));
            g.Columns.Add(Cot("ChuyenCan", "Chuyên cần", 80, "0.00"));
            g.Columns.Add(Cot("GiuaKy", "Giữa kỳ", 70, "0.00"));
            g.Columns.Add(Cot("CuoiKy", "Cuối kỳ", 70, "0.00"));
            g.Columns.Add(Cot("XepLoai", "Xếp loại", 80));

            // Tích xong là ghi nhận ngay (không cần rời khỏi ô)
            g.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (g.IsCurrentCellDirty)
                    g.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            g.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && g.Columns[e.ColumnIndex].Name == "colChon")
                    CapNhatDem();
            };

            // Bấm vào bất kỳ ô nào trên dòng cũng đảo trạng thái tích
            g.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (g.Columns[e.ColumnIndex].Name == "colChon") return;   // ô checkbox tự xử lý

                var o = g.Rows[e.RowIndex].Cells["colChon"];
                bool hienTai = o.Value != null && o.Value != DBNull.Value && Convert.ToBoolean(o.Value);
                o.Value = !hienTai;
                g.EndEdit();
                CapNhatDem();
            };

            return g;
        }

        private static DataGridViewTextBoxColumn Cot(string prop, string header, float weight, string format = null)
        {
            var c = new DataGridViewTextBoxColumn
            {
                Name = "col" + prop,
                DataPropertyName = prop,
                HeaderText = header,
                FillWeight = weight,
                MinimumWidth = 60,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            if (format != null) c.DefaultCellStyle.Format = format;
            return c;
        }

        // =====================================================================
        //  CHẾ ĐỘ KHÓA / MỞ KHÓA
        // =====================================================================
        private void DatCheDo(bool khoa, bool taiLai)
        {
            _cheDoKhoa = khoa;

            btnCheDoKhoa.FillColor = khoa ? MauChinh : MauNhat;
            btnCheDoKhoa.ForeColor = khoa ? Color.White : MauChinh;
            btnCheDoMo.FillColor = khoa ? MauNhat : MauChinh;
            btnCheDoMo.ForeColor = khoa ? MauChinh : Color.White;

            btnXacNhan.Text = khoa ? "Khóa điểm đã chọn" : "Mở khóa đã chọn";
            btnXacNhan.FillColor = khoa ? MauDo : MauXanh;

            lblHuongDan.Text = khoa
                ? "Tích chọn các sinh viên cần khóa điểm."
                : "Tích chọn các sinh viên cần mở khóa điểm.";

            if (taiLai) TaiDuLieu();
        }

        // =====================================================================
        //  DỮ LIỆU
        // =====================================================================
        private void TaiDuLieu()
        {
            _dangNap = true;
            try
            {
                string lopTruoc = cboLop.SelectedIndex > 0 ? cboLop.SelectedItem.ToString() : null;

                var dt = new DataTable();
                using (var conn = new SqlConnection(DbHelper.ConnStr))
                using (var da = new SqlDataAdapter(Sql, conn))
                {
                    da.SelectCommand.Parameters.Add("@DaKhoa", SqlDbType.Bit).Value = !_cheDoKhoa;
                    da.Fill(dt);
                }

                _dt = dt;
                dgv.DataSource = _dt;

                NapDanhSachLop(lopTruoc);
                chkTatCa.Checked = false;
                ApDungLoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc dữ liệu: " + ex.Message, "Khóa điểm",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dangNap = false;
            }
            CapNhatDem();
        }

        // Nạp các mã lớp học phần có trong dữ liệu vào ô lọc
        private void NapDanhSachLop(string chonLai)
        {
            var ds = new List<string>();
            foreach (DataRow r in _dt.Rows)
            {
                string ma = Convert.ToString(r["MaLopHP"]);
                if (!ds.Contains(ma)) ds.Add(ma);
            }
            ds.Sort();

            cboLop.Items.Clear();
            cboLop.Items.Add("Tất cả lớp học phần");
            foreach (string ma in ds) cboLop.Items.Add(ma);

            int idx = chonLai != null ? cboLop.Items.IndexOf(chonLai) : 0;
            cboLop.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void ApDungLoc()
        {
            if (_dt == null) return;

            var dieuKien = new List<string>();

            string kw = txtTim.Text.Trim();
            if (kw.Length > 0)
            {
                string s = kw.Replace("'", "''")
                             .Replace("[", "[[]")
                             .Replace("%", "[%]")
                             .Replace("*", "[*]");
                dieuKien.Add(string.Format(
                    "(MaSV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR MaLopHP LIKE '%{0}%' OR MonHoc LIKE '%{0}%')", s));
            }

            if (cboLop.SelectedIndex > 0)
            {
                string lop = cboLop.SelectedItem.ToString().Replace("'", "''");
                dieuKien.Add(string.Format("MaLopHP = '{0}'", lop));
            }

            _dt.DefaultView.RowFilter = string.Join(" AND ", dieuKien.ToArray());
            CapNhatDem();
        }

        // =====================================================================
        //  CHỌN / XÁC NHẬN
        // =====================================================================
        private void ChkTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (_dangNap || _dt == null) return;

            dgv.EndEdit();
            var view = _dt.DefaultView;
            for (int i = 0; i < view.Count; i++)
                view[i]["Chon"] = chkTatCa.Checked;

            dgv.Refresh();
            CapNhatDem();
        }

        private int DemDaChon()
        {
            int n = 0;
            if (_dt == null) return 0;
            foreach (DataRow r in _dt.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                if (r["Chon"] != DBNull.Value && (bool)r["Chon"]) n++;
            }
            return n;
        }

        private void CapNhatDem()
        {
            int daChon = DemDaChon();
            int hienThi = _dt == null ? 0 : _dt.DefaultView.Count;
            lblDaChon.Text = string.Format("Đã chọn: {0}   |   Đang hiển thị: {1}", daChon, hienThi);
            btnXacNhan.Enabled = daChon > 0;
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            dgv.EndEdit();

            var ds = new List<string[]>();
            foreach (DataRow r in _dt.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                if (r["Chon"] != DBNull.Value && (bool)r["Chon"])
                    ds.Add(new[] { Convert.ToString(r["MaSV"]), Convert.ToString(r["MaLopHP"]) });
            }

            if (ds.Count == 0)
            {
                MessageBox.Show("Vui lòng tích chọn ít nhất một sinh viên.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string hanhDong = _cheDoKhoa ? "khóa" : "mở khóa";
            var rs = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn {0} điểm của {1} sinh viên đã chọn?", hanhDong, ds.Count),
                "Xác nhận " + hanhDong + " điểm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (rs != DialogResult.Yes) return;

            try
            {
                int soDong = 0;
                using (var conn = new SqlConnection(DbHelper.ConnStr))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = new SqlCommand(
                                "UPDATE diem SET dakhoa = @DaKhoa WHERE masv = @MaSV AND malophp = @MaLopHP",
                                conn, tran))
                            {
                                cmd.Parameters.Add("@DaKhoa", SqlDbType.Bit).Value = _cheDoKhoa;
                                var pSV = cmd.Parameters.Add("@MaSV", SqlDbType.VarChar, 100);
                                var pLop = cmd.Parameters.Add("@MaLopHP", SqlDbType.VarChar, 100);

                                foreach (string[] m in ds)
                                {
                                    pSV.Value = m[0];
                                    pLop.Value = m[1];
                                    soDong += cmd.ExecuteNonQuery();
                                }
                            }
                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show(string.Format("Đã {0} điểm cho {1} sinh viên.", hanhDong, soDong), "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái khóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}