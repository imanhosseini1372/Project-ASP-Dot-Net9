using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Dto.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int CommentBy { get; set; }
        public string CommentByName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool isApproved { get; set; }
        public string PageTitle { get; set; }
        public string CateguryTitle { get; set; }
        
    }
}
