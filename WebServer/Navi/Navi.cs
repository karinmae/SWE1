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
using System.Xml;

namespace Interface
{
    public class Navi : IPlugin
    {
        private NetworkStream stream;
        bool laden = false; 
        private Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();
        private string city = "";
        private string street = "";

        public void start()
        {
            Console.WriteLine("Navi loaded");
            Load();
        }

        public string getName()
        {
            return "Navi";
        }

        public void handleRequest(Url url, NetworkStream clientStream)
        {
            stream = clientStream;
            Url newUrl = new Url();
            newUrl = (Url)url;
            string[] split = newUrl.getSplitUrl();
            string pluginName = newUrl.getPluginName();
            string searchedstreet;

            if (pluginName == "Navi")
            {
                
                if (split.Length == 2)
                {

                   searchedstreet = split[1];
                   searchedstreet = searchedstreet.Replace("+", " ");  
                   searchedstreet = searchedstreet.Replace("%DF", "ß");
                   searchedstreet = searchedstreet.Replace("%E4", "ä");
                   searchedstreet = searchedstreet.Replace("%F6", "ö");
                   searchedstreet = searchedstreet.Replace("%FC", "ü");

                    
                     
                   if(laden == true)
                   {
                       StreamWriter sw = new StreamWriter(stream); 

                       sw.WriteLine("HTTP/1.1 200 OK");
                       sw.WriteLine("connection: close");
                       sw.WriteLine("content-type: text/html; charset=UTF-8");
                       sw.WriteLine();
                       sw.WriteLine("<html><body>");
                       sw.WriteLine("<h3>Straßenkarte wird vorbereitet!</h3>");
                       sw.WriteLine("</body></html>");
                       sw.Flush();
                   }
                   else
                   {
                       Ausgabe(searchedstreet);
                   }
  
                }
                Console.WriteLine("{0}: handleRequest", pluginName);
            }
        }

        private void Load()
        {
            Console.WriteLine("Starting reading Card"); 
            Thread load_maps = new Thread(new ThreadStart(Load_Thread));
            load_maps.Start();
        }

        private void Load_Thread()
        {
            list.Clear();
            laden = true;
            using (var file = File.OpenRead(@".\navi\austria-latest.osm"))
            {
                
                using (var xml = new XmlTextReader(file))
                {
                    
                    while (xml.Read())
                    {
                        if (xml.NodeType == System.Xml.XmlNodeType.Element && xml.Name == "osm")
                        {
                            try
                            {
                                Read_XML(xml);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                    }
                }
            }
            laden = false;
            Console.WriteLine("Load Finished :D");
            return;
        }

        private void Read_XML(XmlTextReader xml)
        {
            using (var osm = xml.ReadSubtree())
            {
                while (osm.Read())
                {
                    if (osm.NodeType == XmlNodeType.Element && (osm.Name == "node"))
                    {
                        
                        using (var node = osm.ReadSubtree())
                        {
                            while (node.Read())
                            {
                                if (node.NodeType == XmlNodeType.Element && node.Name == "tag")
                                {
                                    ReadTag(node);
                                }
                            }
                            street = "";
                            city = "";
                        }
                    }
                }
            }

        }

        private void ReadTag(XmlReader element)
        {
            string tagType = element.GetAttribute("k");
            string value = element.GetAttribute("v");
            switch (tagType)
            {
                case "addr:city":
                    city = value;
                    break;
                case "addr:street":
                    street = value;
                    if (list.ContainsKey(street) == false && (city != ""))
                    {
                        list.Add(street, new List<string>());


                    }
                    if ((city != "") && (list[street].Contains(city) == false))
                    {
                        list[street].Add(city);

                    }
                    street = "";
                    city = "";
                    break;
            }


        }

        private void Ausgabe(string street)
        {
            StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine("HTTP/1.1 200 OK");
                sw.WriteLine("connection: close");
                sw.WriteLine("content-type: text/html; charset=UTF-8");
                sw.WriteLine();
                sw.WriteLine("<html><body>");
                sw.WriteLine("<h3>");
            if (list.ContainsKey(street))
            {
               
                foreach (string city in list[street])
                {
                    sw.WriteLine(city);
                    sw.WriteLine("</br>");

                }
                
            }

            else
            {
                sw.WriteLine("Straße nicht vorhanden!");
                sw.WriteLine("<form action='http://127.0.0.1:8080/Navi'");
                sw.WriteLine("<input type='button' value='Zurück'>");
                sw.WriteLine("</form>");
                
            }
                sw.WriteLine("</h3>");
                sw.WriteLine("</body></html>");
                sw.Flush();
        }
    }
}
