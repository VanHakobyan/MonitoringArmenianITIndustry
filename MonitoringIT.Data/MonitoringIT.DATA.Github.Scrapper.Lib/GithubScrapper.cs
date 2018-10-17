using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.Data.ProxyParser;
using MonitoringIT.DAL.WithEF6;
using OpenQA.Selenium.Firefox;

namespace MonitoringIT.DATA.Github.Scrapper.Lib
{
    public class GithubScrapper
    {
        private const string Url = @"https://github.com/search?l=&p={page}&q=location%3Aarmenia+repos%3A%3E0&ref=advsearch&type=Users&utf8=%E2%9C%93";

        public static string FirefoxProfilePath { get; } = ConfigurationManager.AppSettings["FirefoxProfilePath"];

        private const string RootGithub = @"https://github.com/";
        private static List<Proxy> _proxies;
        private const string RepoUrlString = "?tab=repositories";

        /// <summary>
        /// Start all scrapping methods
        /// </summary>
        public void Start()
        {
            ScrapProxyFromHideMe();
            GetRepositories();
            GetGithubProfileSelenum();
        }

        /// <summary>
        /// Get and save in DB Github profile using selenum 
        /// </summary>
        public static void GetGithubProfileSelenum()
        {
            using (var db = new MonitoringEntities())
            {

                var profileFirefox = new FirefoxProfile(FirefoxProfilePath);
                var driver = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
                foreach (var link in db.Profiles.Select(x => x.Url).ToList())
                {
                    driver.Navigate().GoToUrl(link);
                    Thread.Sleep(2000);
                    var profile = GetGithubProfileSelenum(driver.PageSource, link);
                    if (profile != null) db.Profiles.AddOrUpdate(profile);
                }
            }
        }

        /// <summary>
        /// Get and save in DB all  Github profile repasitories
        /// </summary>
        public static void GetRepositories()
        {
            using (var db = new MonitoringEntities())
            {
                var profiles = db.Profiles.ToList();
                foreach (var profile in profiles)
                {
                    Thread.Sleep(5000);
                    var repositorieses = GetRepositories(profile.Url);
                    if (repositorieses != null)
                    {
                        profile.Repositories = repositorieses;
                        db.SaveChanges();
                    }
                }
            }
        }


        /// <summary>
        /// Scrape and save in DB proxies
        /// <param name="cookie">Hideme request cookie</param>
        /// </summary>
        public static void ScrapProxyFromHideMe(string cookie = null)
        {
            var pr = new HidemeParser();
            var proxies = pr.GetProxy(cookie).Result.ToList();
            using (var db = new MonitoringEntities())
            {
                foreach (var proxy in proxies)
                {
                    db.Proxies.Add(new Proxy { Country = proxy.Country, Port = proxy.Port, Type = proxy.Type, Ip = proxy.Ip });
                }
                db.SaveChanges();
                _proxies = db.Proxies.Where(x => x.Type == "HTTPS").ToList();
            }
        }


        /// <summary>
        /// Get Github profile urls
        /// </summary>
        /// <returns>all finded profile url</returns>
        private static List<string> GetGithubUrls()
        {
            var links = new List<string>();
            var proxyCounter = 0;
            var client = new WebClient();
            for (var i = 1; i <= 100; i++)
            {
                while (true)
                {
                    var wp = new WebProxy($"{_proxies[proxyCounter].Ip}:{_proxies[proxyCounter].Port}");
                    client.Proxy = wp;
                    var document = new HtmlDocument();
                    try
                    {
                        var pageSource = client.DownloadString(Url.Replace("{page}", i.ToString()));
                        document.LoadHtml(pageSource);
                        var profilsNode = document.DocumentNode.SelectSingleNode(".//div[@class='user-list']");
                        var linksSelect = profilsNode.SelectNodes(".//a").Select(x => x.InnerText).Where(x => !x.Contains("\n")).Select(x => x.Insert(0, RootGithub));
                        foreach (var link in linksSelect)
                        {
                            if (!links.Contains(link) && !link.Contains("/&#")) links.Add(link);
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
            return links;
        }

        /// <summary>
        /// Get Gihub profile general information
        /// </summary>
        /// <param name="content">Github general page HTML content </param>
        /// <param name="link">Github general page url</param>
        /// <returns>General information from profile </returns>
        public static Profile GetGithubProfileSelenum(string content, string link)
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

                var profile = new Profile
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

        /// <summary>
        /// Get all repositories for user
        /// </summary>
        /// <param name="urlProfile">User url</param>
        /// <returns>List of repositories</returns>
        public static List<Repository> GetRepositories(string urlProfile)
        {
            var repositories = new List<Repository>();
            var proxyCounter = 0;
            var proxyCounterNested = 0;
            var listRepoLink = new List<string>();
            var webClient = new WebClient();
            var linkToRepos = $"{urlProfile}{RepoUrlString}";


            while (true)
            {
                try
                {
                    var wp = new WebProxy($"{_proxies[proxyCounter].Ip}:{_proxies[proxyCounter].Port}");
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
                var repo = new Repository();
                while (true)
                {
                    try
                    {

                        var client = new WebClient();
                        var wpNested = new WebProxy($"{_proxies[proxyCounterNested].Ip}:{_proxies[proxyCounterNested].Port}");
                        client.Proxy = wpNested;
                        var repoData = client.DownloadString($"{RootGithub}{repoLink}");
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
                            repo.Languages = new List<Language>();
                            var langs = repositoryLangStats.SelectNodes(".//span[@class='lang']");
                            var percents = repositoryLangStats.SelectNodes(".//span[@class='percent']");
                            for (int i = 0; i < langs.Count; i++)
                            {
                                try
                                {
                                    var language = new Language();
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

                        repo.Url = $"{RootGithub}{repoLink}";
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
