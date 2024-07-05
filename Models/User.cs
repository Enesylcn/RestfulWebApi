using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulWebApi.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? NickName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public int Phone { get; set; }

        public string Adress { get; set; } = string.Empty;


        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}