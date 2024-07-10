using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulWebApi.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        
        // Virtual is for lazy loading default execution
        public virtual User? User {get; set;}
        
    }
}