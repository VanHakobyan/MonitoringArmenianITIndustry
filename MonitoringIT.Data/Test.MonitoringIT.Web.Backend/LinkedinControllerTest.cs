using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Web.Backend.MonitoringIT.Controllers;
using Xunit;

namespace Test.MonitoringIT.Web.Backend
{
    public class LinkedinControllerTest
    {
        private readonly LinkedinController _linkedinController;

        public LinkedinControllerTest()
        {
            _linkedinController = new LinkedinController();

        }

        //[Fact]
        //public void GetActionTest()
        //{
        //    var result = _linkedinController.Get();
        //    Assert.IsAssignableFrom<IEnumerable<string>>(result);
        //}

        [Fact]
        public void GetByIdActionTest()
        {
            var resultNotFound = _linkedinController.GetById(int.MaxValue);
            Assert.IsType<NotFoundResult>(resultNotFound);
            var resultOk = _linkedinController.GetById(15);
            Assert.IsType<OkObjectResult>(resultOk);
        }

        [Fact]
        public void GetByUsernameActionTest()
        {
            var resultNotFound = _linkedinController.GetByUsername(string.Empty);
            Assert.IsType<NotFoundResult>(resultNotFound);
            var resultOk = _linkedinController.GetByUsername("vanikhakobyan");
            Assert.IsType<OkObjectResult>(resultOk);
        }
        
    }
}
