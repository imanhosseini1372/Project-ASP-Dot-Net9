using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Dto.Users
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "لطفا فیلدنام کاربری را پرنمایید")]
        [MinLength(3, ErrorMessage = " فیلد نام کاربری نمیتواند کمتر از 3 کارکتر باشد ")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا فیلد پسورد را پرنمایید")]
        [RegularExpression("^(?=.{8,15}$)(?=\\p{L})(?=.*\\p{N}.*$).*", ErrorMessage = " فیلد پسورد باید ترکیبی از حروف بزرگ و کوچک وکارکتر خاص  باشد ")]

        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(3,ErrorMessage = " فیلد  تکرار پسورد نمیتواند کمتر از 3 کارکتر باشد ")]
        [Compare("Password", ErrorMessage = " تکرار کلمه با کلمه عبور  عبور یکسان نیست")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(10, ErrorMessage = " فیلد ایمیل نمیتواند کمتر از 10 کارکتر باشد ")]
        [EmailAddress( ErrorMessage = " ساختار ایمیل اشتباه می باشد ")]
        public string Email { get; set; }


    }
}
