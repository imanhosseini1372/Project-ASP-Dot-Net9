using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.DataLayer.Dto.Categuries;
using NuGet.Protocol.Core.Types;

namespace MyCMS.WebSite.Components
{
    public class MainMenuComponent:ViewComponent
    {
        private readonly ICateguryService _categuryService;

        public MainMenuComponent(ICateguryService categuryService)
        {
            _categuryService = categuryService;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            List<CateguryDto> categury=new List<CateguryDto>();
            foreach (var item in _categuryService.GetAllCateguries())
            {
                CateguryDto c=new CateguryDto() 
                {
                    CateguryTitle=item.CateguryTitle,
                    Id=item.Id,
                };
                categury.Add(c);
            }
        return View(categury);
        }
    }
}
