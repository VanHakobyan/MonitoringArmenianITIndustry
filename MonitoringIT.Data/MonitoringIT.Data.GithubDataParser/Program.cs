using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.Data.ProxyParser;
using MonitoringIT.DAL.Models;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.GithubDataParser
{
    class Program
    {
        static string Url = @"https://github.com/search?l=&o=desc&p={page}&q=location%3Aarmenia&s=repositories&ref=advsearch&type=Users&utf8=%E2%9C%93&_pjax=%23js-pjax-container";
        private static string rootGithub = @"https://github.com/";
        private static List<Proxies> Proxies;
        private static List<string> _links = new List<string>();
        static void Main(string[] args)
        {
            MonitoringContext db = new MonitoringContext();
            //HidemeParser hidemeParser=new HidemeParser();
            //var proxys = hidemeParser.GetProxy().Result;
            //db.Proxies.AddRange(proxys.Select(x => new Proxies{Ip = x.Ip,Type = x.Type,Port = x.Port,Country = x.Country}));
            //db.SaveChanges();
            Proxies = db.Proxies.Where(x=>x.Type=="HTTPS").ToList();
            //GetGithubUrls(_links);
            
            var profileFirefox = new FirefoxProfile("C:\\Users\\vanik.hakobyan\\AppData\\Roaming\\Mozilla\\Firefox\\Profiles\\fu4ylhbv.f9p3vimg.Test");
            var driver = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
            foreach (var link in _links)
            {
                driver.Navigate().GoToUrl(link);
                Thread.Sleep(2000);
                var profile = GetGithubProfileSelenum(driver.PageSource, link);
                if (profile != null) db.Profiles.Add(profile);
            }
            Console.WriteLine();
        }
        //E37962750
        private static void GetGithubUrls(List<string> Links)
        {
            var proxyCounter = 0;
            WebClient client = new WebClient();
            for (int i = 1; i <= 100; i++)
            {
                while (true)
                {
                    var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                    client.Proxy = wp;
                    HtmlDocument document = new HtmlDocument();
                    try
                    {
                        var pageSource = client.DownloadString(Url.Replace("{page}", i.ToString()));
                        document.LoadHtml(pageSource);
                        var profilsNode = document.DocumentNode.SelectSingleNode(".//div[@class='user-list']");
                        var links = profilsNode.SelectNodes(".//a").Select(x => x.InnerText).Where(x => !x.Contains("\n"))
                            .Select(x => x.Insert(0, rootGithub));
                        foreach (var link in links)
                        {
                            if (!Links.Contains(link) && !link.Contains("/&#")) Links.Add(link);
                        }
                        Thread.Sleep(2500);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        proxyCounter++;
                    }
                }
            }
        }


        public static Profiles GetGithubProfileSelenum(string content, string link)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);

            try
            {
                var imageUrl = document.DocumentNode.SelectSingleNode(".//img[@class='avatar width-full rounded-2']").GetAttributeValue("src", "");
                var email = document.DocumentNode.SelectSingleNode(".//a[@class='u-email']").GetAttributeValue("href", "").Split(new[] { "mailto:" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                var name = document.DocumentNode.SelectSingleNode(".//span[@class='p-name vcard-fullname d-block overflow-hidden']").InnerText;
                var userName = document.DocumentNode.SelectSingleNode(".//span[@class='p-nickname vcard-username d-block']").InnerText;
                var company = document.DocumentNode.SelectSingleNode(".//a[@class='user-mention']").InnerText;
                var location = document.DocumentNode.SelectSingleNode(".//span[@class='p-label']").InnerText;
                var social = document.DocumentNode.SelectSingleNode(".//a[@class='u-url']").InnerText;
                var bio = document.DocumentNode.SelectSingleNode(".//div[@class='p-note user-profile-bio']").InnerText;
                var starCount = document.DocumentNode.SelectSingleNode(".//span[@class='Counter']").InnerText;

                var profile = new Profiles
                {
                    Url = link,
                    Bio = bio,
                    BlogOrWebsite = social,
                    Company = company,
                    Email = email,
                    ImageUrl = imageUrl,
                    Location = location,
                    Name = name,
                    UserName = userName,
                    StarsCount = int.Parse(starCount)
                };
                return profile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void GetGitHubData()
        {
            var urlJson = File.ReadAllText(@"D:\linksGithub.json");
            var listUrls = JsonConvert.DeserializeObject<List<string>>(urlJson);
            var proxyCounter = 0;
            foreach (var url in listUrls)
            {
                while (true)
                {
                    var client = new WebClient();
                    var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                    client.Proxy = wp;
                    try
                    {
                        var contentProfile = client.DownloadString(url);
                        var document = new HtmlDocument();
                        document.LoadHtml(contentProfile);

                    }
                    catch (Exception e)
                    {
                        proxyCounter++;
                    }

                }

            }
        }
    }
}
