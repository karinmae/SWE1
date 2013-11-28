using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using System.Reflection;
using WebLibrary;

namespace WebServer
{
    class PluginManager
    {
        //Plugins
        List<IPlugin> plugins = new List<IPlugin>();

        public PluginManager()
        {
            //In der Schleife wird der Ordner mit den dlls durchsucht
            foreach (var filename in System.IO.Directory.GetFiles(".\\plugins", "*.dll"))
            {

                Assembly myDll = Assembly.LoadFrom(filename);

                //var types = myDll.GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t));

                //foreach (var type in types)
                //{
                //    var obj = (Interface.IPlugin)Activator.CreateInstance(type);
                //    plugins.Add(obj);
                //}

                foreach (Type asmtype in myDll.GetTypes())
                {
                    if (asmtype.GetInterface("IPlugin") != null)
                    {
                        IPlugin plugs = (IPlugin)Activator.CreateInstance(asmtype);
                        plugins.Add(plugs);
                    }
                }
            }


            foreach (var plugin in plugins)
            {
                plugin.start();
                //plugin.handleRequest();

            }
        }

        public void HandleRequest(Url url, string name = "")
        {
            foreach (IPlugin p in plugins)
            {
                p.handleRequest(url);
            }
        }
    }
}
