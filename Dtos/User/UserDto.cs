using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Dtos.Comment;

namespace RestfulWebApi.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }

        public string Adress { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}