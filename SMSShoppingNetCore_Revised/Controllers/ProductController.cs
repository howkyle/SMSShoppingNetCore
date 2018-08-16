using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSShoppingNetCore_Revised.Models;

namespace SMSShoppingNetCore_Revised.Controllers
{
    public class ProductController : Controller
    {

        //private readonly DBContext db;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMessageViewService _messageViewService;

        public ProductController(IProductService productService, IUserService userService, IMessageViewService messageViewService)
        {
            _productService = productService;
            _userService = userService;
            _messageViewService = messageViewService;
        }

        // GET: Product

        [AllowAnonymous]
        public ActionResult Index()
        {
            //ViewBag.sessionUser = User.Identity.Name;
            var products = _productService.GetProducts();
            return View(products);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product prod;
            try
            {
                //prod = db.Products.Single(p => p.Id == id);
               prod =  _productService.GetProduct(id);
            }
            catch
            {
                //if the product doesnt exist error
                return View();
            }
            return View(prod);
        }

        public ActionResult AddToCart(int id)
        {
            
            var result = _userService.AddToCart(id);
            if (result){
                _messageViewService.ItemAddedSuccess();
            }
            else
            {
                _messageViewService.ItemAddedError();
            }
            
            return RedirectToAction("Index");
        }


        public ActionResult RemoveFromCart(int id)
        {
            Boolean result;

            result = _userService.RemoveFromCart(id);
            if (result)
            {
                _messageViewService.ItemRemovedSuccess();
            }
            else
            {
                _messageViewService.ItemRemovedError();
            }
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            //code for getting and displaying cart

            var cart = _userService.GetCart();

            return View(cart);

        }

        public ActionResult Checkout()
        {

            //code for allowing user to checkout items int cart
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Checkout(CheckOutViewModel vm)
        {
            Boolean result;
            
            if (ModelState.IsValid)
            {
                result = await _userService.CheckoutAsync(vm.CreditCardNum);
                if (result)
                {
                    return RedirectToAction("ConfirmCheckout");
                }
                
            }

            return View();
            
        }

        public ActionResult ConfirmCheckout()
        {
            Boolean result;

            result = _userService.ConfirmCheckout();
            if (result)
            {
                _messageViewService.SuccessfulCheckout();
            }
            else
            {
                _messageViewService.UnsuccessfulCheckout();
            }

            return RedirectToAction("ViewCart");
        }
    }
}