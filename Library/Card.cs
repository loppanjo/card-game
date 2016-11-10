using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Index { get; set; }
        public int Value { get; private set; }
        public Suit Suit { get; private set; }

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

        public override string ToString()
        {
            return $"{Suit} {Value}";
        }
    }
}