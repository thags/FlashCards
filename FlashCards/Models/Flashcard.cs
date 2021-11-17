using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    class Flashcard
    {
        public int Id { get; set; }
        public string stack { get; set; }
        public string front { get; set; }
        public string back { get; set; }
    }
}
