using System;
using System.Collections.Generic;

namespace FlashCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> configs = XmlManager.ReadConfig();
            Console.WriteLine(configs["test"]);
            Console.WriteLine(configs["anotherTest"]);
        }
    }
}
