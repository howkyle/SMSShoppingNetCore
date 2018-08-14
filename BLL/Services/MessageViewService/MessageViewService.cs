using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class MessageViewService : IMessageViewService
    {
        private string _status;
        private string _message;
        private readonly IHttpContextAccessor _contextAccessor;
        private ITempDataDictionaryFactory _factory;
        private ITempDataDictionary _tempData;
        

        public  MessageViewService(IHttpContextAccessor contextAccessor)
        {
            //_controller = New Con;
            _contextAccessor = contextAccessor;
            _factory = _contextAccessor.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
            _tempData = _factory.GetTempData(_contextAccessor.HttpContext);
        }

        public string GetMessage()
        {
            //var message = _tempData["message"];
            if (_tempData["message"] != null)
            {
                return _tempData["message"].ToString();
            }
            else
            {
                return null;
            }
            
        }

        public string GetStatus()
        {
            var status = _tempData["message-status"];
            if (status != null)
            {
                return status.ToString();
            }
            else
            {
                return null;
            }
        }


        public void ItemAddedSuccess()
        {
            
            _tempData["message"] = "Item added to cart";
            _tempData["message-status"] = "success";
        }

        public void ItemAddedError()
        {
            _tempData["message"] = "Item not added to cart";
            _tempData["message-status"] = "error";
        }

        public void IncorrectLoginDetails()
        {
            _tempData["message"] = "Incorrect username or password";
            _tempData["message-status"] = "error";  
        }

        public void SuccessfulCheckout()
        {
            _tempData["message"] = "Successful Checkout";
            _tempData["message-status"] = "success";
        }

        public void UnsuccessfulCheckout()
        {
            _tempData["message"] = "Error Checking out";
            _tempData["message-status"] = "error";
        }

        public void SuccessfulRegistration()
        {
            _tempData["message"] = "Registration successful, proceed to login";
            _tempData["message-status"] = "success";
        }

        public void UnsuccesfullRegistration()
        {
            _tempData["message"] = "An error occurred during registration please try again";
            _tempData["message-status"] = "error";
        }
    }

    
}
