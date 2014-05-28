using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;

using System.Threading;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSocket
{
    
    class cSocket
    {
        int mRecvBuffer = 20;
        string ip;
        int port;
        public cSocket(string _ip, int _port)
        {
            ip = _ip;
            port = _port;
        }

        Socket mSocket;
        public void Start()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mSocket.Connect(endpoint);

            Thread thread = new Thread(new ThreadStart(SocketListener));
            thread.Start();
            while (true)
            {
                string s = Console.ReadLine();
                if (s == "exit")
                    break;
                if (s.Length > mRecvBuffer)
                {
                    Console.WriteLine("This Sting is Over " + mRecvBuffer);
                    continue;
                }
                Send(s);
            }
            thread.Abort();
            Environment.Exit(0);
        }
        void SocketListener()
        {
            while (true)
            {
                EndPoint ServerEP = (EndPoint)mSocket.RemoteEndPoint;

                int recv;
                byte[] bytes = new byte[mRecvBuffer];
                recv = mSocket.ReceiveFrom(bytes,ref ServerEP);

                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(bytes);
                string s = System.Text.Encoding.Default.GetString(bytes);

                Console.WriteLine(s);

            }
        }


        public void Send(string msg)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(msg);
            mSocket.Send(bytes, bytes.Length, SocketFlags.None);
        }
    }
}
