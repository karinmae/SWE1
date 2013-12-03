﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using WebLibrary;

namespace WebServer
{
    class Server
    {

        private TcpListener Listener;
        private Thread listenThread;
        private PluginManager plugin;

        //Basic Settings
        private int port = 8080;
        private IPAddress localAddr = IPAddress.Any;
        int Threadcount = 0;

        public Server(ref PluginManager pm)
        {
            Listener = new TcpListener(localAddr, port);
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
            plugin = pm;
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
                Console.WriteLine("Client found");

                //Thread erstellen
                Thread clientThread = new Thread(new ParameterizedThreadStart(ClientComm));
                clientThread.Start(client);
                ++Threadcount;
                Console.WriteLine("Creating Thread No. {0} *~", Threadcount);
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

                            //Neues URL Objekt
                            Url theNew = new Url();

                            //Einlesen
                            Request neuerReader = new Request(clientStream);
                            theNew = (Url)neuerReader.getURL();
                            string pluginName = theNew.getPluginName();
                            string[] splitString = theNew.getSplitUrl();
                                                        
                            //Nachricht an Client
                            if(splitString.Length<=1)
                            {
                                Response neuerWriter = new Response(clientStream, theNew);
                            }
                            
                            

                            //An Plugin Manager weiterreichen
                            if (!String.IsNullOrEmpty(pluginName))
                            {
                                plugin.HandleRequest(theNew, clientStream);
                            }

                            //Console.WriteLine("PluginName: {0}", pluginName);
                            
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

    
