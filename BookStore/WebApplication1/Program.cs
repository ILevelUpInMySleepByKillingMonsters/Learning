class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        string connection = "Data Source=helloapp.db";
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.MapGet("/api/users", async (ApplicationContext db) => await db.Users.ToListAsync());

        app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
        {
            // получаем пользовател€ по id
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

            // если не найден, отправл€ем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

            // если пользователь найден, отправл€ем его
            return Results.Json(user);
        });

        app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
        {
            // получаем пользовател€ по id
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

            // если не найден, отправл€ем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

            // если пользователь найден, удал€ем его
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.Json(user);
        });

        app.MapPost("/api/users", async (User user, ApplicationContext db) =>
        {
            // добавл€ем пользовател€ в массив
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        });

        app.MapPut("/api/users", async (User userData, ApplicationContext db) =>
        {
            // получаем пользовател€ по id
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

            // если не найден, отправл€ем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

            // если пользователь найден, измен€ем его данные и отправл€ем обратно клиенту
            user.Age = userData.Age;
            user.Name = userData.Name;
            await db.SaveChangesAsync();
            return Results.Json(user);
        });

        app.Run();
    }
}