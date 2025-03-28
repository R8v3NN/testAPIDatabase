namespace testAPIDatabase.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        //public ICollection<BookGenre> BookGenres { get; set; }
    }

}
