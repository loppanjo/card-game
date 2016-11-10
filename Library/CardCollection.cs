using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class CardCollection
    {
        public CardCollection()
        {
            Cards = new List<Card>();
        }

        public CardCollection(List<Card> cards)
        {
            Cards = cards;
        }

        public int Count { get { return Cards.Count; } }

        protected List<Card> Cards { get; private set; }

        public void Take(Card card)
        {
            Cards.Add(card);
        }
        
        public Card Give(Suit suit, int value)
        {
            Card card = Cards.FirstOrDefault(c => c.Value == value && c.Suit == suit);
            Cards.Remove(card);
            return card;
        }

        /// <summary>
        /// Hittar alla kort med en färg.
        /// </summary>
        /// <param name="suit">Färg</param>
        /// <returns></returns>
        public List<Card> FindCards(Suit suit)
        {
            return (from card in Cards
                    where card.Suit == suit
                    select card).ToList();
        }

        /// <summary>
        /// Hittar alla kort med en valör.
        /// </summary>
        /// <param name="value">Valör</param>
        /// <returns></returns>
        public List<Card> FindCards(int value)
        {
            return (from card in Cards
                    where card.Value == value
                    select card).ToList();
        }

        /// <summary>
        /// Hittar ett kort i handen.
        /// </summary>
        /// <param name="suit">Färg</param>
        /// <param name="value">Valör</param>
        /// <returns></returns>
        public Card GetCard(Suit suit, int value)
        {
            return Cards.FirstOrDefault(c => c.Value == value && c.Suit == suit);
        }

        /// <summary>
        /// Byter plats på två kort i leken.
        /// </summary>
        /// <param name="from">Från</param>
        /// <param name="to">Till</param>
        protected void Swap(int from, int to)
        {
            Card temp = Cards[to];
            Cards[to] = Cards[from];
            Cards[from] = temp;
        }
    }
}
