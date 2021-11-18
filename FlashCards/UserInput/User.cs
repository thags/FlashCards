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
    }
}
