using System;
using System.Text.RegularExpressions;

namespace FlashCards
{
    class UserInput
    {
        public static void GetUserInput()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                Console.WriteLine("\n --------------------------");
                Console.WriteLine("0 to exit");
                Console.WriteLine("S to Manage Stacks");
                Console.WriteLine("F to Manage FlashCards");
                Console.WriteLine("-------------------------- \n");

                string choice = GetUserMenuChoice();
                switch (choice)
                {
                    case "0":
                        exitProgram = true;
                        break;
                    case "S":
                        Console.Clear();
                        StacksMenu();
                        break;
                    case "F":
                        Console.Clear();
                        FlashCardsMenu();
                        break;
                    default:
                        Console.WriteLine("Incorrect input, try again");
                        WaitForUser();
                        break;
                }
            }
        }
        private static void StacksMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n --------------------------");
                Console.WriteLine("0 to return to main menu");
                Console.WriteLine("V to View all Stacks");
                Console.WriteLine("C to Create a Stack");
                Console.WriteLine("R to Rename a stack");
                Console.WriteLine("D to Delete a stack");

                Console.WriteLine("-------------------------- \n");

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
                        WaitForUser();
                        break;
                    case "C":
                        Console.Clear();
                        bool isInputCorrect = GetNewStackName(out string newStackName);
                        if (isInputCorrect)
                        {
                            StackController.InsertStack(newStackName);
                            Console.WriteLine("Newly created stack is:");
                            TableVisualisationEngine.ViewTable(StackController.GetLastStack());
                            WaitForUser();
                        }
                        else
                        {
                            Console.WriteLine("No new stack created, invalid name input");
                            WaitForUser();
                        }
                        break;
                    case "R":
                        Console.Clear();
                        Console.WriteLine("5 Most recent stacks are: \n");
                        TableVisualisationEngine.ViewTable(StackController.GetStacks(5, "DESC"));
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
                        Console.WriteLine("5 Most recent stacks are: \n");
                        TableVisualisationEngine.ViewTable(StackController.GetStacks(5, "DESC"));
                        chosenStackExists = GetCurrentStack(out string stackToDelete);
                        if (chosenStackExists)
                        {
                            StackController.Delete(stackToDelete);
                        }
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input, try again");
                        WaitForUser();
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
                Console.WriteLine("5 Most recent stacks are: \n");
                TableVisualisationEngine.ViewTable(StackController.GetStacks(5, "DESC"));
                Console.WriteLine("\n Choose a stack of flashcards to interact with: ");
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
                Console.WriteLine("\n --------------------------");
                Console.WriteLine($"Current working stack: {currentStackToWorkOn} \n");
                Console.WriteLine("0 to return to main menu");
                Console.WriteLine("X to change current stack");
                Console.WriteLine("V to view all Flashcards in stack");
                Console.WriteLine("A to view X amount of cards in stack");
                Console.WriteLine("C to Create a Flashcard in current stack");
                Console.WriteLine("E to Edit a Flashcard");
                Console.WriteLine("D to Delete a Flashcard");

                Console.WriteLine("-------------------------- \n");

                string choice = GetUserMenuChoice();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        exit = true;
                        break;
                    case "X":
                        Console.Clear();
                        Console.WriteLine("\n --------------------------");
                        Console.WriteLine("Choose a stack of flashcards to interact with: ");
                        Console.WriteLine("-------------------------- \n");
                        bool didReturnRealStack = GetCurrentStack(out string potentialStack);
                        if (didReturnRealStack)
                        {
                            currentStackToWorkOn = potentialStack;
                        }
                        else
                        {
                            Console.WriteLine("Inputted stack name does not exist");
                            WaitForUser();
                        }
                        break;
                    case "V":
                        Console.Clear();
                        TableVisualisationEngine.ViewTable(FlashcardController.GetAllCardsInStack(currentStackToWorkOn));
                        WaitForUser();
                        break;
                    case "A":
                        Console.Clear();
                        Console.WriteLine("\n --------------------------");
                        Console.WriteLine("What number of cards would you like to see?");
                        Console.WriteLine("-------------------------- \n");
                        bool isInt = int.TryParse(Console.ReadLine(), out int amount);
                        if (isInt)
                        {
                            TableVisualisationEngine.ViewTable(FlashcardController.GetXCardsInStack(currentStackToWorkOn, amount));
                            WaitForUser();
                        }
                        else
                        {
                            Console.WriteLine("Inputted value was not a number");
                            WaitForUser();
                        }
                        break;
                    case "C":
                        Console.Clear();
                        string frontOfCard = GetFrontFlashCard();
                        string backOfCard = GetBackFlashCard();
                        FlashcardController.CreateFlashCard(currentStackToWorkOn, frontOfCard, backOfCard);
                        Console.WriteLine("Newly created card is: \n");
                        TableVisualisationEngine.ViewTable(FlashcardController.GetXCardsInStack(currentStackToWorkOn, 1));
                        WaitForUser();
                        break;
                    case "E":
                        Console.Clear();
                        bool realCard = GetCardId(out int cardId);
                        if (realCard)
                        {
                            FlashCardEditMenu(cardId);
                            Console.WriteLine("Card after edit is: \n");
                            TableVisualisationEngine.ViewTable(FlashcardController.GetCardById(cardId));
                            WaitForUser();
                        }
                        else
                        {
                            Console.WriteLine("Inputted card ID does not exist");
                            WaitForUser();
                        }
                        break;
                    case "D":
                        Console.Clear();
                        realCard = GetCardId(out cardId);
                        if (realCard)
                        {
                            FlashcardController.Delete(cardId);
                            Console.WriteLine("Flashcard deleted");
                            WaitForUser();
                        }
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input, try again");
                        WaitForUser();
                        break;
                }
            }
        }
        private static void FlashCardEditMenu(int cardId)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                TableVisualisationEngine.ViewTable(FlashcardController.GetCardById(cardId));
                Console.WriteLine("\n");
                Console.WriteLine("--------------------------");
                Console.WriteLine("0 to return to previous menu");
                Console.WriteLine("F to edit the front of the card");
                Console.WriteLine("B to edit the back of the card");
                Console.WriteLine("-------------------------- \n");

                string userChoice = GetUserMenuChoice();
                Console.Clear();
                switch (userChoice)
                {
                    case "0":
                        Console.Clear();
                        exit = true;
                        break;
                    case "F":
                        string front = GetFrontFlashCard();
                        FlashcardController.UpdateCard(cardId, front, "Front");
                        break;
                    case "B":
                        string back = GetBackFlashCard();
                        FlashcardController.UpdateCard(cardId, back, "Back");
                        break;
                    default:
                        break;
                }
            }

        }
        private static void WaitForUser()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();
        }
        private static string RemoveSpecials(string s)
        {
            s = Regex.Replace(s, "[^a-zA-Z0-9' ']", "");
            return s;
        }
        private static string GetUserMenuChoice() => Console.ReadLine().ToUpper();
        private static string GetFrontFlashCard()
        {
            Console.WriteLine("\n --------------------------");
            Console.WriteLine("Input information for the front of the flashcard");
            Console.WriteLine("-------------------------- \n");
            string input = Console.ReadLine();
            Console.Clear();
            return RemoveSpecials(input);
        }
        private static string GetBackFlashCard()
        {
            Console.WriteLine("\n --------------------------");
            Console.WriteLine("Input information for the back of the flashcard");
            Console.WriteLine("-------------------------- \n");
            string input = Console.ReadLine();
            Console.Clear();
            return RemoveSpecials(input);
        }
        private static bool GetCardId(out int cardId)
        {
            cardId = -1;
            bool correctInput = false;
            bool realCard = false;
            while (!correctInput && !realCard)
            {
                Console.WriteLine("\n --------------------------");
                Console.WriteLine("Input an ID of a flashcard");
                Console.WriteLine("Or 0 to exit");
                Console.WriteLine("-------------------------- \n");
                string input = Console.ReadLine();
                Console.Clear();

                correctInput = int.TryParse(input, out int userInput);

                if (!correctInput)
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect Input, try again \n");
                }
                else
                {
                    switch (userInput)
                    {
                        case 0:
                            return false;
                        default:
                            realCard = FlashcardController.CheckCardExists(userInput);
                            if (!realCard)
                            {
                                Console.Clear();
                                Console.WriteLine("Not a valid flashcard id, try again");
                            }
                            else
                            {
                                cardId = userInput;
                                return true;
                            }
                            break;
                    }

                }

            }
            return false;
        }
        private static bool GetNewStackName(out string newStackName)
        {
            //we want to get a new stack name and verify that the name doesn't already exist in the DB, as a stack
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("\n --------------------------");
                Console.WriteLine("Input a name for the new stack");
                Console.WriteLine("Or input 0 to go back");
                Console.WriteLine("-------------------------- \n");
                string input = Console.ReadLine();
                Console.Clear();
                string userChoice = RemoveSpecials(input);
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
        private static bool GetCurrentStack(out string stackName)
        {
            //we want to get a current stack name and verify that the name already exists in the DB, as a stack
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("\n --------------------------");
                Console.WriteLine("Input a current stack name");
                Console.WriteLine("Or input 0 to exit input");
                Console.WriteLine("-------------------------- \n");
                string input = Console.ReadLine();
                string userChoice = RemoveSpecials(input);
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
    }
}
