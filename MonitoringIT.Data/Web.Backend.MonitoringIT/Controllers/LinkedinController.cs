using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var  dal=new MonitoringDAL("");
            var linkedinProfiles = dal.LinkedinProfileDal.GetAll();
            return Ok(linkedinProfiles);
        }
    }
}