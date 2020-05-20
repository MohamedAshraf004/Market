using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketWeb.Repositories;
using MarketWeb.Services;
using MarketWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace MarketWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal =await _shoppingCart.GetTotal()
            };
            return View(shoppingCartViewModel);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.AllProduct.FirstOrDefault(p => p.Id == productId);

            if (selectedProduct != null)
            {
               await _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }
        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.AllProduct.FirstOrDefault(p => p.Id == productId);

            if (selectedProduct != null)
            {
                await _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}
