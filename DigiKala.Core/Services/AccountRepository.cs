using DigiKala.Core.Interfaces;
using DigiKala.Core.Classes;
using DigiKala.DataAccessLayer.Context;
using DigiKala.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DigiKala.Core.Interfaces.IAccountRepository;

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
            u.Mobile == mobileNumber &&
            !u.IsActive);

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

        public bool ExistsMobileNumber(string mobile)
        {
            return _context.Users.Any(u => u.Mobile == mobile);
        }

        public string ForgetPasswordActivationCode(string Mobile)
        {
            User user = _context.Users.FirstOrDefault(u => u.Mobile == Mobile);
            if (user == null)
            {
                return "notfound";
            }
            try
            {
                user.ActivationCode = CodeGenerators.ActivationCode();
                _context.SaveChanges();
                return user.ActivationCode;
            }
            catch (Exception)
            {
                return "notsuccessful";
            }
        }

        public int GetMaxRoleId() => _context.Roles.Max(r => r.Id);

        public bool IsActive(string mobile)
        {
            return _context.Users.Any(u => u.Mobile == mobile && u.IsActive);
        }

        public User LoginUser(string Mobile, string HashedPassword)
        {
            return _context.Users
                .FirstOrDefault(u => u.Mobile == Mobile && u.Password == HashedPassword);
        }

        public string RegenerateActivationCode(string Mobile,bool IsActiveIncluded=true)
        {
            User user=_context.Users
                .FirstOrDefault(u => u.Mobile == Mobile);
            if (user == null)
            {
                return "notfound";
            }
            
            if(IsActiveIncluded && user.IsActive)
            {
                return "active";
            }
            try
            {
                user.ActivationCode = CodeGenerators.ActivationCode();
                _context.SaveChanges();
                return user.ActivationCode;
            }
            catch (Exception)
            {
                return "notsuccessful";
            }
            
        }
        public ResetPasswordResults ResetPassword(string Mobile, string ActivationCode, string HashedNewPassword)
        {
            User user = _context.Users.FirstOrDefault(u => u.Mobile==Mobile);
            if (user==null)
            {
                return ResetPasswordResults.UserNotFound;
            }
            else if (user.ActivationCode!=ActivationCode)
            {
                return ResetPasswordResults.ActivationCodeNotEqual;
            }
            else if (!user.IsActive)
            {
                return ResetPasswordResults.UserIsNotActive;
            }
            user.Password = HashedNewPassword;
            user.ActivationCode = null;
            _context.SaveChanges();
            return ResetPasswordResults.SuccessfullyChanged;
        }
    }
}
