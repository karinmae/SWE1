using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace WebServer
{
    class Server
    {

        private TcpListener Listener;
        private Thread listenThread;
        
        //Basic Settings
        private int port = 8080;
        private IPAddress localAddr = IPAddress.Any;
        int Threadcount = 0;

        public Server()
        {
            Listener = new TcpListener(localAddr, port);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
        }

        private void ListenForClients()
        {
            //fängt zum lauschen an
            Listener.Start();

            //in einer Schleife und macht für jeden Clienten einen Thread auf
            while (true)
            {
                //neuer Client gefunden
                TcpClient client = Listener.AcceptTcpClient();
                Console.WriteLine("____________________________");
                Console.WriteLine("Client found <3");

                //Thread erstellen
                Thread clientThread = new Thread(new ParameterizedThreadStart(ClientComm));
                clientThread.Start(client);
                ++Threadcount;
                Console.WriteLine("Let's do some Magic...~* Creating Thread No. {0} *~", Threadcount);
            }

        }

        private void ClientComm(object client)
        {
            using (TcpClient tcpClient = (TcpClient)client)
            {
                //Neuen NetworkStream anlegen
                using (NetworkStream clientStream = tcpClient.GetStream())
                {
                    while (true)
                    {
                        try
                        {
                            if (tcpClient.Available == 0) break;

                            //Einlesen
                            Request neuerReader = new Request(clientStream);
                            
                            //Nachricht an Client
                            Response neuerWriter = new Response(clientStream);

                            Console.WriteLine("No. {0} disconnected *~ ", Threadcount);
                            Console.WriteLine("____________________________");
                            return;
                        }

                        catch
                        {
                            Console.WriteLine("Magic failed... :(");
                            return;
                        }
                    }
                }
            }
        }

    }
}

    
