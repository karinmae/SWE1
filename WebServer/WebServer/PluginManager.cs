using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorCloud;
using System.Reflection;

namespace WebServer
{
    class PluginManager
    {
        public PluginManager()
        {
            
            //Wenn /SensorCloud dann... wobei ich glaub es soll "getTemperature" sein...


            //Plugins
            List<Interface.ISensorCloud> plugins = new List<Interface.ISensorCloud>();
            
            //In der Schleife wird der Ordner mit den dlls durchsucht
            foreach (var filename in System.IO.Directory.GetFiles(".\\plugins", "*.dll"))
            {

                Assembly myDll = Assembly.LoadFrom(filename);

                var types = myDll.GetTypes().Where(t => typeof(Interface.ISensorCloud).IsAssignableFrom(t));
                foreach (var type in types)
                {
                    var obj = (Interface.ISensorCloud)Activator.CreateInstance(type);
                    plugins.Add(obj);
                }
            }


            foreach (var plugin in plugins)
            {
                plugin.ReadTemperature();
                plugin.getTemperature();

            }

            //Wenn /StaticFile dann...

            //Wenn /Navi dann..

            //Wenn "Eigenes Plugin" dann...
        }
    }
}
