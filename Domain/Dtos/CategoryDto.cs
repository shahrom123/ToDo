using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string CategoryName { get; set; }
        public int TodoTaskId { get; set; }  
       
    }
}
