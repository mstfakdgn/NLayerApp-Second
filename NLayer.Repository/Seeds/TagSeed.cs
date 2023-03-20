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
    public class TagSeed : IEntityTypeConfiguration<Tag>
    {
        void IEntityTypeConfiguration<Tag>.Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(new Tag { Id = 1, Name = "Tag1", CreatedDate = DateTime.Now },
                new Tag { Id = 2, Name = "Tag2", CreatedDate = DateTime.Now },
                new Tag { Id = 3, Name = "Tag3", CreatedDate = DateTime.Now },
                new Tag { Id = 4, Name = "Tag4", CreatedDate = DateTime.Now },
                new Tag { Id = 5, Name = "Tag5", CreatedDate = DateTime.Now },
                new Tag { Id = 6, Name = "Tag6", CreatedDate = DateTime.Now },
                new Tag { Id = 7, Name = "Tag7", CreatedDate = DateTime.Now },
                new Tag { Id = 8, Name = "Tag8", CreatedDate = DateTime.Now },
                new Tag { Id = 9, Name = "Tag9", CreatedDate = DateTime.Now },
                new Tag { Id = 10, Name = "Tag10", CreatedDate = DateTime.Now }
                );
        }
    }
}
