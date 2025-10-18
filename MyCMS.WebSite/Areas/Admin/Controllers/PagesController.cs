using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.Application.Repositories.Pages.Interfaces;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Dto.Pages;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCMS.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {

        #region Inject
        private readonly IPageService _pageService;
        private readonly ICateguryService _categuryService;

        public PagesController(IPageService pageService, ICateguryService categuryService)
        {
            _pageService = pageService;
            _categuryService = categuryService;
        }

        #endregion

        #region Methods

        #region PageList
        public  IActionResult PageList()
        {
            var res = _pageService.GetAllPages(true);

            return View(res);
        }

        #endregion

        #region CreatePage
        [Route("CreatePage")]
        public IActionResult CreatePage()
        {
            ViewData["Categury"] = new SelectList(_categuryService.GetAllCateguries(), "Id", "CateguryTitle");
            return View();
        }

        [Route("CreatePage")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePage(PageDto page)
        {
            page.PageImg = "NoPhoto.png";
            if (page.imgNews != null)
            {
                page.PageImg = Guid.NewGuid().ToString() + Path.GetExtension(page.imgNews.FileName);
                string savePath = $"{Directory.GetCurrentDirectory()}/wwwroot/PageImages/{page.PageImg}";
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    page.imgNews.CopyTo(stream);
                }
            }


            if (ModelState.IsValid)
            {
                var p = new Page()
                {
                    PageTitle = page.PageTitle,
                    PageImg = page.PageImg,
                    ShortDesc = page.ShortDesc,
                    Content = page.Content,
                    PageTags = page.PageTags,
                    PageVisit = 0,
                    isShowSlider = page.isShowSlider,
                    CateguryId = page.CateguryId,
                    Categury = _categuryService.GetCateguryById(page.CateguryId),
                };


                _pageService.CreatePage(p);
                return RedirectToAction("PageList");
            }

            ViewBag.Categury = new SelectList(_categuryService.GetAllCateguries(), "Id", "CateguryTitle", page.CateguryId);
            return View(page);
        }
        #endregion

        #region EditPage

        public IActionResult EditPage(int id)
        {
          

            var page =_pageService.getpageById(id);
            var p = new PageDto()
            {
                Id = page.Id,
                RegisterDate=page.RegisterDate,
                PageTitle = page.PageTitle,
                PageImg = page.PageImg,
                ShortDesc = page.ShortDesc,
                Content = page.Content,
                PageTags = page.PageTags,
                PageVisit = page.PageVisit,
                isShowSlider = page.isShowSlider,
                CateguryId = page.CateguryId,

            };
            ViewData["Categury"] = new SelectList(_categuryService.GetAllCateguries(), "Id", "CateguryTitle", page.CateguryId);
            return View(p);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPage(PageDto page)
        {
            if (page.imgNews != null)
            {
                if (page.PageImg!= "NoPhoto.png")
                {
                    string DeletePath = $"{Directory.GetCurrentDirectory()}/wwwroot/PageImages/{page.PageImg}";
                    if(System.IO.File.Exists(DeletePath))
                    {
                        System.IO.File.Delete(DeletePath);
                    }
                }

                page.PageImg = Guid.NewGuid().ToString() + Path.GetExtension(page.imgNews.FileName);
                string savePath = $"{Directory.GetCurrentDirectory()}/wwwroot/PageImages/{page.PageImg}";
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    page.imgNews.CopyTo(stream);
                }
            }

            if (ModelState.IsValid)
            {
                var p = new Page()
                { 
                    Id=page.Id,
                    RegisterDate=page.RegisterDate,
                    PageTitle = page.PageTitle,
                    PageImg = page.PageImg,
                    ShortDesc = page.ShortDesc,
                    Content = page.Content,
                    PageTags = page.PageTags,
                    PageVisit = page.PageVisit.Value,
                    isShowSlider = page.isShowSlider,
                    CateguryId = page.CateguryId,
                    Categury = _categuryService.GetCateguryById(page.CateguryId),
                };
                _pageService.EditPage(p);
                return RedirectToAction("PageList");
            }
            ViewData["Categury"] = new SelectList(_categuryService.GetAllCateguries(), "Id", "CateguryTitle", page.CateguryId);
            return View(page);
        }
        #endregion

        #region DeletePage
        public IActionResult DeletePage(int id)
        {
            _pageService.DeletePage(id);
            return RedirectToAction("PageList");
        }


        #endregion



        #endregion
    }
}
