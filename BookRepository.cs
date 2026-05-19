using System;
using System.Collections.Generic;
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
        public static List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            using var connection = DatabaseHelper.GetConnection();
            var query = "SELECT id, titlu, autor, editura, anul, pret FROM books ORDER BY id";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book
                {
                    Id = reader.GetInt32(0),
                    Titlu = reader.GetString(1),
                    Autor = reader.GetString(2),
                    Editura = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Anul = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Pret = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5)
                };
                books.Add(book);
            }

            return books;
        }
    }
}
