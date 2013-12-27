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
using System.Collections;
using WebLibrary;

namespace Interface
{
    public class Navi : IPlugin
    {
        private NetworkStream stream;

        public void start()
        {
            Console.WriteLine("Navi loaded");
        }

        public void handleRequest(Url url, NetworkStream clientStream)
        {
            stream = clientStream;
            Url newUrl = new Url();
            newUrl = (Url)url;
            string pluginName = newUrl.getPluginName();

            if (pluginName == "Navi")
            {
                Console.WriteLine("{0}: handleRequest", pluginName);
            }
        }

        public string getName()
        {
            return "Navi";
        }
    }
}
