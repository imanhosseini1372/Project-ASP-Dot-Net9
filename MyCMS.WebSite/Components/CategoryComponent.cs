using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Categuries.Interfaces;

namespace MyCMS.WebSite.Components
{
    public class CategoryComponent:ViewComponent
    {
        private readonly ICateguryService _categuryService;

        public CategoryComponent(ICateguryService categuryService)
        {
            _categuryService = categuryService;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {

            return View(_categuryService.GetCateguriesByPageCount().ToList());
        }
    }
}
