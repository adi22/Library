namespace Library.UserCommunication
{
    public abstract class MenuBase : IMenu
    {
        protected static bool doYouWantExit = false;
        protected static int chosenOption = -1;
        public abstract void ShowMenu();

        public int GetChosenOption()
        {
            return chosenOption;
        }

        public bool DoYouWantExit()
        {
            return doYouWantExit;
        }
    }
}
