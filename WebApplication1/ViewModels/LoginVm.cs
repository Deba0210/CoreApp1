using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class LoginVm
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage ="Please enter your name!")]
        [Display(Name ="Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please your password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememeberMe { get; set; }
    }
}
