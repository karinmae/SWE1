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
            Console.WriteLine("Navi Plugin loaded");
        }

        public byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }


      
        public void handleRequest(Url url, NetworkStream clientStream)
        {
            stream = clientStream;
            Url newUrl = new Url();
            newUrl = (Url)url;
            string pluginName = newUrl.getPluginName();
            string[] filenameSplit;

            if (pluginName == "StaticFile")
            {
                Console.WriteLine("{0}: handleRequest", pluginName);
                filenameSplit = newUrl.getSplitUrl();
            }
        }

        public string getName()
        {
            return "Navi";
        }
    }
}
