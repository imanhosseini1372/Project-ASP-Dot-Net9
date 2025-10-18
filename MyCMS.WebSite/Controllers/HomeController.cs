using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.Application.Repositories.Pages.Interfaces;
using MyCMS.DataLayer.Dto.Pages;
using MyCMS.WebSite.Models;

namespace MyCMS.WebSite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Inject
        private readonly ILogger<HomeController> _logger;
        private readonly ICateguryService _categuryService;
        private readonly IPageService _pageService;

        public HomeController(ILogger<HomeController> logger , ICateguryService categuryService,IPageService pageService)
        {
            _logger = logger;
            _categuryService = categuryService;
            _pageService = pageService;
        }
        #endregion

        #region Methods 

        #region MainPage
        [AllowAnonymous]
        public IActionResult Index(int Id)
        {
            ViewBag.CateguryId = Id;
            return View();
        }
        #endregion

        #region ShowPage
        [Route("ShowPage")]
        public IActionResult ShowPage(int id) 
        {
            var p=_pageService.getpageById(id);
            
            PageDto pageDto = new PageDto() 
            {Id=p.Id,
            PageTitle =p.PageTitle,
            PageImg=p.PageImg,
            ShortDesc=p.ShortDesc,
            Content=p.Content,
            RegisterDate=p.RegisterDate,
            PageTags=p.PageTags,
            PageVisit=p.PageVisit

            };
            ++p.PageVisit;
            _pageService.EditPage(p);
            return View(pageDto);
        }
        #endregion
        #endregion

    }
}
