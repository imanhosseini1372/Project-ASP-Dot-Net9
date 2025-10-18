using MyCMS.Application.Repositories.Framework;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Users.Interfaces
{
    public interface IUserService :IBaseGenericRepository<User>
    {
        #region Queries
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsers(int pageNumber,int pageSize);
        User GetUserById(int userId);
        User GetUserByUserNameOrEmail(string userNameOrEmail);
        public bool IsExistUserName(string UserName);
        public bool IsExistEmail(string email);

        public List<Role> GetRoles();
        public Role GetRoleById(int RoleId);
        public int PageCount(int pageSize); 
        #endregion

        #region Commands
        int AddUser(User user);
        bool DeleteUser(int userId);
        bool UpdateUser(User user);

        #endregion
       
    }
}
