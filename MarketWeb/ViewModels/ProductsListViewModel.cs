using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products{ get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentBrand { get; set; }
        public string SearchQuery { get; set; }
    }
}
