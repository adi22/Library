namespace Library.UI.UserCommunication
{
    public interface IMenu
    {
        void ShowMenu();
        int GetChosenOption();
        bool DoYouWantExit();
    }
}