using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.Application.Repositories.Comments.Interfaces;
using MyCMS.Application.Repositories.Framework;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Dto.Categuries;
using MyCMS.DataLayer.Dto.Comments;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Comments.Services
{
    public class CommentService : BaseGenericRepository<Comment>,ICommentService
    {
        #region Inject
        private readonly MyCmsDbContext _db;

        public CommentService(MyCmsDbContext db):base(context:db)
        {
            _db = db;
        }


        #endregion

        #region Queries
        public IEnumerable<Comment> GetAllComments(bool isInclude = false)
        {
            if (isInclude)
            {
                return _db.Comments.Include(e => e.User)
                .Include(e => e.Page)
                .ThenInclude(e => e.Categury);
                
            }
            return _db.Comments;
        }

        public Comment GetCommentById(int commentId, bool isInclude = false)
        {
            if (isInclude)
            {
                return _db.Comments.Include(e => e.User)
                .Include(e => e.Page)
                .ThenInclude(e => e.Categury)
                .SingleOrDefault(e => e.Id == commentId);
            }
            return _db.Comments.SingleOrDefault(e => e.Id == commentId);
        }

        public IEnumerable<CommentDto> GetAllComments(int pageNumber, int pageSize)
        {
            return _db.Comments
                .Include(e => e.Page)
                .ThenInclude(e => e.Categury)
                .Include(e=>e.User)
                .Select(e => new CommentDto () 
                 {
                 Id = e.Id,
                 CommentText = e.CommentText,
                 CommentBy = e.CommentBy,
                 CommentByName = e.User.Email,
                 PageTitle=e.Page.PageTitle,
                 CateguryTitle=e.Page.Categury.CateguryTitle,
                 RegisterDate=e.RegisterDate,
                 isApproved=e.isApproved

                 });
        }

        public int PageCount(int pageSize)
        {
            var res = Math.Ceiling(_db.Comments.Count() / (decimal)pageSize);
            return Convert.ToInt32(res);
        }


        #endregion

        #region Commands
     
        public int AddComment(Comment comment)
        {
            try
            {
                comment.Id = 0;
                _db.Add(comment);
                _db.SaveChanges();
                return comment.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteComment(int CommentId)
        {
            try
            {
                var comment = GetCommentById(CommentId);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateComment(Comment comment)
        {
            try
            {
                var c = GetCommentById(comment.Id);

                if (c != null)
                {

                    c.CommentText = comment.CommentText;
                    c.CommentBy = comment.CommentBy;
                    c.isApproved = comment.isApproved;
                    c.Id= comment.Id;
                    c.PageId= comment.PageId;
                    c.Page=comment.Page;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

      


        #endregion
    }
}
