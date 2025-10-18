using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Dto.Users
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "لطفا فیلد پسورد را پرنمایید")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "لطفا فیلد پسورد را پرنمایید")]
        // [RegularExpression("^(?=.{8,15}$)(?=\\p{L})(?=.*\\p{N}.*$).*", ErrorMessage = " فیلد پسورد باید ترکیبی از حروف بزرگ و کوچک وکارکتر خاص  باشد ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(3, ErrorMessage = " فیلد  تکرار پسورد نمیتواند کمتر از 3 کارکتر باشد ")]
        [Compare("Password", ErrorMessage = " تکرار کلمه با کلمه عبور  عبور یکسان نیست")]
        public string RePassword { get; set; }

    }
}
