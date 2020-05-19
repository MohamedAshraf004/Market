using Core.Models;
using MarketWeb.Data;
using MarketWeb.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> AllCategories() =>await _dbContext.Categories.ToListAsync();

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Name==name);
        }
    }
}
