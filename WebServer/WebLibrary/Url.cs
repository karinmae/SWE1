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
using WebLibrary;

namespace WebLibrary
{
    public class Url
    {
        private String FullUrl;
        private String PluginName; 
        //private String new_http_url;
        private String[] SplitUrl;

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
    }
}
