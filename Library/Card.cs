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
    public enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    public class Card
    {
        private const float RAD_TO_DEG = 180.0f / (float)Math.PI;

        private static Bitmap faces;
        private static Bitmap back;
        private static int width;
        private static int height;
        
        static Card()
        {
            try
            {
                faces = new Bitmap("resources/images/cardfaces.png");
                back = new Bitmap("resources/images/cardback.png");
                width = faces.Width / 13;
                height = faces.Height / 4;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Tom konstruktor för att kunna skapa blanka kort.
        public Card() { }

        //Konstruktor som tar emot ett argument för att bestämma vad kortet ska få för värde & valör.
        public Card(int index)
        {
            // Index modul 13 ger ett tal mellan 0-12.
            Index = (index % 13);

            // Värdet ska vara 1-13. Då tas Index plus ett.
            Value = Index + 1;

            // Det finns fyra valörer(suit) därför anges valör genom index modul 4.
            Suit = (Suit)(index % 4); 
        }

        public int Index { get; private set; }
        public int Value { get; private set; }
        public Suit Suit { get; private set; }

        public bool Hidden { get; set; }

        public override string ToString()
        {
            return $"{Suit} {Value}";
        }

        public void Draw(Graphics graphics)
        {
            if (Hidden)
                DrawBack(graphics, 0);
            else
                DrawFace(graphics, 0);
        }

        public void DrawBack(Graphics graphics, float rotation)
        {
            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(-width / 2, -height / 2, MatrixOrder.Append);
            graphics.RotateTransform(rotation * RAD_TO_DEG, MatrixOrder.Append);

            graphics.DrawImage(back, 0, 0, width, height);

            graphics.Restore(state);
        }

        private void DrawFace(Graphics graphics, float rotation)
        {
            int srcX = Index * width;      //Räkna ut vart på Bitmapen kortet finns i x-led.
            int srcY = (int)Suit * height; //Räkna ut vart på Bitmapen kortet finns i y-led.
            //Rita ut ett kort på angiven position.

            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(-width / 2, -height / 2, MatrixOrder.Append);
            graphics.RotateTransform(rotation * RAD_TO_DEG, MatrixOrder.Append);
            graphics.DrawImage(
                faces,
                new Rectangle(0, 0, width, height),
                new Rectangle(srcX, srcY, width, height),
                GraphicsUnit.Pixel
            );
            graphics.Restore(state);
        }
    }
}