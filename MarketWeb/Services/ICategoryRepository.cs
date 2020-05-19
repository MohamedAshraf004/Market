using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Services
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> AllCategories();
        Task<Category> GetCategoryByName(string name);

    }
}
