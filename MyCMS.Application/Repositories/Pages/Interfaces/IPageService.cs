using MyCMS.Application.Repositories.Framework;
using MyCMS.DataLayer.Dto.Pages;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Pages.Interfaces
{
    public interface IPageService:IBaseGenericRepository<Page>
    {
        IEnumerable<Page> GetAllPages(bool isIncluded=false);
        IEnumerable<Page> GetAllPages(int pageNum,int pageSize);
        IEnumerable<PageShortDto> GetPagesShortNews( int topNumber ,int categuryId = 0, bool isShowSlider=false );
        IEnumerable<PageShortDto> GetPagesLastNews(int categuryId=-1 );
        
        Page getpageById(int pageId);
        int CreatePage(Page page);
        bool EditPage(Page page);
        bool DeletePage(int pageId);



    }
}
