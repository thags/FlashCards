using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.WriteLine("S to View, Create, Delete or Rename Stacks");
                Console.WriteLine("F to View, Create, Delete or Rename Flashcards");
                Console.WriteLine("--------------------------");

                string choice = GetUserMenuChoice();
                switch (choice)
                {
                    case "0":
                        exitProgram = true;
                        break;
                    case "S":
                        StacksMenu();
                        break;
                    case "F":
                        FlashCardsMenu();
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
                Console.WriteLine("V to View all Stacks");
                Console.WriteLine("C to Create a Stack");
                Console.WriteLine("R to Rename a stack");
                Console.WriteLine("D to Delete a stack");

                Console.WriteLine("--------------------------");

                string choice = GetUserMenuChoice();
                switch (choice)
                {
                    case "0":
                        exit = true;
                        Console.Clear();
                        break;
                    case "V":
                        Console.Clear();
                        TableVisualisationEngine.ViewTable(StackController.GetStacks());
                        break;
                    case "C":
                        Console.Clear();
                        bool isInputCorrect = GetNewStackName(out string newStackName);
                        if (isInputCorrect)
                        {
                            StackController.InsertStack(newStackName);
                            Console.WriteLine("Newly created stack is:");
                            TableVisualisationEngine.ViewTable(StackController.GetStacks(1, "DESC"));
                        }
                        else
                        {
                            Console.WriteLine("No new stack created, invalid name input");
                        }
                        break;
                    case "R":
                        Console.Clear();
                        bool chosenStackExists = GetCurrentStack(out string stackToUpdate);
                        if (chosenStackExists)
                        {
                            bool updateNameNotExist = GetNewStackName(out string newName);
                            if (updateNameNotExist)
                            {
                                StackController.UpdateStackName(stackToUpdate, newName);
                            }
                        }
                        break;
                    case "D":
                        Console.Clear();
                        chosenStackExists = GetCurrentStack(out string stackToDelete);
                        if (chosenStackExists)
                        {
                            StackController.Delete(stackToDelete);
                        }
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input, try again");
                        break;
                }
            }
        }
        private static void FlashCardsMenu()
        {
            Console.Clear();
            string currentStackToWorkOn = "none";
            bool exit = true;
            bool stackCheck = false;
            while (!stackCheck)
            {
                Console.WriteLine("Choose a stack of flashcards to interact with: ");
                stackCheck = GetCurrentStack(out currentStackToWorkOn);
                if (stackCheck)
                {
                    exit = false;
                }
                else
                {
                    break;
                }
            }
            
            
            while (!exit)
            {
                //May be better to choose which stack to be within first, and then a new menu
                //where a user can edit/view/create/delete cards within that stack
                Console.WriteLine("--------------------------");
                Console.WriteLine($"Current working stack: {currentStackToWorkOn} \n");
                Console.WriteLine("0 to return to main menu");
                Console.WriteLine("V to view all Flashcards in stack");
                Console.WriteLine("C to Create a Flashcard in current stack");
                //Console.WriteLine("R to Edit a Flashcard");
                //Console.WriteLine("D to Delete a Flashcard");

                Console.WriteLine("--------------------------");

                string choice = GetUserMenuChoice();
                switch (choice)
                {
                    case "0":
                        exit = true;
                        Console.Clear();
                        break;
                    case "V":
                        Console.Clear();
                        TableVisualisationEngine.ViewTable(FlashcardController.GetAllCardsInStack(currentStackToWorkOn));
                        //TODO: Input a stack ID or name and view all flashcards of that stack
                        break;
                    case "C":
                        Console.Clear();
                        string frontOfCard = GetFrontFlashCard();
                        string backOfCard = GetBackFlashCard();
                        FlashcardController.CreateFlashCard(currentStackToWorkOn, frontOfCard, backOfCard);
                        break;
                    case "R":
                        Console.Clear();
                        
                        break;
                    case "D":
                        Console.Clear();
                        
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input, try again");
                        break;
                }
            }
        }
        private static string GetUserMenuChoice() => Console.ReadLine().ToUpper();
        private static string RemoveSpecials(string s)
        {
            s = Regex.Replace(s, "[^a-zA-Z0-9' ']", "");
            return s;
        }
        private static string GetFrontFlashCard()
        {
            Console.WriteLine("Input information for the front of the flashcard");
            return RemoveSpecials(Console.ReadLine());
        }
        private static string GetBackFlashCard()
        {
            Console.WriteLine("Input information for the back of the flashcard");
            return RemoveSpecials(Console.ReadLine());
        }

        public static bool GetNewStackName(out string newStackName)
        {
            //we want to get a new stack name and verify that the name doesn't already exist in the DB, as a stack
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("Input a name for the new stack");
                Console.WriteLine("Or input 0 to go back");
                string userChoice = RemoveSpecials(Console.ReadLine());
                if (userChoice == "0")
                {
                    break;
                }

                correctInput = !StackController.CheckStackExists(userChoice);
                if (correctInput)
                {
                    newStackName = userChoice;
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Stack {userChoice} already exists, try again.");
                }
            }
            newStackName = "Invalid";
            return correctInput;
        }
        public static bool GetCurrentStack(out string stackName)
        {
            //we want to get a current stack name and verify that the name already exists in the DB, as a stack
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("Input a current stack name");
                Console.WriteLine("Or input 0 to exit input");
                string userChoice = RemoveSpecials(Console.ReadLine());
                if (userChoice == "0")
                {
                    stackName = "none";
                    return false;
                }

                correctInput = StackController.CheckStackExists(userChoice);
                if (correctInput)
                {
                    stackName = userChoice;
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Stack {userChoice} doesn't exist, try again.");
                }
            }
            stackName = "Invalid";
            return correctInput;
        }

        public static int GetStackId()
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
