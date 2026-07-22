using System;
using System.IO;

namespace WinFormsAppV3FlorenBooksV3
{
    internal static class BookCoverStorage
    {
        private const long MaximumImageSizeBytes = 5 * 1024 * 1024;

        public static string SaveCover(string sourcePath)
        {
            var source = new FileInfo(sourcePath);
            if (!source.Exists)
            {
                throw new FileNotFoundException("Fisierul imaginii nu a fost gasit.", sourcePath);
            }

            if (source.Length > MaximumImageSizeBytes)
            {
                throw new InvalidOperationException("Imaginea selectata depaseste limita de 5 MB.");
            }

            var directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "FlorenBooks",
                "Covers");
            Directory.CreateDirectory(directory);

            var extension = Path.GetExtension(source.Name).ToLowerInvariant();
            var destination = Path.Combine(directory, $"{Guid.NewGuid():N}{extension}");
            File.Copy(source.FullName, destination);
            return destination;
        }
    }
}
