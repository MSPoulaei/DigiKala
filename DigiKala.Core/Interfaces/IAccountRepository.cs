using DigiKala.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Interfaces
{
    public interface IAccountRepository
    {
        bool AddUser(User user);
        bool ExistsMobileNumber(string mobileNumber);
        int GetMaxRoleId();
    }
}
