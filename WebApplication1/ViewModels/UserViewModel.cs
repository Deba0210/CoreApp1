using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public string Title { get; set; }
        [BindProperty(SupportsGet=true)]
        public string SearchTerm { get; set; }
        public long PageNumber { get; set; }
        public long TotalItems{ get; set; }
    }
}
