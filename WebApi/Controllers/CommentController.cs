using Domain.Dto;
using Domain.Response;
using Infrastructre.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class ComemntController : ControllerBase
    {
        private CommentService _commentService;

        public ComemntController(CommentService commentService)
        {
            _commentService= commentService;
        }
        [HttpGet("GetComemnt")]
        public async Task<Response<List<CommentDto>>> Get()
        {
            return await _commentService.GetComment();

        }
        [HttpPost("AddComemnt")]
        public async Task<Response<CommentDto>> Add(CommentDto commentDto)
        {
            if (ModelState.IsValid)
            {
                return await _commentService.Add(commentDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<CommentDto>(HttpStatusCode.BadRequest, errors);
             }
        }

        [HttpPut("UpdateComemnt")]
        public async Task<Response<CommentDto>> Put([FromBody] CommentDto commentDto) => await _commentService.Update(commentDto);

        [HttpDelete("DeleteComemnt")]
        public async Task<Response<string>> Delete(int id) => await _commentService.Delete(id);
    }

}
