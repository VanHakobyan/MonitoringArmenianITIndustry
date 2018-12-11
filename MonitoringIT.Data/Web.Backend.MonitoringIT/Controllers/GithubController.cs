using System;
using System.Collections.Generic;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Backend.MonitoringIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubController : ControllerBase
    {


        /// <summary>
        /// Get all github profiles 
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Get()
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var githubProfiles = dal.GithubProfileDal.GetAll();
                    if (githubProfiles is null) return NotFound();
                    return Ok(JsonConvert.SerializeObject(githubProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
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
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var githubProfile = dal.GithubProfileDal.GetById(id);
                    if (githubProfile is null) return NotFound();
                    return Ok(JsonConvert.SerializeObject(githubProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }


        /// <summary>
        /// Get github profile by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("user/{username}")]
        public IActionResult GetByUserName(string username)
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var githubProfile = dal.GithubProfileDal.GetByUserName(username);
                    if (githubProfile is null) return NotFound();
                    return Ok(JsonConvert.SerializeObject(githubProfile,Formatting.Indented,new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }

    }
}
