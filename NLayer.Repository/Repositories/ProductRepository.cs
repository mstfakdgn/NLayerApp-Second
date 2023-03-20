using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<List<Product>> GetProductsWithTags()
        {
            return await _context.Products.Include(x => x.ProductTags).ToListAsync();
        }

        public async Task AddTagToProduct(ProductTag productTag)
        {
            await _context.ProductTags.AddAsync(productTag);
        }

        public async Task RemoveTagFromProduct(ProductTag productTag)
        {
            _context.ProductTags.Remove(productTag);
        }

        public async Task<Product> GetProductWithTags(int productId)
        {
            return await _context.Products.Include(x => x.ProductTags).FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
