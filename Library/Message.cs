using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library
{
    public class Message
    {
        public Message() { }

        public Message(string command, object data)
        {
            Command = command;
            Data = data;
            Type = data.GetType().FullName;
        }

        public string Command { get; set; }
        public object Data { get; set; }
        public string Type { get; set; }

        // Serialiserar meddelandet till XML
        public byte[] Serialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Message), new Type[] { typeof(Card), typeof(Player), typeof(List<Player>), typeof(List<Card>), typeof(Ask) });
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, this);
            return Encoding.ASCII.GetBytes(sw.ToString());
        }
    }
}
