using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.Data.LinkedinPageParser;
using MonitoringIT.DAL.Models;
using Newtonsoft.Json;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.LinkedinDataParser
{
    class Program
    {
        private const string rootLinkedin = @"https://www.linkedin.com";
        const string linkedinArmeninanLinkCSYSU = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetSchool=%5B%2210063%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLinkCSOtherSelected = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetSchool=%5B%2210034%22%2C%2210032%22%2C%2210047%22%2C%2210064%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLinkSO = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&origin=FACETED_SEARCH&title=Software%20engineer%20&page={page}";
        const string linkedinArmeninanLinkDeveloper = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&origin=FACETED_SEARCH&title=Developer&page={page}";

        const string linkedinArmeninanABetPicMentorEpam = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetPastCompany=%5B%222457%22%2C%22664495%22%2C%222697%22%2C%223007630%22%2C%223189499%22%2C%224972%22%2C%22253982%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLinkCurrent = @"https://www.linkedin.com/search/results/people/v2/?facetCurrentCompany=%5B%222457%22%2C%222697%22%2C%222988%22%2C%22115439%22%2C%22253982%22%2C%223007630%22%2C%223189499%22%5D&facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLink3plisCS = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetNetwork=%5B%22O%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLink3plusIT = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%2296%22%5D&facetNetwork=%5B%22O%22%5D&origin=FACETED_SEARCH&page={page}";
        private static List<Proxies> Proxies;


        static void Main(string[] args)
        {

            Linkedin linkedin=new Linkedin();
            linkedin.GetAlLinkedinProfiles();
            //List<string> all = new List<string>();
            //for (int i = 1; i < 8; i++)
            //{
            //    var readAllText = File.ReadAllText($"D:\\linkedin{i}.json");
            //    var deserializeObject = JsonConvert.DeserializeObject<List<string>>(readAllText);
            //    all.AddRange(deserializeObject);
            //}

            //var enumerable = all.Distinct().ToList();
            //File.WriteAllText("D:\\linkedinFull.json", JsonConvert.SerializeObject(enumerable));
            var listOfLinks = new List<string>();
            var proxyCounter = 0;
            //var webClient = new WebClient();
            //var context = new MonitoringContext();
            //var profileFirefox = new FirefoxProfile("C:\\Users\\vanik.hakobyan\\AppData\\Roaming\\Mozilla\\Firefox\\Profiles\\fu4ylhbv.f9p3vimg.Test");
            var profileFirefox = new FirefoxProfile();
            profileFirefox.SetPreference("permissions.default.image", 2);
            //var driver = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
            var driver = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
            //Proxies = context.Proxies.Where(x => x.Type == "HTTPS").OrderByDescending(x => x).ToList();
            for (var i = 1; i <= 90; i++)
            {
                //var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                //webClient.Proxy = wp;
                //var repositoryPage = webClient.DownloadString(linkedinArmeninanLinkCSYSU.Replace("{page}",i.ToString()));

                driver.Navigate().GoToUrl(linkedinArmeninanLinkCSYSU.Replace("{page}", i.ToString()));
                Scroll(driver);
                var repositoryPage = driver.PageSource;
                Thread.Sleep(10000);
                GetLink(repositoryPage, listOfLinks);
            }
            var serializeObject1 = JsonConvert.SerializeObject(listOfLinks);
            File.WriteAllText("D:\\linkedin1.json", serializeObject1);

            for (var i = 1; i <= 96; i++)
            {
                //var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                //webClient.Proxy = wp;
                //var repositoryPage = webClient.DownloadString(linkedinArmeninanLinkCSOtherSelected);
                driver.Navigate().GoToUrl(linkedinArmeninanLinkCSOtherSelected.Replace("{page}", i.ToString()));
                Scroll(driver);
                var repositoryPage = driver.PageSource;
                Thread.Sleep(10000);
                GetLink(repositoryPage, listOfLinks);
            }

            var serializeObject2 = JsonConvert.SerializeObject(listOfLinks);
            File.WriteAllText("D:\\linkedin2.json", serializeObject2);

            //var serializeObject2 = JsonConvert.SerializeObject(listOfLinks);
            //File.WriteAllText("D:\\linkedin2.json", serializeObject2);
            //listOfLinks.Clear();
            for (var i = 1; i <= 88; i++)
            {
                //var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                //webClient.Proxy = wp;
                //var repositoryPage = webClient.DownloadString(linkedinArmeninanLinkSO);
                driver.Navigate().GoToUrl(linkedinArmeninanLinkSO.Replace("{page}", i.ToString()));
                Scroll(driver);
                var repositoryPage = driver.PageSource;
                Thread.Sleep(10000);
                GetLink(repositoryPage, listOfLinks);
            }

            var serializeObject3 = JsonConvert.SerializeObject(listOfLinks);
            File.WriteAllText("D:\\linkedin3.json", serializeObject3);
            listOfLinks.Clear();
            for (var i = 1; i <= 84; i++)
            {
                //var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                //webClient.Proxy = wp;
                //var repositoryPage = webClient.DownloadString(linkedinArmeninanLinkDeveloper);
                driver.Navigate().GoToUrl(linkedinArmeninanLinkDeveloper.Replace("{page}", i.ToString()));
                Scroll(driver);
                var repositoryPage = driver.PageSource;
                Thread.Sleep(10000);
                GetLink(repositoryPage, listOfLinks);
            }



            var serializeObject4 = JsonConvert.SerializeObject(listOfLinks);
            File.WriteAllText("D:\\linkedin4.json", serializeObject4);



        }
        private static void Scroll(FirefoxDriver driver)
        {
            driver.ExecuteScript("scroll(0, 100);");
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 150);");
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 200);");
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 500);");
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 800);");
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 1000);");
        }

        public static void GetLink(string content, List<string> _links)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(content);
            var links = document.DocumentNode.SelectNodes(".//a[@class='search-result__result-link ember-view']")?.Select(x => x?.GetAttributeValue("href", "")).Distinct();

            if (links != null) _links.AddRange(links.Select(x => $"{rootLinkedin}{x}"));
        }



    }
}
