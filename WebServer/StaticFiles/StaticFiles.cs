﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticFiles
{
    public class StaticFiles : Interface.IStaticFiles
    {
        public void Files()
        {
            Console.WriteLine("Static Files Plugin loaded");
        }
    }
}