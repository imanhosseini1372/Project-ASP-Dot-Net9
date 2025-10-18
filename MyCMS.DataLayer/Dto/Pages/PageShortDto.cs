namespace MyCMS.DataLayer.Dto.Pages
{
    public class PageShortDto
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string? PageImg { get; set; }
        public string ShortDesc { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool isShowSlider { get; set; }

    }
}
