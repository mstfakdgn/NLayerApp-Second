using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, CategoryId = 1, Name = "Kalem 1", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 2, CategoryId = 1, Name = "Kalem 2", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 3, CategoryId = 1, Name = "Kalem 3", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 4, CategoryId = 1, Name = "Kalem 4", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 5, CategoryId = 2, Name = "Kitap 1", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 6, CategoryId = 2, Name = "Kitap 2", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 7, CategoryId = 2, Name = "Kitap 3", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 8, CategoryId = 2, Name = "Kitap 4", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 9, CategoryId = 2, Name = "Defter 4", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 10, CategoryId = 2, Name = "Defter 4", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 11, CategoryId = 2, Name = "Defter 4", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 12, CategoryId = 2, Name = "Defter 4", Price = 100, Stock = 20, CreatedDate = DateTime.Now }
                );
        }
    }
}
