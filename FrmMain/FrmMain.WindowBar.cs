using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UI
{
    partial class FrmMain
    {
        private bool _windowMaximized;
        private System.Drawing.Rectangle _previousBounds;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void WindowBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, new IntPtr(0x2), IntPtr.Zero);
            }
        }

        private void WindowMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void WindowMaximize_Click(object sender, EventArgs e)
        {
            if (!_windowMaximized)
            {
                _previousBounds = this.Bounds;
                this.WindowState = FormWindowState.Maximized;
                _windowMaximized = true;
                btnWindowMaximize.Text = "❐";
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Bounds = _previousBounds;
                _windowMaximized = false;
                btnWindowMaximize.Text = "□";
            }
        }

        private void WindowClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowBar_Resize(object sender, EventArgs e)
        {
            if (pnlWindowBar == null) return;

            btnWindowClose.Left = pnlWindowBar.ClientSize.Width - btnWindowClose.Width;
            btnWindowMaximize.Left = btnWindowClose.Left - btnWindowMaximize.Width;
            btnWindowMinimize.Left = btnWindowMaximize.Left - btnWindowMinimize.Width;
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                _windowMaximized = true;
                if (btnWindowMaximize != null) btnWindowMaximize.Text = "❐";
            }
            else if (WindowState == FormWindowState.Normal && _windowMaximized)
            {
                _windowMaximized = false;
                if (btnWindowMaximize != null) btnWindowMaximize.Text = "□";
            }
        }
    }
}
