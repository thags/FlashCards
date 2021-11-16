using System;

namespace FlashCards
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer = XmlManager.ReadConfig();
            Console.WriteLine(answer);
        }
    }
}
