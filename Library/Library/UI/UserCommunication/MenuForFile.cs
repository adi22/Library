using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;
using Spectre.Console;

namespace Library.UI.UserCommunication
{
    public class MenuForFile : MenuBase
    {
        private readonly UserOptions<FileRepository<Book>> _userOptionsInFile;

        public MenuForFile(
            UserOptions<FileRepository<Book>> userOptionsInFile)
        {
            _userOptionsInFile = userOptionsInFile;
        }

        public override void ShowMenu()
        {

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("You work with json file, select the option")
                .PageSize(20)
                .AddChoices(new[]
                {
                    "Show library",
                    "Add book",
                    "Remove book",
                    "Save changes\n",
                    "Show books ordered by title",
                    "Show specific page number",
                    "Show unique authors",
                    "Show average rating of all books",
                    "Show maximal length from all books\n",
                    "Import data from CSV file",
                    "Export data to XML file\n",
                    "Back to the main menu\n",
                    "Exit"
                }
                ));

            switch (menuOption)
            {
                case "Show library":
                    _userOptionsInFile.ShowLibrary();
                    break;
                case "Add book":
                    _userOptionsInFile.AddBook();
                    break;
                case "Remove book":
                    _userOptionsInFile.RemoveBook();
                    break;
                case "Save changes\n":
                    _userOptionsInFile.SaveChanges();
                    break;
                case "Show books ordered by title":
                    _userOptionsInFile.OrderByTitle();
                    break;
                case "Show specific page number":
                    _userOptionsInFile.ShowPageNumber();
                    break;
                case "Show unique authors":
                    _userOptionsInFile.ShowUniqueAuthors();
                    break;
                case "Show average rating of all books":
                    _userOptionsInFile.ShowAverageRatingOfAllBooks();
                    break;
                case "Show maximal length from all books\n":
                    _userOptionsInFile.ShowMaximalLengthFromAllBooks();
                    break;
                case "Import data from CSV file":
                    _userOptionsInFile.ImportDataFromCsv();
                    break;
                case "Export data to XML file\n":
                    _userOptionsInFile.ExportDataToXml();
                    break;
                case "Back to the main menu\n":
                    chosenOption = 0;
                    break;
                case "Exit":
                    doYouWantExit = true;
                    break;
            }
        }
    }
}