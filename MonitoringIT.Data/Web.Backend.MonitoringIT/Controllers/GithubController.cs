using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT;
using DAL.MonitoringIT.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Backend.MonitoringIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        // GET: api/Github
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var data = dal.GithubProfileDal.GetAll();
                    return Ok(data);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }

        // GET: api/Github/5
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var githubProfile = dal.GithubProfileDal.GetById(id);
                    if (githubProfile is null) return NotFound();
                    return Ok(githubProfile);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }

        // GET: api/Github/5
        [HttpGet("user/{username}")]
        public ActionResult GetByUserName(string username)
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
