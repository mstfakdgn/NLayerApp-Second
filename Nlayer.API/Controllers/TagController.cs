using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace Nlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IService<Tag> service;

        public TagController(IMapper mapper, IService<Tag> service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var tags = await service.GetAllAsync();
            var tagDtos = mapper.Map<List<TagDto>>(tags.ToList());

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
            return CreateActionResult(CustomResponseDto<List<TagDto>>.Success(200, tagDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await service.GetByIdAsync(id);
            var tagDto = mapper.Map<TagDto>(tag);

            return CreateActionResult(CustomResponseDto<TagDto>.Success(200, tagDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(TagDto requestTag)
        {
            var tag = await service.AddAsync(mapper.Map<Tag>(requestTag));

            return CreateActionResult(CustomResponseDto<TagDto>.Success(201, mapper.Map<TagDto>(tag)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TagDto requestTag)
        {
            await service.UpdateAsync(mapper.Map<Tag>(requestTag));

            return CreateActionResult(CustomResponseDto<TagDto>.Success(200, requestTag));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await service.GetByIdAsync(id);

            await service.RemoveAsync(tag);

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
        public async Task<IActionResult> AddRange(List<TagDto> requestTags)
        {
            var tags = await service.AddRangeAsync(mapper.Map<List<Tag>>(requestTags));
            var tagDtos = mapper.Map<List<TagDto>>(tags);

            return CreateActionResult(CustomResponseDto<List<TagDto>>.Success(200, tagDtos));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveRange(List<int> tagIds)
        {
            var tags = new List<Tag>();

            foreach (int tagId in tagIds)
            {
                var tag = await service.GetByIdAsync(tagId);
                tags.Add(tag);
            }

            await service.RemoveRangeAsync(tags);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
