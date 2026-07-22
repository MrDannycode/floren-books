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
        private string? selectedCoverPath;

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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Adauga carte
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Titlul si autorul sunt obligatorii!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var book = new Book
                {
                    Titlu = textBox1.Text.Trim(),
                    Autor = textBox2.Text.Trim(),
                    Editura = textBox3.Text.Trim()
                };

                if (int.TryParse(textBox4.Text, out int anul))
                {
                    book.Anul = anul;
                }

                if (decimal.TryParse(textBox5.Text, out decimal pret))
                {
                    book.Pret = pret;
                }

                if (!string.IsNullOrWhiteSpace(selectedCoverPath))
                {
                    book.CoverImagePath = BookCoverStorage.SaveCover(selectedCoverPath);
                }

                BookRepository.AddBook(book);
                MessageBox.Show("Cartea a fost adaugata cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Clear fields after success
                button2_Click(sender, e);
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la adaugarea cartii in baza de date: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Reset
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            selectedCoverPath = null;
            pictureBoxCover.Image?.Dispose();
            pictureBoxCover.Image = null;
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
                        book.Status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la incarcarea cartilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefreshBooks_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void buttonChooseCover_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Alege coperta cartii",
                Filter = "Imagini|*.jpg;*.jpeg;*.png;*.bmp|Toate fisierele|*.*"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                using var image = Image.FromFile(dialog.FileName);
                pictureBoxCover.Image?.Dispose();
                pictureBoxCover.Image = new Bitmap(image);
                selectedCoverPath = dialog.FileName;
            }
            catch (Exception)
            {
                MessageBox.Show("Fisierul selectat nu este o imagine valida.", "Imagine invalida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
