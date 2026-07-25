using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppV3FlorenBooksV3
{
    using WinFormsAppV3FlorenBooksV3.Models;

    public partial class Librarydashboard : Form
    {
        private readonly User _currentUser;

        public Librarydashboard(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            AccountSettingsButton.AddTo(this, _currentUser, RefreshDashboardTitle);
            try
            {
                BookRepository.InitializeDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not initialize books database: " + ex.Message);
            }
        }

        private void Librarydashboard_Load(object sender, EventArgs e)
        {
            RefreshDashboardTitle();
            LoadBooks();
        }

        private void RefreshDashboardTitle() => Text = $"Library Dashboard - {_currentUser.Email} [{_currentUser.Role}]";

        private void LoadBooks()
        {
            try
            {
                var books = BookRepository.GetAllBooks();
                dataGridViewBooks.Rows.Clear();

                foreach (var book in books)
                {
                    dataGridViewBooks.Rows.Add(
                        LoadCoverThumbnail(book.CoverImagePath),
                        book.Id,
                        book.Titlu,
                        book.Autor,
                        book.Editura,
                        book.Anul,
                        book.Pret,
                        $"{book.StocRamas} / {book.StocVanzare}",
                        book.Status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la incarcarea cartilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdaugaCarte_Click(object sender, EventArgs e)
        {
            using var adaugaForm = new AdaugaCarteForm();
            if (adaugaForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void buttonSetStocVanzare_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectează o carte din tabel.", "Nicio selecție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridViewBooks.SelectedRows[0];
            int bookId = Convert.ToInt32(row.Cells["colId"].Value);
            string bookTitle = row.Cells["Titlu"].Value?.ToString() ?? "carte";

            using var inputForm = new Form
            {
                Text = "Setează Stoc Vânzare",
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(340, 130),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var label = new Label
            {
                Text = $"Număr cărți (stoc) disponibile pentru vânzare:\n\"{bookTitle}\":",
                Location = new Point(12, 12),
                Size = new Size(316, 40),
                AutoSize = false
            };

            var numericUpDown = new NumericUpDown
            {
                Location = new Point(12, 58),
                Size = new Size(80, 23),
                Minimum = 0,
                Maximum = 9999,
                Value = 10 // Default logic
            };
            
            // Try parse existing stock string if any
            string stocStr = row.Cells["Stoc"].Value?.ToString() ?? "";
            if (stocStr.Contains("/")) {
                string totalStr = stocStr.Split('/')[1].Trim();
                if (int.TryParse(totalStr, out int currentTotal)) {
                    numericUpDown.Value = currentTotal;
                }
            }

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

            if (inputForm.ShowDialog(this) != DialogResult.OK) return;

            int stocVanzare = (int)numericUpDown.Value;

            try
            {
                BookRepository.SetBookStocVanzare(bookId, stocVanzare);
                MessageBox.Show(
                    $"Stocul pentru \"{bookTitle}\" a fost setat la {stocVanzare}.",
                    "Actualizat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la actualizare:\n{ex.Message}",
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefreshBooks_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private static Image? LoadCoverThumbnail(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                using var image = Image.FromFile(path);
                return new Bitmap(image, new Size(40, 55));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
