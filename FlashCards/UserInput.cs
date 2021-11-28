using FlashCards.Models.DTOs;
using System;
using System.Collections.Generic;
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
                Console.WriteLine("R to Study");
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
                    case "R":
                        Console.Clear();
                        StudyMenu();
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
                        var viewStacks = TableVisualisationEngine.MapStacksToDTO(StackController.GetStacks());
                        TableVisualisationEngine.ViewTable(viewStacks);
                        WaitForUser();
                        break;
                    case "C":
                        Console.Clear();
                        bool isInputCorrect = GetNewStackName(out string newStackName);
                        if (isInputCorrect)
                        {
                            StackController.InsertStack(newStackName);
                            Console.WriteLine("Newly created stack is:");
                            viewStacks = TableVisualisationEngine.MapStacksToDTO(StackController.GetLastStack());
                            TableVisualisationEngine.ViewTable(viewStacks);
                        }
                        else
                        {
                            Console.WriteLine("No new stack created, invalid name input");
                        }
                        WaitForUser();
                        break;
                    case "R":
                        Console.Clear();
                        Console.WriteLine("5 Most recent stacks are: \n");
                        DisplayLatest5Stacks();
                        bool chosenStackExists = GetCurrentStack(out string stackToUpdate);
                        if (chosenStackExists)
                        {
                            bool updateNameNotExist = GetNewStackName(out string newName);
                            if (updateNameNotExist)
                            {
                                StackController.UpdateStackName(stackToUpdate, newName);
                            }
                        }
                        Console.Clear();
                        break;
                    case "D":
                        Console.Clear();
                        Console.WriteLine("5 Most recent stacks are: \n");
                        DisplayLatest5Stacks();
                        chosenStackExists = GetCurrentStack(out string stackToDelete);
                        if (chosenStackExists)
                        {
                            StackController.DeleteStack(stackToDelete);
                        }
                        Console.Clear();
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
                DisplayLatest5Stacks();
                Console.WriteLine("\n Choose a stack of flashcards to interact with: ");
                stackCheck = GetCurrentStack(out currentStackToWorkOn);
                if (stackCheck)
                {
                    exit = false;
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }
            
            
            while (!exit)
            {
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
                        var viewFlashcards = TableVisualisationEngine.MapFlashcardsToDTO(FlashcardController.GetAllCardsInStack(currentStackToWorkOn));
                        TableVisualisationEngine.ViewTable(viewFlashcards, currentStackToWorkOn);
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
                            viewFlashcards = TableVisualisationEngine.MapFlashcardsToDTO(FlashcardController.GetXCardsInStack(currentStackToWorkOn, amount));
                            TableVisualisationEngine.ViewTable(viewFlashcards, currentStackToWorkOn);
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
                        viewFlashcards = TableVisualisationEngine.MapFlashcardsToDTO(FlashcardController.GetLastCardInStack(currentStackToWorkOn));
                        TableVisualisationEngine.ViewTable(viewFlashcards, currentStackToWorkOn);
                        WaitForUser();
                        break;
                    case "E":
                        Console.Clear();
                        Console.WriteLine("5 most recent cards in stack are: ");
                        viewFlashcards = TableVisualisationEngine.MapFlashcardsToDTO(FlashcardController.GetXCardsInStack(currentStackToWorkOn, 5));
                        TableVisualisationEngine.ViewTable(viewFlashcards, currentStackToWorkOn);
                        bool realCard = GetCardId(out int cardId);
                        if (realCard)
                        {
                            FlashCardEditMenu(cardId, currentStackToWorkOn);
                            Console.WriteLine("Card after edit is: \n");
                            viewFlashcards = TableVisualisationEngine.MapFlashcardsToDTO(FlashcardController.GetCardById(cardId));
                            TableVisualisationEngine.ViewTable(viewFlashcards, currentStackToWorkOn);
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
        private static void StudyMenu()
        {
            bool exit = false;
            string stackChoice;
            //choose a stack
            bool correctChoice;
            do
            {
                Console.WriteLine("5 Most recent stacks are: \n");
                DisplayLatest5Stacks();
                correctChoice = GetCurrentStack(out stackChoice);
                if (stackChoice == "none!")
                {
                    exit = true;
                    Console.Clear();
                    break;
                }
            }
            while (!correctChoice);
            Console.Clear();

            //loop through the flashcards in the stack
            //get an input and check that the answer matches the solution
            int amountCorrect = 0;
            int totalGuesses = 0;
            while (!exit)
            {
                var flashcardsFromStack = FlashcardController.GetAllCardsInStack(stackChoice);
                var flashcardsDTOFromStack = TableVisualisationEngine.MapFlashcardsToDTO(flashcardsFromStack);
                foreach (FlashcardsToView fcard in flashcardsDTOFromStack)
                {
                    var frontOfCard = new List<string>();
                    frontOfCard.Add(fcard.Front);
                    TableVisualisationEngine.ViewCard(frontOfCard, stackChoice);
                    string guess = GetUserCardGuess();
                    if (guess == "0")
                    {
                        exit = true;
                    }
                    else
                    {
                        bool correctAnswer = CheckAnswer(guess, fcard.Back);
                        if (correctAnswer)
                        {
                            amountCorrect++;
                        }
                        totalGuesses++;
                        DisplayAnswerCorrectness(correctAnswer, guess, fcard.Back);
                        WaitForUser();
                    }

                    if (exit)
                    {
                        Console.WriteLine("Exiting Study session");
                        Console.WriteLine($"You got {amountCorrect} right out of {totalGuesses}");
                        StudyController.InsertStudySession(new Models.StudySession
                        {
                            StackId = StackController.GetIdFromName(stackChoice),
                            CorrectAnswers = amountCorrect,
                            TotalGueses = totalGuesses,
                        });
                        WaitForUser();
                        break;
                    }
                    
                }
            }
            

        }
        private static void FlashCardEditMenu(int cardId, string currentStackToWorkOn)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                var viewFlashcards = TableVisualisationEngine.MapFlashcardsToDTO(FlashcardController.GetCardById(cardId));
                TableVisualisationEngine.ViewTable(viewFlashcards, currentStackToWorkOn);
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
                    stackName = "none!";
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
        private static void DisplayLatest5Stacks()
        {
            var viewStacks = TableVisualisationEngine.MapStacksToDTO(StackController.GetXStacks(5, "DESC"));
            TableVisualisationEngine.ViewTable(viewStacks);
        }
        private static string GetUserCardGuess()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Input your answer to this card");
            Console.WriteLine("Or 0 to exit \n");
            string guess =  Console.ReadLine();
            Console.WriteLine("\n");
            return guess;
        }
        private static bool CheckAnswer(string guess, string answer)
        {
            if (guess.ToUpper() == answer.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void DisplayAnswerCorrectness(bool wasCorrect, string userGuess, string answer)
        {
            string statusMessage;
            bool displayDifference;
            if (wasCorrect)
            {
                statusMessage = "correct!";
                displayDifference = false;
            }
            else
            {
                statusMessage = "wrong.";
                displayDifference = true;
            }
            Console.WriteLine($"Your answer was {statusMessage}");
            if (displayDifference)
            {
                Console.WriteLine($"\n You answered {userGuess}");
                Console.WriteLine($"The Correct answer was {answer} \n");
            }
        }
    }
}
