using System.ComponentModel.DataAnnotations;

namespace MyCMS.DataLayer.Models
{
    public class User:BaseEntity 
    {
        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(3)]
        public string Password { get; set; }
        [Required(ErrorMessage = "لطفا فیلد را پرنمایید")]
        [MinLength(10)]
        [EmailAddress]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }

}
