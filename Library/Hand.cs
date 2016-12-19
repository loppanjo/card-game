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
        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                GraphicsState state = graphics.Save();
                Cards[i].Position = new PointF((-HandWidth / 2 + Card.Width / 2) + i * (Card.Width), 0);
                graphics.TranslateTransform(Cards[i].Position.X, Cards[i].Position.Y);
                Cards[i].Draw(graphics);
                graphics.Restore(state);
            }
        }
    }
}
