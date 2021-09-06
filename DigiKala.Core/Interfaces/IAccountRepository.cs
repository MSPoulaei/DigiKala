using DigiKala.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Interfaces
{
    public interface IAccountRepository
    {
        bool AddUser(User user);
        bool ExistsMobileNumber(string mobile);
        bool IsActive(string mobile);
        int GetMaxRoleId();
        bool ActivateUser(string activationCode, string mobileNumber);
        User LoginUser(string Mobile, string HashedPassword);
        string RegenerateActivationCode(string Mobile, bool IsActiveIncluded = true);
        string ForgetPasswordActivationCode(string Mobile);
        public enum ResetPasswordResults { SuccessfullyChanged = 0, UserNotFound, ActivationCodeNotEqual, UserIsNotActive };
        public ResetPasswordResults ResetPassword(string Mobile, string ActivationCode, string HashedNewPassword);
    }
}
