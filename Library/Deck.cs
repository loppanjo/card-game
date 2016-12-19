using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Deck : CardCollection
    {
        private static Random random = new Random();

        public Deck(int count = 52)
        {
            for (int i = 0; i < count; i++)
                Cards.Add(new Card(i));
        }
        
        // Blandar leken.
        public void Shuffle()
        {
            for (int i = 0; i < Count; i++)
            {
                int to = random.Next(Count);
                Swap(i, to);
            }
        }

        // Ge ut ett kort
        public Card Deal()
        {
            Card card = null;
            if (Count > 0)
            {
                card = Cards[0];
                Cards.RemoveAt(0);
            }
            return card;
        }
    }
}
