using System;
using System.Windows.Forms;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class Superdashboard : Form
    {
        private readonly User _currentUser;
        private bool _showingBooks;

        // Available roles — must match the PostgreSQL user_role enum
        private static readonly string[] Roles =
            { "user", "borrowAdmin", "libraryAdmin", "superAdmin" };

        public Superdashboard(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void Superdashboard_Load(object sender, EventArgs e)
        {
            this.Text = $"Super Dashboard  —  {_currentUser.Email}  [{_currentUser.Role}]";
            LoadUsers();
        }

        // ----------------------------------------------------------------
        // Grid population
        // ----------------------------------------------------------------

        private void LoadUsers()
        {
            try
            {
                _showingBooks = false;
                ConfigureUsersGrid();
                var users = UserRepository.GetAllUsers();
                foreach (var u in users)
                    dataGridView1.Rows.Add(u.Id, u.Email, u.Role);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load users:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBooks()
        {
            try
            {
                _showingBooks = true;
                ConfigureBooksGrid();
                var books = BookRepository.GetAllBooks();
                foreach (var book in books)
                {
                    dataGridView1.Rows.Add(book.Id, book.Titlu, book.Autor, book.Editura, book.Anul, book.Pret, book.Status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load books:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureUsersGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Id",
                Name = "colId",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                FillWeight = 45F,
                HeaderText = "Email",
                Name = "Email"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                FillWeight = 25F,
                HeaderText = "Role",
                Name = "role"
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                FillWeight = 10F,
                HeaderText = "Edit Email",
                Name = "colEditEmail",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                FillWeight = 10F,
                HeaderText = "Change Role",
                Name = "colChangeRole",
                Text = "Role",
                UseColumnTextForButtonValue = true
            });
            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                FillWeight = 10F,
                HeaderText = "Delete",
                Name = "colDelete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            });
        }

        private void ConfigureBooksGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Id",
                Name = "colId",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Titlu", Name = "Titlu" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Autor", Name = "Autor" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Editura", Name = "Editura" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anul", Name = "Anul" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Pret", Name = "Pret" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", Name = "Status" });
        }

        // ----------------------------------------------------------------
        // Inline button actions
        // ----------------------------------------------------------------

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_showingBooks) return;

            var row = dataGridView1.Rows[e.RowIndex];
            int userId = Convert.ToInt32(row.Cells["colId"].Value);
            string currentEmail = row.Cells["Email"].Value?.ToString() ?? "";
            string currentRole  = row.Cells["role"].Value?.ToString() ?? "";

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            switch (colName)
            {
                case "colEditEmail":
                    HandleEditEmail(userId, currentEmail);
                    break;

                case "colChangeRole":
                    HandleChangeRole(userId, currentEmail, currentRole);
                    break;

                case "colDelete":
                    HandleDelete(userId, currentEmail);
                    break;
            }
        }

        // ── Edit Email ──────────────────────────────────────────────────

        private void HandleEditEmail(int userId, string currentEmail)
        {
            string? newEmail = ShowInputDialog("Edit Email",
                $"New email for:\n{currentEmail}", currentEmail);

            if (newEmail == null) return; // cancelled

            newEmail = newEmail.Trim();
            if (string.IsNullOrEmpty(newEmail) || newEmail == currentEmail) return;

            try
            {
                UserRepository.UpdateUserEmail(userId, newEmail);
                MessageBox.Show("Email updated successfully.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update email:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Change Role ─────────────────────────────────────────────────

        private void HandleChangeRole(int userId, string currentEmail, string currentRole)
        {
            string? newRole = ShowRoleDialog("Change Role",
                $"Select new role for:\n{currentEmail}", currentRole);

            if (newRole == null || newRole == currentRole) return;

            try
            {
                UserRepository.UpdateUserRole(userId, newRole);
                MessageBox.Show("Role updated successfully.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update role:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Delete ──────────────────────────────────────────────────────

        private void HandleDelete(int userId, string currentEmail)
        {
            // Prevent deleting yourself
            if (currentEmail.Equals(_currentUser.Email, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("You cannot delete your own account.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete:\n{currentEmail}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                UserRepository.DeleteUser(userId);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete user:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------
        // Mini helper dialogs
        // ----------------------------------------------------------------

        /// <summary>Returns the typed text, or null if cancelled.</summary>
        private static string? ShowInputDialog(string title, string prompt, string defaultValue)
        {
            using var form  = new Form();
            using var label = new Label();
            using var textBox = new TextBox();
            using var btnOk  = new Button();
            using var btnCancel = new Button();

            form.Text = title;
            form.ClientSize = new System.Drawing.Size(360, 140);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            label.Text = prompt;
            label.SetBounds(12, 12, 336, 36);
            label.AutoSize = false;

            textBox.Text = defaultValue;
            textBox.SetBounds(12, 54, 336, 22);

            btnOk.Text = "OK";
            btnOk.SetBounds(192, 90, 75, 26);
            btnOk.DialogResult = DialogResult.OK;

            btnCancel.Text = "Cancel";
            btnCancel.SetBounds(273, 90, 75, 26);
            btnCancel.DialogResult = DialogResult.Cancel;

            form.Controls.AddRange(new Control[] { label, textBox, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

        /// <summary>Shows a ComboBox with available roles. Returns chosen role or null if cancelled.</summary>
        private static string? ShowRoleDialog(string title, string prompt, string currentRole)
        {
            using var form  = new Form();
            using var label = new Label();
            using var combo = new ComboBox();
            using var btnOk = new Button();
            using var btnCancel = new Button();

            form.Text = title;
            form.ClientSize = new System.Drawing.Size(360, 140);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            label.Text = prompt;
            label.SetBounds(12, 12, 336, 36);
            label.AutoSize = false;

            combo.SetBounds(12, 54, 336, 22);
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.Items.AddRange(Roles);
            int idx = Array.IndexOf(Roles, currentRole);
            combo.SelectedIndex = idx >= 0 ? idx : 0;

            btnOk.Text = "OK";
            btnOk.SetBounds(192, 90, 75, 26);
            btnOk.DialogResult = DialogResult.OK;

            btnCancel.Text = "Cancel";
            btnCancel.SetBounds(273, 90, 75, 26);
            btnCancel.DialogResult = DialogResult.Cancel;

            form.Controls.AddRange(new Control[] { label, combo, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? combo.SelectedItem?.ToString() : null;
        }

        // ── Unused designer stubs ───────────────────────────────────────
        private void toolStripComboBox1_Click(object sender, EventArgs e) { }
        private void toolStripButton3_Click(object sender, EventArgs e) { }
        private void addToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            CsvExportHelper.ExportDataGridView(dataGridView1, _showingBooks ? "carti_biblioteca.csv" : "utilizatori.csv");
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void buttonBooks_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }
    }
}
