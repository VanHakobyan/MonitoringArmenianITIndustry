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

namespace MonitoringIT.Data.GithubDataParser
{
    class Program
    {
        static string Url = @"https://github.com/search?l=&o=desc&p={page}&q=location%3Aarmenia&s=repositories&ref=advsearch&type=Users&utf8=%E2%9C%93&_pjax=%23js-pjax-container";
        private static string rootGithub = @"https://github.com/";
        private static List<ProxyModel> Proxies;
        private static List<string> _links = new List<string>();
        static void Main(string[] args)
        {

            var hidemeParser = new HidemeParser();
            var proxies = hidemeParser.GetProxy().Result.Where(x => x.Type.Contains("HTTPS")).ToList();
            var proxyJson = JsonConvert.SerializeObject(proxies);
            File.WriteAllText(@"D:\proxies.json", proxyJson);
            Proxies = proxies;
            GetGithubUrls(_links);

            var linksJson = JsonConvert.SerializeObject(_links);
            //File.WriteAllText(@"D:\linksGithub.json", linksJson);
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
                        var document=new HtmlDocument();
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
