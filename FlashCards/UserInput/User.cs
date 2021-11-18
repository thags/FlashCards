using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.UserInput
{
    class User
    {
        public static string getUserChoice() => Console.ReadLine().ToUpper();

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
