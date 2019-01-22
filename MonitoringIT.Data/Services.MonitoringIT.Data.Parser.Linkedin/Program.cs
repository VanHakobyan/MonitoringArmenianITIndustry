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

            var service = new LinkedinService();

            if (Environment.UserInteractive)
            {
                service.TestAndStart();
            }

            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { service };
                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
