using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library
{
    public class Hand : CardCollection
    {
        public Hand() { }

        public Hand(List<Card> cards)
            : base(cards)
        {
            
        }

        [XmlIgnore]
        public int HandWidth { get { return (Card.Width) * Count; } }

        // Rita alla kort i handen
        public void Draw(Graphics graphics, float boardWidth)
        {
            float cardOffset = 0;
            float offset = 0;

            if (HandWidth > boardWidth)
            {
                cardOffset = (HandWidth - boardWidth) / (Cards.Count - 1);
                offset = (HandWidth - boardWidth) / Cards.Count;
            }

            GraphicsState state = graphics.Save();
            graphics.ScaleTransform(-1, -1);
            graphics.TranslateTransform(-((Card.Width - offset) * Cards.Count / 2) + Card.Width / 2 - Card.Width, -Card.Height);
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].Draw(graphics);
                graphics.TranslateTransform(Card.Width - cardOffset, 0);
            }
            graphics.Restore(state);
        }
    }
}
