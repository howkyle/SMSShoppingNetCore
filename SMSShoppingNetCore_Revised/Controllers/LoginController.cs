﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMSShoppingNetCore_Revised.Models;
using System.Threading.Tasks;

namespace SMSShoppingNetCore_Revised.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserService  _userService;
        private readonly IMessageViewService _messageViewService;
       
        public LoginController(IUserService userService, IMessageViewService messageViewService)
        {
            _userService = userService;
            _messageViewService = messageViewService;
            
        }

        
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LogInViewModel loginViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _userService.LogIn(loginViewModel.Email, loginViewModel.Password);

                if (result.Succeeded)
                {
                    ViewBag.sessionUser = User.Identity.Name;
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    _messageViewService.IncorrectLoginDetails();
                }
            }
            return View();

        }

        public ActionResult Logout()
        {
             _userService.LogOut();
            return RedirectToAction("Index", "Landing");
        }

    }
}