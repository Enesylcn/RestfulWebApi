using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Dtos.User;
using RestfulWebApi.Helpers;
using RestfulWebApi.Models;

namespace RestfulWebApi.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(QueryObject query);
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(int id,UpdateUserRequestDto UserDto);
        Task<User?> DeleteAsync(int id);
        Task<bool> UserExists(int id);
    }
}