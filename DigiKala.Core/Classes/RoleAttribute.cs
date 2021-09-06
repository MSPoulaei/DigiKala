using DigiKala.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Classes
{
    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        int _permissionId;
        IUserRepository userRepository; 
        public RoleAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {//isLogined
                string username = context.HttpContext.User.Identity.Name;
                userRepository = (IUserRepository)context.HttpContext.RequestServices.GetService(typeof(IUserRepository));
                int roleId = userRepository.GetUserRoleId(username);
                if (!userRepository.ExistsPermission(_permissionId, roleId))
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
