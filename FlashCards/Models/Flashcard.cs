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
        public string Stack { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
    }
}
