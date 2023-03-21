using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nlayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace Nlayer.API.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IProductService service;

        public ProductsController(IMapper mapper, IProductService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await service.GetAllAsync();
            var productDtos = mapper.Map<List<ProductDto>>(products.ToList());

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await service.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productRequest)
        {
            var product = await service.AddAsync(mapper.Map<Product>(productRequest));
            var productDto = mapper.Map<ProductDto> (product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productRequest)
        {
            await service.UpdateAsync(mapper.Map<Product>(productRequest));

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await service.GetByIdAsync(id);
            await service.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            var exist = await service.AnyAsync(x => x.Id == id);

            if (exist)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }

            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, new List<string> { "No Record" }));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveRange(List<ProductDto> productRequest)
        {
            var products = await service.AddRangeAsync(mapper.Map<List<Product>>(productRequest));
            var productsDto = mapper.Map<List<ProductDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveRange(List<int> productIds)
        {
            var products = new List<Product>();

            foreach (var productId in productIds)
            {
                var product = await service.GetByIdAsync(productId);
                products.Add(product);
            }

            await service.RemoveRangeAsync(products);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await service.GetProductsWithCategory());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithTags()
        {
            return CreateActionResult(await service.GetProductsWithTags());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithTags(int productId)
        {
            return CreateActionResult(await service.GetProductWithTags(productId));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> AddTagToProduct(int productId, int tagId)
        {
            return CreateActionResult(await service.AddTagToProduct(productId, tagId));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveTagFromProduct(int productId, int tagId)
        {
            return CreateActionResult(await service.RemoveTagFromProduct(productId, tagId));
        }
    }
}
