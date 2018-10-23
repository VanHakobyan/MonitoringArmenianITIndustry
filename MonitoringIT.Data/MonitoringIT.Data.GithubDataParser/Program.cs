using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using DAL.MonitoringIT;
using HtmlAgilityPack;
using Lib.MonitoringIT.DATA.Github.Scrapper;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.Firefox;

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
