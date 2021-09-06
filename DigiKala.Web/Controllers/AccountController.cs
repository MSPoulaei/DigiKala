using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiKala.Core.ViewModels;
using DigiKala.Core.Classes;
using DigiKala.Core.Interfaces;
using DigiKala.DataAccessLayer.Entities;

namespace DigiKala.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_accountRepository.ExistsMobileNumber(model.Mobile))
            {
                //Go To Login
            }

            User user = new User()
            {
                Mobile = model.Mobile,
                Password = model.Password.MD5Encrypt(),
                JoinedDate = DateTime.Now.ToShamsi(),
                ActivationCode = CodeGenerators.ActivationCode(),
                IsActive = false,
                Code=null,
                FullName=null,
                RoleId=_accountRepository.GetMaxRoleId()
            };
            _accountRepository.AddUser(user);
            //SMS
            MessageSender.SMS(user.Mobile, "سلام. به فروشگاه داروخانه دکتر پولایی خوش آمدید\n" + "کد فعالسازی:" + user.ActivationCode);
            //Activate
            return View(nameof(Activate),new ActivateViewModel() { Mobile=user.Mobile});
        }
        public ActionResult Activate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Activate(ActivateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_accountRepository.ActivateUser(model.ActivationCode, model.Mobile))
            {
                //Go To Login
                return RedirectToAction(nameof(Index), "Home");
            }
            ModelState.AddModelError("ActivationCode", "کد فعال سازی معتبر نمی باشد");
            return View(model);
        }
    }
}
