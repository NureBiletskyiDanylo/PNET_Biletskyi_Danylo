using BookCatalog_API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookCatalog_API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Author> Authors {get; set;}
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookCreateLog> BookCreateLogs { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Log>().ToTable("Logs", t => t.ExcludeFromMigrations());
        builder.Entity<Author>()
            .HasMany(s => s.Books)
            .WithOne(s => s.Author)
            .HasForeignKey(k => k.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Favourite>()
            .HasKey(k => new { k.UserId, k.BookId });

        builder.Entity<Favourite>()
            .HasOne(s => s.FavoriteBy)
            .WithMany(b => b.Favourites)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Favourite>()
            .HasOne(s => s.Book)
            .WithMany(b => b.BeingFavourite)
            .HasForeignKey(k => k.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BookGenre>()
            .HasKey(k => new { k.BookId, k.GenreId });

        builder.Entity<BookGenre>()
            .HasOne(s => s.Genre)
            .WithMany(b => b.BooksWithGenre)
            .HasForeignKey(s => s.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BookGenre>()
            .HasOne(s => s.Book)
            .WithMany(s => s.BookGenres)
            .HasForeignKey(s => s.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BookCreateLog>()
            .HasOne(b => b.Book)
            .WithMany() // no reverse nav
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<BookCreateLog>()
            .HasOne(b => b.Author)
            .WithMany() // no reverse navigation
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Entity<Book>()
        .ToTable("Books", t => t.HasTrigger("trg_LogBookInsert"));

    }
}
