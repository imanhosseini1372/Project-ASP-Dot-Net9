namespace MyCMS.DataLayer.Models
{
    public class Page:BaseEntity
    {
        public string PageTitle { get; set; }
        public string PageImg { get; set; }
        public string ShortDesc { get; set; }
        public string Content { get; set; }
        public string PageTags { get; set; }
        public DateTime RegisterDate { get; set; }
        public int PageVisit { get; set; }
        public bool isShowSlider { get; set; }
        public int CateguryId { get; set; }
        public Categury? Categury { get; set; }
        public ICollection<Comment>? Comment { get; set; }
        
    }
}
