using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace LabManagementSystem.Common
{
    public static class DataGridViewStyler
    {
        public static void Apply(DataGridView dgv)
        {
            if (dgv == null) return;

            // Enable Double Buffering via reflection to eliminate flicker and stutter
            EnableDoubleBuffering(dgv);

            dgv.BackgroundColor = ThemeManager.CardBackground;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ThemeManager.Border;

            dgv.RightToLeft = RightToLeft.Yes;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoGenerateColumns = false;

            // Column Headers Style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ThemeManager.PrimaryNavy,
                ForeColor = Color.White,
                Font = ThemeManager.BodyBoldFont,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(6, 0, 6, 0),
                WrapMode = DataGridViewTriState.False
            };

            // Default Row Style
            dgv.RowTemplate.Height = 38;
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ThemeManager.CardBackground,
                ForeColor = ThemeManager.MainText,
                Font = ThemeManager.BodyFont,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = ColorTranslator.FromHtml("#DBEAFE"), // Soft blue highlight
                SelectionForeColor = ThemeManager.MainText,
                Padding = new Padding(4, 2, 4, 2)
            };

            // Alternating Row Style
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ColorTranslator.FromHtml("#F8FAFC"), // Very subtle slate-gray
                ForeColor = ThemeManager.MainText,
                Font = ThemeManager.BodyFont,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                SelectionBackColor = ColorTranslator.FromHtml("#DBEAFE"),
                SelectionForeColor = ThemeManager.MainText,
                Padding = new Padding(4, 2, 4, 2)
            };

            // Custom border painting
            dgv.Paint += (s, e) =>
            {
                if (dgv.RowCount == 0)
                {
                    string emptyMsg = "لا توجد بيانات مسجلة لعرضها حالياً";
                    using var brush = new SolidBrush(ThemeManager.MutedText);
                    using var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(emptyMsg, ThemeManager.SubtitleFont, brush, dgv.ClientRectangle, sf);
                }
            };
        }

        public static void EnableDoubleBuffering(DataGridView dgv)
        {
            try
            {
                var dgvType = dgv.GetType();
                var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi?.SetValue(dgv, true, null);
            }
            catch
            {
                // Fallback safe ignore
            }
        }
    }
}

