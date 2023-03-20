using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductWithTagsDto :ProductDto
    { 
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
