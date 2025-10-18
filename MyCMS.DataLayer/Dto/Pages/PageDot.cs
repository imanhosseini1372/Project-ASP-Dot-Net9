using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Dto.Pages
{
    public class PageDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="عنوان نمیتواند خالی باشد")]
        public string PageTitle { get; set; }
        public string? PageImg { get; set; }
        [Required(ErrorMessage = "توضیح کوتاه نمیتواند خالی باشد")]
        public string ShortDesc { get; set; }
        [Required(ErrorMessage = "محتوا نمیتواند خالی باشد")]
        public string Content { get; set; }
        public DateTime RegisterDate { get; set; }
        [Required(ErrorMessage = "برچسب نمیتواند خالی باشد")]
        public string? PageTags { get; set; }
        public int? PageVisit { get; set; }
        public bool isShowSlider { get; set; }
        public int CateguryId { get; set; }
        public IFormFile? imgNews { get; set; }
    }
}
