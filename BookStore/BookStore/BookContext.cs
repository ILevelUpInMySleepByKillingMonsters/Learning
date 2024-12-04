using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    internal class BookContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public BookContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Author = "Достоевский", Title = "Идиот", Date = new DateTime(2015, 7, 20) },
                new Book { Id = 2, Author = "Пушкин", Title = "Умный", Date = new DateTime(2011, 3, 2) },
                new Book { Id = 3, Author = "Пушкин", Title = "Смелый", Date = new DateTime(2011, 3, 3) },
                new Book { Id = 4, Author = "Пушкин", Title = "Храбрый", Date = new DateTime(2011, 4, 2) },
                new Book { Id = 5, Author = "А", Title = "Б", Date = new DateTime(2011, 3, 3) },
                new Book { Id = 6, Author = "Б", Title = "А", Date = new DateTime(2012, 2, 2) }
                );
        }
    }
}
