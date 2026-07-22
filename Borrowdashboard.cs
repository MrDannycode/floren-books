using System;
using System.Windows.Forms;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class Borrowdashboard : Form
    {
        private readonly User _currentUser;

        public Borrowdashboard(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            AccountSettingsButton.AddTo(this, _currentUser, RefreshDashboardTitle);
        }

        private void Borrowdashboard_Load(object sender, EventArgs e)
        {
            RefreshDashboardTitle();
            LoadBorrowedBooks();
        }

        private void RefreshDashboardTitle() => Text = $"Borrow Dashboard - {_currentUser.Email} [{_currentUser.Role}]";

        private void LoadBorrowedBooks()
        {
            try
            {
                var borrowedBooks = BookRepository.GetBorrowedBooks();
                dataGridView1.Rows.Clear();

                foreach (var borrowedBook in borrowedBooks)
                {
                    bool isReturned = borrowedBook.ReturnDate.HasValue;
                    dataGridView1.Rows.Add(
                        borrowedBook.Id,
                        borrowedBook.UserEmail,
                        borrowedBook.BookTitle,
                        borrowedBook.BookAuthor,
                        borrowedBook.BorrowDate,
                        borrowedBook.ReturnDate,
                        isReturned ? "Returned" : "Active",
                        isReturned ? "" : "Return");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load borrowed books:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName != "colReturn") return;

            var row = dataGridView1.Rows[e.RowIndex];
            string status = row.Cells["Status"].Value?.ToString() ?? "";
            if (status == "Returned") return;

            int borrowedBookId = Convert.ToInt32(row.Cells["colId"].Value);
            string title = row.Cells["BookTitle"].Value?.ToString() ?? "Unknown";

            var confirm = MessageBox.Show(
                $"Mark '{title}' as returned?",
                "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                BookRepository.MarkBorrowedBookReturned(borrowedBookId);
                MessageBox.Show("Borrowed book marked as returned.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBorrowedBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update borrowed book:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadBorrowedBooks();
        }

        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            CsvExportHelper.ExportDataGridView(dataGridView1, "imprumuturi.csv");
        }
    }
}
