﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Implementation;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT
{
    public class MonitoringDAL : IDisposable
    {
        private MonitoringContext _monitoringContext;
        private readonly string _connectionString;


        private MonitoringContext MonitoringContext => _monitoringContext ?? (_monitoringContext = new MonitoringContext(_connectionString));
        public MonitoringDAL(MonitoringContext monitoringContext)
        {
            _monitoringContext = monitoringContext;
        }

        public MonitoringDAL(string connectionString)
        {
            _connectionString = connectionString;
        }


        private IGithubProfileDAL _githubProfileDal;
        public IGithubProfileDAL GithubProfileDal => _githubProfileDal ?? (_githubProfileDal = new GithubProfileDAL(MonitoringContext));

        private ILinkedinProfileDAL _linkedinProfileDal;
        public ILinkedinProfileDAL LinkedinProfileDal => _linkedinProfileDal ?? (_linkedinProfileDal = new LinkedinProfileDAL(MonitoringContext));

        private ICrossProfileDAL _crossProfileDal;
        public ICrossProfileDAL CrossProfileDal => _crossProfileDal ?? (_crossProfileDal = new CrossProfileDAL(MonitoringContext));

        private ICompanyDAL _companyDal;
        public ICompanyDAL CompanyDal => _companyDal ?? (_companyDal = new CompanyDAL(MonitoringContext));

        private IJobDAL _jobDal;
        public IJobDAL JobDal => _jobDal ?? (_jobDal = new JobDAL(MonitoringContext));

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _monitoringContext?.Dispose();
                }
            }
            disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _monitoringContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _monitoringContext.SaveChangesAsync();
        }
    }
}
