using Library.Data;
using Library.Entities;
using Library.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

const string logFile = "log.txt";
var _logBuffer = new List<string>();
var bookInFileRepository = new InFileRepository<Book>();

bookInFileRepository.GetAll();

bookInFileRepository.BookAdded += BookRepositoryBookAdded;
bookInFileRepository.BookDeleted += BookRepositoryBookDeleted;
bookInFileRepository.Done += BookRepositoryActionDone;

Menu();

while (true)
{
    var input = Console.ReadLine();

    if (input == "Q" || input == "q")
    {
        break;
    }
    switch(input)
    {
        case "1":
            ShowLibrary(bookInFileRepository);
            break;
        case "2":
            AddBook(bookInFileRepository); 
            break;
        case "3":
            RemoveBook(bookInFileRepository);
            break;
        case "4":
            SaveChanges(bookInFileRepository, _logBuffer);
            break;
        default:
            Console.WriteLine("Wrong option!");
            break;

    }
}

void BookRepositoryBookAdded(object? sender, Book e)
{
    _logBuffer.Add($"[{DateTime.Now}]-BookAdded-[{e}]");
}

void BookRepositoryBookDeleted(object? sender, Book e)
{
    _logBuffer.Add($"[{DateTime.Now}]-BookDeleted-[{e}]");
}

void BookRepositoryActionDone(object? sender, Book e)
{
    Console.WriteLine("Done!");
}

static void ShowLibrary(IRepository<Book> repository)
{
    var list = repository.Show();

    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}

static void AddBook(IRepository<Book> repository)
{
    Console.WriteLine("Enter the title of book you want to add to your library:");
    try
    {
        var input = ValidateTitle(Console.ReadLine());
        repository.Add(new Book() { Title = input });
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

//static void AddMagazines(IRepository<Magazine> repository)
//{
//    repository.Add(new Magazine() { Title = "CD Action" });
//    repository.Save();
//}

static void RemoveBook(IRepository<Book> repository)
{
    Console.WriteLine("Enter the Id of book you want to delete:");
    try
    {
        var input = ValidateId(Console.ReadLine());
        var item = repository.GetById(input);
        repository.Remove(item);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void SaveChanges(IRepository<Book> repository, List<string> buffer)
{
    Console.WriteLine("Do you want to save changes? (y)es/(n)o");

    var input = Console.ReadLine();

        switch (input)
        {
            case "y":
                repository.Save();
                logBufferSave(buffer);
                break;
            case "n":
                break;
            default:
                Console.WriteLine("Wrong option");
                break;
        }
}

static void logBufferSave(List<string> buffer)
{
    foreach (var item in buffer)
    {
        using (var writer = File.AppendText(logFile))
        {
            writer.WriteLine(item);
        }
    }
}

static string ValidateTitle(string input)
{
    if (string.IsNullOrEmpty(input) || input.Length > 30)
    {
        throw new Exception("Wrong input");
    }
    else
    {
        return input;
    }
}

static int ValidateId(string input)
{
   if (!int.TryParse(input, out int result)) 
    {
        throw new Exception("Wrong Id");
    }
    else 
    {
        return result;
    }
}

static void Menu()
{
    Console.WriteLine("Hello in Library App");
    Console.WriteLine("Choose:");
    Console.WriteLine("1. To show the library");
    Console.WriteLine("2. To add book to the library");
    Console.WriteLine("3. To remove book from the library");
    Console.WriteLine("4. To save changes");
    Console.WriteLine("Q. To quit application");
}