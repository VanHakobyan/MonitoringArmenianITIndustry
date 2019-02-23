using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database.MonitoringIT.DB.EfCore.Models;
using HtmlAgilityPack;

namespace Lib.MonitoringIT.Data.Staff_am.Scrapper
{
    public class StaffScrapper
    {
        private static ChromeDriver driver;
        private const string Link = "https://staff.am/en/companies";
        private const string CompanyCustomLink = "https://staff.am";

        private static readonly List<string> Links = new List<string>();

        public void InitDriver()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Link);
        }


        //public List<string> GetCompanyLinks()
        //{
        //    for (int i = 1; i < 15; i++)
        //    {
        //        Scroll(i);
        //    }
        //
        //    var driverPageSource = driver.PageSource;
        //    HtmlDocument document = new HtmlDocument();
        //    document.Load(driverPageSource);
        //    return null;
        //}


        public void StartScrapping()
        {
            GetCompanyLinks();
            GetCompanies();
        }

        public void GetCompanyLinks()
        {
            var links = new List<string>();
            for (int i = 1; i < 100; i++)
            {
                try
                {
                    HtmlDocument document = new HtmlDocument();
                    var pageContent = SendGetRequest($"https://staff.am/en/companies?page={i}").Result;
                    document.LoadHtml(pageContent);
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


        public void GetCompanies()
        {
            foreach (var link in Links)
            {
                try
                {
                    var company = new Company();
                    var companyHtmlContent = SendGetRequest(link).Result;
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(companyHtmlContent);


                    //


                    company.Job = GetJobs(null);
                    
                    
                    //

                    using (MonitoringContext context = new MonitoringContext())
                    {
                        context.Company.Add(company);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    //
                }
            }
        }


        public List<Job> GetJobs(List<string> jobLinks)
        {
            List<Job> jobs = new List<Job>();
            foreach (var jobLink in jobLinks)
            {
                try
                {
                    var jobHtmlContent = SendGetRequest(jobLink).Result;
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(jobHtmlContent);
                    var job = new Job();
                    jobs.Add(job);
                }
                catch (Exception e)
                {
                    //
                }
            }

            return jobs;

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

        /// <summary>
        /// Get request 
        /// </summary>
        /// <param name="uri">Url</param>
        /// <param name="cookie">Cookie in site</param>
        /// <returns></returns>
        private async Task<string> SendGetRequest(string uri, string cookie = null)
        {
            var response = "";

            try
            {
                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DnsRefreshTimeout = 1000;
                ServicePointManager.UseNagleAlgorithm = false;


                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.Headers.Add("accept-language", "en-US,en;q=0.9,hy;q=0.8,mt;q=0.7");
                using (var stream = (await request.GetResponseAsync()).GetResponseStream())
                {
                    if (stream != null)
                    {
                        stream.ReadTimeout = 30000;
                        using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                        {
                            response = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(1000);
            }


            return response;
        }
    }
}
