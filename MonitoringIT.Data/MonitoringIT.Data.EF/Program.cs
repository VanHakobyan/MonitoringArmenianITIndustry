using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringIT.Data.Common;

namespace MonitoringIT.Data.EF
{
    class Program
    {
        static void Main(string[] args)
        {
            MonitoringContext monitoringContext=new MonitoringContext();
            monitoringContext.Repositories.Add(new Repository());
        }
    }
}
