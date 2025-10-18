namespace MyCMS.DataLayer.Models
{
    public class Comment : BaseEntity 
    {
        public string CommentText { get; set; }
        public int CommentBy { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool isApproved { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
        public User User { get; set; }
    }
}
