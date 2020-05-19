using MarketWeb.Models;
using MarketWeb.Services;
using MarketWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MarketWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository ProductRepository, ILogger<HomeController> logger)
        {
            this._productRepository = ProductRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                ProductsOfTheWeak = _productRepository.ProductOfTheWeek
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
