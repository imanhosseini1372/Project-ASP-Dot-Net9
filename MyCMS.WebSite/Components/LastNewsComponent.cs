using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MyCMS.Application.Repositories.Pages.Interfaces;

namespace MyCMS.WebSite.Components
{

    public class LastNewsComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public LastNewsComponent(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categuryId) 
        {
            var res = _pageService.GetPagesLastNews(categuryId).ToList();
            return View(res);
        }
    }
}
