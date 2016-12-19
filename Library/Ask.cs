using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Ask
    {
        public Ask() { }

        public Ask(string player, Card card)
        {
            Player = player;
            Card = card;
        }

        public string Player { get; set; }
        public Card Card { get; set; }
    }
}
