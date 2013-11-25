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


namespace WebServer
{
    class Request
    {
        private String http_method;
        protected String http_url;
        private NetworkStream stream;
        //private String[,] httpHeaders;
        public Hashtable httpHeaders = new Hashtable();

        public Request(object clientStream)
        {
            stream = (NetworkStream)clientStream;
            var sr = new StreamReader(stream);
            string data = sr.ReadLine();
            parseRequest(data);
            readHeaders(sr);
            if (http_method=="GET")
            {
                //handleGETRequest();
                Url u = new Url(http_url);
               // Console.WriteLine("Ganze URL aus URL Klasse:" + u.getFullUrl());
               //Test split URl:
                //foreach (string o in u.getSplitUrl())
                //{

                //    Console.WriteLine(o);

                //}

              
            }
            else if (http_method=="POST")
            {
                handlePOSTRequest();
            }
        }

        private void parseRequest(string data)
        {
            String[] tokens = data.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            String new_http_url = http_url;
            Console.WriteLine("Received {0}", http_url);
        }

        private void readHeaders(StreamReader sr)
        {
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Equals(""))
                {
                    Console.WriteLine("Got headers ^_^");
                    return;
                }
                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;

                //Leerzeichen beseitigen
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);
                //Console.WriteLine("header: {0}:{1}\n", name, value);
                //string[,] httpHeaders = new string[,] {{name, value}};
                httpHeaders[name] = value;
            }
        }

        //private const int BUF_SIZE = 4096;
        //private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB
        private void handlePOSTRequest()
        {
            Console.WriteLine("POST");
            //int content_len = 0;
            //MemoryStream ms = new MemoryStream();
            //if (this.httpHeaders.ContainsKey("Content-Length"))
            //{
            //    content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
            //    if (content_len > MAX_POST_SIZE)
            //    {
            //        throw new Exception(
            //            String.Format("POST Content-Length({0}) too big",
            //              content_len));
            //    }
            //    byte[] buf = new byte[BUF_SIZE];
            //    int to_read = content_len;
            //    while (to_read > 0)
            //    {
            //        Console.WriteLine("starting Read, to_read={0}", to_read);

            //        int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
            //        Console.WriteLine("read finished, numread={0}", numread);
            //        if (numread == 0)
            //        {
            //            if (to_read == 0)
            //            {
            //                break;
            //            }
            //            else
            //            {
            //                throw new Exception("client disconnected during post");
            //            }
            //        }
            //        to_read -= numread;
            //        ms.Write(buf, 0, numread);
            //    }
            //    ms.Seek(0, SeekOrigin.Begin);
            //}
            //Console.WriteLine("get post data end");
        }
    }
}
