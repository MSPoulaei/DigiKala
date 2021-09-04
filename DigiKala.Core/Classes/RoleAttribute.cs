using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Classes
{
    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        string _roleName;
        public RoleAttribute(string roleName)
        {
            _roleName = roleName;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {//isLogined
                string username = context.HttpContext.User.Identity.Name;
            }
            else
            {
                //#TODO: Go to Login
            }
        }
    }
}
