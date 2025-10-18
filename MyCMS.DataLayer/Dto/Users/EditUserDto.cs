using System.ComponentModel.DataAnnotations;

namespace MyCMS.DataLayer.Dto.Users
{
    public class EditUserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا فیلدنام کاربری را پرنمایید")]
        [MinLength(3, ErrorMessage = " فیلد نام کاربری نمیتواند کمتر از 3 کارکتر باشد ")]
        public string UserName { get; set; }
       

        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(10, ErrorMessage = " فیلد ایمیل نمیتواند کمتر از 10 کارکتر باشد ")]
        [EmailAddress(ErrorMessage = " ساختار ایمیل اشتباه می باشد ")]
        public string Email { get; set; }

        public  int RoleId { get; set; }
        public string? RoleName { get; set; } 


    }
}
