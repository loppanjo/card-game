using Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Game : Form
    {
        public const float RAD_TO_DEG = 180.0f / (float)Math.PI;
        
        private Player player;
        private List<Player> players;
        private Graphics graphics;

        private string name = "no name";
        private string prevGameState = "NONE";
        private string gameState = "NONE";
        private int decksize = 0;

        private Player selectedPlayer = null;
        private Card selectedCard = null;
        private Rectangle selectedCardRect;

        private const int SEED = 1337;
        
        public Game()
        {
            InitializeComponent();
            players = new List<Player>();

            // Fråga efter spelarens namn
            name = Interaction.InputBox("Enter your name", "Player Name", "no name");

            player = new Player(name);
            player.Client.ReceivedMessageEvent += Client_ReceivedMessageEvent;

            graphics = panelBoard.CreateGraphics();
        }

        private void Client_ReceivedMessageEvent(Library.Message message)
        {
            // Skriv ut kommandet för felsökning
            Console.WriteLine(message.Command);

            // Kör kommandet
            switch (message.Command)
            {
                case "GAME STATE":
                    prevGameState = gameState;
                    gameState = (string)message.Data;
                    break;
                case "SET PLAYERS":
                    players = (List<Player>)message.Data;
                    break;
                case "ADD PLAYER":
                    players.Add((Player)message.Data);
                    break;
                case "SET HAND":
                    player.Hand.Set((List<Card>)message.Data);
                    break;
                case "ADD CARDS":
                    player.Hand.TakeAll((List<Card>)message.Data);
                    break;
                case "ADD CARD":
                    player.Hand.Take((Card)message.Data);
                    break;
                case "REMOVE ALL":
                    player.Hand.GiveAll((int)message.Data);
                    break;
                case "DECK SIZE":
                    decksize = (int)message.Data;
                    break;
                case "DRAW":
                    panelBoard.Invalidate();
                    break;
                default:
                    break;
            }

            // Göm alla motståndares kort
            //for (int i = 0; i < players.Count; i++)
            //    players[i].Hand.HideAll();

            // Uppdatera skärmen
            
        }

        private void menuItemNewGame_Click(object sender, EventArgs e)
        {
            //client.Send("HEJSAN");
            /*
             * Start a server and connect
             */
        }

        private void menuItemConnect_Click(object sender, EventArgs e)
        {
            Connect connForm = new Connect();
            while (true)
            {
                if (connForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Anslut till servern
                        player.Client.Connect(connForm.Ip, connForm.Port);

                        // Skicka spelarens namn till servern
                        player.Client.Send(new Library.Message("SET NAME", name));

                        menuItemConnect.Enabled = false;
                        menuItemDisconnect.Enabled = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                else
                    break;
            }
        }

        private void menuItemDisconnect_Click(object sender, EventArgs e)
        {
            // Koppla från spelaren från servern
            player.Client.Disconnect();
            menuItemConnect.Enabled = true;
            menuItemDisconnect.Enabled = false;
        }

        private void menuItemQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Client.Disconnect();
        }

        public void Draw()
        {
            // Sudda allt på skärmen
            graphics.Clear(Color.Green);

            // Hitta den minsta dimensionen av skärmen
            float min = Math.Min(panelBoard.Width, panelBoard.Height);

            // Rita sjön
            DrawPond(min);

            // Start-vinkeln är 270 grader för att spelaren alltid ska vara längst ner på skärmen
            const float startAngle = (float)(Math.PI / 2);

            // Räkna ut vinkeln mellan varje spelare (360 / antal spelare)
            float angle = (float)(Math.PI * 2) / (players.Count + 1);

            // Räkna ut mitten på skärmen
            float hw = panelBoard.Width / 2;
            float hh = panelBoard.Height / 2;
            
            // Rita alla motståndare
            for (int i = 0; i < players.Count; i++)
                DrawPlayer(players[i], graphics, startAngle + angle * (i + 1), hw, hh, min);

            // Rita spelaren
            DrawPlayer(player, graphics, startAngle, hw, hh, min);

            // Visa spelets nuvarande "tillstånd"
            GameMessage.Show(gameState, panelBoard.Width / 2, 5, graphics);
        }

        // Rita sjön
        private void DrawPond(float min)
        {
            // Skapa en "Random" instans med ett "frö"
            Random random = new Random(SEED);

            // Skapa ett instans av ett gömt kort
            Card card = new Card();
            card.Hidden = true;

            // Rita bara så många kort som det finns i kortleken på servern
            for (int i = 0; i < decksize; i++)
            {
                // Hitta en slumpmässig vinkel mellan 0 och 2 PI (mellan 0 och 360 grader fast i radianer)
                float angle = (float)Math.PI * 2 * (float)random.NextDouble();

                // Hitta en slumpmässig radius mellan 0 och minsta skärm-dimensionen
                float radius = min * 0.2f * (float)random.NextDouble();

                // Spara tillståndet av grafiken innan ritning
                GraphicsState state = graphics.Save();

                // Transformera hela grafiken till mitten av skärmen och till 
                // den slumpmässiga positionen med hjälp av de polära koordinaterna
                graphics.TranslateTransform(
                    panelBoard.Width / 2 + (float)Math.Cos(angle) * radius,
                    panelBoard.Height / 2 + (float)Math.Sin(angle) * radius
                );

                // Rita kortet
                card.Draw(graphics);

                // Återställ grafiken som det var innan
                graphics.Restore(state);
            }
        }

        // Rita en spelare
        private void DrawPlayer(Player player, Graphics graphics, float angle, float hw, float hh, float min)
        {
            // Räkna ut vart spelaren ska ritas med hjälp av polära koordinater
            float x = hw + (float)Math.Cos(angle) * (min * 0.45f - Card.Height / 2);
            float y = hh + (float)Math.Sin(angle) * (min * 0.45f - Card.Height / 2);

            // Spara tillståndet av grafiken innan ritning
            GraphicsState state = graphics.Save();

            // Transformera hela grafiken till spelarens position på skärmen
            graphics.TranslateTransform(x, y);

            // Räkna ut skillnaden mellan mitten på skärmen och spelaren
            float dx = x - hw;
            float dy = y - hh;

            // Transformera hela grafiken till mitten av ett kort
            graphics.TranslateTransform(Card.Width / 2, Card.Height / 2);

            // Rotera grafiken så korten pekar in mot mitten
            graphics.RotateTransform((float)Math.Atan2(dy, dx) * RAD_TO_DEG + 90);

            // Transformera hela grafiken tillbaka till kanten av ett kort
            graphics.TranslateTransform(-Card.Width / 2, -Card.Height / 2);

            // Rita spelaren
            player.Draw(graphics, panelBoard.Width);

            // Återställ grafiken som den var innan
            graphics.Restore(state);

            if (selectedCard != null)
            {
                graphics.DrawRectangle(Pens.Red, selectedCardRect);
            }
        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void panelBoard_MouseClick(object sender, MouseEventArgs e)
        {
            // Kolla om spelaren ska "fiska", om sant så skickar den ett svar till servern
            if (gameState == "GO FISH") player.Client.Send(new Library.Message("FISH", new Card()));

            // Kolla så det verkligen är spelarens tur att fråga
            if (gameState != "YOU ASK") return;
            
            // Räkna ut alla värden för att ta reda på vart spelaren har sina kort
            const float startAngle = (float)(Math.PI / 2);
            float hw = panelBoard.Width / 2;
            float hh = panelBoard.Height / 2;
            float min = Math.Min(panelBoard.Width, panelBoard.Height);
            float x1 = hw + (float)Math.Cos(startAngle) * (min * 0.45f - Card.Height / 2) -
                       player.Hand.HandWidth / 2;
            float y1 = hh + (float)Math.Sin(startAngle) * (min * 0.45f - Card.Height / 2) - Card.Height / 2;
            float clickAngle = NormalizeAngle((float)Math.Atan2(e.Y - hh, e.X - hw));
            float angle = (float)(Math.PI * 2) / (players.Count + 1);
            
            for (int i = 0; i < players.Count; i++)
            {
                float a = NormalizeAngle(startAngle / (players.Count + 1) + angle * (i + 1));
                float b = a + angle;
                if (clickAngle > a && clickAngle < b)
                {
                    selectedPlayer = players[i];
                    lblSelectedPlayer.Text = $"Player to ask: { selectedPlayer.Name }";
                    break;
                }
            }

            // Kör igenom alla kort i spelarens hand
            for (int i = 0; i < player.Hand.Cards.Count; i++)
            {
                // Gör en rektangel för ett kort i handen
                Rectangle cardRect = new Rectangle((int)x1 + Card.Width * i, (int)y1, Card.Width, Card.Height);

                // Kolla om musen är innanför kortet när spelaren klickar
                if (e.X > cardRect.X && e.Y > cardRect.Y &&
                    e.X < cardRect.X + cardRect.Width &&
                    e.Y < cardRect.Y + cardRect.Height)
                {
                    selectedCard = player.Hand.Cards[i];
                    selectedCardRect = cardRect;
                    panelBoard.Invalidate();
                    break;
                }
            }
        }

        private float NormalizeAngle(float angle)
        {
            while (angle < 0) angle += (float)Math.PI * 2;
            while (angle > Math.PI * 2) angle -= (float)Math.PI * 2;
            return angle;
        }

        private void panelBoard_Resize(object sender, EventArgs e)
        {
            // Uppdatera grafiken varje gång fönstret ändras
            graphics = panelBoard.CreateGraphics();
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null && selectedCard != null)
            {
                // Skicka "frågan" om korten till servern
                player.Client.Send(new Library.Message("ASK", new Ask(selectedPlayer.IP, selectedCard)));

                selectedPlayer = null;
                selectedCard = null;
            }
        }
    }
}
