using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GameRules
    {
        public int StartCards { get; set; } = 3;
        public int DeckSize { get; set; } = 52;
        public int MaxPlayers { get; set; } = 4;
        public int MinPlayers { get; set; } = 2;
    }
}
