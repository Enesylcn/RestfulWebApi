using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulWebApi.Dtos.User
{
    public class UpdateUserRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 over characters")]
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }

        public string Adress { get; set; } = string.Empty;

    }
}