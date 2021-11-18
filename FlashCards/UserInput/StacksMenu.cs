using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.DatabaseManagement;

namespace FlashCards.UserInput
{
    class StacksMenu
    {
        public static void Loop()
        {
            bool continueLooping = true;
            while (continueLooping)
            {
                ShowUserChoices();
                string userChoice = User.getUserChoice();
                continueLooping = UserChoiceResult(userChoice);
            }
            
                   
        }
        private static void ShowUserChoices()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("0 to return to main menu");
            Console.WriteLine("V to view all stacks");
            Console.WriteLine("C to Create a stack");
            Console.WriteLine("R to Rename a stack");
            Console.WriteLine("D to Delete a stack");

            Console.WriteLine("--------------------------");
        }
        private static bool UserChoiceResult(string choice)
        {
            bool continueLooping = true;
            switch (choice)
            {
                case "0":
                    continueLooping = false;
                    break;
                case "V":
                    //Change to a TableVisualization engine
                    StackManager.GetStacks();
                    break;
                case "C":
                    string newStackName = User.GetStackName();
                    StackManager.InsertStack(newStackName);
                    break;
                case "R":
                    Console.WriteLine("Current Stacks are");
                    StackManager.GetStacks();
                    StackManager.UpdateStackName(User.GetStackId(), User.GetStackName());
                    break;
                case "D":
                    Console.WriteLine("Current Stacks are");
                    StackManager.GetStacks();
                    StackManager.Delete(User.GetStackId());
                    break;

                default:
                    Console.WriteLine("Incorrect input, try again");
                    break;
            }
            return continueLooping;
        }
    }
}
