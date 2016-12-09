using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Opponent
    {
        public Opponent(Player player)
        {
            Name = player.Name;
            CardsInhand = player.Hand.Count;
            ClientId = player.ClientId;
        }

        public string Name { get; set; }
        public int CardsInhand { get; private set; }
        public string ClientId { get; set; }
    }
}
