using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Hand : CardCollection
    {
        public Hand() { }

        public Hand(List<Card> cards)
            : base(cards)
        {
            
        }
    }
}
