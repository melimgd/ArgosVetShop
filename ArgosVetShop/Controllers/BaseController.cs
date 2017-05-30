using ArgosVetShop.Models;
using ArgosVetShop.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArgosVetShop.Controllers
{
    public class BaseController:Controller
    {
        private ApplicationRoleManager _roleManager = null;
        private ApplicationDbContext _applicationDbContext = null;
        private ApplicationUserManager _userManager = null;
        private UserManagmentService _userService = null;
        private VetManagmentService _vetService = null;

        protected ApplicationRoleManager RoleManager
        {
            get
            {
                if(_roleManager != null)
                {
                    return _roleManager;
                }
                _roleManager = Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
                return _roleManager;
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                if(_userManager != null)
                {
                    return _userManager;
                }
                _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return _userManager;
            }
        }

        protected UserManagmentService UserService
        {
            get
            {
                if(_userService != null)
                {
                    return _userService;
                }
                _userService = new UserManagmentService(ApplicationDbContext,UserManager);
                return _userService;
            }
        }

        protected VetManagmentService VetService
        {
            get
            {
                if (_vetService != null)
                {
                    return _vetService;
                }
                _vetService = new VetManagmentService(ApplicationDbContext, UserManager);
                return _vetService;
            }
        }

        protected ApplicationDbContext ApplicationDbContext
        {
            get
            {
                if (_applicationDbContext != null)
                {
                    return _applicationDbContext;
                }
                _applicationDbContext = Request.GetOwinContext().Get<ApplicationDbContext>();
                return _applicationDbContext;
            }
        }

    }
}