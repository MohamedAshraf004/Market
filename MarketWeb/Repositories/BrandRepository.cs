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
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Brand>> AllBrands() => await _dbContext.Brands.ToListAsync();
        public async Task<Brand> GetBrandById(int id) => await _dbContext.Brands.FindAsync(id);

    }
}
