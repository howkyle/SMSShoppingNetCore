using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSShoppingNetCore_Revised.Models;

namespace SMSShoppingNetCore_Revised.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageViewService _messageViewService;

        public SignUpController(IUserService userService, IMessageViewService messageViewService)
        {
            _userService = userService;
            _messageViewService = messageViewService;
        }

        // GET: SignUp
        //[AllowAnonymous]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(SignUpViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignUp(vm.FirstName, vm.LastName, vm.Email, vm.Password, vm.PasswordConfirmation);

                if (result.Succeeded)
                {
                    _messageViewService.SuccessfulRegistration();

                    return RedirectToAction("Index", "Landing");
                }
                else
                {
                    //action if user was not successfully added to the database
                    _messageViewService.UnsuccesfullRegistration();
                }
            }
            
            return View();
           

        }
    }
}