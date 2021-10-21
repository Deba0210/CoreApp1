using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class UserDetailViewModel
    {
        public string EmailId { get; set; }
        public string UserPassword { get; set; }
        public byte? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
    }
}
