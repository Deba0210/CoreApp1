using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public class ModelRepository : IRepository
    {
        QuickKartDBContext context;
        public ModelRepository()
        {
            context = new QuickKartDBContext();
        }

        public bool DeleteUser(User user)
        {
            try
            {
                List<PurchaseDetail> purchaseDetails = (from p in context.PurchaseDetails
                                                        where p.EmailId == user.EmailId
                                                        select p).ToList();
                foreach (var item in purchaseDetails)
                {
                    context.PurchaseDetails.Remove(item);
                    context.SaveChanges();
                }
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditUser(User userForEdit)
        {
            try
            {
                User user = GetUserById(userForEdit.EmailId);
                //User user = new User();
                user.EmailId = userForEdit.EmailId;
                user.UserPassword = userForEdit.UserPassword;
                user.Gender = userForEdit.Gender;
                user.Address = userForEdit.Address;
                user.DateOfBirth = userForEdit.DateOfBirth;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public User GetUserById(string mailId)
        {
            User user = (from u in context.Users
                         where u.EmailId == mailId
                         select u).SingleOrDefault();
            return user;
        }

        public List<User> GetUsers(int id)
        {
            List<User> lstUsers = new List<User>();
            lstUsers = (from u in context.Users
                        select u).Skip(id*10-10).Take(10).ToList();
            return lstUsers;
        }
        public List<User> GetUsers()
        {
            List<User> lstUsers = new List<User>();
            lstUsers = (from u in context.Users
                        select u).ToList();
            return lstUsers;
        }

        public List<User> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                List<User> users = GetUsers();
                return users;
            }
            List<User> lstUsers = (from u in context.Users
                                   where u.EmailId.Contains(searchTerm) | u.Address.Contains(searchTerm)
                                   select u).ToList();
            return lstUsers;
        }

    }
}
