using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Client
    {
        private string UserName { get; set; }
        private IHubProxy HubProxy { get; set; }
        private string ServerURI { get; set; }
        private HubConnection Connection { get; set; }

        public Client(string username, string uri)
        {
            UserName = username;
            ServerURI = uri;
        }

        public void Connect()
        {
            if (!string.IsNullOrEmpty(UserName))
                ConnectAsync();
        }

        public void Disconnect()
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }

        public void Send(string message)
        {
            HubProxy.Invoke("Send", UserName, message);
        }

        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Disconnect;
            HubProxy = Connection.CreateHubProxy("MyHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread 
            HubProxy.On<string, string>("AddMessage", (name, message) =>
                Console.WriteLine($"{name}: {message}")
            );
            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                //No connection: Don't enable Send button or show chat UI 
                return;
            }
        }
    }
}
