namespace LabManagementSystem.Common
{
    public static class GridHelper
    {
        public static void ApplyModernRtlStyle(DataGridView dgv)
        {
            DataGridViewStyler.Apply(dgv);
        }

        public static bool IsHeaderClick(DataGridViewCellEventArgs e)
        {
            return e.RowIndex < 0;
        }
    }
}

