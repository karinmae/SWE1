using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;

namespace Interface
{
    public interface IPlugin
    {
        void start();
        void handleRequest(Url url);
        string getName();
    }

}