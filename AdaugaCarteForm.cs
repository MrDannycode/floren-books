using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class AdaugaCarteForm : Form
    {
        private string? selectedCoverPath;

        public AdaugaCarteForm()
        {
            InitializeComponent();
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

                DialogResult = DialogResult.OK;
                Close();
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
    }
}
