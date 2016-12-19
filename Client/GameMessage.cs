using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class GameMessage
    {
        private static Font font;

        static GameMessage()
        {
            font = new Font("Arial", 16);
        }

        // Rita en text
        public static void Show(string text, float x, float y, Graphics graphics)
        {
            graphics.DrawString(text, font, Brushes.Black, x - graphics.MeasureString(text, font).Width / 2, y);
        }
    }
}
