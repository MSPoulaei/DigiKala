using DigiKala.Core.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiKala.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [RoleAttribute(1)]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
