using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

namespace ObligatoryTCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(IPAddress.Loopback, 4646);
            Server myserver = new Server();
            myserver.Start(serverSocket);
            while (true)
            {
                Task.Run(() =>
                {
                    myserver.DoClient(serverSocket);

                });
            }
        }
    }
}
