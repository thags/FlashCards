﻿using System;
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
                Console.WriteLine("S to View, Create, Delete or Rename Stacks");
                Console.WriteLine("F to View, Create, Delete or Rename Flashcards");
                Console.WriteLine("--------------------------");

                string choice = GetUserChoice();
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

                string choice = GetUserChoice();
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
                        string newStackName = GetStackName();
                        StackController.InsertStack(newStackName);
                        Console.WriteLine("Newly created stack is:");
                        TableVisualisationEngine.ViewTable(StackController.GetStacks(1, "DESC"));
                        break;
                    case "R":
                        Console.Clear();
                        StackController.UpdateStackName(GetStackId(), GetStackName());
                        break;
                    case "D":
                        Console.Clear();
                        StackController.Delete(GetStackId());
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
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("0 to return to main menu");
                Console.WriteLine("V to view all Flashcards");
                Console.WriteLine("C to Create a Flashcard");
                Console.WriteLine("R to Edit a Flashcard");
                Console.WriteLine("D to Delete a Flashcard");

                Console.WriteLine("--------------------------");

                string choice = GetUserChoice();
                switch (choice)
                {
                    case "0":
                        exit = true;
                        Console.Clear();
                        break;
                    case "V":
                        Console.Clear();
                        TableVisualisationEngine.ViewTable(FlashcardController.GetAllCards());
                        break;
                    case "C":
                        Console.Clear();
                        
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
        private static string GetUserChoice() => Console.ReadLine().ToUpper();

        public static string GetStackName()
        {
            Console.WriteLine("Input a name for the stack");
            return Console.ReadLine();
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
