using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Models;

namespace RestfulWebApi.Interfaces
{
   public interface IAuthService
{
    User Authenticate(string username, string surname);
}
}