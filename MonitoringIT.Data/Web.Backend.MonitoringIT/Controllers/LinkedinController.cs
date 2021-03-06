﻿using System;
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
    public class LinkedinController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get all linkedin profiles
        /// </summary>
        /// <returns>ActionResult of IEnumerable of string></returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var linkedinProfiles = dal.LinkedinProfileDal.GetAll();
                    if (linkedinProfiles is null)
                    {
                        Logger.Info("GetAll is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(linkedinProfiles, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(linkedinProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get Linkedins ByPage
        /// </summary>
        /// <returns>ActionResult of IEnumerable of string></returns>
        [HttpGet, Route("GetByPage/{count}/{page}")]
        public IActionResult GetByPage(int count, int page)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var linkedinProfiles = dal.LinkedinProfileDal.GetLinkedinsByPage(count,page);
                    if (linkedinProfiles is null)
                    {
                        Logger.Info("GetByPage is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(linkedinProfiles, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(linkedinProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }


        /// <summary>
        /// Get all linkedin profiles
        /// </summary>
        /// <returns>ActionResult of IEnumerable of string></returns>
        [HttpGet, Route("GetFavorites/{count}")]
        public IActionResult GetFavorites(int count)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var linkedinProfiles = dal.LinkedinProfileDal.GetFavorites(count);
                    if (linkedinProfiles is null)
                    {
                        Logger.Info("GetFavoriteLinkedins is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(linkedinProfiles, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(linkedinProfiles, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
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
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var linkedinProfile = dal.LinkedinProfileDal.GetById(id);
                    if (linkedinProfile is null)
                    {
                        Logger.Info("GetById is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(linkedinProfile, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(linkedinProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
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
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var linkedinProfile = dal.LinkedinProfileDal.GetByUserName(username);
                    if (linkedinProfile is null)
                    {
                        Logger.Info("GetByUsername is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(linkedinProfile, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(linkedinProfile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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