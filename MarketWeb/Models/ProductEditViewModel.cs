using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Models
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public IFormFile Photo { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }

    }
}
