using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using System.Net;

namespace Infrastructre.Services
{
    public class TodoTaskService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TodoTaskService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<TodoTaskDto>>> GetTodoTask()
        {
            try
            {
                var result = _context.TodoTasks.ToList();
                var mapped = _mapper.Map<List<TodoTaskDto>>(result);
                return new Response<List<TodoTaskDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<TodoTaskDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }

        public async Task<Response<TodoTaskDto>> Add(TodoTaskDto todoTaskDto)
        {
            try
            {
                var todoTask = _mapper.Map<TodoTask>(todoTaskDto);
                await _context.TodoTasks.AddAsync(todoTask);
                await _context.SaveChangesAsync();
                return new Response<TodoTaskDto>(todoTaskDto);
            }
            catch (Exception e)
            {
                return new Response<TodoTaskDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }

        public async Task<Response<TodoTaskDto>> Update(TodoTaskDto todoTaskDto)
        {
            try
            {
                var res = _context.Comments.FindAsync(todoTaskDto.Id);
                if (res == null) return new Response<TodoTaskDto>(HttpStatusCode.BadRequest, new List<string>() { "Album not Found" });

                var mapped = _mapper.Map<TodoTask>(todoTaskDto);
                _context.TodoTasks.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<TodoTaskDto>(todoTaskDto);
            }
            catch (Exception ex)
            {
                return new Response<TodoTaskDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> Delete(int id)
        {
            var todo = await _context.TodoTasks.FindAsync(id);
            if (todo == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });

            _context.TodoTasks.Remove(todo);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
