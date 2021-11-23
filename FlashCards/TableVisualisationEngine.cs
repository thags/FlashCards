using System.Collections.Generic;
using System;
using ConsoleTableExt;
using FlashCards.Models;
using System.Data;

namespace FlashCards
{
    class TableVisualisationEngine
    {
        public static void ViewTable(List<Stack> tableData)
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

        public static void ViewTable(List<Flashcard> tableData)
        {
            if (tableData.Count == 0)
            {
                Console.WriteLine("Currently empty!");
            }
            else
            {
                DataTable parsedTable = ParseFlashcardTableData(tableData);

                ConsoleTableBuilder
               .From(parsedTable)
               .WithTitle($"Stack: {tableData[0].StackName}")
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
            }
            Console.Write("\n");
        }

        private static DataTable ParseFlashcardTableData(List<Flashcard> tableData)
        {
            DataTable fcardTable = new DataTable();
            fcardTable.Columns.Add("Id", typeof(int));
            fcardTable.Columns.Add("Front", typeof(string));
            fcardTable.Columns.Add("Back", typeof(string));

            foreach (Flashcard fcard in tableData)
            {
                fcardTable.Rows.Add(fcard.Id, fcard.Front, fcard.Back);
            }

            return fcardTable;
        }
    }
}


