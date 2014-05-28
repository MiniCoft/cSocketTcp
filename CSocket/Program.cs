using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Target IP(Ex:127.0.0.1):");
            string ip = Console.ReadLine();

            Console.WriteLine("Your Port(Ex:8888):");
            int port = Convert.ToInt32(Console.ReadLine());

            cSocket s = new cSocket(ip,port);

            s.Start();
            Console.ReadLine();
            
        }
    }
}
