using System;
using Npgsql;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3
{
    public static class BookRepository
    {
        public static void InitializeDatabase()
        {
            using var connection = DatabaseHelper.GetConnection();
            using var command = new NpgsqlCommand(@"
                CREATE TABLE IF NOT EXISTS books (
                    id SERIAL PRIMARY KEY,
                    titlu VARCHAR(255) NOT NULL,
                    autor VARCHAR(255) NOT NULL,
                    editura VARCHAR(255),
                    anul INT,
                    pret DECIMAL(10, 2),
                    created_at TIMESTAMP DEFAULT NOW()
                );
            ", connection);
            command.ExecuteNonQuery();
        }

        public static void AddBook(Book book)
        {
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                INSERT INTO books (titlu, autor, editura, anul, pret)
                VALUES (@titlu, @autor, @editura, @anul, @pret)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("titlu", book.Titlu);
            command.Parameters.AddWithValue("autor", book.Autor);
            command.Parameters.AddWithValue("editura", string.IsNullOrEmpty(book.Editura) ? DBNull.Value : book.Editura);
            command.Parameters.AddWithValue("anul", book.Anul.HasValue ? book.Anul.Value : DBNull.Value);
            command.Parameters.AddWithValue("pret", book.Pret.HasValue ? book.Pret.Value : DBNull.Value);

            command.ExecuteNonQuery();
        }
    }
}
