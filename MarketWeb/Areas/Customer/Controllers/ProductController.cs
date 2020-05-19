using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using MarketWeb.Services;
using MarketWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductController(IProductRepository productRepository
                                 , ICategoryRepository categoryRepository
                                 , IBrandRepository brandRepository)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._brandRepository = brandRepository;
        }
        // GET: /<controller>/
        public async Task<IActionResult> List(ProductResourceParameters RP)
        {
            IEnumerable<Product> products;
            string title;


            if (!string.IsNullOrEmpty(RP.Category))
            {

                products = await _productRepository.Search(RP.Category, RP.Brand, RP.SearchQuery);
                var category= await _categoryRepository.GetCategoryByName(RP.Category);
                title = category.Name;

            }
            //else if (!string.IsNullOrEmpty(RP.Brand))
            //{
            //    products =await _productRepository.Search(RP.Category, RP.Brand, RP.SearchQuery);
            //    var brand=await _brandRepository.GetBrandByName(RP.Brand);
            //    title = brand.BrandName;
            //}
            else if (!string.IsNullOrEmpty(RP.SearchQuery))
            {
                products =await _productRepository.Search(RP.Category, RP.Brand, RP.SearchQuery);
                title = RP.SearchQuery;
            }
            else
            {
                products = _productRepository.AllProduct.OrderBy(p => p.Id);
                title = "All Products";
            }
            var productsListViewModel = new ProductsListViewModel
            {
                Products = products,
                CurrentCategory = title
            };
            return View(productsListViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product =await _productRepository.GetProductById(id);
            if (product == null)
                return View("NotFound");

            return View(new ProductDetailViewModel() { Product = product });
        }
    }
}
