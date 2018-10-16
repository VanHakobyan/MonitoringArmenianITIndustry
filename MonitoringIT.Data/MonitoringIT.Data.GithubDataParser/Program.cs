using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using MonitoringIT.Data.ProxyParser;
using MonitoringIT.DAL.Models;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.Data.GithubDataParser
{
    class Program
    {
        static string Url = @"https://github.com/search?l=&p={page}&q=location%3Aarmenia+repos%3A%3E0&ref=advsearch&type=Users&utf8=%E2%9C%93";

        private static string rootGithub = @"https://github.com/";
        private static List<Proxies> Proxies;
        private static List<string> _links = new List<string>();
        private const string repoUrlString = "?tab=repositories";
        static void Main(string[] args)
        {
            var db = new MonitoringContext();
            //HidemeParser pr=new HidemeParser();
            //var proxies = pr.GetProxy().Result.ToList();
            //foreach (var proxy in proxies)
            //{
            //    db.Proxies.Add(new Proxies{Country = proxy.Country,Port = proxy.Port,Type = proxy.Type,Ip = proxy.Ip});
            //}

            //db.SaveChanges();
            Proxies = db.Proxies.Where(x => x.Type == "HTTPS").OrderByDescending(x => x).ToList();
            var profileses = db.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).ToList();

            foreach (var profile in profileses)
            {
                Thread.Sleep(5000);
                var repositorieses = GetRepositories(profile.Url);
                if (repositorieses != null)
                {
                    profile.Repositories = repositorieses;
                    db.SaveChanges();
                }
            }

            var profileFirefox = new FirefoxProfile("C:\\Users\\vanik.hakobyan\\AppData\\Roaming\\Mozilla\\Firefox\\Profiles\\fu4ylhbv.f9p3vimg.Test");
            var driver = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
            foreach (var link in db.Profiles.Select(x => x.Url).ToList())
            {
                driver.Navigate().GoToUrl(link);
                Thread.Sleep(2000);
                var profile = GetGithubProfileSelenum(driver.PageSource, link);
                if (profile != null) db.Profiles.Add(profile);
            }

            Console.WriteLine();
        }

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
                        var links = profilsNode.SelectNodes(".//a").Select(x => x.InnerText).Where(x => !x.Contains("\n")).Select(x => x.Insert(0, rootGithub));
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
            if (content.Contains("org-name lh-condensed")) return null;
            var document = new HtmlDocument();
            document.LoadHtml(content);

            try
            {
                var imageUrl = document.DocumentNode.SelectSingleNode(".//img[@class='avatar width-full rounded-2']").GetAttributeValue("src", "");
                var email = document.DocumentNode.SelectSingleNode(".//a[@class='u-email']")?.GetAttributeValue("href", "")?.Split(new[] { "mailto:" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                var name = document.DocumentNode.SelectSingleNode(".//span[@class='p-name vcard-fullname d-block overflow-hidden']")?.InnerText;
                var userName = document.DocumentNode.SelectSingleNode(".//span[@class='p-nickname vcard-username d-block']")?.InnerText;
                var company = document.DocumentNode.SelectSingleNode(".//a[@class='user-mention']")?.InnerText;
                var location = document.DocumentNode.SelectSingleNode(".//span[@class='p-label']")?.InnerText;
                var social = document.DocumentNode.SelectSingleNode(".//a[@class='u-url']")?.InnerText;
                var bio = document.DocumentNode.SelectSingleNode(".//div[@class='p-note user-profile-bio']")?.InnerText;
                var starCount = document.DocumentNode.SelectSingleNode(".//span[@class='Counter']")?.InnerText?.Replace(" ", "").Replace("\n", "").Replace("\r", " ").Trim() ?? "-1";

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


        public static List<Repositories> GetRepositories(string urlProfile)
        {
            var repositories = new List<Repositories>();
            var proxyCounter = 0;
            var proxyCounterNested = 0;
            var listRepoLink = new List<string>();
            var webClient = new WebClient();
            var linkToRepos = $"{urlProfile}{repoUrlString}";


            while (true)
            {
                try
                {
                    var wp = new WebProxy($"{Proxies[proxyCounter].Ip}:{Proxies[proxyCounter].Port}");
                    webClient.Proxy = wp;
                    var repositoryPage = webClient.DownloadString(linkToRepos);
                    Thread.Sleep(3500);
                    var repoDocument = new HtmlDocument();
                    repoDocument.LoadHtml(repositoryPage);
                    var reposLink = repoDocument.DocumentNode.SelectNodes(".//a[@itemprop='name codeRepository']")?.Select(x => x?.GetAttributeValue("href", "")).ToList();
                    if (reposLink == null) return null;
                    listRepoLink.AddRange(reposLink);
                    var pagination = repoDocument.DocumentNode.SelectSingleNode(".//div[@class='pagination']");
                    var nextPageUrl = pagination?.SelectSingleNode(".//a[@rel='nofollow']").GetAttributeValue("href", "");
                    if (nextPageUrl != null && nextPageUrl.Contains("after")) linkToRepos = nextPageUrl;
                    else break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var repoLink in listRepoLink)
            {
                var repo = new Repositories();
                while (true)
                {
                    try
                    {

                        var client = new WebClient();
                        var wpNested = new WebProxy($"{Proxies[proxyCounterNested].Ip}:{Proxies[proxyCounterNested].Port}");
                        client.Proxy = wpNested;
                        var repoData = client.DownloadString($"{rootGithub}{repoLink}");
                        Thread.Sleep(3500);
                        var repoDoc = new HtmlDocument();
                        repoDoc.LoadHtml(repoData);
                        var starsCount = repoDoc.DocumentNode.SelectSingleNode(".//a[@class='social-count js-social-count']")?.InnerText?.Replace("\n", "")?.Replace("\r", "")?.Replace("\t", "")?.Trim();
                        var forksCount = repoDoc.DocumentNode.SelectNodes(".//a[@class='social-count']")?.LastOrDefault()?.InnerText?.Replace("\n", "")?.Replace("\r", "")?.Replace("\t", "")?.Trim();
                        var readme = repoDoc.DocumentNode.SelectSingleNode(".//article[@class='markdown-body entry-content']")?.InnerHtml;

                        try
                        {
                            repo.Name = repoDoc.DocumentNode.SelectSingleNode(".//strong[@itemprop='name']").InnerText;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        var numberSummary = repoDoc.DocumentNode.SelectNodes(".//ul[@class='numbers-summary']");
                        if (numberSummary != null)
                        {
                            var summary = numberSummary[0].ChildNodes?.Where(x => x.OuterHtml.Contains("li"))?.ToList();
                            int.TryParse(summary[0].InnerText?.Replace("commits", "").Replace("\n", "")?.Replace("\r", "")?.Replace("\t", "")?.Trim(), out var commitsCount);
                            int.TryParse(summary[1].InnerText?.Replace("branch", "").Replace("\n", "")?.Replace("\r", "")?.Replace("\t", "")?.Trim(), out var branchsCount);
                            int.TryParse(summary[3].InnerText?.Replace("contributor", "").Replace("\n", "")?.Replace("\r", "")?.Replace("\t", "")?.Trim(), out var contributorsCount);


                            repo.BranchCount = branchsCount;
                            repo.CommitCount = commitsCount;
                            repo.ContributorsCount = contributorsCount != 0 ? contributorsCount : 1;
                        }
                        var repositoryLangStats = repoDoc.DocumentNode.SelectSingleNode(".//div[@class='repository-lang-stats']");
                        if (repositoryLangStats != null)
                        {
                            repo.Languages = new List<Languages>();
                            var langs = repositoryLangStats.SelectNodes(".//span[@class='lang']");
                            var percents = repositoryLangStats.SelectNodes(".//span[@class='percent']");
                            for (int i = 0; i < langs.Count; i++)
                            {
                                try
                                {
                                    var language = new Languages();
                                    language.Name = langs[i]?.InnerText;
                                    decimal.TryParse(percents[i]?.InnerText.Replace("%", ""), out var p);
                                    language.Percent = p;
                                    repo.Languages.Add(language);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                            }
                        }

                        repo.Url = $"{rootGithub}{repoLink}";
                        repo.Readme = readme;
                        int.TryParse(forksCount, out var fork);
                        repo.ForksCount = fork;
                        int.TryParse(starsCount, out var star);
                        repo.StarsCount = star;

                        break;
                    }
                    catch (Exception e)
                    {
                        proxyCounterNested++;
                    }
                }
                repositories.Add(repo);
            }
            return repositories;

        }
    }
}
