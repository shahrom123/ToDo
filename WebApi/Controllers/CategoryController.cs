using Domain.Dto;
using Domain.Response;
using Infrastructre.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetCategory")]
        public async Task<Response<List<CategoryDto>>> Get()
        {
            return await _categoryService.GetCategory();

        }
        [HttpPost("AddCategory")]
        public async Task<Response<CategoryDto>> Add(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                return await _categoryService.Add(categoryDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<CategoryDto>(HttpStatusCode.BadRequest, errors);
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<Response<CategoryDto>> Put([FromBody] CategoryDto categoryDto) => await _categoryService.Update(categoryDto);

        [HttpDelete("DeleteCategory")]
        public async Task<Response<string>> Delete(int id) => await _categoryService.Delete(id);
    }

}
