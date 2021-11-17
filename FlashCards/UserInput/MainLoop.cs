using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.DatabaseManagement;

namespace FlashCards.UserInput
{
    class MainLoop
    {
        bool continueLooping;
        public MainLoop()
        {
            this.continueLooping = true;
            Loop();
        }
        public void Loop()
        {
            while (this.continueLooping)
            {
                showUserChoices();
                userChoiceResult(getUserChoice());
            }
        }
        private static void showUserChoices()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("0 to exit");
            Console.WriteLine("S to show stacks");

            Console.WriteLine("--------------------------");
        }
        private static string getUserChoice() => Console.ReadLine().ToUpper();

        private void userChoiceResult(string choice)
        {
            switch (choice)
            {
                case "0":
                    this.continueLooping = false;
                    break;
                case "S":
                    DBManager.GetStacks();
                    break;
                default:
                    Console.WriteLine("Incorrect input, try again");
                    break;
            }
        }
    }
}
