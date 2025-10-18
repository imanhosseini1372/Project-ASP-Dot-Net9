using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Framework;
using MyCMS.Application.Repositories.Pages.Interfaces;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Dto.Pages;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Pages.Services
{
    public class PageService : BaseGenericRepository<Page>, IPageService
    {

        #region Inject
        private readonly MyCmsDbContext _context;
        public PageService(MyCmsDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion


        #region Queries
        public IEnumerable<Page> GetAllPages(bool isIncluded = false)
        {
            if (!isIncluded)
            {
                return _context.Pages.AsNoTracking();
            }
            return _context.Pages.Include(e => e.Categury).AsNoTracking().ToList();
        }

        public IEnumerable<Page> GetAllPages(int pageNum, int pageSize)
        {

            return _context.Pages.Include(e => e.Categury).Skip((pageNum - 1) * PageCount(pageSize)).Take(pageSize).AsNoTracking();
        }

        public Page getpageById(int pageId)
        {
            return _context.Pages.Include(e => e.Categury).SingleOrDefault(e => e.Id == pageId);
        }
        public int PageCount(int pageSize)
        {
            var res = Math.Ceiling(_context.Pages.Count() / (decimal)pageSize);
            return Convert.ToInt32(res);
        }

        public IEnumerable<PageShortDto> GetPagesShortNews(int topNumber, int categuryId = 0, bool isShowSlider = false)
        {
            if (isShowSlider)
            {
                if (categuryId!= 0)
                {
                    return _context.Pages.Where(e => e.CateguryId == categuryId && e.isShowSlider)
               .OrderByDescending(e => e.PageVisit)
         .Take(topNumber)
         .Select(e => new PageShortDto()
         {
             Id = e.Id,
             PageTitle = e.PageTitle,
             PageImg = e.PageImg,
             ShortDesc = e.ShortDesc,
             RegisterDate = e.RegisterDate,
             isShowSlider = e.isShowSlider
         }

         ).AsNoTracking();
                }
                return _context.Pages.Where(e=>e.isShowSlider)
                    .OrderByDescending(e => e.PageVisit)
              .Take(topNumber)
              .Select(e => new PageShortDto()
              {
                  Id = e.Id,
                  PageTitle = e.PageTitle,
                  PageImg = e.PageImg,
                  ShortDesc = e.ShortDesc,
                  RegisterDate = e.RegisterDate,
                  isShowSlider = e.isShowSlider
              }

              ).AsNoTracking();
            }
            return _context.Pages.OrderByDescending(e => e.PageVisit)
                    .Take(topNumber)
                    .Select(e => new PageShortDto()
                    {
                        Id = e.Id,
                        PageTitle = e.PageTitle,
                        PageImg = e.PageImg,
                        ShortDesc = e.ShortDesc,
                        RegisterDate = e.RegisterDate,
                        isShowSlider = e.isShowSlider
                    }
                    ).AsNoTracking();
        }

        public IEnumerable<PageShortDto> GetPagesLastNews(int categuryId = 0)
        {
           if (categuryId!=0)
            {
                return _context.Pages
                    .Where(e => e.CateguryId == categuryId)
                    .OrderByDescending(e=>e.RegisterDate )
                    .Select(e => new PageShortDto()
                    {
                        Id = e.Id,
                        PageTitle = e.PageTitle,
                        PageImg = e.PageImg,
                        ShortDesc = e.ShortDesc,
                        RegisterDate = e.RegisterDate,
                        isShowSlider = e.isShowSlider
                    }
                    ).AsNoTracking();
            }
            return _context.Pages
                .OrderByDescending(e => e.RegisterDate)
            .Select(e => new PageShortDto()
            {
                Id = e.Id,
                PageTitle = e.PageTitle,
                PageImg = e.PageImg,
                ShortDesc = e.ShortDesc,
                RegisterDate = e.RegisterDate,
                isShowSlider = e.isShowSlider
            }
            ).AsNoTracking();
        }
        #endregion


        #region Commands

        public int CreatePage(Page page)
        {
            try
            {
                page.Id = 0;
                page.RegisterDate = DateTime.Now;
                _context.Add(page);
                _context.SaveChanges();
                return page.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeletePage(int pageId)
        {
            try
            {
                var p = getpageById(pageId);
                p.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditPage(Page page)
        {
            try
            {

                var p = getpageById(page.Id);
                if (p != null)
                {
                    p.PageTitle = page.PageTitle;
                    p.PageImg = page.PageImg;
                    p.ShortDesc = page.ShortDesc;
                    p.Content = page.Content;
                    p.PageTags = page.PageTags;
                    p.PageVisit = page.PageVisit;
                    p.isShowSlider = page.isShowSlider;
                    p.CateguryId = page.CateguryId;
                    p.Categury = page.Categury;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

  

        #endregion
    }
}
