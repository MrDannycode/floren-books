using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class Userdashboard : Form
    {
        public Userdashboard()
        {
            InitializeComponent();
            this.Load += Userdashboard_Load;
        }

        private void Userdashboard_Load(object sender, EventArgs e)
        {
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
                    dataGridView1.Rows.Add(book.Titlu, book.Autor, book.Editura, book.Anul, book.Pret);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
