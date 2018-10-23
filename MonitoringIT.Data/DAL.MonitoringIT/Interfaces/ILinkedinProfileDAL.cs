using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    interface ILinkedinProfileDAL:IBaseDAL
    {
        List<LinkedinProfile> GetAll();
    }
}
