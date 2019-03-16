﻿using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.MonitoringIT.Implementation
{
    public class CompanyDAL : BaseDAL<Company>, ICompanyDAL
    {
        public CompanyDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public new IQueryable<Company> GetAllQuery()
        {
            return _dbContext.Company;
        }

        public List<Company> GetAllCompany()
        {
            return GetAllQuery().ToList();
        }

        public List<Company> GetCompaniesByIndustry(string industry)
        {
            return GetAllQuery().Where(x => x.Industry == industry).ToList();
        }

        public List<Company> GetAllCompanyWithJob()
        {
            return GetAllQuery().Where(x => x.Job != null && x.Job.Count > 0).Include(x => x.Job).ToList();
        }
    }
}