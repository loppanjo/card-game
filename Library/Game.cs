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
        private GameRules rules;

        public Game(GameRules rules)
        {
            this.rules = rules;
            Deck = new Deck(rules.DeckSize);
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
                Deal();
                NextTurn();
            }
        }

        public void AddPlayer(Player player)
        {
            if(!Playing)
                Players.Add(player);
        }

        private void Deal()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < rules.StartCards; j++)
                    Players[i].Hand.Take(Deck.Deal());
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

        public void Draw(Drawer drawer)
        {
            for (int i = 0; i < Players.Count; i++)
                Players[i].Draw(drawer, i, Players.Count);
        }
    }
}
