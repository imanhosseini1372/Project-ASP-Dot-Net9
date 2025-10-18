using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Pages.Interfaces;

namespace MyCMS.WebSite.Components
{
    public class MostViewsNewsComponent:ViewComponent
    {
        IPageService _pageService;
        public MostViewsNewsComponent(IPageService pageService)
        {
             _pageService = pageService;   
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var res = _pageService.GetPagesShortNews(4).ToList();
           
            return View(res);
        }
    }
}
