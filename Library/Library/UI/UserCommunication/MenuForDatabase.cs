using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;
using Spectre.Console;

namespace Library.UI.UserCommunication
{
    public class MenuForDatabase : MenuBase
    {
        private readonly UserOptions<SqlRepository<Book>> _userOptionsInDatabase;

        public MenuForDatabase(
            UserOptions<SqlRepository<Book>> userOptionsInDatabase)
        {
            _userOptionsInDatabase = userOptionsInDatabase;
        }

        public override void ShowMenu()
        {

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("You work with database, select the option")
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
                    _userOptionsInDatabase.ShowLibrary();
                    break;
                case "Add book":
                    _userOptionsInDatabase.AddBook();
                    break;
                case "Remove book":
                    _userOptionsInDatabase.RemoveBook();
                    break;
                case "Save changes\n":
                    _userOptionsInDatabase.SaveChanges();
                    break;
                case "Show books ordered by title":
                    _userOptionsInDatabase.OrderByTitle();
                    break;
                case "Show specific page number":
                    _userOptionsInDatabase.ShowPageNumber();
                    break;
                case "Show unique authors":
                    _userOptionsInDatabase.ShowUniqueAuthors();
                    break;
                case "Show average rating of all books":
                    _userOptionsInDatabase.ShowAverageRatingOfAllBooks();
                    break;
                case "Show maximal length from all books\n":
                    _userOptionsInDatabase.ShowMaximalLengthFromAllBooks();
                    break;
                case "Import data from CSV file":
                    _userOptionsInDatabase.ImportDataFromCsv();
                    break;
                case "Export data to XML file\n":
                    _userOptionsInDatabase.ExportDataToXml();
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