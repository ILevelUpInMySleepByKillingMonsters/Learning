using BookStore;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        string connection = "Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=1111";
        builder.Services.AddDbContext<BookContext>(options => options.UseNpgsql(connection));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();


        app.MapGet("/api/books", async (BookContext db) => await db.Books.ToListAsync());

        app.MapGet("/api/booksAuthor", async (BookContext db) => await db.Books.OrderBy(b => b.Author).ToListAsync());
        app.MapGet("/api/booksTitle", async (BookContext db) => await db.Books.OrderBy(b => b.Title).ToListAsync());
        app.MapGet("/api/booksDate", async (BookContext db) => await db.Books.OrderBy(b => b.Date).ToListAsync());

        app.MapDelete("/api/books/{id:int}", async (int id, BookContext db) =>
        {
            Book? book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) 
            {
                return Results.NotFound(new { message = " нига не найдена" }); 
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Results.Json(book);
        });

        app.Run();
    }
}