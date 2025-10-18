using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.DataLayer.Dto.Categuries;
using MyCMS.DataLayer.Models;

namespace MyCMS.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CateguryController : Controller
    {
        #region Inject
        private readonly ICateguryService _categuryService;

        public CateguryController(ICateguryService categuryService)
        {
            _categuryService = categuryService;
        }

        #endregion

        #region Methods

        #region CateguryList
        [Route("CateguryList")]
        public IActionResult CateguryList(int PageNum = 1, int pageSize = 10)
        {
            ViewBag.Previous = PageNum - 1;
            ViewBag.Next = PageNum + 1;
            ViewBag.PageNum = PageNum;
            ViewBag.Count = _categuryService.PageCount(pageSize);
             var res=_categuryService.GetCateguriesByPageCount(PageNum, pageSize).ToList();
            return View(res);
        }
        #endregion

        #region CreateCategury

        [Route("CreateCategury")]
        public IActionResult CreateCategury()
        {
            return View();
        }

        [Route("CreateCategury")]
        [HttpPost]
        public IActionResult CreateCategury(CateguryDto categuryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categuryDto);
            }
            Categury categury = new()
            {
                CateguryTitle=categuryDto.CateguryTitle
            }; 
            _categuryService.AddCategury(categury);

            return RedirectToAction("CateguryList");
        }
        #endregion

        #region Edit
        [Route("EditCategury")]
        
        public IActionResult EditCategury(int categuryId) 
        {
            var dto = _categuryService.GetCateguryById(categuryId);
          
            var res = new CateguryDto()
            {
                Id = dto.Id,
                CateguryTitle = dto.CateguryTitle,
             
            };
            return View(res);
        }
        [Route("EditCategury")]
        [HttpPost]
        public IActionResult EditCategury(CateguryDto categury) 
        {
            if (!ModelState.IsValid)
            {
                return View(categury);
            }

            var c = new Categury()
            {
                Id = categury.Id,
                CateguryTitle = categury.CateguryTitle,
             
            };
            _categuryService.UpdateCategury(c);

            return RedirectToAction("CateguryList");
        }

        #endregion

        #region DeleteUser
        [Route("DeleteCategury")]
        public IActionResult DeleteCategury(int categuryId)
        {
            _categuryService.DeleteCategury(categuryId);
            return RedirectToAction("CateguryList");
        }
        #endregion
        #endregion
    }
}
