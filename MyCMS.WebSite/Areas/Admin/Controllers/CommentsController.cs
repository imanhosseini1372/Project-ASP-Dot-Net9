using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Comments.Interfaces;

namespace MyCMS.WebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        #region Inject
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        #endregion

        #region Methods

        #region CommentsList
        public IActionResult CommentsList(int pageNumber)
        {
            var pageSize = 10;
            ViewBag.Previous = pageNumber - 1;
            ViewBag.Next = pageNumber + 1;
            ViewBag.PageNum = pageNumber;
            ViewBag.Count = _commentService.PageCount(pageSize);
            var res = _commentService.GetCommentsByDto(pageNumber, pageSize);
            return View(res);
        }
        #endregion

        #region IsApproved
        public IActionResult CommentsNotApproved(int pageNumber)
        {
            var pageSize = 10;
            ViewBag.Previous = pageNumber - 1;
            ViewBag.Next = pageNumber + 1;
            ViewBag.PageNum = pageNumber;
            ViewBag.Count = _commentService.PageCount(pageSize);

            var res = _commentService.GetCommentsByDtoNotApproved(pageNumber, pageSize).ToList();
            return View(res);
        }
        public IActionResult ApprovedComment(int commentId)
        {
            if (commentId == -1)
            {
                _commentService.AllApprovedComment();
            }
            else
            {
                _commentService.ChangeApprovedComment(commentId);
            }
            return RedirectToAction("CommentsList");
        }
        #endregion


        #endregion

    }
}
