using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void Draw(Drawer drawer, int index, RectangleF rectangle, float rotation)
        {
            for (int i = 0; i < Cards.Count; i++)
                Cards[i].Draw(drawer, rectangle, rotation);
        }
    }
}
