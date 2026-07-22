namespace WinFormsAppV3FlorenBooksV3.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public string Editura { get; set; }
        public int? Anul { get; set; }
        public decimal? Pret { get; set; }
        public string? CoverImagePath { get; set; }
        public string Status { get; set; } = "Disponibila";
    }
}
