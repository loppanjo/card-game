using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
			game.ClientDisconnectedEvent += Game_ClientDisconnectedEvent;
			game.WriteToConsoleEvent += Game_WriteToConsoleEvent;
		}

		private void Game_ClientConnectedEvent(Player player)
		{
			WriteToConsole($"Player { player.Name } connected!");
		}
		private void Game_ClientDisconnectedEvent(Player player)
		{
			WriteToConsole($"Player { player.Name } disconnected!");
		}
		private void Game_WriteToConsoleEvent(string text)
		{
			WriteToConsole(text);
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
				
				if (game.StartServer((int)numPort.Value))
				{
					btToggleServer.Enabled = false;
					numPort.Enabled = false;
					WriteToConsole("Server Started.");
				}
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

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.StopServer();
        }
    }
}
