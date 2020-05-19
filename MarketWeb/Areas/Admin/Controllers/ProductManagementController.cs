using MarketWeb.Models;
using MarketWeb.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace MarketWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductManagementController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUploadFileRepository uploadFile;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductManagementController(IProductRepository productRepository
                                            , ICategoryRepository categoryRepository
                                            , IBrandRepository brandRepository,
                                            IUploadFileRepository uploadFile
                                            , IWebHostEnvironment hostingEnvironment)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._brandRepository = brandRepository;
            this.uploadFile = uploadFile;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var products = _productRepository.AllProduct.OrderBy(p => p.Name);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var categories = await _categoryRepository.AllCategories();

            ProductEditViewModel productEditViewModel = new ProductEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList(),
                CategoryId = categories.FirstOrDefault().Id

            };
            return View(productEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductEditViewModel productEditViewModel)
        {

            if (ModelState.IsValid)
            {
                productEditViewModel.Product.Image = uploadFile.ProcessUploadedFile(productEditViewModel.Photo, "Products"); ;
                productEditViewModel.Product.CategoryId = productEditViewModel.CategoryId;

                await _productRepository.CreateProduct(productEditViewModel.Product);
                return RedirectToAction("Index");
            }
            else
            {
                var categories = _categoryRepository.AllCategories();

                productEditViewModel = new ProductEditViewModel
                {
                    Categories = categories.GetAwaiter().GetResult().Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList(),
                    CategoryId = categories.GetAwaiter().GetResult().Select(c => c.Id).FirstOrDefault()
                };
            }
            return View(productEditViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct([FromRoute]int Id)
        {
            var categories = await _categoryRepository.AllCategories();

            var Product = _productRepository.AllProduct.FirstOrDefault(p => p.Id == Id);

            var ProductEditViewModel = new ProductEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList(),
                Product = Product,
                CategoryId = Product.CategoryId,

            };
            var item = ProductEditViewModel.Categories.FirstOrDefault(c => c.Value == Product.Category.Id.ToString());
            item.Selected = true;
            return View(ProductEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductEditViewModel ProductEditViewModel)
        {
            ProductEditViewModel.Product.CategoryId = ProductEditViewModel.CategoryId;
            if (ModelState.GetValidationState("Product.Price") == ModelValidationState.Valid && ProductEditViewModel.Product.Price < 0)
            {
                ModelState.AddModelError(nameof(ProductEditViewModel.Product.Price), "invalid price");
            }
            if (ModelState.IsValid)
            {
                if (ProductEditViewModel.Photo != null)
                {
                    ProductEditViewModel.Product.Image = uploadFile.ProcessUploadedFile(ProductEditViewModel.Photo, "Products");
                }
                ProductEditViewModel.Product.CategoryId = ProductEditViewModel.CategoryId;

                await _productRepository.UpdateProduct(ProductEditViewModel.Product);
                return RedirectToAction("Index");
            }

            var categories = await _categoryRepository.AllCategories();

            ProductEditViewModel = new ProductEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }).ToList(),
                CategoryId = ProductEditViewModel.Product.CategoryId
            };
            var item = ProductEditViewModel.Categories.FirstOrDefault(c => c.Value == ProductEditViewModel.CategoryId.ToString());
            item.Selected = true;
            return View(ProductEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {

            await _productRepository.DeleteProduct(ProductId, hostingEnvironment.WebRootPath);

            return RedirectToAction(nameof(Index));
        }
    }
}
