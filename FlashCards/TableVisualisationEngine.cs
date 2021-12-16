using ConsoleTableExt;
using FlashCards.Models;
using FlashCards.Models.DTOs;
using System;
using System.Collections.Generic;


namespace FlashCards
{
    class TableVisualisationEngine
    {
        public static void ViewTable<T>(List<T> tableData, string title = "") where T : class
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                ConsoleTableBuilder
               .From(tableData)
               .WithTitle(title)
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
            }
            Console.Write("\n");
        }


        public static List<StacksToView> MapStacksToDTO(List<Stack> unMapped)
        {
            List<StacksToView> stackView = new List<StacksToView> { };
            foreach (Stack s in unMapped)
            {
                StacksToView mappedStack = new StacksToView
                {
                    Name = s.Name
                };
                stackView.Add(mappedStack);
            }
            return stackView;
        }

        public static List<FlashcardsToView> MapFlashcardsToDTO(List<Flashcard> unMapped)
        {
            List<FlashcardsToView> stackView = new List<FlashcardsToView> { };
            int id = 1;
            foreach (Flashcard f in unMapped)
            {
                FlashcardsToView mappedStack = new FlashcardsToView
                {
                    Id = id,
                    Front = f.Front,
                    Back = f.Back
                };
                stackView.Add(mappedStack);
                id++;
            }
            return stackView;
        }

    }
}


