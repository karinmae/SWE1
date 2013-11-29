using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;

namespace Interface
{
    public class StaticFiles : IPlugin
    {
        public void start()
        {
            Console.WriteLine("Static Files Plugin loaded");
        }

        public void handleRequest(Url url)
        {
            Url newUrl = new Url();
            newUrl = (Url)url;
            string pluginName = newUrl.getPluginName();
            if (pluginName == "StaticFile")
            {
                Console.WriteLine("{0}: handleRequest", pluginName);
            }
        }

        public string getName()
        {
            return "StaticFile";
        }
    }
}
