using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Lib.MonitoringIT.Data.Github.Api;

namespace Services.MonitoringIT.Data.Integration.Github
{
    public partial class GithubIntegrationService : ServiceBase
    {
        public GithubIntegrationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            GithubApiProcessor apiProcessor = new GithubApiProcessor();
            apiProcessor.UpdateGithubProfilesInDb().GetAwaiter().GetResult();
        }

        protected override void OnStop()
        {
        }

        public void TestAndStart()
        {
            OnStart(null);
            Console.ReadKey();
            OnStop();
        }
    }
}
