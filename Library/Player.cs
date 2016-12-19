using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library
{
    public class Player
    {
        public Player() { }

        public Player(string name)
        {
            Name = name;
            Hand = new Hand();
            Client = new Client(name);
        }

        public Player(TcpClient client)
        {
            Hand = new Hand();
            Client = new Client(client);
            IP = client.Client.RemoteEndPoint.ToString();
        }

        public string Name { get; set; }
        public string IP { get; set; }

        public Hand Hand { get; set; }

        [XmlIgnore]
        public Client Client { get; set; }
        public bool Opponent { get; set; }

        public void Draw(Graphics graphics)
        {
            Hand.Draw(graphics);
        }
    }
}
