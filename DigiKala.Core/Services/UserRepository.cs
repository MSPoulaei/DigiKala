using DigiKala.Core.Interfaces;
using DigiKala.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigiKala.Core.Services
{
    public class UserRepository : IUserRepository
    {
        DigiKalaDbContext _context;
        public UserRepository(DigiKalaDbContext context)
        {
            _context = context;
        }
        public bool ExistsPermission(int permissionId, int roleId)
        {
            return _context.RolePermissions
                .Any(rp => rp.PermissionId == permissionId && rp.RoleId == roleId);
        }

        public int GetUserRoleId(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Mobile == username).RoleId;
        }
    }
}
