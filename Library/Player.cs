using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Player
    {
        public Player()
        {
            Hand = new Hand();
        }

        public string Name { get; set; }
        public Hand Hand { get; private set; }
    }
}
