using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulWebApi.Helpers
{
    public class CommentQueryObject
    {
        public int UserId { get; set; }
        public bool IsDecsending { get; set; } = true;
    }
}