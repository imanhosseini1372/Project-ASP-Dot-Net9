using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Comments.Interfaces;

namespace MyCMS.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
           _commentService = commentService;
        }
        public IActionResult CommentsList()
        {
            return View();
        }
        public IActionResult CommentsNotApproved(int pageNumber)
        {
            var pageSize = 10;
            _commentService.GetCommentsByDto(pageNumber,pageSize).ToList();
            return View();
        }



    }
}
