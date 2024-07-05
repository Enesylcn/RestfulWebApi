using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Models;

namespace RestfulWebApi.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }
        public string Adress { get; set; } = string.Empty;
         public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}