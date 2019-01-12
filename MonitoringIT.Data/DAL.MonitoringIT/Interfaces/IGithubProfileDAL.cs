﻿using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    public interface IGithubProfileDAL:IBaseDAL
    {
        List<GithubProfile> GetAll();
        List<GithubProfile> GetAllWithReadme();
        GithubProfile GetById(int id);
        GithubProfile GetByIdWithReadme(int id);
        GithubProfile GetByUserName(string username);
    }
}
