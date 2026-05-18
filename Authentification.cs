using System;
using System.Windows.Forms;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class Authentification : Form
    {
        public Authentification()
        {
            InitializeComponent();
        }

        // Sign In button
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            // --- Validation ---
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your email and password.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Authenticate ---
            try
            {
                var user = UserRepository.GetUserByEmail(email);

                if (user == null || !UserRepository.VerifyPassword(password, user.Password))
                {
                    MessageBox.Show("Invalid email or password.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(
                    $"Welcome, {user.Email}!\nRole: {user.Role}",
                    "Login Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // TODO: Navigate to the appropriate dashboard based on user.Role
                // e.g.: if (user.Role == "superAdmin") { new AdminDashboard().Show(); this.Hide(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reset button — clears both fields
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        // "Don't have an account? Sign Up" link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var signUpForm = new SignUp();
            signUpForm.FormClosed += (s, args) => this.Show();
            this.Hide();
            signUpForm.Show();
        }
    }
}
