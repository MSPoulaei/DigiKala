using DigiKala.Core.Interfaces;
using DigiKala.Core.Classes;
using DigiKala.DataAccessLayer.Context;
using DigiKala.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigiKala.Core.Services
{
    public class AccountRepository : IAccountRepository
    {
        DigiKalaDbContext _context;
        public AccountRepository(DigiKalaDbContext context)
        {
            _context = context;
        }

        public bool ActivateUser(string activationCode, string mobileNumber)
        {
            User user = _context.Users.FirstOrDefault(u =>
            u.ActivationCode == activationCode &&
            u.Mobile == mobileNumber && !u.IsActive);

            if (user == null)
            {
                return false;
            }
            user.IsActive = true;
            user.ActivationCode = null;
            _context.SaveChanges();
            return true;
        }

        public bool AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistsMobileNumber(string mobileNumber)
        {
            return _context.Users.Any(u => u.Mobile == mobileNumber);
        }

        public int GetMaxRoleId() => _context.Roles.Max(r => r.Id);
    }
}
