using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Services.MonitoringIT.Data.Parser.Linkedin
{
    static class Program
    {
        static void Main()
        {
#if DEBUG
            var service = new LinkedinService();
            service.TestAndStart();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new LinkedinService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
