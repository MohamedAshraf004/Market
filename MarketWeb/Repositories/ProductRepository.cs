using Core.Models;
using MarketWeb.Data;
using MarketWeb.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IEnumerable<Product> AllProduct => _db.Products;
        public IEnumerable<Product> ProductOfTheWeek => _db.Products/*.Include(c => c.Brand)*/
                        .Include(c => c.Category).Where(p => p.IsProductOfTheWeek && p.Quantity > 0).ToList();


        public async Task<IEnumerable<Product>> Search(string Category, string Brand, string SearchQuery)
        {
            if (Category == null && Brand == null && SearchQuery == null)
            {
                throw new ArgumentNullException("Resouce Parmater Error");
            }
            var collection = _db.Products as IQueryable<Product>;
            if (!string.IsNullOrWhiteSpace(Category))
            {
                var mainCategory = Category.Trim();
                collection = collection.Where(a => a.Category.Name == mainCategory);
            }
            //if (!string.IsNullOrWhiteSpace(Brand))
            //{
            //    var mainBrand = Brand.Trim();
            //    collection = collection.Where(a => a.Brand.BrandName == mainBrand);
            //}
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var searchQuery = SearchQuery.Trim();
                collection = collection.Where(c => c.Category.Name.ToLower().Contains(searchQuery.ToLower())
                //|| c.Brand.BrandName.ToLower().Contains(searchQuery.ToLower())
                || c.Name.ToLower().Contains(searchQuery.ToLower()));
            }
            return await collection.ToListAsync();
        }


        public async Task UpdateProduct(Product product)
        {
            var oldProduct = _db.Products.Attach(product);
            oldProduct.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task CreateProduct(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product> DeleteProduct(int productId, string path)
        {
            var deletedProduct = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (deletedProduct.Image != null)
            {
                string uploadsFolder = Path.Combine(path, "images");
                string filePath = Path.Combine(uploadsFolder, deletedProduct.Image);
                File.Delete(filePath);
            }
            _db.Products.Remove(deletedProduct);
            await _db.SaveChangesAsync();
            return deletedProduct;
        }
    }
}
