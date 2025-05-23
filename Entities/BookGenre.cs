﻿namespace testAPIDatabase.Entities
{
    public class BookGenre
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public required Book Book { get; set; }
        public Guid GenreId { get; set; }
        public required Genre Genre { get; set; }
    }
}
