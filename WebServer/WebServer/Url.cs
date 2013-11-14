using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebServer
{
    class Url
    {
        private String FullUrl;
        private String new_http_url;
       // private string[] SplitUrl;

        public Url(String http_url)
        {
            new_http_url = http_url;
            FullUrl = http_url;
            handleGETRequest();
        }

        public String getFullUrl()
        {
            return FullUrl;
        }

        public void setFullUrl(String Url)
        {
            FullUrl = Url;
        }

        //public String getSplitUrl()
        //{ 
        //    return string[] SplitUrl;
        //}

        //public void setSplitUrl(String SpUrl)
        //{

        //}

        //Test

        public void handleGETRequest()
        {
            //Console.WriteLine("GET");
            //erstes '/' abschnieden
            new_http_url = new_http_url.Substring(1);
            string[] split = Regex.Split(new_http_url, "/");
            //böses favicon
            bool favicon = new_http_url.StartsWith("favicon.ico", System.StringComparison.CurrentCultureIgnoreCase);
            if (favicon == false)
            {
                bool Temp = new_http_url.StartsWith("Temp", System.StringComparison.CurrentCultureIgnoreCase);
                if (Temp == true)
                {
                    string plugin = "Temp";
                    //PluginManager Temp = new PluginManager(plugin);
                }
                //string plugin_type = split[0];
                //string year = split[1];
                //string month = split[2];
                //string day = split[3];

                Console.WriteLine("Got this:");
                foreach (var o in split)
                {
                    
                    Console.WriteLine(o);
                }
            }
        }
    }
}
