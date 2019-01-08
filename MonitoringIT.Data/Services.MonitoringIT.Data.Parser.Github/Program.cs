using System.ServiceProcess;
using Services.MonitoringIT.Data.Parser.Linkedin;

namespace Services.MonitoringIT.Data.Parser.Github
{
    static class Program
    {
        static void Main()
        {
#if DEBUG
            var service = new GithubService();
            service.TestAndStart();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new LinkedinService(),
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
