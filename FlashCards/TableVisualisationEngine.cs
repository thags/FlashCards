﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using FlashCards.Models;


namespace FlashCards
{
    class TableVisualisationEngine
    {
        public static void ViewTable(List<Stack> tableData)
        {
            ConsoleTableBuilder
               .From(tableData)
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
        }

        public static void ViewTable(List<Flashcard> tableData)
        {
            ConsoleTableBuilder
               .From(tableData)
               .WithFormat(ConsoleTableBuilderFormat.Alternative)
               .ExportAndWriteLine(TableAligntment.Left);
        }
    }
}

