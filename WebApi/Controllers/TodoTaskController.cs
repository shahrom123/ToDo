using Domain.Dto;
using Domain.Response;
using Infrastructre.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class TodoTaskController : ControllerBase
    {
        private TodoTaskService _todoTaskService;

        public TodoTaskController(TodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
            
        }
        [HttpGet("GetTodoTask")]
        public async Task<Response<List<TodoTaskDto>>> Get()
        {
            return await _todoTaskService.GetTodoTask();

        }
        [HttpPost("AddTodoTask")]
        public async Task<Response<TodoTaskDto>> Add(TodoTaskDto todoTaskDto)
        {
            if (ModelState.IsValid)
            {
                return await _todoTaskService.Add(todoTaskDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<TodoTaskDto>(HttpStatusCode.BadRequest, errors);
            }
        }

        [HttpPut("UpdateTodoTask")]
        public async Task<Response<TodoTaskDto>> Put([FromBody] TodoTaskDto todoTaskDto) => await _todoTaskService.Update(todoTaskDto);

        [HttpDelete("DeleteTodoTask")]
        public async Task<Response<string>> Delete(int id) => await _todoTaskService.Delete(id);
    }

}
