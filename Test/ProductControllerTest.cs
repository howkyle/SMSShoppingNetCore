using BLL.Services;
using DAL.Models;
using SMSShoppingNetCore_Revised.Controllers;
using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SMSShoppingNetCore_Revised.Models;

namespace Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productService;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IMessageViewService> _messageService;
        int productID = 1;


        /// <summary>
        /// constructor
        /// </summary>
        public ProductControllerTest()
        {
            _productService = new Mock<IProductService>();
            _userService = new Mock<IUserService>();
            _messageService = new Mock<IMessageViewService>();
            

        }


        /// <summary>
        /// The details action should return a view result with no model data if a product was not retrieved from the database
        /// </summary>
        [Fact]
        public void ShouldReturnDetailsViewResultWithNoViewModelData()
        {
            int productID = 1;

            _productService.Setup(service => service.GetProduct(productID)).Returns(null as Product);
            ProductController controller = new ProductController(_productService.Object, userService: null, messageViewService: null);
            var result = controller.Details(productID);
            //assert that an view result is returned
            var actionResult = Assert.IsType<ViewResult>(result);
            //Assert that no data is present in the model
            Assert.Null(actionResult.Model);

        }

        /// <summary>
        /// The details action should return a view result with product data if a prodcut was retrieved from the database
        /// </summary>
        [Fact]
        public void ShouldReturnDetailsViewResultWithProductViewModelData()
        {
           

            _productService.Setup(service => service.GetProduct(productID)).Returns(new Product { Id = 1, ProductName = "Test Product 1", ProductPrice = 500 });
            ProductController controller = new ProductController(_productService.Object, userService: null, messageViewService: null);
            var result = controller.Details(productID);
            //assert that a view result is returned
            var actionResult = Assert.IsType<ViewResult>(result);
            //assert that the model type is Product
            Assert.IsType<Product>(actionResult.Model);
            //assert that the view model contains data
            Assert.NotNull(actionResult.Model);

        }


        /// <summary>
        /// Verifies that when a product is successfully added to a cart a the success message function is called by the message service
        /// </summary>
        [Fact]
        public void ShouldDisplaySuccessAddingToCart()
        {
            //service method mocked to return true
            _userService.Setup(service => service.AddToCart(productID)).Returns(true);
            _messageService.Setup(service => service.ItemAddedSuccess()).Verifiable();
            _messageService.Setup(service => service.ItemAddedError()).Verifiable();
            ProductController controller = new ProductController(productService:null, userService:_userService.Object, messageViewService: _messageService.Object);
            var result = controller.AddToCart(1);
            //Assert.IsType<Boolean>(result);
            _messageService.Verify(service => service.ItemAddedSuccess(), Times.Once());
            _messageService.Verify(service => service.ItemAddedError(), Times.Never());
            //verifies that the user gets redirected to the index action
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);

        }

        /// <summary>
        /// Verifies that when a product is not successfully added to a cart a the error message function is called by the message service
        /// </summary>
        [Fact]
        public void ShouldDisplayErrorAddingToCart()
        {
            //service method mocked to return false
            _userService.Setup(service => service.AddToCart(productID)).Returns(false);
            _messageService.Setup(service => service.ItemAddedSuccess()).Verifiable();
            _messageService.Setup(service => service.ItemAddedError()).Verifiable();
            ProductController controller = new ProductController(productService: null, userService: _userService.Object, messageViewService: _messageService.Object);
            var result = controller.AddToCart(1);
            //Assert.IsType<Boolean>(result);
            _messageService.Verify(service => service.ItemAddedError(), Times.Once());
            _messageService.Verify(service => service.ItemAddedSuccess(), Times.Never());
            //verifies that the user gets redirected to the index action
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);

        }

        /// <summary>
        /// Verifies that when a product is not successfully removed from a cart a the error message function is called by the message service
        /// </summary>
        [Fact]
        public void ShouldDisplaySuccessRemovingFromCart()
        {
            //service method mocked to return false
            _userService.Setup(service => service.RemoveFromCart(productID)).Returns(true);
            _messageService.Setup(service => service.ItemRemovedSuccess()).Verifiable();
            _messageService.Setup(service => service.ItemRemovedError()).Verifiable();
            ProductController controller = new ProductController(productService: null, userService: _userService.Object, messageViewService: _messageService.Object);
            var result = controller.RemoveFromCart(1);
            //Assert.IsType<Boolean>(result);
            _messageService.Verify(service => service.ItemRemovedSuccess(), Times.Once());
            _messageService.Verify(service => service.ItemRemovedError(), Times.Never());
            //verifies that the user gets redirected to the viewcart action
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewCart", actionResult.ActionName);

        }

        /// <summary>
        /// Verifies that when a product is not successfully removed from a cart a the error message function is called by the message service
        /// </summary>
        [Fact]
        public void ShouldDisplayErrorRemovingFromCart()
        {
            //service method mocked to return false
            _userService.Setup(service => service.RemoveFromCart(productID)).Returns(false);
            _messageService.Setup(service => service.ItemRemovedSuccess()).Verifiable();
            _messageService.Setup(service => service.ItemRemovedError()).Verifiable();
            ProductController controller = new ProductController(productService: null, userService: _userService.Object, messageViewService: _messageService.Object);
            var result = controller.RemoveFromCart(1);
            //Assert.IsType<Boolean>(result);
            _messageService.Verify(service => service.ItemRemovedError(), Times.Once());
            _messageService.Verify(service => service.ItemRemovedSuccess(), Times.Never());
            //verifies that the user gets redirected to the viewcart action
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewCart", actionResult.ActionName);

        }

        /// <summary>
        /// When the model state is valid and the checkout method is successful then the user should be redirected to the confirm checkout method
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ShouldRedirectToConfirmCheckoutAsync()
        {
            CheckOutViewModel vm = new CheckOutViewModel{
                //valid credit card number
                CreditCardNum = "4111111111111111"
            };

            _userService.Setup(service => service.CheckoutAsync(vm.CreditCardNum)).ReturnsAsync(true);
            ProductController controller = new ProductController(productService: null, userService: _userService.Object, messageViewService: null);
            
            var result = await controller.Checkout(vm);
            //verify that the model state is valid
            Assert.True(controller.ModelState.IsValid);

            //verify that a redirect action takes place when checkout successfully occurs
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            //verify that the user is redirected to the confirm checkout action 
            Assert.Equal("ConfirmCheckout", actionResult.ActionName);
        }

        [Theory]
        [InlineData(1)]
        public void ShouldReturnAProduct(int productID)
        {
            //var product = _productService.GetProduct(productID);
            //Assert.NotNull(product);
            //Assert.IsType<Product>(product);

        }
    }
}
