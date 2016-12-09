using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Game : Form
    {
        public const float RAD_TO_DEG = 180.0f / (float)Math.PI;

        Library.Client client;
        List<Player> players;

        GoFish shithead;

        public Game()
        {
            InitializeComponent();

            client = new Library.Client("Frosberg", "http://192.168.204.149:3000");
            client.Connect();
            
            shithead = new GoFish(new GameRules(), gameWindow1);
            gameWindow1.Game = shithead;
        }

        private void menuItemNewGame_Click(object sender, EventArgs e)
        {
            client.Send("HEJSAN");
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
                    //Use library method to connect
                    //IF no error
                    menuItemConnect.Enabled = false;
                    menuItemDisconnect.Enabled = true;
                    //continue;
                    //ELSE
                    //break;
                }
                else
                    break;
            }
        }

        private void menuItemDisconnect_Click(object sender, EventArgs e)
        {
            //Use library method to disconnect
            menuItemConnect.Enabled = true;
            menuItemDisconnect.Enabled = false;
        }

        private void menuItemQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
        }

        private void gameWindow1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Draw(Graphics graphics)
        {
            float angle = (float)(Math.PI * 2) / players.Count;
            float hw = gameWindow1.Width / 2;
            float hh = gameWindow1.Height / 2;
            float min = Math.Min(gameWindow1.Width, gameWindow1.Height);
            for (int i = 0; i < players.Count; i++)
            {
                float x = hw + (float)Math.Cos(angle * i) * (min * 0.45f - Card.Height / 2);
                float y = hh + (float)Math.Sin(angle * i) * (min * 0.45f - Card.Height / 2);

                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(x, y);

                float dx = x - hw;
                float dy = y - hh;

                graphics.TranslateTransform(Card.Width / 2, Card.Height / 2);
                graphics.RotateTransform((float)Math.Atan2(dy, dx) * RAD_TO_DEG + 90);
                graphics.TranslateTransform(-Card.Width / 2, -Card.Height / 2);

                players[i].Draw(graphics);

                graphics.Restore(state);
            }
        }
    }
}
