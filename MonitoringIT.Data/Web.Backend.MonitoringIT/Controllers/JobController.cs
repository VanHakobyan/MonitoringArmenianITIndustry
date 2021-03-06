﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DAL.MonitoringIT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;

namespace Web.Backend.MonitoringIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("FrontPolicy")]
    public class JobController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get all jobs
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var jobs = dal.JobDal.GetAllJob();
                    if (jobs is null)
                    {
                        Logger.Info("GetAll is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(jobs, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(jobs, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get Jobs By Page
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetByPage/{count}/{page}")]
        public IActionResult GetByPage(int count, int page)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var jobs = dal.JobDal.GetJobsByPage(count, page);
                    if (jobs is null)
                    {
                        Logger.Info("GetByPage is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(jobs, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(jobs, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
        /// <summary>
        /// Get Favorite Jobs
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetFavorites/{count}")]
        public IActionResult GetFavorites(int count)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var jobs = dal.JobDal.GetFavorites(count);
                    if (jobs is null)
                    {
                        Logger.Info("GetFavoritesJob is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(jobs, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(jobs, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all jobs
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetByCategory/{category}")]
        public IActionResult GetJobsByCategory(string category)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var jobs = dal.JobDal.GetJobsByCategory(category);
                    if (jobs is null)
                    {
                        Logger.Info("GetAllCompany is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(jobs, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(jobs, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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