using DAL.MonitoringIT;
using Lib.MonitoringIT.DATA.Github.Scrapper;

namespace MonitoringIT.Data.GithubDataParser
{
    class Program
    {
        static void Main()
        {
            MonitoringDAL monitoringDal=new MonitoringDAL("");
            var profiles = monitoringDal.GithubProfileDal.GetAll();

            var githubScrapper = new GithubScrapper();
            githubScrapper.Start();
        }
    }
}
