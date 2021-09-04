using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Interfaces
{
    public interface IUserRepository
    {
        bool ExistsPermission(int permissionId, int roleId);
        int GetUserRoleId(string username);
    }
}
