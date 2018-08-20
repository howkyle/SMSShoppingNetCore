using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; 

        public UserService(IHttpContextAccessor httpContextAccessor, ApplicationDBContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = httpContextAccessor;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        //QUERIES
        public string GetCurrentUserName()
        {
           //returns the current users username
            string userIdString;
            userIdString = _context.HttpContext.User.Identity.Name;

            return userIdString;
        }

        //fix access to session

        
        public async Task<User> GetUser(String emailAddress)
        {
            User user;
            try
            {
                //asynchronously retrieves the user from the database
                user = await _userManager.FindByEmailAsync(emailAddress);
            }
            catch
            {
                user = null;
            }
            return user;
        }

        public Cart GetCart()
        {
            var sessionUser = GetCurrentUserName();
            var user = _db.Users.Include(u => u.Cart).ThenInclude(c => c.Items).ThenInclude(i => i.Product).Single(u => u.UserName == sessionUser);
            var cart = user.Cart;
            return cart;
        }

        //COMMANDS

        //public void SetCurrentUser(String userName)
        //{
        //    byte[] sessionUser = Encoding.UTF8.GetBytes(userName);
        //    _context.HttpContext.Session.Set("user", sessionUser);

        //}

        public async Task<IdentityResult> SignUp(String firstName, String lastName, String emailAddress, String password, String passwordConfirmation)
        {
            IdentityResult result = IdentityResult.Failed();
            User user = null;

            //user = await GetUser(emailAddress);
            if (user == null)
            {
                //if no user exists with that email address proceed to create user;
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = emailAddress,
                    UserName = emailAddress,
                    Cart = new Cart()
                };
                if (password == passwordConfirmation)
                {
                    
                  result =  await _userManager.CreateAsync(user, password);
                    
                }
            }

            return result;
        }

        public async Task<SignInResult> LogIn(string emailAddress, string password)
        {
            SignInResult result = null;
            var user =  await _userManager.FindByEmailAsync(emailAddress);
            if (user != null)
            {
                result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            }
            else
            {
                result = SignInResult.Failed;
            }
            return result;
           
        }

        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public Boolean AddToCart(int productID)
        {
            try
            {
                var sessionUser = GetCurrentUserName();
                var user = _db.Users.Include(u => u.Cart).Single(u => u.UserName == sessionUser);
                //gets the users cart
                var cart = user.Cart;
                //gets the product to be added to the cart
                var product = _db.Products.Single(p => p.Id == productID);
                CartItem cartItem;

                //checks if an item set for the product already exists in the cart
                try
                {
                    cartItem = _db.CartItems.Single(c => c.ProductID == productID && c.CartID == cart.Id);
                    cartItem.Quantity++;
                    cartItem.Price = cartItem.Price+product.ProductPrice;
                }
                catch
                {
                    //if an item has not been found
                    //creates a new cart Item using the product

                    cartItem = new CartItem()
                    {
                        Cart = cart,
                        Product = product,
                        Quantity = 1,
                        Price = product.ProductPrice
                    };
                    
                    _db.CartItems.Add(cartItem);

                }


                ////if an itemset is found
                //if (cartItem != null)
                //{
                //    cartItem.Quantity++;
                //}
                //else
                //{
                //    //if an item has not been found
                //    //creates a new cart Item using the product

                //    cartItem = new CartItem()
                //    {
                //        Cart = cart,
                //        Product = product,
                //        Quantity = 1
                //    };
                //    _db.CartItems.Add(cartItem);
                //}

                cart.Count++;
                cart.Value += product.ProductPrice;

                
                _db.SaveChanges();

            }
            catch
            {
                return false;
            }

            return true;
            
        }
        public Boolean RemoveFromCart(int itemID)
        {
            Boolean result;

            try{
                
                var cartItem = _db.CartItems.Include(c => c.Product).Include(c=>c.Cart).Single(c => c.Id == itemID);
                var cart = cartItem.Cart;
                var product = cartItem.Product;

                if(cartItem.Quantity == 1)
                {
                    //if its the last item in the cart then remove the itemset
                    _db.CartItems.Remove(cartItem);
                }
                else
                {
                    //reduce the quantity of that item by 1 and price by the product price
                    cartItem.Quantity--;
                    cartItem.Price = cartItem.Price - product.ProductPrice;
                }
                
                //reduce the total number of items in the cart by 1
                cart.Count-- ;
                //reduce the value of the cart by the value of the removed item
                cart.Value -= product.ProductPrice;
                //_db.CartItems.Remove(cartItem);
                _db.SaveChanges();
                result = true;

            }
            catch
            {
                result = false;
            }

            return result;        
        }

        

        public async Task<bool> CheckoutAsync(String creditCardNum)
        {
            //gets credit card details and does something then redirects to checkout confirm
            //var creditCardNum = col["creditCardNum"];
            Boolean result;
            try
            {
                var sessionUser = GetCurrentUserName();
                var user = await _userManager.FindByNameAsync(sessionUser);
                user.CardNumber = creditCardNum;
                _db.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public Boolean ConfirmCheckout()
        {
            Boolean result;
            try
            {
                var sessionUser = GetCurrentUserName();
                var user = _db.Users.Include(u => u.Cart).ThenInclude(c => c.Items).Single(u => u.UserName == sessionUser);
                var cartItems = user.Cart.Items;
                user.Cart.Value = 0;
                user.Cart.Count = 0;
                cartItems.RemoveAll(x => true);
                _db.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
            
        }

        //public bool SignUp(string firstName, string lastName, string emailAddress, string password, string passwordConfirmation)
        //{
        //    throw new NotImplementedException();
        //}

       
        //end of class
    }
}
