using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository _repository;
        private IRepositoryNew _repositoryNew;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public HomeController(IRepository repository,IRepositoryNew repositoryNew,ILogger<HomeController> logger)
        {
            _logger = logger;
            _repository = repository;
            _repositoryNew = repositoryNew;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ShowUsers(int pageNumber=1, string SearchTerm="Default")
        {
            if (pageNumber<=0)
            {
                return RedirectToAction("ShowAllUsers");
            }

            else
            {
                if (SearchTerm!="Default")
                {
                    List<User> users = _repository.SearchUsers(SearchTerm);
                    UserViewModel userVm = new UserViewModel();
                    userVm.Users = users;
                    userVm.Title = "All Users";
                    // userVm.SearchTerm = "Default";
                    return View(userVm);
                }
                else
                {
                    List<User> lstUsers = _repository.GetUsers(pageNumber);
                    List<User> lstAllUsers = _repository.GetUsers();
                    UserViewModel userVm = new UserViewModel();
                    userVm.Title = "All Users";
                    userVm.Users = lstUsers;
                    userVm.PageNumber = pageNumber;
                    userVm.TotalItems = lstAllUsers.Count();
                    return View(userVm);
                }
               
            }
            
        }

        [HttpGet]
        [Authorize]
        public IActionResult ShowAllUsers(string SearchTerm="Default")
        {
            try
            {
                if (SearchTerm != "Default")
                {
                    List<User> users = _repository.SearchUsers(SearchTerm);
                    UserViewModel allUserVm = new UserViewModel();
                    allUserVm.Users = users;
                    allUserVm.Title = "All Users";
                    //allUserVm.SearchTerm = "Default";
                    allUserVm.PageNumber = 1;
                    return View(allUserVm);
                }
                else
                {
                    List<User> lstAllUsers = _repository.GetUsers();
                    UserViewModel allUserVm = new UserViewModel();
                    allUserVm.Title = "All Users";
                    allUserVm.Users = lstAllUsers;
                    allUserVm.PageNumber = 1;
                    //allUserVm.SearchTerm = "Default";
                    return View(allUserVm);
                }

            }
            catch (Exception ex)
            {

                return View("Error");
            }
           
        }
        [HttpGet]
        [Authorize]
        public IActionResult Details(string EmailId)
        {
            try
            {
                User user = _repository.GetUserById(EmailId);
                UserDetailViewModel userDetailVM = new UserDetailViewModel();
                userDetailVM.Title = "User Details";
                userDetailVM.Address = user.Address;
                userDetailVM.DateOfBirth = user.DateOfBirth;
                userDetailVM.EmailId = user.EmailId;
                userDetailVM.Gender = user.Gender;
                userDetailVM.UserPassword = user.UserPassword;
                userDetailVM.RoleId = user.RoleId;
                //userDetailVM.Role.RoleName = user.Role.RoleName;
                return View(userDetailVM);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult Edit(string EmailId)
        {
            List<string> lstRoles = new List<string> { "User","Admin"};
            List<string> lstGender = new List<string> { "--Select--","M", "F" };
            ViewBag.RoleId = new SelectList(lstRoles);
            ViewBag.Genders = new SelectList(lstGender);
            User userUpdate = _repository.GetUserById(EmailId);
            return View(userUpdate);
        }
        [Authorize]
        public RedirectToActionResult EditThisUser(User userVm)
        {
            try
            {
                User user = new User();
                user.EmailId = userVm.EmailId;
                user.UserPassword = userVm.UserPassword;
                user.Gender = userVm.Gender;
                user.DateOfBirth = userVm.DateOfBirth;
                user.Address = userVm.Address;
                //user.RoleId = userVm.RoleId;
                if (_repository.EditUser(user))
                {
                    return RedirectToAction("ShowUsers");
                    //return View("Edit");
                }
                return RedirectToAction("Edit", userVm);
            }
            catch (Exception ex)
            {

                //return View("Error") ;
                return RedirectToAction("ShowUsers");
            }
        }
        [Authorize]
        public RedirectToActionResult Delete(string EmailId)
        {
            try
            {
                User userUpdate = _repository.GetUserById(EmailId);
                if (_repository.DeleteUser(userUpdate))
                {
                    return RedirectToAction("ShowUsers");
                }
                return RedirectToAction("UserError");
            }
            catch (Exception ex)
            {

                return RedirectToAction("UserError");
            }
        }
        [HttpGet]
        public IActionResult Registration()
        {
            UserRegistrationViewModel regVm = new UserRegistrationViewModel();
            regVm.Title = "New User Registration";
            regVm.DateOfBirth = DateTime.Now;
            List<string> lstGender = new List<string> { "--Select--","M", "F" };
            ViewBag.Genders = new SelectList(lstGender);
            return View(regVm);
        }

        [HttpPost]
        public IActionResult Registration(UserRegistrationViewModel userRegVm)
        {
            try
            {
                bool isRegistered=_repositoryNew.RegisterUser(userRegVm);
                if (isRegistered)
                {
                    return RedirectToAction("ShowUsers");
                }
                return View("Error");
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        [Route("/Home/useError")]
        public IActionResult UserError()
        {
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
