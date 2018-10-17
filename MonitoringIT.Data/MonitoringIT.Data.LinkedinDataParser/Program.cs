using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.LinkedinDataParser
{
    class Program
    {
        public static string FirefoxProfilePath { get; } = ConfigurationManager.AppSettings["FirefoxProfilePath"];
        private const string rootLinkedin = @"https://www.linkedin.com";

        const string linkedinArmeninanLinkCSYSU = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetSchool=%5B%2210063%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLinkCSOtherSelected = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetSchool=%5B%2210034%22%2C%2210032%22%2C%2210047%22%2C%2210064%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLinkSO = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&origin=FACETED_SEARCH&title=Software%20engineer%20&page={page}";
        const string linkedinArmeninanLinkDeveloper = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&origin=FACETED_SEARCH&title=Developer&page={page}";

        const string linkedinArmeninanABetPicMentorEpam = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetPastCompany=%5B%222457%22%2C%22664495%22%2C%222697%22%2C%223007630%22%2C%223189499%22%2C%224972%22%2C%22253982%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLinkCurrent = @"https://www.linkedin.com/search/results/people/v2/?facetCurrentCompany=%5B%222457%22%2C%222697%22%2C%222988%22%2C%22115439%22%2C%22253982%22%2C%223007630%22%2C%223189499%22%5D&facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLink3plisCS = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%224%22%5D&facetNetwork=%5B%22O%22%5D&origin=FACETED_SEARCH&page={page}";
        const string linkedinArmeninanLink3plusIT = @"https://www.linkedin.com/search/results/people/v2/?facetGeoRegion=%5B%22am%3A0%22%5D&facetIndustry=%5B%2296%22%5D&facetNetwork=%5B%22O%22%5D&origin=FACETED_SEARCH&page={page}";

        static void Main(string[] args)
        {

            var linkedin = new Linkedin.Scrapper.Lib.Linkedin();
            var alLinkedinProfiles = linkedin.GetAlLinkedinProfiles();


            var profileFirefox = new FirefoxProfile(FirefoxProfilePath);
            profileFirefox.SetPreference("permissions.default.image", 2);
            var driver = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
            GetLinks(driver, @"D:\linkedin.json", 100);

        }

        private static void GetLinks(FirefoxDriver driver, string pathToSave, int pageCount)
        {
            var listOfLinks = new List<string>();
            for (var i = 1; i <= pageCount; i++)
            {
                driver.Navigate().GoToUrl(linkedinArmeninanLinkCSOtherSelected.Replace("{page}", i.ToString()));
                Scroll(driver);
                var repositoryPage = driver.PageSource;
                Thread.Sleep(10000);
                GetLink(repositoryPage, listOfLinks);
            }

            var serializeObject = JsonConvert.SerializeObject(listOfLinks);
            File.WriteAllText(pathToSave, serializeObject);
        }

        private static void Scroll(IJavaScriptExecutor driver)
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

        public static void GetLink(string content, List<string> links)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);
            var linksSearchResult = document.DocumentNode.SelectNodes(".//a[@class='search-result__result-link ember-view']")?.Select(x => x?.GetAttributeValue("href", "")).Distinct();

            if (linksSearchResult != null) links.AddRange(linksSearchResult.Select(x => $"{rootLinkedin}{x}"));
        }



    }
}
