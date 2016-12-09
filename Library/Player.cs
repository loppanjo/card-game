using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            Hand = new Hand();
        }

        public Player(HubCallerContext context)
        {
            Name = "Not Set";
            Hand = new Hand();
            ClientId = context.ConnectionId;
        }

        public string Name { get; set; }
        public Hand Hand { get; private set; }
        public string ClientId { get; set; }
        public bool Opponent { get; set; }

        public void Draw(Graphics graphics)
        {
            Hand.Draw(graphics);
        }
    }
}
