using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using Database.MonitoringIT.DAL.WithEF6;
using MonitoringIT.Data.Common;
using Newtonsoft.Json;
using Proxy = Database.MonitoringIT.DAL.WithEF6.Proxy;

namespace Lib.MonitoringIT.Data.Github.Api
{
    public class GithubApiProcessor
    {
        private const string GithubAllArmenianUserUrl = @"https://api.github.com/search/users?q=location:armenia&page={pageNumber}&per_page=100";
        private List<Proxy> _proxies;
        private int _proxyIndex = 0;

        public GithubApiProcessor()
        {
            LoadProxy();
        }

        public void LoadProxy()
        {
            using (MonitoringEntities db = new MonitoringEntities())
            {
                _proxies = db.Proxies.Where(x => x.Type.Contains("HTTP")).ToList();
            }
        }

        public List<GithubUserRootModel> GetAllFromApiAsync()
        {
            var allUsers = new List<GithubUserRootModel>();
            for (var i = 1; i <= 10; i++)
            {
                var allGithubContentJson = SendGetRequest(GithubAllArmenianUserUrl.Replace("{pageNumber}", i.ToString()));
                if (allGithubContentJson is null) continue;
                var githubUserApiAllPage = JsonConvert.DeserializeObject<GithubUserApiAllModel>(allGithubContentJson);
                allUsers.AddRange(githubUserApiAllPage.items);
            }

            return allUsers;
        }

        private string SendGetRequest(string url)
        {
            string allGithubContentJson = null;
            while (true)
            {
                try
                {
                    var client = new WebClient
                    {
                        Proxy = new WebProxy($"{_proxies[_proxyIndex].Ip}:{_proxies[_proxyIndex].Port}")
                    };
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Anything");
                    client.Headers.Add(HttpRequestHeader.ContentType, "applicaton/json");
                    allGithubContentJson = client.DownloadString(url);
                    break;
                }
                catch
                {
                    _proxyIndex++;
                }
            }
            return allGithubContentJson;
        }

        public void UpdateGithubProfilesInDb()
        {
            using (MonitoringEntities db = new MonitoringEntities())
            {
                var githubProfiles = db.GithubProfiles.ToList();
                var allFromApi = GetAllFromApiAsync();
                for (var i = 0; i < allFromApi.Count; i++)
                {
                    var githubApiModel = allFromApi[i];
                    try
                    {
                        var userJson = SendGetRequest(githubApiModel.url);
                        var userModel = JsonConvert.DeserializeObject<GithubApiUserModel>(userJson);

                        var updateProfile = githubProfiles.FirstOrDefault(x => x.UserName == userModel.login);
                        if (updateProfile is null)
                        {
                            var newGithubMember = new GithubProfile
                            {
                                UserName = userModel.login,
                                Email = userModel.email as string,
                                Company = userModel.company as string,
                                Location = userModel.location,
                                ImageUrl = userModel.avatar_url,
                                BlogOrWebsite = userModel.blog,
                                Name = userModel.name,
                                Bio = userModel.bio as string,
                                Url = userModel.html_url,
                                LastUpdate = DateTime.Now
                            };
                            db.GithubProfiles.Add(newGithubMember);
                        }
                        else
                        {
                            if (userModel.email != null) updateProfile.Email = userModel.email as string;
                            if (userModel.company != null) updateProfile.Company = userModel.company as string;
                            if (userModel.location != null) updateProfile.Location = userModel.location;
                            if (userModel.avatar_url != null) updateProfile.ImageUrl = userModel.avatar_url;
                            if (userModel.blog != null) updateProfile.BlogOrWebsite = userModel.blog;
                            if (userModel.name != null) updateProfile.Name = userModel.name;
                            if (userModel.bio != null) updateProfile.Bio = userModel.bio as string;

                            updateProfile.LastUpdate=DateTime.Now;
                            
                            db.GithubProfiles.AddOrUpdate(updateProfile);
                        }

                        db.SaveChanges();
                        //Thread.Sleep(1500);
                    }
                    catch 
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }

    }
}
