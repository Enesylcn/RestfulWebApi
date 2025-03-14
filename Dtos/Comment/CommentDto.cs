using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulWebApi.Dtos.Comment
{
    public class CommentDto
    {
         public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
    }
}