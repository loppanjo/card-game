using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        private static Bitmap faces;
        private static Bitmap back;
        
        public static int Width { get; set; }
        public static int Height { get; set; }
        
        static Card()
        {
            try
            {
                faces = new Bitmap("resources/images/cardfaces.png");
                back = new Bitmap("resources/images/cardback.png");
                Width = faces.Width / 13;
                Height = faces.Height / 4;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        public int Index { get; set; }
        public int Value { get; set; }
        public Suit Suit { get; set; }

        [XmlIgnore]
        public PointF Position { get; set; }

        [XmlIgnore]
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
            graphics.TranslateTransform(-Width / 2, -Height / 2, MatrixOrder.Append);
            //graphics.RotateTransform(rotation * RAD_TO_DEG, MatrixOrder.Append);

            graphics.DrawImage(back, 0, 0, Width, Height);

            graphics.Restore(state);
        }

        private void DrawFace(Graphics graphics, float rotation)
        {
            int srcX = Index * Width;      //Räkna ut vart på Bitmapen kortet finns i x-led.
            int srcY = (int)Suit * Height; //Räkna ut vart på Bitmapen kortet finns i y-led.
            //Rita ut ett kort på angiven position.

            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(-Width / 2, -Height / 2, MatrixOrder.Append);
            //graphics.RotateTransform(rotation * RAD_TO_DEG, MatrixOrder.Append);
            graphics.DrawImage(
                faces,
                new Rectangle(0, 0, Width, Height),
                new Rectangle(srcX, srcY, Width, Height),
                GraphicsUnit.Pixel
            );
            graphics.Restore(state);
        }
    }
}