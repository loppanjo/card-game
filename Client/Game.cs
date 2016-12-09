﻿using Library;
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
        Library.Client client;
        GoFish shithead;

        public Game()
        {
            InitializeComponent();

            client = new Library.Client("Frosberg", "http://192.168.205.39:3000");
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
    }
}