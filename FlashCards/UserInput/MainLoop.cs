﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.DatabaseManagement;

namespace FlashCards.UserInput
{
    class MainLoop
    {
        public static void Loop()
        {
            bool continueLooping = true;
            while (continueLooping)
            {
                showMainMenu();
                string userChoice = User.getUserChoice();
                continueLooping = userChoiceResult(userChoice);
            }
        }
        private static void showMainMenu()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("0 to exit");
            Console.WriteLine("S to Create, Delete or Rename stacks");

            Console.WriteLine("--------------------------");
        }
        

        private static bool userChoiceResult(string choice)
        {
            bool continueLooping = true;
            switch (choice)
            {
                case "0":
                    continueLooping = false;
                    break;
                case "S":
                    StacksMenu.Loop();
                    break;
                default:
                    Console.WriteLine("Incorrect input, try again");
                    break;
            }
            return continueLooping;
        }
    }
}
