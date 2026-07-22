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
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class Userdashboard : Form
    {
        private readonly User _currentUser;
        private bool _showingMyBooks;

        public Userdashboard(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            this.Load += Userdashboard_Load;
        }

        private void Userdashboard_Load(object sender, EventArgs e)
        {
            this.Text = $"User Dashboard — {_currentUser.Email}";
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                var books = BookRepository.GetAllBooks();
                _showingMyBooks = false;
                SetBookActionColumnsVisible(true);
                PopulateBooksGrid(books);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMyBooks()
        {
            try
            {
                var books = BookRepository.GetBooksForUser(_currentUser.Id);
                _showingMyBooks = true;
                SetBookActionColumnsVisible(false);
                PopulateBooksGrid(books);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load your books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateBooksGrid(IEnumerable<Book> books)
        {
            dataGridView1.Rows.Clear();

            foreach (var book in books)
            {
                var rowIndex = dataGridView1.Rows.Add(
                    LoadCoverThumbnail(book.CoverImagePath),
                    book.Id,
                    book.Titlu,
                    book.Autor,
                    book.Editura,
                    book.Anul,
                    book.Pret,
                    book.Status);
                dataGridView1.Rows[rowIndex].Tag = book.CoverImagePath;
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

        private void SetBookActionColumnsVisible(bool visible)
        {
            colBuy.Visible = visible;
            colBorrow.Visible = visible;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            var row = dataGridView1.Rows[e.RowIndex];

            if (colName == "Coperta")
            {
                ShowCoverPreview(row.Tag as string);
                return;
            }

            string titlu = row.Cells["Titlu"].Value?.ToString() ?? "Unknown";
            int bookId = Convert.ToInt32(row.Cells["colId"].Value);

            try
            {
                if (colName == "colBuy")
                {
                    var confirm = MessageBox.Show($"Are you sure you want to buy '{titlu}'?", "Confirm Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        BookRepository.BuyBook(_currentUser.Id, bookId);
                        MessageBox.Show($"You have successfully purchased '{titlu}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (_showingMyBooks)
                        {
                            LoadMyBooks();
                        }
                    }
                }
                else if (colName == "colBorrow")
                {
                    string status = row.Cells["Status"].Value?.ToString() ?? "";
                    if (status == "Imprumutata")
                    {
                        MessageBox.Show($"'{titlu}' este deja imprumutata.", "Indisponibila", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var confirm = MessageBox.Show($"Are you sure you want to borrow '{titlu}'?", "Confirm Borrow", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        BookRepository.BorrowBook(_currentUser.Id, bookId);
                        MessageBox.Show($"You have successfully borrowed '{titlu}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooks();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Transaction failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowCoverPreview(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                MessageBox.Show("Aceasta carte nu are o coperta disponibila.", "Coperta indisponibila", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var sourceImage = Image.FromFile(path);
                using var preview = new Form
                {
                    Text = "Coperta cartii",
                    StartPosition = FormStartPosition.CenterParent,
                    ClientSize = new Size(500, 650),
                    MinimumSize = new Size(300, 400)
                };
                using var pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    Image = new Bitmap(sourceImage),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.White
                };

                preview.Controls.Add(pictureBox);
                preview.ShowDialog(this);
            }
            catch (Exception)
            {
                MessageBox.Show("Imaginea coperții nu poate fi afișată.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            CsvExportHelper.ExportDataGridView(dataGridView1, _showingMyBooks ? "cartile_mele.csv" : "carti.csv");
        }

        private void buttonAllBooks_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void buttonMyBooks_Click(object sender, EventArgs e)
        {
            LoadMyBooks();
        }
    }
}
