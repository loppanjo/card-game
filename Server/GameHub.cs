using Library;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameHub : Hub
    {
        public void Send(string name, string message)
        {
            Program.MainForm.WriteToConsole($"{name}: {message}");
            Clients.All.addMessage(name, message);
        }
        
        public override Task OnConnected()
        {
            base.Clients.Client(Context.ConnectionId);
            Program.MainForm.AddConnection(Context.ConnectionId);
            Clients.All.addMessage(Context.ConnectionId, "connected");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Program.MainForm.RemoveConnection(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
