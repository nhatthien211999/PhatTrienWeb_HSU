using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
  
    public class RegisterModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Password tối thiểu 6 ký tự")]
        [StringLength(50)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu không khớp")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password tối thiểu 6 ký tự")]
        [StringLength(50)]

        public string Password { get; set; }
    }
}