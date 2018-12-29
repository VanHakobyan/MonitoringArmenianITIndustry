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
        [Fact]
        public void GetByIdActionTest()
        {
            var resultNotFound = _githubController.GetById(int.MaxValue);
            Assert.IsType<NotFoundResult>(resultNotFound);
            var resultOk = _githubController.GetById(15);
            Assert.IsType<OkObjectResult>(resultOk);
        }

        [Fact]
        public void GetByUsernameActionTest()
        {
            var resultNotFound = _githubController.GetByUserName(string.Empty);
            Assert.IsType<NotFoundResult>(resultNotFound);
            var resultOk = _githubController.GetByUserName("vanhakobyan");
            Assert.IsType<OkObjectResult>(resultOk);
        }
    }
}
