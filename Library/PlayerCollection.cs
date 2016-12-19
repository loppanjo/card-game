using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class PlayerCollection
    {
        private List<Player> players = new List<Player>();

        public int Count { get { return players.Count; } }

        public Player this[int index] { get { return players[index]; } }

        public Player Add(TcpClient socket)
        {
            Player player = new Player(socket);
            players.Add(player);
            return player;
        }

        // Skicka ett meddelande till alla spelare
        public void All(Message message)
        {
            for (int i = 0; i < players.Count; i++)
                players[i].Client.Send(message);
        }

        // Skicka ett meddelande till alla förutom en spelare
        public void AllExept(Player player, Message message)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] != player)
                    players[i].Client.Send(message);
            }
        }

        // Returnera alla spelare förutom en
        public List<Player> GetAllExept(Player player)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < this.players.Count; i++)
            {
                if (this.players[i] != player)
                    players.Add(this.players[i]);
            }
            return players;
        }

        // Ta alla kort från en frågad spelare
        public List<Card> TakeFrom(Ask ask)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].IP == ask.Player)
                {
                    players[i].Client.Send(new Message("REMOVE ALL", ask.Card.Value));
                    return players[i].Hand.GiveAll(ask.Card.Value);
                }
            }
            return new List<Card>();
        }

        private void SendMessage(Client client, Message message)
        {
            client.Send(message);
        }
    }
}