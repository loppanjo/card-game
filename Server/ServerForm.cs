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
		private IDisposable SignalR { get; set; }
		const string ServerURI = "http://localhost:42069";

		public ServerForm()
		{
			InitializeComponent();
		}

		private void btStartServer_Click(object sender, EventArgs e)
		{
			WriteToConsole("Starting server...");
			btStartServer.Enabled = false;
			Task.Run(() => StartServer());
		}
		private void btStopServer_Click(object sender, EventArgs e)
		{
			// stänger ner servern
			Close();
		}
		private void StartServer()
		{
			try
			{
				SignalR = WebApp.Start(ServerURI);
			}
			catch (TargetInvocationException)
			{
				WriteToConsole("Server failed to start. A server is already running on " + ServerURI);
				//Re-enable button to let user try  
				//to start server again 
				this.Invoke((Action)(() => btStartServer.Enabled = true));
				return;
			}
			this.Invoke((Action)(() => btStopServer.Enabled = true));
			WriteToConsole("Server started at " + ServerURI);
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
			Program.MainForm.WriteToConsole("Client connected: " + Context.ConnectionId);
			return base.OnConnected();
		}
		public override Task OnDisconnected(bool stopCalled)
		{
			Program.MainForm.WriteToConsole("Client disconnected: " + Context.ConnectionId);
			return base.OnDisconnected(stopCalled);
		}
	}
}
