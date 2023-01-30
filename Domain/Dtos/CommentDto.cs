using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string TextComment { get; set; }
    }
}
