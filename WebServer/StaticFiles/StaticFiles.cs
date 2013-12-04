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


        private void SendFile()//clientStream, byte[] byteData
        {

            StreamWriter sw = new StreamWriter(stream);
            string path = @".\\files\\" + filename;
            byte[] buffer = ReadAllBytes(path);


            stream.Write(buffer, 0, buffer.Length);
            
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
