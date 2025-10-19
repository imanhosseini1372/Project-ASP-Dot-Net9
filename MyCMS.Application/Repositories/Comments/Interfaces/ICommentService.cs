using MyCMS.Application.Repositories.Framework;
using MyCMS.DataLayer.Dto.Categuries;
using MyCMS.DataLayer.Dto.Comments;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Comments.Interfaces
{
    public interface ICommentService:IBaseGenericRepository<Comment>
    {
        #region Queries
        public IEnumerable<Comment> GetAllComments(bool isInclude = false);
        public Comment GetCommentById(int commentId, bool isInclude = false);
        public IEnumerable<CommentDto> GetCommentsByDto(int pageNumber, int pageSize);
        public IEnumerable<CommentDto> GetCommentsByDtoNotApproved(int pageNumber, int pageSize);
        public int PageCount(int pageSize);
        #endregion

        #region Commands
        int AddComment(Comment comment);
        bool DeleteComment(int CommentId);
        bool UpdateComment(Comment comment);
        bool AllApprovedComment();
        bool ChangeApprovedComment(int commentId);

        List<Comment> GetCommentsApprovedByPageId(int pageId);

        #endregion
    }
}
