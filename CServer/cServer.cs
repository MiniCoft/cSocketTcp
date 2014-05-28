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
    class cServer
    {
        int port;
        int mRecvBuffer = 20;
        public cServer(int _port)
        {
            port = _port;
        }
        
        Thread thread;
        List<Socket> Sockets;
        public void Start()
        {
            Sockets = new List<Socket>();
            thread = new Thread(new ThreadStart(SocketListener));
            thread.Start();
        }
        public void Exit()
        {
            thread.Abort();
            Environment.Exit(0);
        }

        Socket aSocket;
        Thread aThread;
        void SocketListener()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);
            Socket mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mSocket.Bind(endpoint);

            EndPoint Remote = (EndPoint)(endpoint);

            mSocket.Listen(20);

            while (true)
            {
                aSocket = mSocket.Accept();
                if (aSocket != null)
                {
                    Sockets.Add(aSocket);
                    aThread = new Thread(aListen);
                    aThread.IsBackground = true;
                    aThread.Start();
                }
            }
        }
        void aListen()
        {
            Socket socket = aSocket;
            Thread thread = aThread;
            while (true)
            {
                try
                {
                    int recv;
                    byte[] bytes = new byte[mRecvBuffer];
                    recv = socket.Receive(bytes);

                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream stream = new MemoryStream(bytes);
                    string s = System.Text.Encoding.Default.GetString(bytes);

                    Console.WriteLine("Recv:" + s);

                    bytes = System.Text.Encoding.Default.GetBytes("Server :" + s);
                    aSocket.Send(bytes);
                }
                catch
                {
                    Sockets.Remove(socket);
                    thread.Abort();
                }

            }
        }
    }
}
