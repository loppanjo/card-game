using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
	public class Hub
	{
		// events för att kommunicera med serverformen
		public delegate void PlayerConnected(Player player);
		public delegate void PlayerDisconnected(Player player);
		public delegate void WriteToConsole(string text);

		public event PlayerConnected ClientConnectedEvent;
		public event PlayerDisconnected ClientDisconnectedEvent;
		public event WriteToConsole WriteToConsoleEvent;

		private TcpListener listener;
		private GameRules gameRules;
		private Thread dcThread;

		public Hub(GameRules gameRules)
		{
			dcThread = new Thread(DisconnectChecker);
			this.gameRules = gameRules;
			Players = new PlayerCollection();
		}

		protected PlayerCollection Players { get; private set; }
		public bool ServerStarted { get; set; }
		public bool GameStarted { get; set; }

		// Starta servern på en port
		public bool StartServer(int port)
		{
			if (listener != null && ServerStarted)
				listener.Stop();
			listener = new TcpListener(IPAddress.Any, port);

			// startar servern, om något går fel ger den ett error,
			// ett specifikt vid portfel, annars default.
			try
			{
				listener.Start();

				ServerStarted = true;

				// Börja att ta emot clienter
				BeginAccept();
				dcThread.Start();
				return true;
			}
			catch (SocketException)
			{
				WriteToConsoleEvent("Port is already in use!");
			}
			catch (Exception ex)
			{
				WriteToConsoleEvent(ex.Message);
			}
			WriteToConsoleEvent("Starting server failed!");
			return false;
		}

		// Stoppa servern
		public virtual void StopServer()
		{
			listener?.Stop();
			dcThread.Abort();
			ServerStarted = false;
		}

		// Vänta på ett speciellt kommand från en spelare
		public Message WaitForCommandFrom(Player player, string command)
		{
			Message message;
			try
			{
				while ((message = player.Client.Receive()).Command != command)
				{ }
			}
			catch
			{
				return new Message(command, "");
			}
			return message;
		}

		private void BeginAccept()
		{
			listener.BeginAcceptTcpClient(AcceptClient, listener);
		}

		private void AcceptClient(IAsyncResult res)
		{
			// Fortsätt att acceptera clienter rekursivt
			BeginAccept();

			// Kolla om det inte redan finns tillräckligt med spelare och om spelet redan startat
			if (Players.Count < gameRules.MaxPlayers && !GameStarted)
			{
				// Lägg till spelaren
				TcpClient socket = listener.EndAcceptTcpClient(res);
				Player player = Players.Add(socket);

				// Ta emot spelarens namn
				Message message = player.Client.Receive();
				player.Name = (string)message.Data;

				// Meddela spelaren att den är med i spelet
				player.Client.Send(new Message("GAME STATE", "WAITING"));
				player.Client.Send(new Message("DRAW", ""));

				// Kalla på eventet som meddelar lyssnare om en ny spelare
				ClientConnectedEvent(player);
			}
		}

		// kör i en separat tråd och letar efter klienter som kopplar ifrån.
		private void DisconnectChecker()
		{
			while (true)
			{
				Thread.Sleep(500);
				for (int i = 0; i < Players.Count; i++)
				{
					if (!SocketConnected(Players[i].Client.Socket.Client))
					{
						ClientDisconnectedEvent(Players[i]);
						Players.Remove(Players[i]);
					}
				}
			}
		}
		// kollar hur om en socket är uppkopplad till servern.
		private bool SocketConnected(Socket s)
		{
			bool part1 = s.Poll(1000, SelectMode.SelectRead);
			bool part2 = (s.Available == 0);
			if (part1 && part2)
				return false;
			else
				return true;
		}
	}
}

