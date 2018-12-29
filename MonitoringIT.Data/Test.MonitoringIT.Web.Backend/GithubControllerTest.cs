using System;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.MonitoringIT.Controllers;
using Xunit;

namespace Test.MonitoringIT.Web.Backend
{
    public class GithubControllerTest
    {
        private readonly GithubController _githubController;

        public GithubControllerTest()
        {
            _githubController = new GithubController();
            
        }

        [Fact]
        public void GetActionTest()
        {
            var result = _githubController.Get();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
