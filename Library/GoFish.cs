using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class GoFish : Game
    {
        public GoFish(GameRules rules)
            : base(rules)
        { }
        
        public override void Turn(Player player)
        {
            // Låt nuvarande spelare fråga en spelare efter ett kort
            player.Client.Send(new Message("GAME STATE", "YOU ASK"));

            player.Client.Send(new Message("DRAW", ""));

            // Vänta på svar från spelare
            Message message = WaitForCommandFrom(player, "ASK");
            Ask ask = (Ask)message.Data;

            // Ta alla kort från spelaren som blir frågad
            List<Card> cards = Players.TakeFrom(ask);

            // Kolla om spelaren faktiskt hade några kort
            if (cards.Count > 0)
            {
                // Spelaren får köra igen om frågade spelaren hade ett kort
                GoAgain = true;

                // Ge korten till spelaren
                player.Hand.TakeAll(cards);
                if (player.Hand.Count == 0 && Deck.Count > 0)
                {
                    Card card = Deck.Deal();
                    player.Hand.Take(card);
                    cards.Add(card);
                }
                player.Client.Send(new Message("ADD CARDS", cards));
            }
            else if(Deck.Count > 0)
            {
                // Om den frågade spelaren inte hade några kort så får 
                // nuvarande spelaren ta ett kort från sjön
                player.Client.Send(new Message("GAME STATE", "GO FISH"));
                player.Client.Send(new Message("DRAW", ""));
                WaitForCommandFrom(player, "FISH");

                Card card = Deck.Deal();

                player.Hand.Take(card);
                player.Client.Send(new Message("ADD CARD", card));
            }
            
            // Kolla om det finnns kort kvar
            GameStarted = Deck.Count > 0;

            if (!GameStarted)
            {
                // Hitta spelaren med mest par
                Player best = null;
                for (int i = 0; i < Players.Count; i++)
                {
                    if (best == null || Players[i].Hand.Pairs.Count > best.Hand.Pairs.Count)
                        best = Players[i];
                }

                // Meddela alla spelare vem som vunnit
                Players.AllExept(best, new Message("GAME STATE", best.Name + " WIN!"));
                best.Client.Send(new Message("GAME STATE", "YOU WIN"));
            }
        }
    }
}
