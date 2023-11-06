using Spectre.Console;

namespace Library.UI.UserCommunication
{
    public class MenuMain : MenuBase
    {
        public override void ShowMenu()
        {
            Console.Clear();

            var menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Hello in library app. Select the service you want to work with:")
                .AddChoices(new[]
                {
                    "Library in json file",
                    "Library in database\n",
                    "Exit"
                }
                ));

            switch (menuOption)
            {
                case "Library in json file":
                    chosenOption = 1;
                    break;
                case "Library in database\n":
                    chosenOption = 2;
                    break;
                case "Exit":
                    doYouWantExit = true;
                    break;
            }
        }
    }
}