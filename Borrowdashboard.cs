using System;
using System.Drawing;
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
                        borrowedBook.BookId,
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

        private void buttonSetExemplare_Click(object sender, EventArgs e)
        {
            using var formCarti = new Form
            {
                Text = "Toate Cărțile - Setează Exemplare",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            grid.Columns.Add("colId", "Id");
            grid.Columns["colId"].Visible = false;
            grid.Columns.Add("colTitlu", "Titlu");
            grid.Columns.Add("colAutor", "Autor");
            grid.Columns.Add("colExemplare", "Exemplare");

            void LoadGrid()
            {
                grid.Rows.Clear();
                foreach (var b in BookRepository.GetAllBooks())
                {
                    grid.Rows.Add(b.Id, b.Titlu, b.Autor, b.Exemplare);
                }
            }
            LoadGrid();

            var panelBottom = new Panel { Dock = DockStyle.Bottom, Height = 50 };
            var btnSet = new Button
            {
                Text = "Setează Exemplare",
                Location = new Point(10, 10),
                Size = new Size(150, 30)
            };
            panelBottom.Controls.Add(btnSet);

            formCarti.Controls.Add(grid);
            formCarti.Controls.Add(panelBottom);

            btnSet.Click += (s, ev) =>
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selectează o carte din listă.", "Nicio selecție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = grid.SelectedRows[0];
                int bookId = Convert.ToInt32(row.Cells["colId"].Value);
                string bookTitle = row.Cells["colTitlu"].Value?.ToString() ?? "carte";
                int currentExemplare = Convert.ToInt32(row.Cells["colExemplare"].Value);

                using var inputForm = new Form
                {
                    Text = "Setează număr exemplare",
                    StartPosition = FormStartPosition.CenterParent,
                    ClientSize = new Size(340, 130),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var label = new Label
                {
                    Text = $"Număr exemplare disponibile pentru:\n\"{bookTitle}\":",
                    Location = new Point(12, 12),
                    Size = new Size(316, 40),
                    AutoSize = false
                };

                var numericUpDown = new NumericUpDown
                {
                    Location = new Point(12, 58),
                    Size = new Size(80, 23),
                    Minimum = 1,
                    Maximum = 9999,
                    Value = currentExemplare
                };

                var btnOk = new Button
                {
                    Text = "OK",
                    Location = new Point(148, 56),
                    Size = new Size(80, 27),
                    DialogResult = DialogResult.OK
                };

                var btnCancel = new Button
                {
                    Text = "Anulează",
                    Location = new Point(240, 56),
                    Size = new Size(80, 27),
                    DialogResult = DialogResult.Cancel
                };

                inputForm.Controls.AddRange(new Control[] { label, numericUpDown, btnOk, btnCancel });
                inputForm.AcceptButton = btnOk;
                inputForm.CancelButton = btnCancel;

                if (inputForm.ShowDialog(formCarti) != DialogResult.OK) return;

                int exemplare = (int)numericUpDown.Value;

                try
                {
                    BookRepository.SetBookExemplare(bookId, exemplare);
                    MessageBox.Show(
                        $"Cartea \"{bookTitle}\" are acum {exemplare} exemplar{(exemplare == 1 ? "" : "e")} disponibil{(exemplare == 1 ? "" : "e")}.",
                        "Actualizat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrid();
                    LoadBorrowedBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la actualizare:\n{ex.Message}",
                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            formCarti.ShowDialog(this);
        }
    }
}
