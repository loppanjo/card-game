using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Hand
    {
        private List<Card> cards = new List<Card>();

        public Hand() { }

        public Hand(List<Card> cards)
        {
            this.cards = cards;
        }

        public List<Card> FindCards(Suit suit)
        {
            
        }

        public List<Card> FindCards(int value)
        {

        }
    }
}
