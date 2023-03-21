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
        private readonly IGenericRepository<Tag> _tagReporsitory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository, IGenericRepository<Tag> tagReporsitory) : base(genericRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = productRepository;
            _tagReporsitory = tagReporsitory;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var productsWithCategory = await _repository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(productsWithCategory);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);

        }

        public async Task<CustomResponseDto<List<ProductWithTagsDto>>> GetProductsWithTags()
        {
            var productsWithTags = await _repository.GetProductsWithTags();

            var productOutput = new List<ProductWithTagsDto>();
            foreach (var productDto in productsWithTags)
            {
                var tagDtos = new List<TagDto>();
                foreach (var productTag in productDto.ProductTags)
                {
                    var tag = await _tagReporsitory.GetByIdAsync(productTag.TagId);
                    tagDtos.Add(_mapper.Map<TagDto>(tag));
                }

                var productWithTags = _mapper.Map<ProductWithTagsDto>(productDto);
                productWithTags.Tags = tagDtos;
                productOutput.Add(productWithTags);
            }

            return CustomResponseDto<List<ProductWithTagsDto>>.Success(200, productOutput);
        }

        public async Task<CustomResponseDto<ProductWithTagsDto>> GetProductWithTags(int productId)
        {
            var product = await _repository.GetProductWithTags(productId);

            var tagDtos = new List<TagDto>();
            foreach (var productTag in product.ProductTags)
            {
                var tag = await _tagReporsitory.GetByIdAsync(productTag.TagId);
                tagDtos.Add(_mapper.Map<TagDto>(tag));
            }

            var productWithTags = _mapper.Map<ProductWithTagsDto>(product);
            productWithTags.Tags = tagDtos;

            return CustomResponseDto<ProductWithTagsDto>.Success(200, productWithTags);
        }

        public async Task<CustomResponseDto<NoContentDto>> AddTagToProduct(int productId, int tagId)
        {
            var product = await _repository.GetByIdAsync(productId);
            var tag = await _tagReporsitory.GetByIdAsync(tagId);

            var productTag = new ProductTag
            {
                ProductId = productId,
                TagId = tagId,
                Tag = tag,
                Product = product
            };

            if (product == null || tag == null)
            {
                throw new Exception("Product or tag could not be found");
            }

            await _repository.AddTagToProduct(productTag);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(201);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveTagFromProduct(int productId, int tagId)
        {
            var productTag = new ProductTag
            {
                ProductId = productId,
                TagId = tagId
            };

            await _repository.RemoveTagFromProduct(productTag);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(201);
        }
    } 
}
