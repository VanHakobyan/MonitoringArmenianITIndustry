using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.MonitoringIT;
using Lib.MonitoringIT.DATA.Github.Scrapper;

namespace Services.MonitoringIT.Data.Github
{
    class Program
    {
        static void Main()
        {
            var githubScrapper = new GithubScrapper();
            githubScrapper.Start();
        }
    }
}
