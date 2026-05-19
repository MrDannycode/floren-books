using Npgsql;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    /// <summary>
    /// Handles all user-related database operations.
    /// </summary>
    public static class UserRepository
    {
        // ----------------------------------------------------------------
        // Seeding
        // ----------------------------------------------------------------

        /// <summary>
        /// Inserts the superAdmin account if it doesn't already exist.
        /// Called once at application startup.
        /// </summary>
        public static void SeedSuperAdmin()
        {
            const string email = "superadmin@lib.com";
            const string plainPassword = "admin11";
            const string role = "superAdmin";

            try
            {
                using var conn = DatabaseHelper.GetConnection();

                // Check if the superAdmin already exists
                using var checkCmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM users WHERE email = @email", conn);
                checkCmd.Parameters.AddWithValue("email", email);
                long count = (long)(checkCmd.ExecuteScalar() ?? 0);

                if (count == 0)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                    using var insertCmd = new NpgsqlCommand(
                        "INSERT INTO users (email, password, role) VALUES (@email, @password, @role::user_role)",
                        conn);
                    insertCmd.Parameters.AddWithValue("email", email);
                    insertCmd.Parameters.AddWithValue("password", hashedPassword);
                    insertCmd.Parameters.AddWithValue("role", role);
                    insertCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Database error during seeding:\n{ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------
        // Registration
        // ----------------------------------------------------------------

        /// <summary>
        /// Registers a new user in the database.
        /// </summary>
        /// <param name="email">User email (must be unique).</param>
        /// <param name="plainPassword">Plain-text password — will be hashed.</param>
        /// <param name="role">One of: superAdmin, libraryAdmin, borrowAdmin, user.</param>
        /// <returns>True if registration succeeded, false if the email already exists.</returns>
        public static bool RegisterUser(string email, string plainPassword, string role = "user")
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            using var conn = DatabaseHelper.GetConnection();

            // Check email uniqueness
            using var checkCmd = new NpgsqlCommand(
                "SELECT COUNT(*) FROM users WHERE email = @email", conn);
            checkCmd.Parameters.AddWithValue("email", email);
            long count = (long)(checkCmd.ExecuteScalar() ?? 0);

            if (count > 0)
                return false; // Email already taken

            using var insertCmd = new NpgsqlCommand(
                "INSERT INTO users (email, password, role) VALUES (@email, @password, @role::user_role)",
                conn);
            insertCmd.Parameters.AddWithValue("email", email);
            insertCmd.Parameters.AddWithValue("password", hashedPassword);
            insertCmd.Parameters.AddWithValue("role", role);
            insertCmd.ExecuteNonQuery();

            return true;
        }

        // ----------------------------------------------------------------
        // Authentication
        // ----------------------------------------------------------------

        /// <summary>
        /// Retrieves a user by email from the database.
        /// </summary>
        public static User? GetUserByEmail(string email)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new NpgsqlCommand(
                "SELECT id, email, password, role, created_at FROM users WHERE email = @email",
                conn);
            cmd.Parameters.AddWithValue("email", email);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                };
            }

            return null;
        }

        /// <summary>
        /// Verifies a plain-text password against the stored BCrypt hash.
        /// </summary>
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        // ----------------------------------------------------------------
        // Admin queries
        // ----------------------------------------------------------------

        /// <summary>
        /// Returns all users (email + role) ordered by creation date.
        /// Used to populate the Superdashboard grid.
        /// </summary>
        public static List<User> GetAllUsers()
        {
            var users = new List<User>();

            using var conn = DatabaseHelper.GetConnection();
            using var cmd = new NpgsqlCommand(
                "SELECT id, email, password, role, created_at FROM users ORDER BY created_at", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                });
            }

            return users;
        }
    }
}
