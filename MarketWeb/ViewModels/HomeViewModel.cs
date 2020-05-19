using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> ProductsOfTheWeak { get; set; }
    }
}
