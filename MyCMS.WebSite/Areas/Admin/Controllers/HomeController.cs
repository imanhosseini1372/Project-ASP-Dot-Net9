using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Users.Interfaces;

namespace MyCMS.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }



    }
   
}
