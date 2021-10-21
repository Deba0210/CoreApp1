using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Models
{
    public interface IRepository
    {
        List<User> GetUsers(int id);
        List<User> GetUsers();
        User GetUserById(string mailId);
        bool EditUser(User user);
        bool DeleteUser(User user);
        List<User> SearchUsers(string searchTerm);
    }
}
