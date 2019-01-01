using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MonitoringIT.Data.Common;

namespace Lib.MonitoringIT.Data.ProxyParser
{
    public class HidemeParser
    {
        private static string url = "https://hidemyna.me/en/proxy-list/?type=s?start={page}#list";

        /// <summary>
        /// Get request 
        /// </summary>
        /// <param name="uri">Url</param>
        /// <param name="cookie">Cookie in site</param>
        /// <returns></returns>
        private async Task<string> SendGetRequest(string uri, string cookie = null)
        {
            var response = "";

            try
            {
                //uri = "https://hidemyna.me/en/proxy-list/?start=21504#list";
                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DnsRefreshTimeout = 1000;
                ServicePointManager.UseNagleAlgorithm = false;


                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                //request.Headers.Add("accept-encoding", "gzip, deflate, br");
                request.Headers.Add("accept-language", "en-US,en;q=0.9,hy;q=0.8,mt;q=0.7");
                request.Headers.Add("cookie", cookie ?? "__cfduid=d1ae88318939213b04cfacf832c13dcd01538472442; t=77147791; _ga=GA1.2.1018644514.1538472449; PAPVisitorId=9f9ae6258d224e6294217fb075b2JRTI; _ym_uid=1538472449103520384; _ym_d=1538472449; cf_clearance=ffba3b7646abf7cf13ef1a18f3954722dec8bfd3-1539766744-86400-150; _gid=GA1.2.1745799551.1539766746; _ym_wasSynced=%7B%22time%22%3A1539766746101%2C%22params%22%3A%7B%22eu%22%3A0%7D%2C%22bkParams%22%3A%7B%7D%7D; _ym_isad=2; jv_enter_ts_EBSrukxUuA=1539766748993; jv_visits_count_EBSrukxUuA=3; jv_refer_EBSrukxUuA=https%3A%2F%2Fhidemyna.me%2Fen%2Fproxy-list%2F%3Fstart%3D21504; jv_utm_EBSrukxUuA=; _dc_gtm_UA-90263203-1=1; _ym_visorc_42065329=w; _gat_UA-90263203-1=1; jv_pages_count_EBSrukxUuA=4");
                using (var stream = (await request.GetResponseAsync()).GetResponseStream())
                {
                    if (stream != null)
                    {
                        stream.ReadTimeout = 30000;
                        using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                        {
                            response = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(1000);
            }


            return response;
        }

        /// <summary>
        /// Get content from page number
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="cookie">Cookie</param>
        /// <returns>Task</returns>
        private async Task<string> GetContent(int pageNumber, string cookie = null)
        {
            return await SendGetRequest(url.Replace("{page}", pageNumber.ToString()), cookie);
        }

        /// <summary>
        /// Get list of proxies
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns>Task&lt;List&lt;Proxy&gt;&gt;</returns>
        public async Task<List<Proxy>> GetProxy(string cookie = null)
        {
            var proxyList = new List<Proxy>();
            HtmlDocument document = new HtmlDocument();
            var pageSource = await GetContent(64,cookie);
            if (!string.IsNullOrEmpty(pageSource)) document.LoadHtml(pageSource);
            else return null;
            var pagination = document.DocumentNode.SelectSingleNode(".//div[@class='proxy__pagination']");
            var lastPageIndex = pagination.FirstChild.LastChild.InnerText;
            var lastPageStartWith = int.Parse(lastPageIndex) * 64;
            for (var i = 64; i < lastPageStartWith + 1; i += 64)
            {
                try
                {
                    var tbody = document.DocumentNode.SelectSingleNode(".//table[@class='proxy__t']").ChildNodes.FirstOrDefault(x => x.Name == "tbody");
                    if (tbody is null) continue;
                    foreach (var childNode in tbody.ChildNodes)
                    {
                        try
                        {
                            var proxy = new Proxy
                            {
                                Ip = childNode.ChildNodes[0].InnerText,
                                Port = childNode.ChildNodes[1].InnerText,
                                Type = childNode.ChildNodes[4].InnerText,
                                Country = childNode.ChildNodes[2].InnerText.Replace(" &nbsp;", "").Trim()
                            };
                            proxyList.Add(proxy);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    pageSource = await GetContent(i);
                    document.LoadHtml(pageSource);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return proxyList;
        }
    }
}
