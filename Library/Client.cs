using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Library
{
    public class Client
    {
        public delegate void ReceivedMessage(Message message);
        
        private NetworkStream stream;
        private Thread receiveThread;

        public Client(string username)
        {
            UserName = username;
            Socket = new TcpClient();
        }

        public Client(TcpClient socket)
        {
            Socket = socket;
            stream = socket.GetStream();
        }

        public string UserName { get; set; }
        public IPEndPoint Server { get; set; }

        public TcpClient Socket { get; set; }

        public event ReceivedMessage ReceivedMessageEvent;

        // Starta anslutningen till en server
        public void Connect(string ip, int port)
        {
            // Anslut clienten till servern
            Socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

            // Hämta in/ut strömmen till servern
            stream = Socket.GetStream();

            // Starta en ny tråd för att ta emot meddelanden från servern
            receiveThread = new Thread(ReceiveThread);
            receiveThread.Start();
        }

        // Avsluta anslutningen
        public void Disconnect()
        {
            Socket.Close();
            stream?.Close();
            receiveThread?.Abort();
        }

        // Skicka ett meddelande
        public void Send(Message message)
        {
            // Gör en buffer för meddelandet
            byte[] buffer = message.Serialize();

            // Gör en buffer för storleken på meddelandet
            byte[] size = BitConverter.GetBytes(buffer.Length);

            // Skicka storleken på meddelande-buffern
            stream.Write(size, 0, 4);

            // Skicka själva meddelande buffern
            stream.Write(buffer, 0, buffer.Length);
        }

        // En tråd för att ta emot data
        private void ReceiveThread()
        {
            while (true)
            {
                Thread.Sleep(500);
                // Ta bara emot data om det finns något att ta emot
                if (stream.DataAvailable)
                {
                    // Ta emot meddelandet
                    Message message = Receive();

                    // Kolla så meddelandet togs emot korrekt och kalla på eventet
                    if (message != null)
                        ReceivedMessageEvent(message);
                }
            }
        }

        // Ta emot ett meddelande
        public Message Receive()
        {
            // Gör en buffer för ett 32-bitars heltal
            byte[] size = new byte[4];

            // Gör en buffer för själva meddelandet
            byte[] buffer;

            int length;

            // Läs fyra bytes till heltalet
            stream.Read(size, 0, 4);

            // Convertera buffern till heltalet
            length = BitConverter.ToInt32(size, 0);

            // Allokera så många bytes som behövs för buffern
            buffer = new byte[length];

            // Läs hela meddelandet
            stream.Read(buffer, 0, length);

            // Returnera det avserialiserade meddelandet
            return DeserializeMessage(buffer);
        }
        
        // Avserialisera ett meddelande som tagits emot
        private Message DeserializeMessage(byte[] buffer)
        {
            // Konvertera buffern till en string
            string xml = Encoding.ASCII.GetString(buffer);

            // Skriv ut till konsolen för felsökning
            Console.WriteLine(xml);

            // Gör en strängläsare för xml datan
            StringReader sr = new StringReader(xml);

            // Gör en xml avserialiserare
            XmlSerializer xs = new XmlSerializer(typeof(Message), new Type[] { typeof(Card), typeof(Player), typeof(List<Player>), typeof(List<Card>), typeof(Ask) });

            // Avserialisera meddelandet med hjälp av strängläsaren
            Message message = (Message)xs.Deserialize(sr);

            return message;
        }
    }
}
