using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Infrastructre.MapperProfiles
{
    public class InfrastructureProfiles:Profile
    {
        public InfrastructureProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<TodoTask, TodoTaskDto>().ReverseMap();
        }
    }
}
