using System;
using System.ServiceProcess;

namespace Services.MonitoringIT.Data.Parser.Github
{
    static class Program
    {
        static void Main()
        {
            var service = new GithubService();
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
