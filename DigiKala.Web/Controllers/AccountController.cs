using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiKala.Core.ViewModels;
using DigiKala.Core.Classes;
using DigiKala.Core.Interfaces;
using DigiKala.DataAccessLayer.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using static DigiKala.Core.Interfaces.IAccountRepository;

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
                return RedirectToAction(nameof(Login), new { id = model.Mobile });
            }

            User user = new User()
            {
                Mobile = model.Mobile,
                Password = model.Password.MD5Encrypt(),
                JoinedDate = DateTime.Now.ToShamsi(),
                ActivationCode = CodeGenerators.ActivationCode(),
                IsActive = false,
                Code = null,
                FullName = null,
                RoleId = _accountRepository.GetMaxRoleId()
            };
            _accountRepository.AddUser(user);
            //SMS
            MessageSender.SMS(user.Mobile, "سلام. به داروخانه اینترنتی دکتر پولایی خوش آمدید\n" + "کد فعالسازی:" + user.ActivationCode);
            //Activate
            return RedirectToAction(nameof(Activate), new { id = user.Mobile });
        }

        public IActionResult Activate(string id)
        {
            if (id != null)
            {
                return View(new ActivateViewModel { Mobile = id });

            }
            return View();
        }
        [HttpPost]
        public IActionResult Activate(ActivateViewModel model)
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
        [HttpPost]
        public int ResendSMS(string Mobile)
        {
            string activationCode = _accountRepository.RegenerateActivationCode(Mobile);
            if (activationCode == "notfound")
            {
                return 1;
            }
            else if (activationCode == "active")
            {
                return 2;
            }
            else if (activationCode == "notsuccessful")
            {
                return 3;
            }
            else if (!MessageSender.SMS(Mobile, "سلام. به داروخانه اینترنتی دکتر پولایی خوش آمدید\n" + "کد فعالسازی:" + activationCode))
            {
                return 3;
            }
            return 0;
        }
        public IActionResult Login(string id)
        {
            if (id != null)
            {
                return View(new LoginViewModel { Mobile = id });

            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = _accountRepository.LoginUser(model.Mobile, model.Password.MD5Encrypt());
            if (user == null)
            {
                //Go To Login
                ModelState.AddModelError("Password", "اطلاعات وارد شده صحیح نمی باشد");
                return View(model);
            }
            if (!user.IsActive)
            {
                return RedirectToAction(nameof(Activate), new { id = user.Mobile });
            }
            //Log in
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Mobile)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = model.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);

            if (user.RoleId == _accountRepository.GetMaxRoleId())
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return View(model);
        }
        public IActionResult ForgetPassword(string id)
        {
            if (id != null)
            {
                return View(new ForgetPasswordViewModel { Mobile = id });
            }
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_accountRepository.ExistsMobileNumber(model.Mobile))
            {
                ModelState.AddModelError("Mobile", "کاربری با این شماره یافت نشد");
                return View(model);
            }
            if (!_accountRepository.IsActive(model.Mobile))
            {
                ModelState.AddModelError("Mobile", "حساب کاربر فعال نیست. لطفا ابتدا نسبت به فعال سازی حساب اقدام کنید");
                return View(model);
            }
            string ActivationCode=_accountRepository.RegenerateActivationCode(model.Mobile, IsActiveIncluded: false);
            MessageSender.SMS(model.Mobile, "سلام.با کد فعالسازی " + ActivationCode + " میتوانید نسبت به بازیابی رمز عبورتان اقدام نمایید.\n" + "با تشکر. داروخانه اینترنتی دکتر پولایی");
            return RedirectToAction(nameof(ResetPassword),new {id=model.Mobile });
        }
        public IActionResult ResetPassword(string id)
        {
            if (id != null)
            {
                return View(new ResetPasswordViewModel {Mobile=id });
            }
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ResetPasswordResults result = _accountRepository.ResetPassword(
                model.Mobile,
                model.ActivationCode,
                model.Password.MD5Encrypt());

            switch (result)
            {
                case ResetPasswordResults.SuccessfullyChanged:
                    return RedirectToAction(nameof(Login), new { id = model.Mobile });
                case ResetPasswordResults.UserNotFound:
                    ModelState.AddModelError("ConfirmPassword", "کاربر مورد نظر یافت نشد");
                    break;
                case ResetPasswordResults.ActivationCodeNotEqual:
                    ModelState.AddModelError("ActivationCode", "کد تایید معتبر نیست");
                    break;
                case ResetPasswordResults.UserIsNotActive:
                    ModelState.AddModelError("ConfirmPassword", "حساب کاربر مورد نظر فعال نمی باشد");
                    break;
                default:
                    break;
            }
            return View(model);
        }


    }

}
