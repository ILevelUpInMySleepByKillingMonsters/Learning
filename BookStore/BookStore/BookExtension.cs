using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal static class BookExtension
    {
        public static void Show(this Book book)
        {
            Console.WriteLine($"Id - {book.Id}, " +
                    $"Author - {book.Author}," +
                    $" Title - {book.Title}," +
                    $" Date - {book.Date.ToShortDateString()}");
        }
        public static void Show(this List<Book> books)
        {
            if (books == null)
            {
                Console.WriteLine("Не найдено");
                return;
            }
            if (books.Count == 0)
            {
                Console.WriteLine("Не найдено");
                return;
            }

            foreach (Book book in books)
            {
                book.Show();
            }
        }
    }
}
