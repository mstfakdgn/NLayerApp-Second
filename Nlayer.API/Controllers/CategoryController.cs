using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace Nlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IMapper mapper;
        private readonly ICategoryService service;

        public CategoryController(IMapper mapper, ICategoryService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await service.GetAllAsync();
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories.ToList());

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await service.GetByIdAsync(id);
            var categoryDto = mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto requestCategory)
        {
            var category = await service.AddAsync(mapper.Map<Category>(requestCategory));

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, mapper.Map<CategoryDto>(category)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto requestCategory)
        {
            await service.UpdateAsync(mapper.Map<Category>(requestCategory));

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, requestCategory));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await service.GetByIdAsync(id);

            await service.RemoveAsync(category);

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
        public async Task<IActionResult> AddRange(List<CategoryDto> requestCategories)
        {
            var categories = await service.AddRangeAsync(mapper.Map<List<Category>>(requestCategories));
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoryDtos));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveRange(List<int> categoryIds)
        {
            var categories  = new List<Category>();

            foreach (int categoryId in categoryIds)
            {
                var category = await service.GetByIdAsync(categoryId);
                categories.Add(category);
            }

            await service.RemoveRangeAsync(categories);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoriesWithProducts()
        {
            return CreateActionResult(await service.GetCategoriesWithProduct());
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoryWithProduct(int categoryId)
        {
            return CreateActionResult(await service.GetCategoryWithProduct(categoryId));
        }
    }
}
