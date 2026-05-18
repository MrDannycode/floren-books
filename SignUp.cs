using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WinFormsAppV3FlorenBooksV3
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        // Email field changed
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        // Password field changed
        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        // Sign Up button
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            // --- Validation ---
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in both email and password.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Register ---
            try
            {
                bool success = UserRepository.RegisterUser(email, password, role: "user");

                if (success)
                {
                    MessageBox.Show($"Account created successfully!\nEmail: {email}\nRole: User",
                        "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Go back to login
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An account with this email already exists.",
                        "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Back to Login button (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Helper: validate email format
        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }
}
