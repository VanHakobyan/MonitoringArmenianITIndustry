﻿using System;
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
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var linkedinProfiles = dal.LinkedinProfileDal.GetAll();
                    return Ok(linkedinProfiles);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var linkedinProfile = dal.LinkedinProfileDal.GetById(id);
                    if (linkedinProfile is null) return NotFound();
                    return Ok(linkedinProfile);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }
        [HttpGet("user/{username}")]
        public ActionResult GetByUsername(string username)
        {
            using (var dal = new MonitoringDAL(""))
            {
                try
                {
                    var linkedinProfile = dal.LinkedinProfileDal.GetByUserName(username);
                    if (linkedinProfile is null) return NotFound();
                    return Ok(linkedinProfile);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }
    }
}