using System;
using System.Collections.Generic;
using FlashCards.xmlManager;
using FlashCards.DatabaseManagement;

namespace FlashCards
{
    class Program
    {
        static void Main(string[] args)
        {
            string config = XmlManager.ReadConfig("dbConnectionString");
            Console.WriteLine(config);

            DBManager.Connect();
        }
    }
}
