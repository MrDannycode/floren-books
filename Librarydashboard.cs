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
