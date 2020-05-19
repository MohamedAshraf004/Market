using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProduct { get; }
        IEnumerable<Product> ProductOfTheWeek { get; }

        Task CreateProduct(Product product);
        Task<Product> DeleteProduct(int productId, string path);
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> Search(string Category, string Brand, string SearchQuery);
        Task UpdateProduct(Product product);

    }
}
