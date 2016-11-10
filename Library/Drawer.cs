﻿using System;
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

        public Drawer(GameWindow window)
        {
            this.window = window;
            graphics = window.CreateGraphics();
        }

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
            graphics.TranslateTransform(width / 2, height / 2);
            graphics.RotateTransform(rotation);
            graphics.DrawImage(cardBack, x, y, width, height);
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
