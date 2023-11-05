namespace Library.UserCommunication
{
    public interface IMenu
    {
        void ShowMenu();
        int GetChosenOption();
        bool DoYouWantExit();
    }
}
