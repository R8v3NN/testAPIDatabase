using System.ComponentModel.DataAnnotations;

namespace testAPIDatabase.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required DateTime PublishedDate { get; set; }
        //public ICollection<BookGenre> BookGenres { get; set; }
    }
}
