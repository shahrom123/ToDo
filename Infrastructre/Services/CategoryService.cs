using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructre.Services
{
    public class CategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetCategory()
        {
            try
            {
                var result = _context.Categories.ToList();
                var mapped = _mapper.Map<List<CategoryDto>>(result);
                return new Response<List<CategoryDto>>(mapped);
            }
            catch (Exception ex) 
            {
                return new Response<List<CategoryDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<CategoryDto>> Add(CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return new Response<CategoryDto>(categoryDto);
            }
            catch (Exception e)
            {
                return new Response<CategoryDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<CategoryDto>> Update(CategoryDto categoryDto)
        {
            try
            {
                var existing = await _context.Categories.Where(x => x.Id == categoryDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existing == null) return new Response<CategoryDto>(HttpStatusCode.BadRequest, new List<string>() { "Category not Found" });

                var mapped = _mapper.Map<Category>(categoryDto);
                _context.Categories.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<CategoryDto>(categoryDto);
            }
            catch (Exception ex)
            {
                return new Response<CategoryDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Id Not Found" });

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
