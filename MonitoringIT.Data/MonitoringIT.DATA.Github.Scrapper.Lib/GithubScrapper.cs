using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Database.MonitoringIT.DAL.WithEF6;
using HtmlAgilityPack;
using Lib.MonitoringIT.Data.ProxyParser;
using NLog;
using OpenQA.Selenium.Firefox;

namespace Lib.MonitoringIT.DATA.Github.Scrapper
{
    public class GithubScrapper
    {
        private const string Url = @"https://github.com/search?l=&p={page}&q=location%3Aarmenia+repos%3A%3E0&ref=advsearch&type=Users&utf8=%E2%9C%93";
        //private readonly string cookie = "__cfduid=d9bcdc52e419046289c9e9682ec7f5dea1544683349; t=88145399; _ga=GA1.2.1449105534.1544683360; PAPVisitorId=7348119cf106a753ce0ccbe4e14rw0d9; _ym_uid=1544683360469056594; _ym_d=1544683360; cf_clearance=a41c0b16b8383f4a447b957f6b5eb1d29839268f-1544880946-86400-150; _gid=GA1.2.1701641829.1544880949; _fbp=fb.1.1544880949046.1812288277; _ym_wasSynced=%7B%22time%22%3A1544880949168%2C%22params%22%3A%7B%22eu%22%3A0%7D%2C%22bkParams%22%3A%7B%7D%7D; _ym_isad=1; _ym_visorc_42065329=w; jv_enter_ts_EBSrukxUuA=1544880951091; jv_visits_count_EBSrukxUuA=2; jv_refer_EBSrukxUuA=https%3A%2F%2Fhidemyna.me%2Fen%2Fproxy-list%2F%3Ftype%3Ds%3Fstart%3D64; jv_utm_EBSrukxUuA=; jv_pages_count_EBSrukxUuA=4";
        public static string FirefoxProfilePath { get; } = ConfigurationManager.AppSettings["FirefoxProfilePath"];

        private const string RootGithub = @"https://github.com/";
        private static List<Proxy> _proxies;

        private List<string> githubUrls;

        private const string RepoUrlString = "?tab=repositories";
        private static FirefoxDriver driver;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static GithubScrapper()
        {

            var profileFirefox = new FirefoxProfile(FirefoxProfilePath);
            var option = new FirefoxOptions { Profile = profileFirefox };
            driver = new FirefoxDriver();
        }
        /// <summary>
        /// Start all scrapping methods
        /// </summary>
        public void Start()
        {
            //ScrapProxyFromHideMe(cookie);
            GetRepositories();
            GetGithubProfileSelenium();
        }


