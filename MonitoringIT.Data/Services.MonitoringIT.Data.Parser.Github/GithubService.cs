using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Lib.MonitoringIT.DATA.Github.Scrapper;

namespace Services.MonitoringIT.Data.Parser.Github
{
    public partial class GithubService : ServiceBase
    {
        public GithubService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(() =>
            {
                var githubScrapper = new GithubScrapper();
                githubScrapper.Start();
            });

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
