using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using MonitoringIT.DAL.Models;
using MonitoringIT.DATA.Github.Scrapper.Lib;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.GithubDataParser
{
    class Program
    {
        static void Main()
        {
            GithubScrapper githubScrapper=new GithubScrapper();
            githubScrapper.Start();
        }
    }
}