        public void LoadProxy()
        {
            try
            {
                using (var db = new MonitoringEntities())
                {
                    _proxies = db.Proxies.Where(x => x.Type == "HTTPS").ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            }
        }
        public async Task<GithubProfile> GetNewGithubProfile(string link)
        {
            GithubProfile profile;
            try
            {
                using (var db = new MonitoringEntities())
                {
                    driver.Navigate().GoToUrl(link);
                    Thread.Sleep(2000);
                    profile = GetGithubProfileSelenium(driver.PageSource, link);
                    if (profile != null)
                    {
                        var repositories = GetRepositories(link);
                        profile.GithubRepositories = repositories;
                        if (githubUrls.Contains(link)) ProfileUpdate(profile, link, db);
                        else db.GithubProfiles.Add(profile);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return null;
            }
            return profile;
        }

        private void ProfileUpdate(GithubProfile profile, string url, MonitoringEntities db)
        {
            try
            {
                var profileInDb = db.GithubProfiles.FirstOrDefault(x => x.Url == url);
                if (profileInDb == null) return;

                if (!string.IsNullOrEmpty(profile.Bio)) profileInDb.Bio = profile.Bio;
                if (!string.IsNullOrEmpty(profile.BlogOrWebsite)) profileInDb.BlogOrWebsite = profile.BlogOrWebsite;
                if (!string.IsNullOrEmpty(profile.Company)) profileInDb.Company = profile.Company;
                if (!string.IsNullOrEmpty(profile.Email)) profileInDb.Email = profile.Email;
                if (!string.IsNullOrEmpty(profile.Name)) profileInDb.Name = profile.Name;
                if (!string.IsNullOrEmpty(profile.Location)) profileInDb.Location = profile.Location;
                if (!string.IsNullOrEmpty(profile.ImageUrl)) profileInDb.ImageUrl = profile.ImageUrl;
                if (profileInDb.StarsCount != 0) profileInDb.StarsCount = profile.StarsCount;

                profileInDb.LastUpdate = DateTime.Now;
                db.GithubProfiles.AddOrUpdate(profileInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get and save in DB Github profile using selenium 
        /// </summary>
        public static void GetGithubProfileSelenium()
        {
            try
            {
                using (var db = new MonitoringEntities())
                {

                    var profileFirefox = new FirefoxProfile(FirefoxProfilePath);
                    var driverProfile = new FirefoxDriver(new FirefoxOptions { Profile = profileFirefox });
                    foreach (var link in db.GithubProfiles.Select(x => x.Url).ToList())
                    {
                        driverProfile.Navigate().GoToUrl(link);
                        Thread.Sleep(2000);
                        var profile = GetGithubProfileSelenium(driverProfile.PageSource, link);
                        if (profile != null)
                        {
                            profile.LastUpdate = DateTime.Now;
                            db.GithubProfiles.AddOrUpdate(profile);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get and save in DB all  Github profile repositories
        /// </summary>
        public static void GetRepositories()
        {
            try
            {
                using (var db = new MonitoringEntities())
                {
                    var profiles = db.GithubProfiles.ToList();
                    foreach (var profile in profiles)
                    {
                        Thread.Sleep(5000);
                        var repositories = GetRepositories(profile.Url);
                        if (repositories != null)
                        {
                            profile.GithubRepositories = repositories;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            }
        }


        /// <summary>
        /// Scrape and save in DB proxies
        /// <param name="cookie">Hide me request cookie</param>
        /// </summary>
        public static void ScrapProxyFromHideMe(string cookie = null)
        {
            var pr = new HidemeParser();
            try
            {
                var proxies = pr.GetProxy(cookie).Result.ToList();
                using (var db = new MonitoringEntities())
                {
                    foreach (var proxy in proxies)
                    {
                        db.Proxies.Add(new Proxy { Country = proxy.Country, Port = proxy.Port, Type = proxy.Type, Ip = proxy.Ip });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            }
        }


        /// <summary>
        /// Get Github profile urls
        /// </summary>
        /// <returns>all find profile url</returns>
        public static List<string> GetGithubUrls()
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
                        var profileNode = document.DocumentNode.SelectSingleNode(".//div[@class='user-list']");
                        var linksSelect = profileNode.SelectNodes(".//a").Select(x => x.InnerText).Where(x => !x.Contains("\n")).Select(x => x.Insert(0, RootGithub));
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
        /// Get Github profile general information
        /// </summary>
        /// <param name="content">Github general page HTML content </param>
        /// <param name="link">Github general page url</param>
        /// <returns>General information from profile </returns>
        public static GithubProfile GetGithubProfileSelenium(string content, string link)
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

                var profile = new GithubProfile
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
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        /// <summary>
        /// Get all repositories for user
        /// </summary>
        /// <param name="urlProfile">User url</param>
        /// <returns>List of repositories</returns>
        public static List<GithubRepository> GetRepositories(string urlProfile)
        {
            var repositories = new List<GithubRepository>();
            //var proxyCounter = 0;
            var proxyCounterNested = 0;
            var listRepoLink = new List<string>();
            var webClient = new WebClient();
            var linkToRepos = $"{urlProfile}{RepoUrlString}";


            while (true)
            {
                try
                {
                    //var wp = new WebProxy($"{_proxies[proxyCounter].Ip}:{_proxies[proxyCounter].Port}");
                    //webClient.Proxy = wp;
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
                    Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var repoLink in listRepoLink)
            {
                var repo = new GithubRepository();
                while (true)
                {
                    try
                    {

                        var client = new WebClient();
                        //var wpNested = new WebProxy($"{_proxies[proxyCounterNested].Ip}:{_proxies[proxyCounterNested].Port}");
                        //client.Proxy = wpNested;
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
                            Logger.Error(e, MethodBase.GetCurrentMethod().Name);
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
                            repo.GithubLanguages = new List<GithubLanguage>();
                            var langs = repositoryLangStats.SelectNodes(".//span[@class='lang']");
                            var percents = repositoryLangStats.SelectNodes(".//span[@class='percent']");
                            for (int i = 0; i < langs.Count; i++)
                            {
                                try
                                {
                                    var language = new GithubLanguage();
                                    language.Name = langs[i]?.InnerText;
                                    decimal.TryParse(percents[i]?.InnerText.Replace("%", ""), out var p);
                                    language.Percent = p;
                                    repo.GithubLanguages.Add(language);
                                }
                                catch (Exception e)
                                {
                                    Logger.Error(e, MethodBase.GetCurrentMethod().Name);
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
                        Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                        proxyCounterNested++;
                    }
                }
                repositories.Add(repo);
            }
            return repositories;

        }

        public List<string> LoadUrlInDb()
        {
            List<string> urls;
            using (var db = new MonitoringEntities())
            {
                try
                {
                    urls = db.GithubProfiles.Select(x => x.Url).ToList();
                    githubUrls = urls;
                }
                catch (Exception e)
                {
                    Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                    urls = new List<string>();
                    githubUrls = new List<string>();
                }
            }
            return urls;
        }
    }
}
