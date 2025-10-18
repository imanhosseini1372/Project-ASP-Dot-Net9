using System.ComponentModel.DataAnnotations;

namespace MyCMS.DataLayer.Dto.Users
{
    public class LoginDto 
    {
        [Required(ErrorMessage = "لطفا فیلد نام کاربری را پرنمایید")]
        [MinLength(3, ErrorMessage = " فیلد نام کاربری نمیتواند کمتر از 3 کارکتر باشد ")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "لطفا فیلد پسورد را پرنمایید")]
        public string Password { get; set; }
        public  bool RememberMe { get; set; }
    }
}
