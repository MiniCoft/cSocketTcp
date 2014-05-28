using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;

using System.Threading;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Your Port(Ex:8888):");
            int port = Convert.ToInt32(Console.ReadLine());

            cServer s = new cServer(port);

            s.Start();
            Console.ReadLine();
            s.Exit();
            

        }
    }
}
