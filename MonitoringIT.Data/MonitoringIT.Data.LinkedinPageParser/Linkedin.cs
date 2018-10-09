using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.DAL.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.LinkedinPageParser
{
    public class Linkedin
    {
        private static List<string> _linkedinLinks;
        private static List<LinkedinProfile> LinkedinProfiles = new List<LinkedinProfile>();
        FirefoxDriver _firefoxDriver;
        public Linkedin()
        {
            //using (MonitoringContext db = new MonitoringContext())
            //{
            //    _linkedinLinks = db.Profiles.Where(x => x.BlogOrWebsite.Contains("linkedin")).Select(x=>x.BlogOrWebsite).ToList();
            //}
            _linkedinLinks = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(@"D:\linkedinFull.json"));
            _firefoxDriver = new FirefoxDriver();
        }

        public void GetAlLinkedinProfiles()
        {
            foreach (var linkedinLink in _linkedinLinks)
            {
                _firefoxDriver.Navigate().GoToUrl(linkedinLink);
                Thread.Sleep(3000);
                Scroll(_firefoxDriver);
                var linkedinProfile = GetProfile(_firefoxDriver.PageSource);
                if (linkedinProfile != null) LinkedinProfiles.Add(linkedinProfile);
            }
        }
        public LinkedinProfile GetProfile(string content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);
            try
            {
                var fullName = document.DocumentNode.SelectSingleNode(".//h1[@class='pv-top-card-section__name inline Sans-26px-black-85%']")?.InnerText;
                var specialty = document.DocumentNode.SelectSingleNode(".//h2[@class='pv-top-card-section__headline mt1 Sans-19px-black-85%']")?.InnerText;
                var location = document.DocumentNode.SelectSingleNode(".//h3[@class='pv-top-card-section__location Sans-17px-black-55%-dense mt1 inline-block']")?.InnerText;
                var company = document.DocumentNode.SelectSingleNode(".//span[@class='lt-line-clamp__line lt-line-clamp__line--last']")?.InnerText;
                var education = document.DocumentNode.SelectSingleNode(".//span[starts-with(@class,'pv-top-card-v2-section__entity-name pv-top-card-v2-section__school-name')]")?.InnerText;
                var connectionCountStr = document.DocumentNode.SelectSingleNode(".//span[@class='pv-top-card-v2-section__entity-name pv-top-card-v2-section__connections ml2 Sans-15px-black-85%']")?.InnerText;
                var imageUrl = document.DocumentNode.SelectSingleNode(".//div[@class='pv-top-card-section__photo presence-entity__image EntityPhoto-circle-9 ember-view']")?.GetAttributeValue("style", "")?.Split(new[] { "url(&quot;" }, StringSplitOptions.RemoveEmptyEntries)?.ElementAt(1)?.TrimEnd(';', ')', ';');
            }
            catch (Exception e)
            {
            }

            var experienceSection = document.DocumentNode.SelectSingleNode(".//section[@id='experience-section']");
            if (experienceSection != null)
            {
                var companyCollection = experienceSection.SelectNodes(".//div[@class='pv-entity__summary-info pv-entity__summary-info--v2']");
                if (companyCollection != null)
                {
                    foreach (var experince in companyCollection)
                    {
                        try
                        {
                            var title = experince.SelectSingleNode(".//h3")?.InnerText;
                            var companyName = experince.SelectSingleNode(".//span[@class='pv-entity__secondary-title']")?.InnerText;
                            var range = experince.SelectSingleNode(".//h4[starts-with(@class,'pv-entity__date-range')]")?.SelectNodes(".//span")?.Last()?.InnerText;
                            var locationCompany = experince.SelectSingleNode(".//h4[starts-with(@class,'pv-entity__location')]")?.SelectNodes(".//span")?.Last()?.InnerText;
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
            var educationSection = document.DocumentNode.SelectSingleNode(".//section[@id='education-section']");
            if (educationSection != null)
            {
                var educationCollection = educationSection.SelectNodes(".//div[@class='pv-entity__summary-info ']");
                if (educationCollection != null)
                {
                    foreach (var experince in educationCollection)
                    {
                        try
                        {

                            var university = experince.SelectSingleNode(".//h3")?.InnerText;
                            var titleCol = experince.SelectNodes(".//span[@class='pv-entity__comma-item']")?.Select(x => x.InnerText);
                            if (titleCol != null)
                            {
                                var title = string.Join(" of ", titleCol);
                            }

                            var range = experince.SelectSingleNode(".//p[starts-with(@class,'pv-entity__dates')]")?.SelectNodes(".//time")?.Select(x => x?.InnerText);
                            if (range != null)
                            {
                                var dateRange = string.Join(" - ", range);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
            var skillSection = document.DocumentNode.SelectSingleNode(".//section[starts-with(@class,'pv-profile-section pv-skill-categories-section')]");
            if (skillSection != null)
            {
                var educationCollection = skillSection.SelectNodes(".//div[@class='pv-skill-category-entity__skill-wrapper tooltip-container']");
                if (educationCollection != null)
                {
                    foreach (var educationItem in educationCollection)
                    {
                        try
                        {
                            var skillName = educationItem.SelectSingleNode(".//p[@class='pv-skill-category-entity__name ']")?.InnerText;
                            var uproveCount = educationItem.SelectSingleNode(".//span[starts-with(@class,'pv-skill-category-entity__endorsement-count')]")?.InnerText;
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
            var interestsSection = document.DocumentNode.SelectSingleNode(".//section[starts-with(@class,'pv-profile-section pv-interests-section')]");
            if (interestsSection != null)
            {
                var interestsCollection = interestsSection.SelectNodes(".//div[@class='pv-entity__summary-info ember-view']");
                if (interestsCollection != null)
                {

                    foreach (var educationItem in interestsCollection)
                    {
                        try
                        {
                            var name = educationItem.SelectSingleNode(".//span[@class='pv-entity__summary-title-text']")?.InnerText;
                            var followersCount = educationItem.SelectSingleNode(".//p[starts-with(@class,'pv-entity__follower-count')]")?.InnerText;
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }
            }


            try
            {
                var findElementByXPath = _firefoxDriver.FindElementByXPath(".//a[@data-control-name='contact_see_more']");
                findElementByXPath.Click();
                document.LoadHtml(_firefoxDriver.PageSource);
                var phone = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-phone']")?.SelectSingleNode(".//span[@class='Sans-15px-black-85%']")?.InnerText;
                var email = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-email']")?.SelectSingleNode(".//a[@class='pv-contact-info__contact-link Sans-15px-black-85%']")?.GetAttributeValue("href", "")?.Replace("mailto:", "");
                var birthday = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-birthday']")?.SelectSingleNode(".//a[@class='pv-contact-info__contact-item Sans-15px-black-85%']")?.InnerText;
                var website = document.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-websites']")?.SelectSingleNode(".//a[starts-with(@class,'pv-contact-info__contact-link')]")?.GetAttributeValue("href", "");
            }
            catch (Exception e)
            {

            }
            return null;
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
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 1100);");
            Thread.Sleep(250);
            driver.ExecuteScript("scroll(0, 1500);");
            Thread.Sleep(500);
            driver.ExecuteScript("scroll(0, 0);");
        }
    }
}
