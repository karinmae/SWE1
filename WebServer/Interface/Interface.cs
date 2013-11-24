using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
     public interface ISensorCloud
    {
        void getTemperature();
        void ReadTemperature();
    }

    public interface IStaticFiles
    {
        void Files();
    }
}
