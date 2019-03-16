using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Database.MonitoringIT.DAL.WithEF6;
using HtmlAgilityPack;

namespace Lib.MonitoringIT.Data.Staff.am.Scrapper
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
                    using (MonitoringEntities context = new MonitoringEntities())
                    {
                        var cName = link.Split('/').LastOrDefault();
                        var company = context.Companies.FirstOrDefault(c => c.Name.ToLower() == cName);
                        if (company is null) company = new Company();
                        var companyHtmlContent = SendGetRequest(link).Result;
                        HtmlDocument document = new HtmlDocument();
                        document.LoadHtml(companyHtmlContent);

                        var headerInfoNode = document.DocumentNode.SelectSingleNode(".//div[@class='header-info accordion-style']");
                        var companyDetails = document.DocumentNode.SelectSingleNode(".//div[@id='company-details']");
                        var jobsListNode = document.DocumentNode.SelectSingleNode(".//div[@class='accordion-style clearfix company-jobs-list']");


                        var contactDetails = document.DocumentNode.SelectSingleNode(".//div[@id ='company-contact-details']");

                        GetHeaderInfo(company, headerInfoNode);
                        GetDetails(company, companyDetails);
                        GetContact(company, contactDetails);


                        var jobList = jobsListNode.SelectNodes(".//a[@class='load-more btn hb_btn']").Select(x => x.GetAttributeValue("href", "")).ToList();


                        if (company.Jobs.Count > 0)
                        {
                            context.Jobs.RemoveRange(company.Jobs);
                            context.SaveChanges();
                        }

                        var jobs = GetJobs(jobList);
                        company.Jobs = jobs;

                        context.Companies.AddOrUpdate(company);
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
            var image = headerInfoNode.SelectSingleNode(".//div[@class='image']").GetAttributeValue("style", "").Split(new char[] { '(', ')' })[1].TrimStart('/');
            company.Name = name?.HtmlDecode();
            if (int.TryParse(views?.HtmlDecode(), out var view))
            {
                company.Views = view;
            }
            company.Image = image.HtmlDecode();
        }
        private void GetDetails(Company company, HtmlNode aboutCompanyNode)
        {
            var companyInfoDiv = aboutCompanyNode.SelectSingleNode(".//div[@class='company-info']");
            var descriptions = companyInfoDiv.SelectNodes(".//p[@class='professional-skills-description']");
            var industry = descriptions[0].InnerText.Split('\n').Last().Trim();
            var type = descriptions[1].InnerText.Split('\n').Last().Trim();
            var numberOfEmployees = descriptions[2].InnerText.Split('\n').Last().Trim();
            string dateՕfFoundation = null;
            if (descriptions.Count > 3)
            {
                dateՕfFoundation = descriptions[3].InnerText.Split('\n').Last().Trim();
            }

            var about = aboutCompanyNode.SelectSingleNode(".//div[@class='col-lg-8 col-md-8 about-text']").InnerText.Split(new[] { "\n\n" }, StringSplitOptions.None)[2].Trim();

            company.Industry = industry.HtmlDecode();
            company.Type = type.HtmlDecode();
            company.About = about.HtmlDecode();
            company.DateOfFoundation = dateՕfFoundation?.HtmlDecode();
            company.NumberOfEmployees = numberOfEmployees.HtmlDecode();
        }

        private void GetContact(Company company, HtmlNode contactDetails)
        {
            var infoListNodes = contactDetails.SelectNodes(".//p[@class='professional-skills-description']");
            var website = infoListNodes.FirstOrDefault(x => x.InnerText.Contains("Website"))?.SelectSingleNode(".//a").GetAttributeValue("href", "");
            var address = infoListNodes.FirstOrDefault(x => x.InnerText.Contains("Address"))?.InnerText.Split(':').LastOrDefault()?.Trim();

            company.Website = website?.HtmlDecode();
            company.Address = address?.HtmlDecode();

            var testimonial = contactDetails.SelectSingleNode(".//div[@id='testimonial']");
            if (testimonial is null) return;

            var socialMedia = testimonial.SelectSingleNode(".//ul[@class='clearfix']");
            var facebook = socialMedia.ChildNodes.FirstOrDefault(x => x.OuterHtml.Contains("facebook"))?.SelectSingleNode(".//a").GetAttributeValue("href", "");
            var linkedin = socialMedia.ChildNodes.FirstOrDefault(x => x.OuterHtml.Contains("linkedin"))?.SelectSingleNode(".//a").GetAttributeValue("href", "");
            var gPlus = socialMedia.ChildNodes.FirstOrDefault(x => x.OuterHtml.Contains("google-plus"))?.SelectSingleNode(".//a").GetAttributeValue("href", "");
            
            
            company.Facebook = facebook?.HtmlDecode();
            company.Linkedin = linkedin?.HtmlDecode();
            company.GooglePlus = gPlus?.HtmlDecode();
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
                    //var rowNode = jobPostNode.SelectSingleNode(".//div[@class ='row']");
                    //var descriptionNode = jobPostNode.SelectSingleNode(".//div[@class ='job-list-content-desc hs_line_break']");
                    //var skillNode = jobPostNode.SelectSingleNode(".//div[@class ='job-list-content-skills']");


                    var title = jobPostNode.SelectSingleNode(".//div[@class ='col-lg-8']").InnerText.Trim();
                    var deadline = jobPostNode.SelectSingleNode(".//div[@class ='col-lg-4 apply-btn-top']").SelectSingleNode(".//p").InnerText?.Replace("\n", " ").Replace(" Deadline: ", "");
                    var jobInfo = jobPostNode.SelectNodes(".//div[@class ='col-lg-6 job-info']").SelectMany(x => x.SelectNodes(".//p"))?.ToList();
                    if (jobInfo is null) continue;
                    var term = jobInfo.FirstOrDefault(x => x.InnerText.Contains("Employment term")).InnerText.Split(':').LastOrDefault()?.Trim();
                    var type = jobInfo.FirstOrDefault(x => x.InnerText.Contains("Job type")).InnerText.Split(':').LastOrDefault()?.Trim();
                    var category = jobInfo.FirstOrDefault(x => x.InnerText.Contains("Category")).InnerText.Split(':').LastOrDefault()?.Trim();
                    var location = jobInfo.FirstOrDefault(x => x.InnerText.Contains("Location")).InnerText.Split(':').LastOrDefault()?.Trim();

                    var descriptions = jobPostNode.SelectSingleNode(".//div[@class='job-list-content-desc hs_line_break']").InnerText.Trim().Split(new[] { "Job description:", "Job responsibilities", "Required qualifications", "Additional information" }, StringSplitOptions.RemoveEmptyEntries);


                    var profSkills = jobPostNode.SelectNodes(".//div[@class='soft-skills-list clearfix']")?.FirstOrDefault(x => x.InnerText.Contains("Professional skills"))?.SelectNodes(".//p")?.Select(x => x.InnerText.Trim());
                    var softSkills = jobPostNode.SelectNodes(".//div[@class='soft-skills-list clearfix']")?.FirstOrDefault(x => x.InnerText.Contains("Soft skills"))?.SelectNodes(".//p")?.Select(x => x.InnerText.Trim());

                    var additionalInformation = document.DocumentNode.SelectSingleNode(".//div[@class='application-procedures additional-information information_application_block']")?.InnerText;


                    var email = EmailRegex(jobHtmlContent);
                    var job = new Job
                    {
                        Title = title.HtmlDecode(),
                        Deadline = DateTime.Parse(deadline),
                        TimeType = type.HtmlDecode(),
                        Location = location.HtmlDecode(),
                        Category = category.HtmlDecode(),
                        Description = descriptions.Length > 0 ? descriptions[0]?.HtmlDecode() : null,
                        Responsibilities = descriptions.Length > 1 ? descriptions[1]?.HtmlDecode() : null,
                        RequiredQualifications = descriptions.Length > 2 ? descriptions[2]?.HtmlDecode() : null,
                        AdditionalInformation = additionalInformation?.HtmlDecode(),
                        EmploymentTerm = term?.HtmlDecode(),
                        Email = email?.HtmlDecode()
                    };
                    SetSkills(job, profSkills, softSkills);
                    jobs.Add(job);
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            return jobs;

        }

        private void SetSkills(Job job, IEnumerable<string> profSkills, IEnumerable<string> softSkills)
        {
            if (profSkills != null)
            {
                foreach (var profSkill in profSkills)
                {
                    job.StaffSkills.Add(new StaffSkill
                    {
                        Name = profSkill,
                        Type = "prof"
                    });
                }
            }
            if (softSkills != null)
            {

                foreach (var softSkill in softSkills)
                {
                    job.StaffSkills.Add(new StaffSkill
                    {
                        Name = softSkill,
                        Type = "soft"
                    });
                }

            }
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

        public static string EmailRegex(string text)
        {
            try
            {
                const string MatchEmailPattern =
                    @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
                Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                // Find matches.
                MatchCollection matches = rx.Matches(text);
                // Report the number of matches found.

                // Report on each match.
                foreach (Match match in matches)
                {
                    return match.Value;
                }
            }
            catch (Exception e)
            {
            }

            return "";
        }


    }
}
