namespace FlashCards
{
    class Program
    {
        static void Main(string[] args)
        {
            DBManager.CreateDatabase();
            DBManager.CreateStackTable();
            DBManager.CreateFlashCardTable();
            DBManager.CreateStudyTable();
            UserInput.GetUserInput();
        }
    }
}
