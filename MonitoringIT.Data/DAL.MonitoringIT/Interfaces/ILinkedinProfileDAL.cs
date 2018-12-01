using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    /// <inheritdoc />
    public interface ILinkedinProfileDAL : IBaseDAL
    {
        List<LinkedinProfile> GetAll();
        LinkedinProfile GetById(int id);
        LinkedinProfile GetByUserName(string username);
    }
}
