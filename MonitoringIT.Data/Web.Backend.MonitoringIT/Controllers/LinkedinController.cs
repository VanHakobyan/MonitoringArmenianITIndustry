using System;
using System.Collections.Generic;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Backend.MonitoringIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkedinController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var linkedinProfiles = dal.LinkedinProfileDal.GetAll();
                    if (linkedinProfiles is null) return NotFound();
                    return Ok(JsonConvert.SerializeObject(linkedinProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var linkedinProfile = dal.LinkedinProfileDal.GetById(id);
                    if (linkedinProfile is null) return NotFound();
                    return Ok(JsonConvert.SerializeObject(linkedinProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }
        [HttpGet("user/{username}")]
        public IActionResult GetByUsername(string username)
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var linkedinProfile = dal.LinkedinProfileDal.GetByUserName(username);
                    if (linkedinProfile is null) return NotFound();
                    return Ok(JsonConvert.SerializeObject(linkedinProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }
    }
}