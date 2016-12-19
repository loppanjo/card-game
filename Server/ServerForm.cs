using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System.Reflection;
using System.Security.Principal;
using Library;

namespace Server
{
	public partial class ServerForm : Form
	{
		private bool running;
		private GoFish game;

		public ServerForm()
		{
			InitializeComponent();

			game = new GoFish(new GameRules());
            game.ClientConnectedEvent += Game_ClientConnectedEvent;
		}

        private void Game_ClientConnectedEvent(Player player)
        {
            WriteToConsole($"Player { player.Name } connected!");
        }

        private void btToggleServer_Click(object sender, EventArgs e)
		{
			if (running)
			{
                game.StopServer();
				// stänger ner serverfönstret
				Close();
			}
			else
			{
				WriteToConsole("Starting server...");
				btToggleServer.Enabled = false;
                numPort.Enabled = false;
                game.StartServer((int)numPort.Value);
            }
		}
        
		internal void WriteToConsole(string message)
		{
			if (rtbConsole.InvokeRequired)
			{
				Invoke((Action)(() =>
		            WriteToConsole(message)
                ));
				return;
			}
			rtbConsole.AppendText(message + Environment.NewLine);
		}

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            game.StartGame();
        }
    }
}
