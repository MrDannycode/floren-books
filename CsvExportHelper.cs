using System.Text;

namespace WinFormsAppV3FlorenBooksV3
{
    public static class CsvExportHelper
    {
        public static void ExportDataGridView(DataGridView grid, string defaultFileName)
        {
            var columns = grid.Columns
                .Cast<DataGridViewColumn>()
                .Where(column => column.Visible && column is not DataGridViewButtonColumn)
                .OrderBy(column => column.DisplayIndex)
                .ToList();

            if (columns.Count == 0 || grid.Rows.Count == 0)
            {
                MessageBox.Show("Nu exista date pentru export.", "Export CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Export CSV",
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                FileName = defaultFileName,
                DefaultExt = "csv",
                AddExtension = true,
                OverwritePrompt = true
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                File.WriteAllText(dialog.FileName, BuildCsv(grid, columns), new UTF8Encoding(true));
                MessageBox.Show("Exportul CSV a fost finalizat cu succes.", "Export CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exportul CSV a esuat:\n{ex.Message}", "Export CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string BuildCsv(DataGridView grid, IReadOnlyList<DataGridViewColumn> columns)
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Join(",", columns.Select(column => Escape(column.HeaderText))));

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var values = columns.Select(column => Escape(row.Cells[column.Name].FormattedValue?.ToString() ?? ""));
                builder.AppendLine(string.Join(",", values));
            }

            return builder.ToString();
        }

        private static string Escape(string value)
        {
            if (!value.Contains(',') && !value.Contains('"') && !value.Contains('\r') && !value.Contains('\n'))
            {
                return value;
            }

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
    }
}
