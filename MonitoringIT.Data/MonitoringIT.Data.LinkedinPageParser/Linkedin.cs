using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.DAL.WithEF6;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;


namespace MonitoringIT.Data.Linkedin.Scrapper.Lib
{
    public class Linkedin
    {
        public static List<string> _linkedinLinks;
        private readonly FirefoxDriver _driver;


        private readonly string _email = ConfigurationManager.AppSettings["email"];
        private readonly string _password = ConfigurationManager.AppSettings["password"];

        public Linkedin()
        {
            using (var entities = new MonitoringEntities())
            {
                var links = entities.LinkedinProfiles.Select(x => x.Username).ToList().Select(x => $"https://www.linkedin.com/in/{x}").ToList();
                _linkedinLinks = links;
                _driver = new FirefoxDriver();
            }
            Login();
        }

        public void Login()
        {
            _driver.Navigate().GoToUrl("https://www.linkedin.com/");
            Thread.Sleep(5000);
            var email = _driver.FindElementByXPath(".//input[@id='login-email']");
            var password = _driver.FindElementByXPath(".//input[@id='login-password']");
            email?.SendKeys(_email);
            password?.SendKeys(_password);
            var signin = _driver.FindElementByXPath(".//input[@id='login-submit']");

            signin?.Click();
        }
        public List<string> GetAlLinkedinProfiles(List<string> links = null)
        {
            var jsons = new List<string>();
            if (links != null) _linkedinLinks = links;
            foreach (var linkedinLink in _linkedinLinks)
            {

                _driver.Navigate().GoToUrl(linkedinLink);
                Thread.Sleep(4000);
                Scroll(_driver);

                var linkedinProfile = GetProfile(_driver.PageSource);
                Thread.Sleep(1000);
                if (linkedinProfile != null)
                {
                    try
                    {
                        var username = linkedinLink.Split(new[] { "in/" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault()?.TrimEnd('/');
                        linkedinProfile.Username = username;
                        using (MonitoringEntities monitoringEntities = new MonitoringEntities())
                        {
                            var user = monitoringEntities.LinkedinProfiles.Where(x => x.Username == username).ToList().FirstOrDefault();
                            if (user != null)
                            {
                                UpdateLinkedinProfile(linkedinProfile, user);
                                //monitoringEntities.Entry(user).State = EntityState.Modified;
                                monitoringEntities.LinkedinProfiles.AddOrUpdate(user);
                            }
                            else
                            {
                                monitoringEntities.LinkedinProfiles.Add(linkedinProfile); 
                            }
                            monitoringEntities.SaveChanges();
                        }
                        jsons.Add(JsonConvert.SerializeObject(linkedinProfile, Formatting.Indented,new JsonSerializerSettings(){ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            return jsons;

        }

        private static void UpdateLinkedinProfile(LinkedinProfile linkedinProfile, LinkedinProfile user)
        {
            if (linkedinProfile.LinkedinEducations.Count != 0 && user.LinkedinEducations.Count==0) user.LinkedinEducations = linkedinProfile.LinkedinEducations;
            if (linkedinProfile.LinkedinExperiences.Count != 0 && user.LinkedinExperiences.Count==0) user.LinkedinExperiences = linkedinProfile.LinkedinExperiences;
            if (linkedinProfile.LinkedinSkills.Count != 0 && user.LinkedinSkills.Count==0) user.LinkedinSkills = linkedinProfile.LinkedinSkills;
            if (linkedinProfile.LinkedinInterests.Count != 0 && user.LinkedinInterests.Count==0) user.LinkedinInterests = linkedinProfile.LinkedinInterests;
            if (linkedinProfile.LinkedinLanguages.Count != 0 && user.LinkedinLanguages.Count==0) user.LinkedinLanguages = linkedinProfile.LinkedinLanguages;
            if (linkedinProfile.Birthday != null) user.Birthday = linkedinProfile.Birthday;
            if (linkedinProfile.Company != null) user.Company = linkedinProfile.Company;
            if (linkedinProfile.Connected != null) user.Connected = linkedinProfile.Connected;
            if (linkedinProfile.ConnectionCount != null) user.ConnectionCount = linkedinProfile.ConnectionCount;
            if (linkedinProfile.Email != null) user.Email = linkedinProfile.Email;
            if (linkedinProfile.FullName != null) user.FullName = linkedinProfile.FullName;
            if (linkedinProfile.Location != null) user.Location = linkedinProfile.Location;
            if (linkedinProfile.Specialty != null) user.Specialty = linkedinProfile.Specialty;
            if (linkedinProfile.ImageUrl != null) user.ImageUrl = linkedinProfile.ImageUrl;
            if (linkedinProfile.Website != null) user.Website = linkedinProfile.Website;
            if (linkedinProfile.Education != null) user.Education = linkedinProfile.Education;
            if (linkedinProfile.Phone != null) user.Phone = linkedinProfile.Phone;
        }

        private static void SeeMoreSkills(FirefoxDriver jsExecutor)
        {
            try
            {
                var seeMore = jsExecutor.FindElementByXPath(".//button[@data-control-name='skill_details']");
                seeMore?.Click();
            }
            catch (Exception e)
            {
            }
        }

        public LinkedinProfile GetProfile(string content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);
            var linkedinProfile = new LinkedinProfile();
            try
            {
                var fullName = document.DocumentNode.SelectSingleNode(".//h1[starts-with(@class,'pv-top-card-section__name')]")?.InnerText;
                var specialty = document.DocumentNode.SelectSingleNode(".//h2[starts-with(@class,'pv-top-card-section__headline')]")?.InnerText;
                var location = document.DocumentNode.SelectSingleNode(".//h3[starts-with(@class,'pv-top-card-section__location')]")?.InnerText;
                var company = document.DocumentNode.SelectSingleNode(".//span[@class='lt-line-clamp__line lt-line-clamp__line--last']")?.InnerText;
                var education = document.DocumentNode.SelectSingleNode(".//span[starts-with(@class,'pv-top-card-v2-section__entity-name pv-top-card-v2-section__school-name')]")?.InnerText;
                var connectionCountStr = document.DocumentNode.SelectSingleNode(".//span[starts-with(@class,'pv-top-card-v2-section__entity-name pv-top-card-v2-section__connections')]")?.InnerText;
                var imageUrlArray = document.DocumentNode.SelectSingleNode(".//div[starts-with(@class,'pv-top-card-section__photo presence-entity__image')]")?.GetAttributeValue("style", "")?.Split(new[] { "url(&quot;" }, StringSplitOptions.RemoveEmptyEntries);
                var imageUrl = imageUrlArray?.Count() == 0 || imageUrlArray == null ? null : imageUrlArray?.ElementAt(1)?.TrimEnd(';', ')', ';');
                linkedinProfile.FullName = StringBeauty(fullName);
                linkedinProfile.Specialty = StringBeauty(specialty);
                linkedinProfile.Location = StringBeauty(location);
                linkedinProfile.Company = StringBeauty(company);
                linkedinProfile.Education = StringBeauty(education);
                int.TryParse(StringBeauty(connectionCountStr?.Replace("connections", "")?.Replace("+", "")), out var conCountInt);
                linkedinProfile.ConnectionCount = conCountInt;
                linkedinProfile.ImageUrl = HtmlDecode(imageUrl);
            }
            catch (Exception e)
            {
            }

            var experienceSection = document.DocumentNode.SelectSingleNode(".//section[@id='experience-section']");
            if (experienceSection != null)
            {
                var companyCollection = experienceSection.SelectNodes(".//div[starts-with(@class,'pv-entity__summary-info')]");
                if (companyCollection != null)
                {

                    linkedinProfile.LinkedinExperiences = new List<LinkedinExperience>();
                    foreach (var experince in companyCollection)
                    {
                        try
                        {
                            var linkedinExperience = new LinkedinExperience();
                            var title = experince.SelectSingleNode(".//h3")?.InnerText?.Replace("Title", "");
                            var companyName = experince.SelectSingleNode(".//span[@class='pv-entity__secondary-title']")?.InnerText;
                            var range = experince.SelectSingleNode(".//h4[starts-with(@class,'pv-entity__date-range')]")?.SelectNodes(".//span")?.Last()?.InnerText;
                            var locationCompany = experince.SelectSingleNode(".//h4[starts-with(@class,'pv-entity__location')]")?.SelectNodes(".//span")?.Last()?.InnerText;

                            linkedinExperience.Title = StringBeauty(title);
                            linkedinExperience.Company = StringBeauty(companyName);
                            linkedinExperience.Time = StringBeauty(range);
                            linkedinExperience.Location = StringBeauty(locationCompany);

                            linkedinProfile.LinkedinExperiences.Add(linkedinExperience);
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
                var educationCollection = educationSection.SelectNodes(".//div[starts-with(@class,'pv-entity__summary-info')]");
                if (educationCollection != null)
                {
                    linkedinProfile.LinkedinEducations = new List<LinkedinEducation>();
                    foreach (var education in educationCollection)
                    {
                        try
                        {
                            var linkedinEducation = new LinkedinEducation();
                            var university = education.SelectSingleNode(".//h3")?.InnerText;
                            linkedinEducation.Name = HtmlDecode(StringBeauty(university));
                            var titleCol = education.SelectNodes(".//span[starts-with(@class,'pv-entity__comma-item')]")?.Select(x => x.InnerText);
                            if (titleCol != null)
                            {
                                var title = StringBeauty(string.Join(" of ", titleCol));
                                linkedinEducation.Title = title;
                            }

                            var range = education.SelectSingleNode(".//p[starts-with(@class,'pv-entity__dates')]")?.SelectNodes(".//time")?.Select(x => x?.InnerText);
                            if (range != null)
                            {
                                var dateRange = string.Join(" - ", range);
                                linkedinEducation.Time = StringBeauty(dateRange);
                            }

                            linkedinProfile.LinkedinEducations.Add(linkedinEducation);
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
                var skillCollection = skillSection.SelectNodes(".//div[@class='pv-skill-category-entity__skill-wrapper tooltip-container']");
                if (skillCollection != null)
                {
                    linkedinProfile.LinkedinSkills = new List<LinkedinSkill>();
                    foreach (var skillItem in skillCollection)
                    {
                        var linkedinSkill = new LinkedinSkill();
                        try
                        {
                            var skillName = skillItem.SelectSingleNode(".//p[starts-with(@class,'pv-skill-category-entity__name')]")?.InnerText;
                            var uproveCount = skillItem.SelectSingleNode(".//span[starts-with(@class,'pv-skill-category-entity__endorsement-count')]")?.InnerText;
                            linkedinSkill.Name = StringBeauty(skillName);
                            int.TryParse(StringBeauty(uproveCount), out var uproveInt);
                            linkedinSkill.EndorsedCount = uproveInt;

                            linkedinProfile.LinkedinSkills.Add(linkedinSkill);
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
                    linkedinProfile.LinkedinInterests = new List<LinkedinInterest>();
                    foreach (var interesItem in interestsCollection)
                    {
                        try
                        {
                            var linkedinInterest = new LinkedinInterest();

                            var name = interesItem.SelectSingleNode(".//span[@class='pv-entity__summary-title-text']")?.InnerText;
                            var followersCount = interesItem.SelectSingleNode(".//p[starts-with(@class,'pv-entity__follower-count')]")?.InnerText?.Replace("followers", "")?.Replace(",", "");

                            int.TryParse(StringBeauty(followersCount), out var followersInt);

                            linkedinInterest.Name = StringBeauty(name);
                            linkedinInterest.FollowersCount = followersInt;

                            linkedinProfile.LinkedinInterests.Add(linkedinInterest);
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }
            }
            var accomplishmentsSection = document.DocumentNode.SelectSingleNode(".//section[starts-with(@class,'pv-profile-section pv-accomplishments-section')]");
            if (accomplishmentsSection != null)
            {
                var accomplishmentsCollection = accomplishmentsSection.SelectNodes(".//li[@class='pv-accomplishments-block__summary-list-item']");
                if (accomplishmentsCollection != null)
                {
                    linkedinProfile.LinkedinLanguages = new List<LinkedinLanguage>();
                    foreach (var accomplishmentItem in accomplishmentsCollection)
                    {
                        try
                        {
                            var linkedinInterest = new LinkedinLanguage { Name = StringBeauty(accomplishmentItem.InnerText) };

                            linkedinProfile.LinkedinLanguages.Add(linkedinInterest);
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }
            }

            try
            {
                var documentInfo = new HtmlDocument();
                var findElementByXPath = _driver.FindElementByXPath(".//a[@data-control-name='contact_see_more']");
                findElementByXPath.Click();
                Thread.Sleep(700);
                documentInfo.LoadHtml(_driver.PageSource);
                var phone = documentInfo.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-phone']")?.SelectSingleNode(".//span[@class='Sans-15px-black-85%']")?.InnerText;
                var email = documentInfo.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-email']")?.SelectSingleNode(".//a[@class='pv-contact-info__contact-link Sans-15px-black-85%']")?.GetAttributeValue("href", "")?.Replace("mailto:", "");
                var birthday = documentInfo.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-birthday']")?.SelectSingleNode(".//a[@class='pv-contact-info__contact-item Sans-15px-black-85%']")?.InnerText;
                var website = documentInfo.DocumentNode.SelectSingleNode(".//section[@class='pv-contact-info__contact-type ci-websites']")?.SelectSingleNode(".//a[starts-with(@class,'pv-contact-info__contact-link')]")?.GetAttributeValue("href", "");
                //var userName = documentInfo.DocumentNode.SelectSingleNode(".//h1[@id='pv-contact-info']")?.InnerText;


                linkedinProfile.Email = StringBeauty(email);
                linkedinProfile.Phone = StringBeauty(phone);
                linkedinProfile.Birthday = StringBeauty(birthday);
                linkedinProfile.Website = StringBeauty(website);
                //linkedinProfile.Username = StringBeauty(userName);
                Thread.Sleep(250);
                var findElementByXPathClose = _driver.FindElementByXPath(".//button[@class='artdeco-dismiss']");
                findElementByXPathClose?.Click();
                Thread.Sleep(250);
            }
            catch (Exception e)
            {

            }
            try
            {
                var findElementByXPath = _driver.FindElementByXPath(".//button[starts-with(@class,'pv-s-profile-actions pv-s-profile-actions--connect')]");
                findElementByXPath?.Click();
                //pv-s-profile-actions pv-s-profile-actions--connect
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return linkedinProfile;
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
            Thread.Sleep(250);
            SeeMoreSkills(driver);
            driver.ExecuteScript("scroll(0, 1100);");
            Thread.Sleep(650);
            driver.ExecuteScript("scroll(0, 1500);");
            Thread.Sleep(950);
            driver.ExecuteScript("scroll(0, 2000);");
            Thread.Sleep(500);
            driver.ExecuteScript("scroll(0, 3000);");
            Thread.Sleep(400);
            driver.ExecuteScript("scroll(0, 4000);");
            Thread.Sleep(300);
            driver.ExecuteScript("scroll(0, 6000);");
            Thread.Sleep(300);
            driver.ExecuteScript("scroll(0, 9000);");
            Thread.Sleep(500);
            driver.ExecuteScript("scroll(0, 1000);");
            Thread.Sleep(800);
            driver.ExecuteScript("scroll(0, 0);");
            Thread.Sleep(800);
        }

        private static string StringBeauty(string s) => s?.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();

        private static string HtmlDecode(string s) => s == null ? null : WebUtility.HtmlDecode(s);


    }
}
