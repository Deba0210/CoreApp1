using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public class ModelRepositoryNew : IRepositoryNew
    {
        QuickKartDBContext context;

        public ModelRepositoryNew()
        {
            context = new QuickKartDBContext();
        }
        public bool RegisterUser(UserRegistrationViewModel regVm)
        {
            try
            {
                User user = new User();
                user.EmailId = regVm.EmailId;
                user.UserPassword = regVm.UserPassword;
                user.Gender = regVm.Gender;
                user.DateOfBirth = regVm.DateOfBirth;
                user.Address = regVm.Address;
                user.RoleId = Convert.ToByte(2);
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
