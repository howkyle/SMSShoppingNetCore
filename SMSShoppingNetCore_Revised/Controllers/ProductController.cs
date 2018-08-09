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

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        // GET: Product

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.sessionUser = User.Identity.Name;
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
                TempData["message"] = "Item added to cart";
                TempData["message-status"] = "success";
            }
            else
            {
                TempData["message"] = "Item not added to cart";
                TempData["message-status"] = "error";
            }
            
            return RedirectToAction("Index");
        }


        public ActionResult RemoveFromCart(int id)
        {
            
            _userService.RemoveFromCart(id);

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
                    TempData["message"] = "Successful Checkout";
                    TempData["message-status"] = "success";
                }
                else
                {
                    TempData["message"] = "Error Checking out";
                    TempData["message-status"] = "error";
                }
                return RedirectToAction("ConfirmCheckout");
            }

            return View();
            
        }

        public ActionResult ConfirmCheckout()
        {
            
            _userService.ConfirmCheckout();
            return RedirectToAction("ViewCart");
        }
    }
}