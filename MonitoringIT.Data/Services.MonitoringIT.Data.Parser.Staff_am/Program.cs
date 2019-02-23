using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Services.MonitoringIT.Data.Parser.Staff_am
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Service service = new Service();
            if (Environment.UserInteractive)
            {
                service.TestAndStart();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] {service};
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
