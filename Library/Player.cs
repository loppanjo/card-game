using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            Hand = new Hand();
        }

        public string Name { get; set; }
        public Hand Hand { get; private set; }

        public void Draw(Drawer drawer, int index, int count)
        {
            float angle = (float)((Math.PI * 2) / count) * index;
            Hand.Draw(drawer, index, new RectangleF((float)Math.Cos(angle) * drawer.GameHeight, (float)Math.Sin(angle) * drawer.GameHeight, drawer.GameHeight * 0.1f, drawer.GameHeight * 0.2f), angle);
        }
    }
}
