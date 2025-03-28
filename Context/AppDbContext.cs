using Microsoft.EntityFrameworkCore;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Definicja relacji wielu do wielu między Book i Genre
            //modelBuilder.Entity<BookGenre>()
            //    .HasKey(bg => new { bg.BookId, bg.GenreId });  // Ustalamy złożony klucz główny

            //modelBuilder.Entity<BookGenre>()
            //    .HasOne(bg => bg.Book)
            //    .WithMany(b => b.BookGenres)
            //    .HasForeignKey(bg => bg.BookId)
            //    .OnDelete(DeleteBehavior.Cascade);  // Kaskadowe usuwanie powiązanych rekordów

            //modelBuilder.Entity<BookGenre>()
            //    .HasOne(bg => bg.Genre)
            //    .WithMany(g => g.BookGenres)
            //    .HasForeignKey(bg => bg.GenreId)
            //    .OnDelete(DeleteBehavior.Cascade);  // Kaskadowe usuwanie powiązanych rekordów
        }
    }
}
