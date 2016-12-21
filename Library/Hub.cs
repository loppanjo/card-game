using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public class Hub
    {
        public delegate void PlayerConnected(Player player);

        public event PlayerConnected ClientConnectedEvent;

        private TcpListener listener;
        private GameRules gameRules;
        
        public Hub(GameRules gameRules)
        {
            this.gameRules = gameRules;
            Players = new PlayerCollection();
        }
        
        protected PlayerCollection Players { get; private set; }
        public bool ServerStarted { get; set; }
        public bool GameStarted { get; set; }

        // Starta servern på en port
        public void StartServer(int port)
        {
            if (listener != null && ServerStarted)
                listener.Stop();
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            ServerStarted = true;

            // Börja att ta emot clienter
            BeginAccept();
        }

        // Stoppa servern
        public void StopServer()
        {
            listener.Stop();
            ServerStarted = false;
        }
        
        // Vänta på ett speciellt kommand från en spelare
        public Message WaitForCommandFrom(Player player, string command)
        {
            Message message;
            while ((message = player.Client.Receive()).Command != command)
            { }
            return message;
        }
        
        private void BeginAccept()
        {
            listener.BeginAcceptTcpClient(AcceptClient, listener);
        }

        private void AcceptClient(IAsyncResult res)
        {
            // Fortsätt att acceptera clienter rekursivt
            BeginAccept();

            // Kolla om det inte redan finns tillräckligt med spelare och om spelet redan startat
            if (Players.Count < gameRules.MaxPlayers && !GameStarted)
            {
                // Lägg till spelaren
                TcpClient socket = listener.EndAcceptTcpClient(res);
                Player player = Players.Add(socket);

                // Ta emot spelarens namn
                Message message = player.Client.Receive();
                player.Name = (string)message.Data;

                // Meddela spelaren att den är med i spelet
                player.Client.Send(new Message("GAME STATE", "WAITING"));
                player.Client.Send(new Message("DRAW", ""));

                // Kalla på eventet som meddelar lyssnare om en ny spelare
                ClientConnectedEvent(player);
            }
        }
        
    }
}
