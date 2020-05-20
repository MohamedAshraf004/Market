using Core.Models;
using MarketWeb.Data;
using MarketWeb.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketWeb.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(ApplicationDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = await _shoppingCart.GetTotal();

            order.OrderDetails = new List<OrderDetail>();
            //adding the order with its details

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.Id,
                    Price = shoppingCartItem.Product.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            await _appDbContext.Orders.AddAsync(order);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
