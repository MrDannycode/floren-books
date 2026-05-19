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
    using WinFormsAppV3FlorenBooksV3.Models;

    public partial class Librarydashboard : Form
    {
        public Librarydashboard()
        {
            InitializeComponent();
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

                BookRepository.AddBook(book);
                MessageBox.Show("Cartea a fost adaugata cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Clear fields after success
                button2_Click(sender, e);
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
        }
    }
}
