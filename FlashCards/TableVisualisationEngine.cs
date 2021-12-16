using System.Collections.Generic;
using System;
using ConsoleTableExt;
using FlashCards.Models;
using FlashCards.Models.DTOs;
using System.Data;


namespace FlashCards
{
    class TableVisualisationEngine
    {
        public static void ViewTable(List<StacksToView> tableData)
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                ConsoleTableBuilder
               .From(tableData)
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
            }
            Console.Write("\n");
            
        }

        public static void ViewTable(List<FlashcardsToView> tableData, string stackName)
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                int reformatId = 0;
                ConsoleTableBuilder
               .From(tableData)
               .WithTitle($"Stack: {stackName}")
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .WithFormatter(0, (id) =>
               {
                   reformatId++;
                   return reformatId.ToString();
               })
               .ExportAndWriteLine(TableAligntment.Left);
            }
            Console.Write("\n");
        }
        public static void ViewTable(List<StudySessionToView> tableData)
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                ConsoleTableBuilder
               .From(tableData)
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
            }
            Console.Write("\n");
        }
        public static void ViewTable(List<AverageScoreByMonth> tableData, string year)
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                ConsoleTableBuilder
               .From(tableData)
               .WithTitle($"Average per Month for year {year}")
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
            }
            Console.Write("\n");
        }

        public static void ViewCard(List<string> tableData, string stackName)
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                ConsoleTableBuilder
               .From(tableData)
               //.WithTitle($"Stack: {stackName}")
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
            }
        }

        public static List<StacksToView> MapStacksToDTO(List<Stack> unMapped)
        {
            List<StacksToView> stackView = new List<StacksToView> { };
            foreach (Stack s in unMapped)
            {
               StacksToView mappedStack = new StacksToView {
                   Name = s.Name
               };
               stackView.Add(mappedStack);
            }
            return stackView;
        }

        public static List<FlashcardsToView> MapFlashcardsToDTO(List<Flashcard> unMapped)
        {
            List<FlashcardsToView> stackView = new List<FlashcardsToView> { };
            foreach (Flashcard f in unMapped)
            {
                FlashcardsToView mappedStack = new FlashcardsToView
                {
                    Id = f.Id,
                    Front = f.Front,
                    Back = f.Back
                };
                stackView.Add(mappedStack);
            }
            return stackView;
        }

    }
}


