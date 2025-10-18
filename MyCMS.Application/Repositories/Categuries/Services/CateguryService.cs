using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.Application.Repositories.Framework;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Dto.Categuries;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Categuries.Services
{
    public class CateguryService : ICateguryService
    {
        #region Inject
        private readonly MyCmsDbContext _db;

        public CateguryService(MyCmsDbContext db)
        {
            _db = db;
        }

      
        #endregion

        #region Queries
        public IEnumerable<Categury> GetAllCateguries(bool isInclude = false)
        {
            if (isInclude)
            {
                return _db.Categuries.Include(e => e.Pages).AsNoTracking();
            }
            return _db.Categuries.AsNoTracking();
        }

        public IEnumerable<Categury> GetAllCateguries(int pageNumber, int pageSize)
        {
            return _db.Categuries.AsNoTracking();
        }
        public Categury GetCateguryById(int categuryId, bool isInclude = false) 
        {
            if (isInclude)
            {
                return _db.Categuries.Include(e=>e.Pages).SingleOrDefault(e => e.Id == categuryId);
            }
            return _db.Categuries.SingleOrDefault(e=>e.Id==categuryId);
        }
        public IEnumerable<CateguryPageCountDto> GetCateguriesByPageCount()
        {
            return _db.Categuries.Include(e => e.Pages).Select(e => new CateguryPageCountDto()
            {
                Id = e.Id,
                CateguryTitle = e.CateguryTitle,
                PageCount = e.Pages.Count()
            });
        }
        public IEnumerable<CateguryPageCountDto> GetCateguriesByPageCount(int pageNumber, int pageSize)
        {
            return _db.Categuries.Include(e => e.Pages).Select(e => new CateguryPageCountDto()
            {
                Id = e.Id,
                CateguryTitle = e.CateguryTitle,
                PageCount = e.Pages.Count()
            }).Skip((pageNumber - 1) * PageCount(pageSize))
            .Take(pageSize);
        }

        public int PageCount(int pageSize)
        {
            var res = Math.Ceiling(_db.Categuries.Count() / (decimal)pageSize);
            return Convert.ToInt32(res);
        }


        #endregion

        #region Commands
        public bool UpdateCategury(Categury categury)
        {
            try
            {
                var c = GetCateguryById(categury.Id);

                if (c != null)
                {

                    c.CateguryTitle = categury.CateguryTitle;
                 
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public int AddCategury(Categury categury)
        {

            try
            {
                categury.Id = 0;
                _db.Add(categury);
                _db.SaveChanges();
                return categury.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool DeleteCategury(int CateguryId)
        {
            try
            {
                var Categury = GetCateguryById(CateguryId);
                if (Categury != null)
                {
                    Categury.IsDeleted = true;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion
    }
}
