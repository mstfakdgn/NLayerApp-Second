﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetCategoriesWithProduct()
        {
            return await _context.Categories.Include(x => x.Products).ToListAsync();
        }

        public async Task<Category> GetCategoryWithProduct(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
