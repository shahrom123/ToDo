

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comment
    {

        public int Id { get; set; }
        [Required, MaxLength (200)]   
        public string TextComment { get; set; }   
        public int UserId { get; set; }
        public User User { get; set; } 
        public int TodoTaskId { get; set; }
        public List<TodoTask> TodoTasks { get; set; }  

    }
}
