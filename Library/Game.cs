using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Game
    {
        private Player current;
        private GameRules rules;
        private GameWindow window;

        public Game(GameRules rules, GameWindow window)
        {
            this.rules = rules;
            Players = new List<Player>();
            Deck = new Deck(rules.DeckSize);
            this.window = window;
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
                // NextTurn(); DEBUG
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

        public void Draw(Graphics graphics)
        {
            float angle = (float)(Math.PI * 2) / Players.Count;
            float hw = window.Width / 2;
            float hh = window.Height / 2;
            float min = Math.Min(window.Width, window.Height);
            for (int i = 0; i < Players.Count; i++)
            {
                float x = hw + (float)Math.Cos(angle * i) * (min * 0.5f - Card.Height / 2);
                float y = hh + (float)Math.Sin(angle * i) * (min * 0.5f - Card.Height / 2);

                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(x, y);

                Players[i].Draw(graphics);

                graphics.Restore(state);
            }
        }
    }
}
