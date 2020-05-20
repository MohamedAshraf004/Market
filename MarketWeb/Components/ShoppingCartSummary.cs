using MarketWeb.Repositories;
using MarketWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            this._shoppingCart = shoppingCart;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items =await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal =await _shoppingCart.GetTotal()
            };
            return View(shoppingCartViewModel);
        }
    }
}
