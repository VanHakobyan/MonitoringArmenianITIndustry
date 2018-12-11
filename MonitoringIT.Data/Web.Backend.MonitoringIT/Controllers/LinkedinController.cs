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
        /// <summary>
        /// Get all linkedin profiles
        /// </summary>
        /// <returns>ActionResult of IEnumerable of string></returns>
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

        /// <summary>
        /// Get linkedin profile by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Get linkedin profile by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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