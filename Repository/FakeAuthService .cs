using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Interfaces;
using RestfulWebApi.Models;

namespace RestfulWebApi.Repository
{
    public class FakeAuthService : IAuthService
    {
        private List<User> _users = new List<User>()
        {
            new User
            {
                Name = "admin",
                Surname = "admin"
            }
        };

        public User Authenticate(string username, string surname)
        {
            return _users.FirstOrDefault(u => u.Name == username && u.Surname == surname);
        }
    }
}