using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IGenericRepository<Category> genericRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CategoryWithProductsDto>>> GetCategoriesWithProduct()
        {
            var categories = await _categoryRepository.GetCategoriesWithProduct();
            var categoryDtos = _mapper.Map<List<CategoryWithProductsDto>>(categories);

            return CustomResponseDto<List<CategoryWithProductsDto>>.Success(200, categoryDtos);
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetCategoryWithProduct(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryWithProduct(categoryId);
            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);

            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);

        }
    }
}
