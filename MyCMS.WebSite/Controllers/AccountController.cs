using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyCMS.Application.Repositories.Users.Interfaces;
using MyCMS.Application.Security;
using MyCMS.DataLayer.Dto.Users;
using MyCMS.DataLayer.Models;
using System.Security.Claims;

namespace MyCMS.WebSite.Controllers
{
    public class AccountController : Controller
    {
        #region Inject
        IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Register
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {

                return View(register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "باایمیل وارد شده قبلا ثبت نام شده");
                return View(register);
            }
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده در سیستم وجود دارد لطفا نام دیگری انتخاب نمایید");
                return View(register);
            }
            User user = new User()
            {
                UserName = register.UserName,
                Email = register.Email,
                Password = PasswordHasher.HashPassword(register.Password),
                RoleId = 2
            };
            _userService.AddUser(user);

            return View("login", new LoginDto() {UserNameOrEmail=user.Email});
        }
        #endregion

        #region Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.GetUserByUserNameOrEmail(login.UserNameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("UserNameOrEmail", "نام کاربری یا ایمیل وارد شده یافت نشد");
                return View(login);
            }
            else 
            {
                if (!PasswordHasher.VerifyHashedPassword(user.Password,login.Password))
                {
                    ModelState.AddModelError("Password", "کلمه عبور وارد شده اشتباه است");
                    return View(login);
                }
                List<Claim> claims = new List<Claim>() 
                {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("UserId",user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.RoleTitle.ToString())
                };
                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal=new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties() {IsPersistent=login.RememberMe };
                HttpContext.SignInAsync(principal, properties);

                return RedirectToAction("Index", "Home");
            }
                
        }
        #endregion

        #region LogOut
        [Route("Logout")]
        public IActionResult Logout() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
        #endregion
        public IActionResult AccessDenied() 
        {
            return View("Login");
        }
    }
}
