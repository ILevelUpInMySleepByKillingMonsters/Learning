using BookStore;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Введите команду");
            string commandName = Console.ReadLine();
            Command(commandName);
        }
    }

    public static void Command(string command)
    {
        string[] commandSplit = command.Split(" --");
        if (commandSplit[0] != "get" && commandSplit[0] != "buy")
        {
            Console.WriteLine("Доступны команды get и buy");
            return;
        }

        if ((commandSplit.Length <= 1 || commandSplit.Length > 2) && commandSplit[0] == "buy")
        {
            Console.WriteLine("Требуется флаг --id");
            return;
        }

        for (int i = 1; i < commandSplit.Length; i++)
        {
            if (RegexExtension.IsCorrect(@"\D+=\S+", commandSplit[i]) == false)
            {
                return;
            }
        }

        if (commandSplit[0] == "get")
        {
            List<Book> books = new List<Book>();
            books = BookOperation.Get();
            for (int i = 1; i < commandSplit.Length; i++)
            {
                if (books == null)
                {
                    Console.WriteLine("Не найдено");
                    return;
                }
                books = SelectOperation(books, commandSplit[i]);
            }
            books.Show();
            return;
        }

        Buy(commandSplit[1]);
    }

    public static List<Book> SelectOperation(List<Book> books, string flag)
    {
        string[] flagSplit = flag.Split("=");
        switch (flagSplit[0])
        {
            case "title":
                return BookOperation.GetByTitle(books, flagSplit[1]);
            case "author":
                return BookOperation.GetByAuthor(books, flagSplit[1]);
            case "date":
                return BookOperation.GetByDate(books, flagSplit[1]);
            case "orderby":
                return BookOperation.GetOrderByField(books, flagSplit[1]);
            default:
                return null;
        }
    }

    public static void Buy(string flag)
    {
        string[] flagSplit = flag.Split("=");
        switch (flagSplit[0])
        {
            case "id":
                BookOperation.Buy(flagSplit[1]);
                break;
            default:
                break;
        }
    }
}