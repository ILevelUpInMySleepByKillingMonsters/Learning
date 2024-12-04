using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    internal class BookOperation
    {
        public static List<Book> Get()
        {
            using (BookContext bookContext = new BookContext())
            {
                return bookContext.Books.ToList().OrderBy(b => b.Id).ToList();
            }
        }
        public static List<Book> GetByTitle(List<Book> books, string title)
        {
            //using (BookContext bookContext = new BookContext())
            //{
            //    return books.Where(b => EF.Functions.Like(b.Title!, $"%{title}%")).ToList();
            //}
            return books.Where(b => b.Title.Contains(title)).ToList();
        }

        public static List<Book> GetByAuthor(List<Book> books, string author)
        {
            return books.Where(b => b.Author == $"{author}").ToList();
        }

        public static List<Book> GetByDate(List<Book> books, string date)
        {
            if (RegexExtension.IsCorrect(@"\d{4}-\d{2}-\d{2}", date) == false)
            {
                Console.WriteLine("Неверный формат даты");
                return null;
            }

            string[] words = date.Split('-');

            DateTime dateTime = new DateTime(
                Convert.ToInt32(words[0]),
                Convert.ToInt32(words[1]),
                Convert.ToInt32(words[2])
                );

            return books.Where(b => b.Date == dateTime).ToList();
        }

        public static List<Book> GetOrderByField(List<Book> books, string field)
        {
            if (RegexExtension.IsCorrect(@"title|author|date", field) == false)
            {
                return null;
            }

            switch (field)
            {
                case "title":
                    return books.OrderBy(b => b.Title).ToList();
                case "author":
                    return books.OrderBy(b => b.Author).ToList();
                case "date":
                    return books.OrderBy(b => b.Date).ToList();
                default:
                    return null;
            }
        }

        public static void Buy(string id)
        {
            if (RegexExtension.IsCorrect(@"\d+", id) == false)
            {
                return;
            }

            using (BookContext bookContext = new BookContext())
            {
                Book? book = bookContext.Books.FirstOrDefault(b => b.Id == Convert.ToInt32(id));
                if (book == null)
                {
                    Console.WriteLine("Книга отсутствует");
                    return;
                }

                bookContext.Books.Remove(book);
                bookContext.SaveChanges();
                Console.WriteLine("Книга куплена");
            }
        }
    }
}
