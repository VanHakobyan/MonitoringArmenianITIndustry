using System;
using System.Collections.Generic;
using System.Reflection;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using Web.Backend.MonitoringIT.Models;

namespace Web.Backend.MonitoringIT.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [EnableCors("FrontPolicy")]
    public class CrossMapController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Get All Cross Profiles
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("api/crossmap/GetAllCrossProfiles")]
        public IActionResult GetAllCrossProfiles()
        {
            try
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
                Logger.Info($"Messege: {JsonConvert.SerializeObject(crossProfileModels, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                return Ok(JsonConvert.SerializeObject(crossProfileModels, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
        /// <summary>
        /// Get Cross Profile By Username
        /// </summary>
        /// <param name="searchModel">Github or Linkedin profiles username</param>
        /// <returns></returns>
        [HttpPost("api/crossmap/GetCrossProfileByUsername")]
        public IActionResult GetCrossProfileByUsername([FromBody] CrossProfileSearchModel searchModel)
        {
            ProfileCrossModel crossProfileModel;
            try
            {
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
                Logger.Info($"Messege: {JsonConvert.SerializeObject(crossProfileModel, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                return Ok(JsonConvert.SerializeObject(crossProfileModel, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
    }
}