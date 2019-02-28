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


        public void StartSScrapping()
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

                    var headerInfoNode = document.DocumentNode.SelectSingleNode(".//div[@class='header-info accordion-style']");
                    var containerNode = document.DocumentNode.SelectSingleNode(".//div[@class='container']");
                    var jobsListNode = document.DocumentNode.SelectSingleNode(".//div[@class='accordion-style clearfix company-jobs-list']");
                    var aboutCompanyNode = document.DocumentNode.SelectSingleNode(".//div[@class='accordion-style animation-element in-view']");
                    var contactDetails = document.DocumentNode.SelectSingleNode(".//div[@id ='company-contact-details']");

                    GetHeaderInfo(company, headerInfoNode);
                    GetContainer(company, containerNode);
                    GetAbout(company, aboutCompanyNode);
                    GetContent(company, contactDetails);


                    var jobList=jobsListNode.SelectNodes(".//a[@class='load-more btn hb_btn']").Select(x=>x.GetAttributeValue("href","")).ToList();
                    

                    company.Job = GetJobs(jobList);

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

        private void GetHeaderInfo(Company company, HtmlNode headerInfoNode)
        {
            var name = headerInfoNode.SelectSingleNode(".//h1[@class='text-left']").InnerText;
            var views = headerInfoNode.SelectSingleNode(".//span[@class='margin-r-2']").InnerText;
            var image = headerInfoNode.SelectSingleNode(".//div[@class='image']").GetAttributeValue("style", "");

            //company.Name = name;
            //company.Views views;
            //company.Image = image;
        }
        private void GetAbout(Company company, HtmlNode aboutCompanyNode)
        {
            var companyInfoDiv = aboutCompanyNode.SelectSingleNode(".//div[@class='company-info']");
            var descriptions = companyInfoDiv.SelectNodes(".//p[@class='professional-skills-description']");
            var industry = descriptions[0].InnerText.Trim();
            var type = descriptions[1].InnerText.Trim();
            var nOfEmpl = descriptions[2].InnerText.Trim();
            var about = aboutCompanyNode.SelectSingleNode(".//div[@class='col-lg-8 col-md-8 about-text']").InnerText.Trim();


        }
      
       

        private void GetContainer(Company company, HtmlNode containerNode)
        {
            
        }

        private void GetContent(Company company, HtmlNode contactDetails)
        {


        }


        public List<Job> GetJobs(List<string> jobLinks)
        {
            List<Job> jobs = new List<Job>();
            foreach (var jobLink in jobLinks)
            {
                try
                {
                    var jobHtmlContent = SendGetRequest($"{CompanyCustomLink}{jobLink}").Result;
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(jobHtmlContent);

                    var jobPostNode = document.DocumentNode.SelectSingleNode(".//div[@id ='job-post']");
                    var rowNode = jobPostNode.SelectSingleNode(".//div[@class ='row']");
                    var descriptionNode = jobPostNode.SelectSingleNode(".//div[@class ='job-list-content-desc hs_line_break']");
                    var skillNode = jobPostNode.SelectSingleNode(".//div[@class ='job-list-content-skills']");






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
