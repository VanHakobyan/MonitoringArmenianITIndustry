using System.Collections.Generic;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Backend.MonitoringIT.Models;

namespace Web.Backend.MonitoringIT.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CrossMapController : ControllerBase
    {
        [HttpGet("api/crossmap/GetAllCrossProfiles")]
        public IActionResult GetAllCrossProfiles()
        {
            var crossProfileModels = new List<ProfileCrossModel>();
            using (var dal = new MonitoringDAL(""))
            {
                var crossProfiles = dal.CrossProfileDal.GetAllCrossProfiles();
                foreach (var crossProfile in crossProfiles)
                {
                    var crossProfileModel = new ProfileCrossModel
                    {
                        LinkedinProfile = dal.LinkedinProfileDal.GetById(crossProfile.LinkedinUserId),
                        GithubProfile = dal.GithubProfileDal.GetById(crossProfile.GithubUserId)
                    };
                    crossProfileModels.Add(crossProfileModel);
                }
            }
            return Ok(JsonConvert.SerializeObject(crossProfileModels, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        [HttpPost("api/crossmap/GetCrossProfileByUsername")]
        public IActionResult GetCrossProfileByUsername([FromBody] CrossProfileSearchModel searchModel)
        {
            ProfileCrossModel crossProfileModel;
            using (MonitoringDAL dal = new MonitoringDAL(string.Empty))
            {
                switch (searchModel.ContentType)
                {
                    case ContentType.Github:
                        {
                            var githubProfileId = dal.GithubProfileDal.GetByUserName(searchModel.Username).Id;
                            var crossProfile = dal.CrossProfileDal.GetCrossProfileByGithubProfileId(githubProfileId);
                            crossProfileModel = new ProfileCrossModel
                            {
                                LinkedinProfile = dal.LinkedinProfileDal.GetById(crossProfile.LinkedinUserId),
                                GithubProfile = dal.GithubProfileDal.GetById(crossProfile.GithubUserId)
                            };
                        }
                        break;
                    case ContentType.Linkedin:
                        {
                            var linkedinProfileId = dal.GithubProfileDal.GetByUserName(searchModel.Username).Id;
                            var crossProfile = dal.CrossProfileDal.GetCrossProfileByGithubProfileId(linkedinProfileId);
                            crossProfileModel = new ProfileCrossModel
                            {
                                LinkedinProfile = dal.LinkedinProfileDal.GetById(crossProfile.LinkedinUserId),
                                GithubProfile = dal.GithubProfileDal.GetById(crossProfile.GithubUserId)
                            };
                        }
                        break;
                    default:
                        crossProfileModel = null;
                        break;
                }
            }
            return Ok(JsonConvert.SerializeObject(crossProfileModel, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}