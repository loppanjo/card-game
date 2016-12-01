using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public int HandWidth { get { return Card.Width * Count; } }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform((-HandWidth / 2) + i * Card.Width, 0);
                Cards[i].Draw(graphics);
                graphics.Restore(state);
            }
        }
    }
}
