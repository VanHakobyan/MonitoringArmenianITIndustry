using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using HtmlAgilityPack;
using OpenQA.Selenium.Firefox;

namespace Lib.MonitoringIT.Data.Staff_am.Scrapper
{
    public class StaffScrapper
    {
        private static ChromeDriver driver;
        private const string Link = "https://staff.am/en/companies";
        private const string CompanyCustomLink = "https://staff.am";

        private static List<string> Links = new List<string>();

        public void InitDriver()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Link);
        }


        public List<string> GetCompanyLinks()
        {
            for (int i = 1; i < 15; i++)
            {
                Scroll(i);
            }

            var driverPageSource = driver.PageSource;
            HtmlDocument document = new HtmlDocument();
            document.Load(driverPageSource);
            return null;
        }

        public void GetContent()
        {
            var client = new HttpClient();
            var links=new List<string>();
            for (int i = 1; i < 100; i++)
            {
                try
                {
                    HtmlDocument document = new HtmlDocument();
                    var result = client.GetStringAsync($"https://staff.am/en/companies?page={i}").Result;
                    document.LoadHtml(result);
                    var pageLinks = document.DocumentNode.SelectNodes(".//a[@class='load-more btn width100']").Select(x => $"{CompanyCustomLink}{x.GetAttributeValue("href", "")}").ToList();
                    links.AddRange(pageLinks);
                    foreach (var pageLink in pageLinks)
                    {
                        Console.WriteLine(pageLink);
                    }
                }
                catch (Exception e)
                {
                    //
                }
            }
            Links.AddRange(links.Distinct());
        }

        /// <summary>
        /// Scroll in page
        /// </summary>
        /// <param name="item"></param>
        private void Scroll(int item)
        {
            driver.ExecuteScript($"scroll({item * 1000}, {item * 10000});");
            Thread.Sleep(2500);
        }
    }
}
