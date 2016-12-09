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
		private IDisposable SignalR { get; set; }

		public ServerForm()
		{
			InitializeComponent();
			if (!(new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator))
			{
				MessageBox.Show("Run as administrator!");
				Close();
			}
			Game.PlayerConnectedEvent += Game_PlayerConnectedEvent;
			Game.PlayerDisconnectedEvent += Game_PlayerDisconnectedEvent;

			game = new GoFish(new GameRules());
		}

		private void Game_PlayerDisconnectedEvent(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context)
		{
			RemoveConnection(context.ConnectionId);
		}

		private void Game_PlayerConnectedEvent(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context)
		{
			AddConnection(context.ConnectionId);
		}

		private void btToggleServer_Click(object sender, EventArgs e)
		{
			if (running)
			{
				// stänger ner serverfönstret
				Close();
			}
			else
			{
				WriteToConsole("Starting server...");
				btToggleServer.Enabled = false;
				tbServerURI.Enabled = false;
				Task.Run(() => StartServer());
			}
		}

		private void StartServer()
		{
			try
			{
				SignalR = WebApp.Start(tbServerURI.Text);
			}
			catch (TargetInvocationException e)
			{
				WriteToConsole(e.ToString());
				WriteToConsole("Server failed to start. Either a server is already running on " + tbServerURI.Text + ", or the URI is invalid.");
				//Re-enable button to let user try  
				//to start server again 
				Invoke((Action)(() =>
{
	running = false;
	btToggleServer.Text = "Start Server";
	btToggleServer.Enabled = true;
	tbServerURI.Enabled = true;
}));
				return;
			}
			Invoke((Action)(() =>
{
	running = true;
	btToggleServer.Text = "Stop Server";
	btToggleServer.Enabled = true;
}));
			WriteToConsole("Server started at " + tbServerURI.Text);
		}

		internal void WriteToConsole(String message)
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

		internal void groupBoxAdd(string s)
		{
			if (lbxConnections.InvokeRequired || gbConnections.InvokeRequired)
			{
				Invoke((Action)(() =>
		groupBoxAdd(s)
));
				return;
			}
			lbxConnections.Items.Add(s);
			gbConnections.Text = "Connections (" + lbxConnections.Items.Count + ")";
		}

		internal void groupBoxRemove(string s)
		{
			if (lbxConnections.InvokeRequired || gbConnections.InvokeRequired)
			{
				Invoke((Action)(() =>
		groupBoxRemove(s)
));
				return;
			}
			lbxConnections.Items.Remove(s);
			gbConnections.Text = "Connections (" + lbxConnections.Items.Count + ")";
		}

		internal void AddConnection(string s)
		{
			WriteToConsole("Client connected: " + s);
			groupBoxAdd(s);
		}

		internal void RemoveConnection(string s)
		{
			WriteToConsole("Client disconnected: " + s);
			groupBoxRemove(s);
		}

		private void WinFormsServer_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (SignalR != null)
			{
				SignalR.Dispose();
			}
		}
	}

	class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCors(CorsOptions.AllowAll);
			app.MapSignalR();
		}
	}
}
