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

namespace Server
{
	public partial class ServerForm : Form
	{
		private bool running;
		private IDisposable SignalR { get; set; }

		public ServerForm()
		{
			InitializeComponent();
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
			catch (TargetInvocationException)
			{
				WriteToConsole("Server failed to start. Either a server is already running on " + tbServerURI.Text + ", or the URI is invalid.");
				//Re-enable button to let user try  
				//to start server again 
				this.Invoke((Action)(() =>
				{
					running = false;
					btToggleServer.Text = "Start Server";
					btToggleServer.Enabled = true;
					tbServerURI.Enabled = true;
				}));
				return;
			}
			this.Invoke((Action)(() =>
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
				this.Invoke((Action)(() =>
						WriteToConsole(message)
				));
				return;
			}
			rtbConsole.AppendText(message + Environment.NewLine);
		}
		internal void AddConnection(string s)
		{
			WriteToConsole("Client connected: " + s);
			lbxConnections.Items.Add(s);
			gbConnections.Text = "Connections (" + lbxConnections.Items.Count + ")";
		}
		internal void RemoveConnection(string s)
		{
			WriteToConsole("Client disconnected: " + s);
			lbxConnections.Items.Remove(s);
			gbConnections.Text = "Connections (" + lbxConnections.Items.Count + ")";
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
	public class MyHub : Hub
	{
		public void Send(string name, string message)
		{
			Clients.All.addMessage(name, message);
		}
		public override Task OnConnected()
		{
			base.Clients.Client(Context.ConnectionId);
			Program.MainForm.AddConnection(Context.ConnectionId);
			return base.OnConnected();
		}
		public override Task OnDisconnected(bool stopCalled)
		{
			Program.MainForm.RemoveConnection(Context.ConnectionId);
			return base.OnDisconnected(stopCalled);
		}
	}
}
