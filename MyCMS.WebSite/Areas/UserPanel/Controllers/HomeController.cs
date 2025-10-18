using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MyCMS.Application.Repositories.Users.Interfaces;
using MyCMS.Application.Security;
using MyCMS.DataLayer.Dto.Users;

namespace MyCMS.WebSite.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize()]
    public class HomeController : Controller
    {
        #region Inject
        IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Dashboard
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region ChangePassword
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("ChangePassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }

            var id = Convert.ToInt32(User.FindFirst("UserId").Value);
            var CurrentUser = _userService.GetUserById(id);

            if (!PasswordHasher.VerifyHashedPassword(CurrentUser.Password, changePassword.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی اشتباه است");
                return View(changePassword);
            }

            CurrentUser.Password = PasswordHasher.HashPassword(changePassword.Password);
            if (!_userService.UpdateUser(CurrentUser))
            {
                ModelState.AddModelError("OldPassword", "عملیات با شکست مواجه شد لطفا دوباره سعی نمایید");
                return View(changePassword);
            }
            return RedirectToAction("index", "home");

        }
        #endregion
    }
}
