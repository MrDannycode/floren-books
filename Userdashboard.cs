using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                dataGridView1.Rows.Clear();

                foreach (var book in books)
                {
                    dataGridView1.Rows.Add(book.Id, book.Titlu, book.Autor, book.Editura, book.Anul, book.Pret, book.Status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            var row = dataGridView1.Rows[e.RowIndex];
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

        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            CsvExportHelper.ExportDataGridView(dataGridView1, "carti.csv");
        }
    }
}
