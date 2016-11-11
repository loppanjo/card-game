using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        private void menuItemNewGame_Click(object sender, EventArgs e)
        {
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
                    //IF error
                    //continue;
                    //ELSE
                    //break;
                }
                else
                    break;
            }
            menuItemConnect.Enabled = false;
            menuItemDisconnect.Enabled = true;
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
    }
}
