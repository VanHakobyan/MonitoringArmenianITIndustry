﻿using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Implementation;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT
{
    public class MonitoringDAL : IDisposable
    {
        private MonitoringContext _monitoringContext;
        private string _connectionString;


        private MonitoringContext MonitoringContext => _monitoringContext ?? (_monitoringContext = new MonitoringContext(_connectionString));
        public MonitoringDAL(MonitoringContext monitoringContext)
        {
            _monitoringContext = monitoringContext;
        }

        public MonitoringDAL(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IGithubProfileDAL githubProfileDal;
        public IGithubProfileDAL GithubProfileDal => githubProfileDal ?? (githubProfileDal = new GithubProfileDAL(MonitoringContext));

        public ILinkedinProfileDAL linkedinProfileDal;
        public ILinkedinProfileDAL LinkedinProfileDal => linkedinProfileDal ?? (linkedinProfileDal = new LinkedinProfileDAL(MonitoringContext));



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
    }
}
