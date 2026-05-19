using System;
using System.Windows.Forms;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class Superdashboard : Form
    {
        private readonly User _currentUser;

        public Superdashboard(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void Superdashboard_Load(object sender, EventArgs e)
        {
            // Show logged-in admin info in the title bar
            this.Text = $"Super Dashboard  —  {_currentUser.Email}  [{_currentUser.Role}]";

            LoadUsers();
        }

        private void LoadUsers()
        {
            dataGridView1.Rows.Clear();

            try
            {
                var users = UserRepository.GetAllUsers();
                foreach (var u in users)
                {
                    dataGridView1.Rows.Add(u.Email, u.Role);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load users:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
