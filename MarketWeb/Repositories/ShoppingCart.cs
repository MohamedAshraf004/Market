using Core.Models;
using MarketWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Repositories
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _appDbContext;

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public async Task AddToCart(Product product, int amount = 1)
        {
            var shoppingCartItem =await _appDbContext.ShoppingCartItems
                .FirstOrDefaultAsync(c => c.Product.Id== product.Id && c.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Product = product,
                    ShoppingCartId = ShoppingCartId,
                    Amount = amount
                };
               await _appDbContext.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
           await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCart(Product product)
        {
            var shoppingCartItem =await _appDbContext.ShoppingCartItems.SingleOrDefaultAsync(
                        s => s.Product.Id== product.Id && s.ShoppingCartId == ShoppingCartId);
            int localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            await _appDbContext.SaveChangesAsync();

            return localAmount;
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                  (ShoppingCartItems =
                      await _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                          .Include(s => s.Product)
                          .ToListAsync());
        }

        public async Task ClearCart()
        {
            var cartItems = await _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).ToListAsync();
            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<decimal> GetTotal()
        {
            var total =await _appDbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
                            .Select(p => p.Amount * p.Product.Price).SumAsync();
            return total;
        }
    }
}
