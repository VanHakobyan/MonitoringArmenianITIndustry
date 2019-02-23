using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Lib.MonitoringIT.Data.Github.Api;

namespace Services.MonitoringIT.Data.Integration.Github
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            GithubIntegrationService service = new GithubIntegrationService();
            service.TestAndStart();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new GithubIntegrationService()
            };
            ServiceBase.Run(ServicesToRun); 
#endif
        }
    }
}
