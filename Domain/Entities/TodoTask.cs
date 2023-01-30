
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TodoTask
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public DateTime deadline { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }   
        public Category Categories { get; set;}
        public List<Comment> Comments { get;set; } 
    }
}
