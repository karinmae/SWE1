﻿using System;
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
            //Plugins
            List<IPlugin> plugins = new List<IPlugin>();

            //In der Schleife wird der Ordner mit den dlls durchsucht
            foreach (var filename in System.IO.Directory.GetFiles(".\\plugins", "*.dll"))
            {

                Assembly myDll = Assembly.LoadFrom(filename);

                var types = myDll.GetTypes().Where(t => typeof(SensorCloud.IPlugin).IsAssignableFrom(t));
                foreach (var type in types)
                {
                    var obj = (IPlugin)Activator.CreateInstance(type);
                    plugins.Add(obj);
                }
            }


            foreach (var plugin in plugins)
            {
                plugin.Register();
            }
        }
    }
}
