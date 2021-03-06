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
    public class CompanyController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAllCompany()
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var companies = dal.CompanyDal.GetAllCompany();
                    if (companies is null)
                    {
                        Logger.Info("GetAllCompany is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(companies, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(companies, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetByPage/{count}/{page}")]
        public IActionResult GetByPage(int count, int page)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var companies = dal.CompanyDal.GetCompanyByPage(count, page);
                    if (companies is null)
                    {
                        Logger.Info("GetCompaniesByPage is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(companies, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(companies, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
        /// <summary>
        /// Get Favorite Companies
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetFavorites/{count}")]
        public IActionResult GetFavorites(int count)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var companies = dal.CompanyDal.GetFavoriteCompanies(count);
                    if (companies is null)
                    {
                        Logger.Info("GetFavoriteCompanies is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(companies, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(companies, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetAllWithJob")]
        public IActionResult GetAllWithJob()
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var companies = dal.CompanyDal.GetAllCompanyWithJob();
                    if (companies is null)
                    {
                        Logger.Info("GetAllWithJob is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(companies, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(companies, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet, Route("GetByIndustry/{industry}")]
        public IActionResult GetByIndustry(string industry)
        {
            try
            {
                using (var dal = new MonitoringDAL(""))
                {
                    var companies = dal.CompanyDal.GetCompaniesByIndustry(industry);
                    if (companies is null)
                    {
                        Logger.Info("GetCompaniesByIndustry is null");
                        return NotFound();
                    }
                    //Logger.Info($"Messege: {JsonConvert.SerializeObject(companies, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                    return Ok(JsonConvert.SerializeObject(companies, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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