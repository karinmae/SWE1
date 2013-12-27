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
    public class StaticFiles : IPlugin
    {
        private string filename;
        private NetworkStream stream;

        public void start()
        {
            Console.WriteLine("Static Files Plugin loaded");
        }

        //Byte
        public byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = null;
            //Filestream öffnen
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }


        private void SendFile()
        {
            //File mittels StreamWriter direkt an den Clienten senden
            StreamWriter sw = new StreamWriter(stream);
            string path = @".\files\" + filename;
            byte[] buffer = ReadAllBytes(path);

            sw.WriteLine("HTTP/1.1 200 OK");
            sw.WriteLine("connection: close");
           // sw.WriteLine("Content-Type: text/html");
            sw.WriteLine();
            sw.Flush();
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
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
                if (filenameSplit.Length == 2)
                {
                    filename = filenameSplit[1];
                    SendFile();                    
                }
            }
        }

        public string getName()
        {
            return "StaticFile";
        }
    }
}
