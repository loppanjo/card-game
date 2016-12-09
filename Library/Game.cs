using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Game : Hub
    {
        public const float RAD_TO_DEG = 180.0f / (float)Math.PI;

        public delegate void PlayerConnected(HubCallerContext context);
        
        public static event PlayerConnected PlayerConnectedEvent;
        public static event PlayerConnected PlayerDisconnectedEvent;

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
        protected Player CurrentPlayer { get; set; }

        public int CurrentTurn { get; set; }
        public bool Playing { get; set; }

        public void Start()
        {
            if (Players.Count >= rules.MinPlayers)
            {
                Playing = true;
                Deal();
                NextTurn();
            }
        }

        public void AddPlayer(Player player)
        {
            if(!Playing && Players.Count < rules.MaxPlayers)
                Players.Add(player);
        }

        public override Task OnConnected()
        {
            base.Clients.Client(Context.ConnectionId);
            AddPlayer(new Player(Context));
            PlayerConnectedEvent(Context);
            //Program.MainForm.AddConnection(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            PlayerDisconnectedEvent(Context);
            return base.OnDisconnected(stopCalled);
        }

        private void Deal()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < rules.StartCards; j++)
                    Players[i].Hand.Take(Deck.Deal());
                Clients.User(Players[i].ClientId).Deal(Players[i].Hand.);
            }
        }

        protected void NextTurn()
        {
            if (!Playing) return;
            CurrentPlayer = Players[CurrentTurn];
            Clients.User(CurrentPlayer.ClientId).YourTurn();
            Turn(CurrentPlayer);
            CurrentTurn++;
            if (CurrentTurn > Players.Count - 1)
                CurrentTurn = 0;
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
                float x = hw + (float)Math.Cos(angle * i) * (min * 0.45f - Card.Height / 2);
                float y = hh + (float)Math.Sin(angle * i) * (min * 0.45f - Card.Height / 2);

                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(x, y);

                float dx = x - hw;
                float dy = y - hh;

                graphics.TranslateTransform(Card.Width / 2, Card.Height / 2);
                graphics.RotateTransform((float)Math.Atan2(dy, dx) * RAD_TO_DEG + 90);
                graphics.TranslateTransform(-Card.Width / 2, -Card.Height / 2);

                Players[i].Draw(graphics);

                graphics.Restore(state);
            }
        }
    }
}
