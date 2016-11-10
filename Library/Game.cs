using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Game
    {
        private Player current;

        public Game()
        {
            
        }
        
        protected List<Player> Players { get; set; }
        protected Deck Deck { get; set; }

        public int TurnCount { get; set; } = 1;
        public bool Playing { get; set; }

        public void Start()
        {
            if (Players.Count > 0)
            {
                Playing = true;
                NextTurn();
            }
        }

        private void NextTurn()
        {
            current = Players[TurnCount - 1];
            Turn(current);
            TurnCount++;
            if (Playing) NextTurn();
        }

        public abstract void Turn(Player player);
    }
}
