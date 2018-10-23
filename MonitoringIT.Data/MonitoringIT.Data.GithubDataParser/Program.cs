using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using Lib.MonitoringIT.DATA.Github.Scrapper;
using Microsoft.EntityFrameworkCore;
using MonitoringIT.DAL.Models;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.GithubDataParser
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
