using Microsoft.AspNetCore.Mvc;
using Web.Backend.MonitoringIT.Controllers;
using Web.Backend.MonitoringIT.Models;
using Xunit;

namespace Test.MonitoringIT.Web.Backend
{
    public class CrossMapControllerTest
    {

        private readonly CrossMapController _CrossMapController;

        public CrossMapControllerTest()
        {
            _CrossMapController = new CrossMapController();

        }

        [Fact]
        public void GetActionTest()
        {
            var result = _CrossMapController.GetAllCrossProfiles();
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetByIdActionTest()
        {
            var resultNotFound = _CrossMapController.GetCrossProfileByUsername(new CrossProfileSearchModel { ContentType = ContentType.Github, Username = string.Empty });
            Assert.IsType<BadRequestResult>(resultNotFound);
            var resultOk = _CrossMapController.GetCrossProfileByUsername(new CrossProfileSearchModel { ContentType = ContentType.Github, Username = "vanhakobyan" });
            Assert.IsType<OkObjectResult>(resultOk);
        }

    }
}
