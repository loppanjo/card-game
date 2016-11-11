using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class Drawer
    {
        private const float RAD_TO_DEG = 180.0f / (float)Math.PI;

        private static Bitmap cardFaces;
        private static Bitmap cardBack;
        private static int cardWidth;
        private static int cardHeight;

        private GameWindow window;
        private Graphics graphics;
        
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

        public Drawer(GameWindow window, Graphics graphics)
        {
            this.window = window;
            this.graphics = graphics;
            
        }

        public int GameWidth { get { return window.Width; } }
        public int GameHeight { get { return window.Height; } }

        public void DrawCard(Card card, float x, float y, int width, int height, float rotation)
        {
            if (card.Hidden) DrawBack(x, y, width, height, rotation);
            else DrawFace(card, x, y, width, height, rotation);
        }

        public void DrawBack(float x, float y)
        {
            DrawBack(x, y, cardWidth, cardHeight, 0);
        }

        public void DrawBack(float x, float y, int width, int height, float rotation)
        {
            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(-width / 2, -height / 2);
            graphics.RotateTransform(rotation * RAD_TO_DEG);

            graphics.DrawImage(cardBack, x, y, width, height);

            graphics.Restore(state);
        }

        public void DrawFace(Card card, float x, float y)
        {
            DrawFace(card, x, y, cardWidth, cardHeight, 0);
        }

        public void DrawFace(Card card, float x, float y, int width, int height, float rotation)
        {
            int srcX = card.Index * cardWidth;      //Räkna ut vart på Bitmapen kortet finns i x-led.
            int srcY = (int)card.Suit * cardHeight; //Räkna ut vart på Bitmapen kortet finns i y-led.
            //Rita ut ett kort på angiven position.

            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(-width / 2, -height / 2);
            graphics.RotateTransform(rotation * RAD_TO_DEG);
            graphics.DrawImage(
                cardFaces, 
                new Rectangle((int)x, (int)y, width, height), 
                new Rectangle(srcX, srcY, cardWidth, cardHeight), 
                GraphicsUnit.Pixel
            );
            graphics.Restore(state);
        }
    }
}
