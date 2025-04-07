using testAPIDatabase.Entities;

namespace testAPIDatabase.Dtos
{
    public class BookGenreInDto
    {
        public required Guid BookId { get; set; }
        public required Guid GenreId { get; set; }
    }
}
