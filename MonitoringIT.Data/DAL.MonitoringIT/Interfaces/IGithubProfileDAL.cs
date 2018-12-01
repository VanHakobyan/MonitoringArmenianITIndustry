using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    public interface IGithubProfileDAL:IBaseDAL
    {
        List<Profiles> GetAll();
        Profiles GetById(int id);
        Profiles GetByUserName(string username);
    }
}
