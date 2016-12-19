using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public abstract class Game : Hub
    {
        private GameRules rules;
        private Thread gameThread;

        public Game(GameRules rules)
            : base(rules)
        {
            this.rules = rules;
            Deck = new Deck(rules.DeckSize);
            gameThread = new Thread(GameThread);
            gameThread.Start();
        }
        
        protected Deck Deck { get; set; }
        protected Player CurrentPlayer { get; set; }
        public bool GoAgain { get; set; }

        public int CurrentTurn { get; set; }

        public void StartGame()
        {
            // Starta spel om antalet spelare är större eller lika med minsta antal spelare
            if (Players.Count >= rules.MinPlayers)
                GameStarted = true;
        }

        // En tråd som hanterar själva spelet/regler
        public void GameThread()
        {
            while (true)
            {
                Thread.Sleep(500);
                if (GameStarted)
                {
                    Players.All(new Message("GAME STATE", "STARTED"));
                    Deal();
                    NextTurn();  
                }
            }
        }
        
        // Synka alla spelare så de kan se hur många kort de har
        private void SyncPlayers()
        {
            for (int i = 0; i < Players.Count; i++)
                Players[i].Client.Send(new Message("SET PLAYERS", Players.GetAllExept(Players[i])));
            Players.All(new Message("DECK SIZE", Deck.Count));
        }

        // Dela ut kort till alla spelare
        private void Deal()
        {
            Deck.Shuffle();
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < rules.StartCards; j++)
                    Players[i].Hand.Take(Deck.Deal());
                Players[i].Client.Send(new Message("SET HAND", Players[i].Hand.All));
            }
        }

        // Själva "loopen" för hela spelet
        protected void NextTurn()
        {
            // Kolla om spelet är startat
            if (!GameStarted) return;

            // Synka alla spelare
            SyncPlayers();

            // Sätt nuvarande spelare
            CurrentPlayer = Players[CurrentTurn];

            // Skicka till alla andra vems tur det är
            Players.AllExept(CurrentPlayer, new Message("GAME STATE", CurrentPlayer.Name + "'s TURN"));
            
            // Sätt "spela igen" till false
            GoAgain = false;

            // Kör den abstrakta funktionen för själva spelreglerna
            Turn(CurrentPlayer);

            // Öka bara vilken tur det är om nuvarande spelare inte får spela igen
            if(!GoAgain)
                CurrentTurn++;

            // Sätt turen till noll om den gått igenom alla spelare
            if (CurrentTurn > Players.Count - 1)
                CurrentTurn = 0;

            // Kör nästa tur rekursivt
            NextTurn();
        }

        public abstract void Turn(Player player);
    }
}
