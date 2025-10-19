using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.Application.Repositories.Comments.Interfaces;
using MyCMS.Application.Repositories.Pages.Interfaces;
using MyCMS.DataLayer.Dto.Pages;
using MyCMS.DataLayer.Models;
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
        private readonly ICommentService _commentService;

        public HomeController(ILogger<HomeController> logger ,
            ICateguryService categuryService,
            IPageService pageService,
            ICommentService commentService
            )
        {
            _logger = logger;
            _categuryService = categuryService;
            _pageService = pageService;
            _commentService = commentService;
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
            {
                Id = p.Id,
                PageTitle = p.PageTitle,
                PageImg = p.PageImg,
                ShortDesc = p.ShortDesc,
                Content = p.Content,
                RegisterDate = p.RegisterDate,
                PageTags = p.PageTags,
                PageVisit = p.PageVisit,
                Comments = _commentService.GetCommentsApprovedByPageId(p.Id).ToList()

            };
            ++p.PageVisit;
            _pageService.EditPage(p);
            return View(pageDto);
        }
        [HttpPost]
        public IActionResult AddComment(int PageId, string CommentText) 
        {
            var addComment = new Comment()

            {
                CommentText = CommentText,
                CommentBy = Convert.ToInt32( User.FindFirst("UserId").Value),
                PageId=PageId,
                RegisterDate = DateTime.Now,
                isApproved=false,
            };
            _commentService.AddComment(addComment);
            return RedirectToAction("ShowPage", new { id = PageId });
        }
        #endregion
        #endregion

    }
}
