using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    class UserInput
    {
        public static void GetUserInput()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("0 to exit");
                Console.WriteLine("S to Create, Delete or Rename stacks");
                Console.WriteLine("--------------------------");

                string choice = GetInputUserChoice();
                Console.Clear();
                switch (choice)
                {
                    case "0":
                        exitProgram = true;
                        break;
                    case "S":
                        StacksMenu();
                        break;
                    default:
                        Console.WriteLine("Incorrect input, try again");
                        break;
                }
            }
        }
        
        private static void StacksMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("0 to return to main menu");
                Console.WriteLine("V to view all stacks");
                Console.WriteLine("C to Create a stack");
                Console.WriteLine("R to Rename a stack");
                Console.WriteLine("D to Delete a stack");

                Console.WriteLine("--------------------------");

                string choice = GetInputUserChoice();
                Console.Clear();
                switch (choice)
                {
                    case "0":
                        exit = true;
                        break;
                    case "V":
                        Console.Clear();
                        TableVisualisationEngine.ViewTable(StackController.GetStacks());
                        Console.WriteLine("Any key to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "C":
                        string newStackName = GetInputStackName();
                        StackController.InsertStack(newStackName);
                        Console.WriteLine("Newly created Stack: ");
                        TableVisualisationEngine.ViewTable(StackController.GetStacks(1, "DESC"));
                        break;
                    case "R":
                        Console.WriteLine("Current Stacks are");
                        StackController.GetStacks();
                        StackController.UpdateStackName(GetInputStackId(), GetInputStackName());
                        break;
                    case "D":
                        Console.WriteLine("Current Stacks are");
                        StackController.GetStacks();
                        StackController.Delete(GetInputStackId());
                        break;

                    default:
                        Console.WriteLine("Incorrect input, try again");
                        break;
                }
            }
        }
        private static string GetInputUserChoice() => Console.ReadLine().ToUpper();

        public static string GetInputStackName()
        {
            Console.WriteLine("Input a name for the stack");
            return Console.ReadLine();
        }

        public static int GetInputStackId()
        {
            int userInput = -1;
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("Input an ID of a stack");
                correctInput = int.TryParse(Console.ReadLine(), out userInput);
                if (!correctInput)
                {
                    Console.WriteLine("Incorrect Input, try again");
                }
            }
            return userInput;


        }
    }
}
