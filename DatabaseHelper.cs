using Npgsql;

namespace WinFormsAppV3FlorenBooksV3
{
    /// <summary>
    /// Provides the shared Npgsql connection string and a factory method for connections.
    /// </summary>
    public static class DatabaseHelper
    {
        private const string ConnectionString =
            "Host=localhost;Port=5432;Database=florenbooksdb;Username=postgres;Password=psql98dan5;";

        /// <summary>
        /// Opens and returns a new NpgsqlConnection. Caller is responsible for disposal.
        /// </summary>
        public static NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Tests the database connection and returns true if successful.
        /// </summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var conn = GetConnection();
                return conn.State == System.Data.ConnectionState.Open;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
