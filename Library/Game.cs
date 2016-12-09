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
        public delegate void PlayerConnected(HubCallerContext context);
        
        public static event PlayerConnected PlayerConnectedEvent;
        public static event PlayerConnected PlayerDisconnectedEvent;

        private GameRules rules;

        public Game(GameRules rules)
        {
            this.rules = rules;
            Players = new List<Player>();
            Deck = new Deck(rules.DeckSize);
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
            if (!Playing && Players.Count < rules.MaxPlayers)
            {
                Players.Add(player);
                Clients.AllExcept(player.ClientId).OpponentConnect(new Opponent(player));
            }
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

        private void SyncPlayers()
        {
            
        }

        private void Deal()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < rules.StartCards; j++)
                    Players[i].Hand.Take(Deck.Deal());
                Clients.User(Players[i].ClientId).Deal(Players[i].Hand.All);
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
    }
}
