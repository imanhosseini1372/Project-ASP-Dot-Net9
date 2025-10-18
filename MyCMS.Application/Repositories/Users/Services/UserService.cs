using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Framework;
using MyCMS.Application.Repositories.Users.Interfaces;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.Application.Repositories.Users.Services
{
    public class UserService :BaseGenericRepository<User>,IUserService
    {
        #region ContextInject
        MyCmsDbContext _db;
        public UserService(MyCmsDbContext db):base(db) 
        {
            _db = db;
        }
        #endregion

        #region Queries
        public IEnumerable<User> GetAllUsers()
        {
            
            return _db.Users.Include(e=>e.Role).AsNoTracking();
        }
        public IEnumerable<User> GetAllUsers(int pageNumber, int pageSize)
        {
           return _db.Users.Include(e => e.Role)
                .Skip((pageNumber-1)*PageCount(pageSize))
                .Take(pageSize).AsNoTracking();
        }

        public int PageCount(int pageSize)
        {
            var res = Math.Ceiling(_db.Users.Count() /(decimal) pageSize);
            return Convert.ToInt32(res) ;
        }

        public User GetUserById(int userId)
        {
            return _db.Users.SingleOrDefault(e => e.Id == userId);
        }
        public bool IsExistUserName(string UserName) 
        {
            return _db.Users.IgnoreQueryFilters().Any(e=>e.UserName.ToLower().Trim()==UserName.ToLower().Trim());
        }
        public bool IsExistEmail(string email) 
        {
            return _db.Users.IgnoreQueryFilters().Any(e=>e.Email.ToLower().Trim()==email.ToLower().Trim());
        }
        public User GetUserByUserNameOrEmail(string userNameOrEmail)
        {
            _db.Roles.ToList();
            return _db.Users.SingleOrDefault(e=>e.UserName.ToLower().Trim()==userNameOrEmail.ToLower().Trim()
                                                || e.Email.ToLower().Trim() == userNameOrEmail.ToLower().Trim());
        }
        public List<Role> GetRoles() 
        {
           return _db.Roles.ToList();
        }
        public Role GetRoleById(int RoleId) 
        {
           return _db.Roles.Find(RoleId);
        }
        #endregion

        #region Commands
        public int AddUser(User user)
        {
            try
            {
                user.Id = 0;
                _db.Add(user);
                _db.SaveChanges();
                return user.Id;
            }
            catch
            {
                return -1;
            }
        }
        public bool DeleteUser(int userId)
        {
            try
            {
                var user = GetUserById(userId);
                if (user != null)
                {
                    user.IsDeleted = true;
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
        public bool UpdateUser(User user)
        {
            try
            {
                var u = GetUserById(user.Id);

                if (u != null)
                {

                    u.UserName = user.UserName;
                    u.Password = user.Password;
                    u.Email = user.Email;
                    u.RoleId = user.RoleId;
                    u.Role = user.Role;
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
