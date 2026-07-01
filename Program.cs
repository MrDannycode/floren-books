namespace WinFormsAppV3FlorenBooksV3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Seed the superAdmin account if it doesn't exist yet
            UserRepository.SeedSuperAdmin();

            // Initialize books tables (books, purchased_books, borrowed_books)
            BookRepository.InitializeDatabase();

            // Open the login form
            Application.Run(new Authentification());
        }
    }
}