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
                    cover_image_path TEXT,
                    created_at TIMESTAMP DEFAULT NOW()
                );

                ALTER TABLE books ADD COLUMN IF NOT EXISTS cover_image_path TEXT;
                ALTER TABLE books ADD COLUMN IF NOT EXISTS exemplare INT NOT NULL DEFAULT 1;
                ALTER TABLE books ADD COLUMN IF NOT EXISTS stoc_vanzare INT NOT NULL DEFAULT 10;

                CREATE TABLE IF NOT EXISTS purchased_books (
                    id SERIAL PRIMARY KEY,
                    user_id INT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                    book_id INT NOT NULL REFERENCES books(id) ON DELETE CASCADE,
                    purchase_date TIMESTAMP DEFAULT NOW()
                );

                CREATE TABLE IF NOT EXISTS borrowed_books (
                    id SERIAL PRIMARY KEY,
                    user_id INT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                    book_id INT NOT NULL REFERENCES books(id) ON DELETE CASCADE,
                    borrow_date TIMESTAMP DEFAULT NOW(),
                    return_date TIMESTAMP
                );
            ", connection);
            command.ExecuteNonQuery();
        }

        public static void AddBook(Book book)
        {
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                INSERT INTO books (titlu, autor, editura, anul, pret, cover_image_path, exemplare, stoc_vanzare)
                VALUES (@titlu, @autor, @editura, @anul, @pret, @coverImagePath, @exemplare, @stocVanzare)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("titlu", book.Titlu);
            command.Parameters.AddWithValue("autor", book.Autor);
            command.Parameters.AddWithValue("editura", string.IsNullOrEmpty(book.Editura) ? DBNull.Value : book.Editura);
            command.Parameters.AddWithValue("anul", book.Anul.HasValue ? book.Anul.Value : DBNull.Value);
            command.Parameters.AddWithValue("pret", book.Pret.HasValue ? book.Pret.Value : DBNull.Value);
            command.Parameters.AddWithValue("coverImagePath", string.IsNullOrWhiteSpace(book.CoverImagePath) ? DBNull.Value : book.CoverImagePath);
            command.Parameters.AddWithValue("exemplare", book.Exemplare > 0 ? book.Exemplare : 1);
            command.Parameters.AddWithValue("stocVanzare", book.StocVanzare >= 0 ? book.StocVanzare : 10);

            command.ExecuteNonQuery();
        }

        public static void SetBookExemplare(int bookId, int exemplare)
        {
            if (exemplare < 1) throw new ArgumentException("Numărul de exemplare trebuie să fie cel puțin 1.");
            using var connection = DatabaseHelper.GetConnection();
            var query = "UPDATE books SET exemplare = @exemplare WHERE id = @bookId";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("exemplare", exemplare);
            command.Parameters.AddWithValue("bookId", bookId);
            command.ExecuteNonQuery();
        }

        public static void SetBookStocVanzare(int bookId, int stocVanzare)
        {
            if (stocVanzare < 0) throw new ArgumentException("Stocul nu poate fi negativ.");
            using var connection = DatabaseHelper.GetConnection();
            var query = "UPDATE books SET stoc_vanzare = @stocVanzare WHERE id = @bookId";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("stocVanzare", stocVanzare);
            command.Parameters.AddWithValue("bookId", bookId);
            command.ExecuteNonQuery();
        }
        public static List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                SELECT
                    b.id,
                    b.titlu,
                    b.autor,
                    b.editura,
                    b.anul,
                    b.pret,
                    b.cover_image_path,
                    b.exemplare,
                    b.stoc_vanzare,
                    (
                        SELECT COUNT(*)
                        FROM borrowed_books bb
                        WHERE bb.book_id = b.id AND bb.return_date IS NULL
                    ) AS active_borrows,
                    (
                        SELECT COUNT(*)
                        FROM purchased_books pb
                        WHERE pb.book_id = b.id
                    ) AS purchased_count
                FROM books b
                ORDER BY b.id";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int exemplare = reader.GetInt32(7);
                int stocVanzare = reader.GetInt32(8);
                long activeBorrows = reader.GetInt64(9);
                long purchasedCount = reader.GetInt64(10);
                
                int stocRamas = Math.Max(0, stocVanzare - (int)purchasedCount);

                string status = activeBorrows >= exemplare
                    ? "Imprumutata"
                    : $"Disponibila ({exemplare - activeBorrows}/{exemplare})";

                if (stocRamas == 0)
                {
                    status += " | Stoc epuizat";
                }

                var book = new Book
                {
                    Id = reader.GetInt32(0),
                    Titlu = reader.GetString(1),
                    Autor = reader.GetString(2),
                    Editura = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Anul = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Pret = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                    CoverImagePath = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Exemplare = exemplare,
                    StocVanzare = stocVanzare,
                    StocRamas = stocRamas,
                    Status = status
                };
                books.Add(book);
            }

            return books;
        }

        public static List<Book> GetBooksForUser(int userId)
        {
            var books = new List<Book>();
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                SELECT
                    b.id,
                    b.titlu,
                    b.autor,
                    b.editura,
                    b.anul,
                    b.pret,
                    b.cover_image_path,
                    b.exemplare,
                    b.stoc_vanzare,
                    (
                        SELECT COUNT(*)
                        FROM purchased_books pb
                        WHERE pb.book_id = b.id
                    ) AS purchased_count,
                    CASE
                        WHEN EXISTS (
                            SELECT 1
                            FROM borrowed_books bb
                            WHERE bb.book_id = b.id
                                AND bb.user_id = @userId
                                AND bb.return_date IS NULL
                        )
                        THEN 'Imprumutata'
                        ELSE 'Cumparata'
                    END AS status
                FROM books b
                WHERE EXISTS (
                    SELECT 1
                    FROM purchased_books pb
                    WHERE pb.book_id = b.id AND pb.user_id = @userId
                )
                OR EXISTS (
                    SELECT 1
                    FROM borrowed_books bb
                    WHERE bb.book_id = b.id
                        AND bb.user_id = @userId
                        AND bb.return_date IS NULL
                )
                ORDER BY b.titlu";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("userId", userId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int stocVanzare = reader.GetInt32(8);
                long purchasedCount = reader.GetInt64(9);
                int stocRamas = Math.Max(0, stocVanzare - (int)purchasedCount);

                books.Add(new Book
                {
                    Id = reader.GetInt32(0),
                    Titlu = reader.GetString(1),
                    Autor = reader.GetString(2),
                    Editura = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Anul = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                    Pret = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                    CoverImagePath = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Exemplare = reader.GetInt32(7),
                    StocVanzare = stocVanzare,
                    StocRamas = stocRamas,
                    Status = reader.GetString(10)
                });
            }

            return books;
        }

        public static void BuyBook(int userId, int bookId)
        {
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                INSERT INTO purchased_books (user_id, book_id)
                VALUES (@userId, @bookId)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("userId", userId);
            command.Parameters.AddWithValue("bookId", bookId);

            command.ExecuteNonQuery();
        }

        public static void BorrowBook(int userId, int bookId)
        {
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                INSERT INTO borrowed_books (user_id, book_id)
                VALUES (@userId, @bookId)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("userId", userId);
            command.Parameters.AddWithValue("bookId", bookId);

            command.ExecuteNonQuery();
        }

        public static List<BorrowedBook> GetBorrowedBooks()
        {
            var borrowedBooks = new List<BorrowedBook>();
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                SELECT bb.id, bb.book_id, u.email, b.titlu, b.autor, bb.borrow_date, bb.return_date
                FROM borrowed_books bb
                INNER JOIN users u ON u.id = bb.user_id
                INNER JOIN books b ON b.id = bb.book_id
                ORDER BY bb.return_date IS NOT NULL, bb.borrow_date DESC";

            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                borrowedBooks.Add(new BorrowedBook
                {
                    Id = reader.GetInt32(0),
                    BookId = reader.GetInt32(1),
                    UserEmail = reader.GetString(2),
                    BookTitle = reader.GetString(3),
                    BookAuthor = reader.GetString(4),
                    BorrowDate = reader.GetDateTime(5),
                    ReturnDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                });
            }

            return borrowedBooks;
        }

        public static void MarkBorrowedBookReturned(int borrowedBookId)
        {
            using var connection = DatabaseHelper.GetConnection();
            var query = @"
                UPDATE borrowed_books
                SET return_date = NOW()
                WHERE id = @borrowedBookId AND return_date IS NULL";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("borrowedBookId", borrowedBookId);
            command.ExecuteNonQuery();
        }
    }
}
