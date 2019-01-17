using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonitoringIT.Data.Common;
using Newtonsoft.Json;

namespace Lib.MonitoringIT.Data.Github.Api
{
    public class GithubApiProcessor
    {
        private const string GithubAllArmenianUserUrl = @"https://api.github.com/search/users?q=location:armenia&page={pageNumber}&per_page=100";

        public async Task<List<GithubUser>> GetAllAsync()
        {
            var allUsers = new List<GithubUser>();
            for (var i = 1; i <= 10; i++)
            {
                HttpClient client = new HttpClient();
                var allGithubContentJson = await client.GetStringAsync(GithubAllArmenianUserUrl);
                var githubUserApiAllPage = JsonConvert.DeserializeObject<GithubUserApiAll>(allGithubContentJson); 
                allUsers.AddRange(githubUserApiAllPage.items);
            }

            return allUsers;
        }

    }
}
