using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace Infrastructre.Services
{
    public class CommentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CommentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }  
        public async Task<Response<List<CommentDto>>> GetComment()
        {
            try
            {
                var result = _context.Comments.ToList();
                var mapped = _mapper.Map<List<CommentDto>>(result);
                return new Response<List<CommentDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<CommentDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<CommentDto>> Add(CommentDto commentDto)
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDto);
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return new Response<CommentDto>(commentDto);
            }
            catch (Exception e)
            {
                return new Response<CommentDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<CommentDto>> Update(CommentDto commentDto)
        {
            try
            {
                var existing = await _context.Comments.Where(x => x.Id == commentDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (existing == null) return new Response<CommentDto>(HttpStatusCode.BadRequest, new List<string>() { "Comment not Found" });

                var mapped = _mapper.Map<Comment>(commentDto);
                _context.Comments.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<CommentDto>(commentDto);
            }
            catch (Exception ex)
            {
                return new Response<CommentDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
