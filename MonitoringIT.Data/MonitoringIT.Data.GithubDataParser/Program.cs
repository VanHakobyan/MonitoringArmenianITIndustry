using DAL.MonitoringIT;
using Lib.MonitoringIT.Data.Linkedin.Scrapper;
using Lib.MonitoringIT.DATA.Github.Scrapper;

namespace MonitoringIT.Data.GithubDataParser
{
    class Program
    {
        static void Main()
        {
            MonitoringDAL monitoringDal=new MonitoringDAL("");

            //Linkedin  linkedin=new Linkedin();

            //linkedin.GetAllLinkedinProfiles();

            //var profiles = monitoringDal.GithubProfileDal.GetAll();

            var githubScrapper = new GithubScrapper();
            githubScrapper.Start();
        }
    }
}
