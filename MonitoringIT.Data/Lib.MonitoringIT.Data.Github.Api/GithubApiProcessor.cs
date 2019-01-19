using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database.MonitoringIT.DAL.WithEF6;
using MonitoringIT.Data.Common;
using Newtonsoft.Json;

namespace Lib.MonitoringIT.Data.Github.Api
{
    public class GithubApiProcessor
    {
        private const string GithubAllArmenianUserUrl = @"https://api.github.com/search/users?q=location:armenia&page={pageNumber}&per_page=100";

        public async Task<List<GithubUserRootModel>> GetAllFromApiAsync()
        {
            var allUsers = new List<GithubUserRootModel>();
            for (var i = 1; i <= 10; i++)
            {
                var allGithubContentJson = await SendGetRequest(GithubAllArmenianUserUrl);
                var githubUserApiAllPage = JsonConvert.DeserializeObject<GithubUserApiAllModel>(allGithubContentJson);
                allUsers.AddRange(githubUserApiAllPage.items);
            }

            return allUsers;
        }

        private static async Task<string> SendGetRequest(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Anything");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var allGithubContentJson = await client.GetStringAsync(url);
            return allGithubContentJson;
        }

        public async Task UpdateGithubProfilesInDb()
        {
            using (MonitoringEntities db = new MonitoringEntities())
            {
                var githubProfiles = db.GithubProfiles.ToList();
                var allFromApi = await GetAllFromApiAsync();
                for (var i = 0; i < allFromApi.Count; i++)
                {
                    var githubApiModel = allFromApi[i];
                    try
                    {
                        var userJson = await SendGetRequest(githubApiModel.url);
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
                                Url = userModel.html_url
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

                            db.GithubProfiles.AddOrUpdate(updateProfile);
                        }

                        db.SaveChanges();
                        //Thread.Sleep(1500);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }

    }
}
