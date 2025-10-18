using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MyCMS.Application.Repositories.Users.Interfaces;
using MyCMS.Application.Security;
using MyCMS.DataLayer.Dto.Users;
using MyCMS.DataLayer.Models;
using System.Threading.Tasks;

namespace MyCMS.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        #region Inject
        IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region ShowUser
        [Route("UserList")]
        public async Task<IActionResult> UserList(int PageNum=1,int pageSize=10)
        {
            ViewBag.Previous = PageNum-1;
            ViewBag.Next= PageNum+1;
            ViewBag.PageNum= PageNum;
            ViewBag.Count = _userService.PageCount(pageSize);
            var res = _userService.GetAllUsers(PageNum,pageSize).ToList();
            return View(res);
        }
        #endregion

        #region DeleteUser
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int Id)
        {
            _userService.DeleteUser(Id);
            return RedirectToAction("UserList");
        }
        #endregion

        #region EditUser
        [Route("EditUser")]
        public IActionResult EditUser(int Id)
        {   
            
            var dto =_userService.GetUserById(Id);
            ViewBag.Role = _userService.GetRoles().Where(e => e.Id!=dto.RoleId);
            var res = new EditUserDto()
            {
                Id=dto.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                RoleId = dto.RoleId,
                RoleName = dto.Role.RoleTitle
            };
            return View(res);
        }
        [Route("EditUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(EditUserDto editUser)
        {
            var dto = _userService.GetUserById(editUser.Id);
            ViewBag.Role = _userService.GetRoles();
            if (!ModelState.IsValid)
            {
                return View(editUser);
            }
            if (_userService.IsExistEmail(editUser.Email)&& editUser.Email!=dto.Email)
            {
                ModelState.AddModelError("Email", "باایمیل وارد شده قبلا ثبت نام شده");
                return View(editUser);
            }
            if (_userService.IsExistUserName(editUser.UserName) && editUser.UserName != dto.UserName)
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده در سیستم وجود دارد لطفا نام دیگری انتخاب نمایید");
                return View(editUser);
            }
            User user = new User()
            { Id = editUser.Id,
                UserName = editUser.UserName,
                Password= dto.Password,
                Email = editUser.Email,
                RoleId = editUser.RoleId,
                Role=_userService.GetRoleById(editUser.RoleId)
            };
            _userService.UpdateUser(user);

            return RedirectToAction("UserList");
        }
        #endregion

        #region CreateUser

        [Route("CreateUser")]
        public IActionResult CreateUser()
        {
            ViewBag.Role = _userService.GetRoles();
            return View();
        }
        [Route("CreateUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(CreateUserDto register)
        {
            ViewBag.Role = _userService.GetRoles();
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
                RoleId = register.RoleId,
                Role = _userService.GetRoleById(register.RoleId)
            };
            _userService.AddUser(user);
            return RedirectToAction("UserList");
        }

        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(int Id) 
        {
            var res = _userService.GetUserById(Id);
            res.Password = PasswordHasher.HashPassword("123");
            _userService.UpdateUser(res);
           return RedirectToAction("UserList");
        }
        #endregion

    }
}
