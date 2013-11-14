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
    class Response
    {
        private NetworkStream stream;
        public Response(object clientStream)
        {
            stream = (NetworkStream)clientStream;
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine("HTTP/1.1 200 OK");
            sw.WriteLine("connection: close");
            //sw.WriteLine("content-length: 11");
            sw.WriteLine("content-type: text/html");
            sw.WriteLine();
            sw.WriteLine("<html><body><h1>pink fluffy unicorns dancing on rainbows</h1></body></html>");
            sw.Flush();
        }

    }
}
