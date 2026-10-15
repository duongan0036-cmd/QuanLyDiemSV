using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Database;

namespace UI.UserControls
{
    public partial class UcLopHocPhan : UserControl
    {
        private string maLopDangChon = "";
        private bool dangTaiLop = false;

        public UcLopHocPhan()
        {
            InitializeComponent();

            btntim.Click += btntim_click;
            btnlammoi.Click += btnlammoi_click;
            btntimsv.Click += btntimsv_click;
            dglhp.SelectionChanged += dglhp_chon;

            txttim.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    tailhp();
                }
            };
            txttimsv.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    taisv();
                }
            };

            // Tải dữ liệu ban đầu (không chạy trong chế độ Designer)
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                tailhp();
        }


        // Tải danh sách lớp học phần
        private void tailhp()
        {
            try
            {
                dangTaiLop = true;

                string tukhoa = (txttim.Text ?? "").Trim();

                string sql = @"
                    SELECT
                        l.malophp,
                        l.mamon,
                        m.tenmon,
                        m.sotin,
                        l.giangvien,
                        COUNT(d.masv) AS siso,
                        ISNULL(MIN(CAST(d.dakhoa AS int)), 0) AS dakhoa
                    FROM lophocphan l
                    INNER JOIN monhoc m ON m.mamon = l.mamon
                    LEFT JOIN diem d ON d.malophp = l.malophp
                    WHERE
                        l.malophp LIKE @k
                        OR l.mamon LIKE @k
                        OR m.tenmon LIKE @k
                        OR l.giangvien LIKE @k
                    GROUP BY
                        l.malophp,
                        l.mamon,
                        m.tenmon,
                        m.sotin,
                        l.giangvien
                    ORDER BY l.malophp";

                DataTable dt = DbHelper.ExecuteQuery(
                    sql,
                    new SqlParameter("@k", "%" + tukhoa + "%"));

                dglhp.DataSource = null;
                dglhp.DataSource = dt;

                maLopDangChon = "";

                if (dglhp.Rows.Count > 0)
                {
                    dglhp.ClearSelection();
                    dglhp.CurrentCell = dglhp.Rows[0].Cells[0];
                    dglhp.Rows[0].Selected = true;

                    maLopDangChon = Convert.ToString(
                        dglhp.Rows[0].Cells["malophp"].Value) ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách lớp học phần: " + ex.Message,
                    "Thông báo lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                dangTaiLop = false;
            }

            taisv();
        }

        private void taisv()
        {
            try
            {
                if (string.IsNullOrEmpty(maLopDangChon))
                {
                    lblchon.Text = "Chọn một lớp học phần ở bảng trên để xem sinh viên.";
                    dgsv.DataSource = null;
                    return;
                }

                string tukhoa = (txttimsv.Text ?? "").Trim();

                string sql = @"
                    SELECT
                        ROW_NUMBER() OVER (ORDER BY sv.masv) AS stt,
                        sv.masv,
                        sv.tensv,
                        sv.ngaysinh,
                        sv.malp,
                        d.diemcc,
                        d.diemgk,
                        d.diemck,
                        d.dakhoa
                    FROM diem d
                    INNER JOIN sinhvien sv ON sv.masv = d.masv
                    WHERE
                        d.malophp = @l
                        AND (sv.masv LIKE @k OR sv.tensv LIKE @k)
                    ORDER BY sv.masv";

                DataTable dt = DbHelper.ExecuteQuery(
                    sql,
                    new SqlParameter("@l", maLopDangChon),
                    new SqlParameter("@k", "%" + tukhoa + "%"));

                dgsv.DataSource = null;
                dgsv.DataSource = dt;

                lblchon.Text = "SINH VIÊN TRONG LỚP HỌC PHẦN " + maLopDangChon;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách sinh viên: " + ex.Message,
                    "Thông báo lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dglhp_chon(object sender, EventArgs e)
        {
            if (dangTaiLop)
                return;

            if (dglhp.CurrentRow == null || dglhp.CurrentRow.IsNewRow)
            {
                maLopDangChon = "";
                taisv();
                return;
            }

            object giaTri = dglhp.CurrentRow.Cells["malophp"].Value;

            maLopDangChon =
                giaTri == null || giaTri == DBNull.Value
                    ? ""
                    : Convert.ToString(giaTri);

            taisv();
        }

        // Tìm lớp học phần
        private void btntim_click(object sender, EventArgs e)
        {
            tailhp();
        }

        private void btnlammoi_click(object sender, EventArgs e)
        {
            txttim.Clear();
            txttimsv.Clear();
            tailhp();
        }

        private void btntimsv_click(object sender, EventArgs e)
        {
            taisv();
        }
    }
}