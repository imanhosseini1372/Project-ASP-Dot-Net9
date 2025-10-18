using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MyCMS.Application.Repositories.Pages.Interfaces;

namespace MyCMS.WebSite.Components
{

    public class PageShowSliderComponent: ViewComponent
    {
        private readonly IPageService _pageService;

        public PageShowSliderComponent(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categuryId) 
        {
            var res = _pageService.GetPagesShortNews( 8, categuryId, true).ToList();
          
            return View(res);
        }
    }
}
