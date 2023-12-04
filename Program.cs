enum Command
{
    PrintBook,
    PrintBooks,
    InsertBook,
    BorrowBook,
    ReturnBook,
    DeleteBook,
    FindClosestBook,
    ColorFlipCount,
    Quit,
}
namespace GatorLibrary
{
    public static class Program
    {
        static readonly Library GatorLibrary = new();
        static readonly string InputDir = "inputs";
        static readonly string OutputDir = "outputs";
        static string OutputFileName = string.Empty;
        private static void AppendToFile(string text)
        {
            File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), OutputDir, OutputFileName), text + "\n");
        }

        private static void HandleInsertBook(string[] arguments)
        {
            if (arguments.Length < 4)
            {
                AppendToFile("Invalid args for InsertBook");
                return;
            }
            if (!int.TryParse(arguments[0], out int bookId))
            {
                AppendToFile($"Invalid Book ID {arguments[0]} for InsertBook");
                return;
            }
            string bookName = arguments[1], authorName = arguments[2], availabilityStatus = arguments[3];
            if (arguments.Length == 5)
            {
                if (!int.TryParse(arguments[4], out int borrowedBy))
                {
                    AppendToFile($"Invalid Borrowed By {arguments[4]} for InsertBook");
                    return;
                }
                GatorLibrary.InsertBook(bookId, bookName, authorName, availabilityStatus, borrowedBy);
            }
            else
            {
                GatorLibrary.InsertBook(bookId, bookName, authorName, availabilityStatus);
            }
        }

        private static void HandlePrintBook(string[] args)
        {
            if (args.Length < 1 || !int.TryParse(args[0], out int bookId))
            {
                AppendToFile("Invalid args for PrintBook");
                return;
            }
            AppendToFile(GatorLibrary.PrintBook(bookId));
        }

        private static void HandlePrintBooks(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[0], out int bookId1) || !int.TryParse(args[1], out int bookId2))
            {
                AppendToFile("Invalid args for PrintBooks");
                return;
            }
            AppendToFile(GatorLibrary.PrintBooks(bookId1, bookId2));
        }

        private static void HandleFindClosestBook(string[] args)
        {
            if (args.Length < 1 || !int.TryParse(args[0], out int bookId))
            {
                AppendToFile("Invalid args for FindClosestBook");
                return;
            }
            AppendToFile(GatorLibrary.FindClosestBook(bookId));
        }

        private static void HandleDeleteBook(string[] args)
        {
            if (args.Length < 1 || !int.TryParse(args[0], out int bookId))
            {
                AppendToFile("Invalid args for DeleteBook");
                return;
            }
            AppendToFile(GatorLibrary.DeleteBook(bookId));
        }

        private static void HandleBorrowBook(string[] args)
        {
            if (args.Length < 3
                || !int.TryParse(args[0], out int patronId)
                || !int.TryParse(args[1], out int bookId)
                || !int.TryParse(args[2], out int patronPriority))
            {
                AppendToFile("Invalid args for BorrowBook");
                return;
            }
            AppendToFile(GatorLibrary.BorrowBook(patronId, bookId, patronPriority));
        }

        private static void HandleReturnBook(string[] args)
        {
            if (args.Length < 2
                || !int.TryParse(args[0], out int patronId)
                || !int.TryParse(args[1], out int bookId))
            {
                AppendToFile("Invalid args for BorrowBook");
                return;
            }
            AppendToFile(GatorLibrary.ReturnBook(patronId, bookId));
        }

        static void Main(string[] args)
        {
            // Get input file name from args
            string inputFileName = args[0];
            string ext = ".txt";
            try
            {
                OutputFileName = inputFileName + "_output.txt";
                string currentDirectory = Directory.GetCurrentDirectory();
                string inputPath = Path.Combine(currentDirectory, InputDir, inputFileName + ext);
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), OutputDir, OutputFileName), string.Empty);
                var commands = File.ReadAllLines(inputPath);
                foreach (string commandStr in commands)
                {
                    string[] input = commandStr.Split("(");
                    string func = input[0];
                    string[] arguments = input[1].Split(")")[0].Split(",");
                    bool isValidFuncCall = Enum.TryParse(func, out Command command);
                    if (!isValidFuncCall)
                    {
                        AppendToFile("Invalid Command name " + func);
                        continue;
                    }
                    switch (command)
                    {
                        case Command.InsertBook:
                            HandleInsertBook(arguments);
                            break;
                        case Command.PrintBook:
                            HandlePrintBook(arguments);
                            break;
                        case Command.PrintBooks:
                            HandlePrintBooks(arguments);
                            break;
                        case Command.FindClosestBook:
                            HandleFindClosestBook(arguments);
                            break;
                        case Command.DeleteBook:
                            HandleDeleteBook(arguments);
                            break;
                        case Command.BorrowBook:
                            HandleBorrowBook(arguments);
                            break;
                        case Command.ReturnBook:
                            HandleReturnBook(arguments);
                            break;
                        case Command.ColorFlipCount:
                            AppendToFile(GatorLibrary.ColorFlipCount());
                            break;
                        case Command.Quit:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

}