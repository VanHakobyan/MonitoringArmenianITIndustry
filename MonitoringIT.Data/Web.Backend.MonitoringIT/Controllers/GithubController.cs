using System;
using System.Collections.Generic;
using System.Reflection;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;

namespace Web.Backend.MonitoringIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("FrontPolicy")]
    public class GithubController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get all github profiles 
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var githubProfiles = dal.GithubProfileDal.GetAll();
                    if (githubProfiles is null)
                    {
                        Logger.Info("GithubProfiles is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(githubProfiles, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(githubProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
        /// <summary>
        /// Get all github profiles 
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetGithubsByPage/{count}/{page}")]
        public IActionResult GetGithubsByPage(int count, int page)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var githubProfiles = dal.GithubProfileDal.GetByPage(count, page);
                    if (githubProfiles is null)
                    {
                        Logger.Info("GithubProfilesByPage is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(githubProfiles, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(githubProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        /// <summary>
        ///  Get github profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var githubProfile = dal.GithubProfileDal.GetById(id);
                    if (githubProfile is null)
                    {
                        Logger.Info("GithubProfile is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(githubProfile, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(githubProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }


        /// <summary>
        /// Get github profile by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("user/{username}")]
        public IActionResult GetByUserName(string username)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var githubProfile = dal.GithubProfileDal.GetByUserName(username);
                    if (githubProfile is null)
                    {
                        Logger.Info("GithubProfile is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(githubProfile, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(githubProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

    }
}
