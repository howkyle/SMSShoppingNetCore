using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMSShoppingNetCore_Revised.Controllers
{
    public class LandingController : Controller
    {
        private readonly IUserService _userService;
        public LandingController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Landing
        [AllowAnonymous]
        public ActionResult Index()
        {
            //Use method to check if logged in instead
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();

        }
        
    }
}