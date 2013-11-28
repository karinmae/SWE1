using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebLibrary
{
    public class Url
    {
        private String FullUrl;
        private String PluginName; 
        //private String new_http_url;
        private String[] SplitUrl;

        //public Url(String http_url)
        //{
        //    new_http_url = http_url;
        //    FullUrl = http_url;
        //    //handleGETRequest();
        //}

        public String getFullUrl()
        {
            return FullUrl;
        }

        public void setFullUrl(String newUrl)
        {
            FullUrl = newUrl;
        }

        public String[] getSplitUrl()
        { 
            return SplitUrl;
        }

        public void setSplitUrl(String[] newUrl)
        {
            SplitUrl = newUrl;
        }


        public string getPluginName() 
        {
            return PluginName;
        }

        public void setPluginName(String plugin)
        {
            PluginName=plugin;
        }
//Mal sehen obs gehts :)

        //public void handleGETRequest()
        //{
        //    //Console.WriteLine("GET");
        //    //erstes '/' abschnieden
        //    new_http_url = new_http_url.Substring(1);
        //    string[] split = Regex.Split(new_http_url, "/");
        //    //böses favicon
        //    bool favicon = new_http_url.StartsWith("favicon.ico", System.StringComparison.CurrentCultureIgnoreCase);
        //    if (favicon == false)
        //    {
        //        //bool Temp = new_http_url.StartsWith("Temp", System.StringComparison.CurrentCultureIgnoreCase);
        //        //if (Temp == true)
        //        //{
        //        //    string plugin = "Temp";
        //        //    //PluginManager Temp = new PluginManager(plugin);
        //        //}
        //        PluginName = split[0];
        //        //string year = split[1];
        //        //string month = split[2];
        //        //string day = split[3];

        //        //copy the split Array into the SplitUrl array 
        //        SplitUrl = new string[split.Length];
        //        split.CopyTo(SplitUrl, 0);

        //        Console.WriteLine("Got this:");

        //        foreach (string o in SplitUrl)
        //        {

        //            Console.WriteLine(o);

        //        }
        //    }
        //}
    }
}
