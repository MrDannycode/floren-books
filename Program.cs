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

            // Open the login form
            Application.Run(new Authentification());
        }
    }
}