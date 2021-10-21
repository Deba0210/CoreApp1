using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public interface IRepositoryNew
    {
        bool RegisterUser(UserRegistrationViewModel regVm);
    }
}
