using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class Drawer
    {
        private static Bitmap cardFaces;
        private static Bitmap cardBack;
        private static int cardWidth;
        private static int cardHeight;

        private GameWindow window;

        static Drawer()
        {
            try
            {
                cardFaces = new Bitmap("resources/images/cardfaces.png");
                cardBack = new Bitmap("resources/images/cardback.png");
                cardWidth = cardFaces.Width / 13;
                cardHeight = cardFaces.Height / 4;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Drawer(GameWindow window)
        {
            this.window = window;
        }

        public void DrawCard(Graphics graphics, Card card, float x, float y, int width, int height, float rotation)
        {
            if (card.Hidden) DrawBack(graphics, x, y, width, height, rotation);
            else DrawFace(graphics, card, x, y, width, height, rotation);
        }

        public void DrawBack(Graphics graphics, float x, float y)
        {
            DrawBack(graphics, x, y, cardWidth, cardHeight, 0);
        }

        public void DrawBack(Graphics graphics, float x, float y, int width, int height, float rotation)
        {
            graphics.TranslateTransform(width / 2, height / 2);
            graphics.RotateTransform(rotation);
            graphics.DrawImage(cardBack, x, y, width, height);
        }

        public void DrawFace(Graphics graphics, Card card, float x, float y)
        {
            DrawFace(graphics, card, x, y, cardWidth, cardHeight, 0);
        }

        public void DrawFace(Graphics graphics, Card card, float x, float y, int width, int height, float rotation)
        {
            int srcX = card.Index * cardWidth;      //Räkna ut vart på Bitmapen kortet finns i x-led.
            int srcY = (int)card.Suit * cardHeight; //Räkna ut vart på Bitmapen kortet finns i y-led.
            //Rita ut ett kort på angiven position.

            graphics.TranslateTransform(width / 2, height / 2);
            graphics.RotateTransform(rotation);
            graphics.DrawImage(
                cardFaces, 
                new Rectangle((int)x, (int)y, width, height), 
                new Rectangle(srcX, srcY, cardWidth, cardHeight), 
                GraphicsUnit.Pixel
            );
        }
    }
}
