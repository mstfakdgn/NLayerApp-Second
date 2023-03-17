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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(genericRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = productRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var productsWithCategory = await _repository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(productsWithCategory);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);

        }
    }
}
