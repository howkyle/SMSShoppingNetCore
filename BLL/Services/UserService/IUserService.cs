using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

public interface IUserService
{
    string GetCurrentUserName();
    /// <summary>
    /// Searches for a user using the email address
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <returns>Task<IdentityUser></Identity></returns>
    Task<User> GetUser(String emailAddress);
    /// <summary>
    /// Gets the cart of the current logged in user
    /// </summary>
    /// <returns> Cart </returns>
    Cart GetCart();
    /// <summary>
    /// Registers a user with email and password combination 
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <param name="passwordConfirmation"></param>
    /// <returns>IdentityResult</returns>
    Task<IdentityResult> SignUp(String firstName, String lastName, String emailAddress, String password, String passwordConfirmation);
    /// <summary>
    /// Signs in a user with the given email address and password combination
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<SignInResult> LogIn(String emailAddress, String password);
    /// <summary>
    /// Logs the current user out of the application 
    /// </summary>
    /// <returns></returns>
    void LogOut();
    /// <summary>
    /// Adds a selected item to the current users cart
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    Boolean AddToCart(int productID);
    /// <summary>
    /// Removes an item from the current users cart
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    Boolean RemoveFromCart(int itemID);
    /// <summary>
    /// Clears a users cart
    /// </summary>
    /// <param name="creditCardNumber"></param>
    /// <returns></returns>
   Task< Boolean> CheckoutAsync(String creditCardNumber);
    void ConfirmCheckout();
}