using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GoFish : Game
    {
        private bool goAgain;

        public GoFish(GameRules rules, GameWindow window)
            : base(rules, window)
        {

        }

        public void AskForCard(Card card, Player player)
        {
            if (Context.ConnectionId == CurrentPlayer.ClientId &&
                player.ClientId != CurrentPlayer.ClientId)
            {
                List<Card> cards = player.Hand.GiveAll(card);
                CurrentPlayer.Hand.TakeAll(cards);

                if (cards.Count > 0) goAgain = true;
                else NextTurn();
            }
        }

        public override void Turn(Player player)
        {
            Playing = Deck.Count > 0;
        }
    }
}
