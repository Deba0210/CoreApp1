using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class identityRegisterVM
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Please provide your name!")]
        //[EmailAddress(ErrorMessage = "Please provide a valid email address")]
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please provide a valid email address!")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        [Display(Name = "E Mail Address")]
        [Remote(action:"IsEmailAvailable", controller: "IdentityHome")]
        public string EmailId { get; set; }
        [Remote(action: "CityListUpdate", controller: "IdentityHome")]
        public string State { get; set; }
        [Remote(action: "CityListUpdate", controller: "IdentityHome")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        //[MaxLength(length: 15, ErrorMessage = "Maximum permiable length is 15")]
        //[MinLength(length: 6, ErrorMessage = "Minimum permiable length is 6")]
        public string UserPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The passwords are not matching!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Title { get; set; }

    }
}
