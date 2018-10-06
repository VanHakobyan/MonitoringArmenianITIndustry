using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.DAL.Models;
using OpenQA.Selenium.Chrome;

namespace MonitoringIT.Data.LinkedinPageParser
{
    public class Linkedin
    {
        private static List<string> _linkedinLinks;
        private static List<LinkedinProfile> LinkedinProfiles=new List<LinkedinProfile>();
        ChromeDriver _chromeDriver;
        public Linkedin()
        {
            using (MonitoringContext db = new MonitoringContext())
            {
                _linkedinLinks = db.Profiles.Where(x => x.BlogOrWebsite.Contains("linkedin")).Select(x=>x.BlogOrWebsite).ToList();
            }

            _chromeDriver= new ChromeDriver();
        }

        public void GetAlLinkedinProfiles()
        {
            foreach (var linkedinLink in _linkedinLinks)
            {
                _chromeDriver.Navigate().GoToUrl(linkedinLink);
                Thread.Sleep(3000);
                Scroll(_chromeDriver);
                var linkedinProfile = GetProfile(_chromeDriver.PageSource);
                LinkedinProfiles.Add(linkedinProfile);
            }
        }
        public LinkedinProfile GetProfile(string content)
        {
            var document=new HtmlDocument(); 
            document.LoadHtml(content);
            var fullName = document.DocumentNode.SelectSingleNode(".//h1[@class='pv-top-card-section__name inline Sans-26px-black-85%']").InnerText;
            var specialty = document.DocumentNode.SelectSingleNode(".//h2[@class='pv-top-card-section__headline mt1 Sans-19px-black-85%']").InnerText;
            var location = document.DocumentNode.SelectSingleNode(".//h3[@class='pv-top-card-section__location Sans-17px-black-55%-dense mt1 inline-block']").InnerText;
            var company = document.DocumentNode.SelectSingleNode(".//span[@class='pv-top-card-v2-section__entity-name pv-top-card-v2-section__company-name text-align-left ml2 Sans-15px-black-85%-semibold lt-line-clamp lt-line-clamp--multi-line ember-view']").InnerText;
            var education = document.DocumentNode.SelectSingleNode(".//span[@class='pv-top-card-v2-section__entity-name pv-top-card-v2-section__school-name text-align-left ml2 Sans-15px-black-85%-semibold lt-line-clamp lt-line-clamp--multi-line ember-view']").InnerText;
            var connectionCountStr = document.DocumentNode.SelectSingleNode(".//span[@class='pv-top-card-v2-section__entity-name pv-top-card-v2-section__connections ml2 Sans-15px-black-85%']").InnerText;
            var imageUrl = document.DocumentNode.SelectSingleNode(".//div[@class='pv-top-card-section__photo presence-entity__image EntityPhoto-circle-9 ember-view']").GetAttributeValue("style","").Split(new []{"url(\\"},StringSplitOptions.RemoveEmptyEntries)[1];
            var phone = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-phone']").SelectSingleNode(".//span[@class='Sans-15px-black-85%']").InnerText;
            var email = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-email']").SelectSingleNode(".//a[@class='pv-contact-info__contact-link Sans-15px-black-85%']").GetAttributeValue("href","").Replace("mailto:","");
            var birthday = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-birthday']").SelectSingleNode(".//span[@class='pv-contact-info__contact-item Sans-15px-black-85%']").InnerText;

            return null;
        }


        private static void Scroll(ChromeDriver driver)
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
    }
}
