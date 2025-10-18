using MyCMS.Application.Repositories.Framework;
using MyCMS.DataLayer.Dto.Categuries;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Categuries.Interfaces
{
    public interface ICateguryService
    {
        #region Queries
        public IEnumerable<Categury> GetAllCateguries(bool isInclude = false);
        public Categury GetCateguryById(int categuryId , bool isInclude = false);
        public IEnumerable<Categury> GetAllCateguries(int pageNumber, int pageSize);
        public IEnumerable<CateguryPageCountDto> GetCateguriesByPageCount();
        public IEnumerable<CateguryPageCountDto> GetCateguriesByPageCount(int pageNumber, int pageSize);
        public int PageCount(int pageSize);
        #endregion

        #region Commands
        int AddCategury(Categury categury);
        bool DeleteCategury(int CateguryId);
        bool UpdateCategury(Categury categury);
        #endregion
    }
}
