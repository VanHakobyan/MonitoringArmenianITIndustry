using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    /// <inheritdoc />
    public interface ILinkedinProfileDAL : IBaseDAL<LinkedinProfile> 
    {
        List<LinkedinProfile> GetAll();
        List<LinkedinProfile> GetLinkedinsByPage(int count,int page);
        List<LinkedinProfile> GetFavorites(int count);
        LinkedinProfile GetById(int id);
        LinkedinProfile GetByUserName(string username);
    }
}
