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
            Console.WriteLine("Static Files: handleRequest");
        }

        public string getName()
        {
            return "StaticFile";
        }
    }
}
