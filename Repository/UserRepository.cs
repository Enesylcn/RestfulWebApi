using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestfulWebApi.Data;
using RestfulWebApi.Dtos.User;
using RestfulWebApi.Helpers;
using RestfulWebApi.Interfaces;
using RestfulWebApi.Models;

namespace RestfulWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userModel == null)
            {
                return null;
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<List<User>> GetAllAsync(QueryObject query)
        {
            var users = _context.Users.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                users = users.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Surname))
            {
                users = users.Where(s => s.Surname.Contains(query.Surname));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsDecsending ? users.OrderByDescending(s => s.Name) : users.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;


            return await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User?> GetBySymbolAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(s => s.Name == name);
        }

        public Task<bool> UserExists(int id)
        {
            return _context.Users.AnyAsync(s => s.Id == id);
        }

        public async Task<User?> UpdateAsync(int id, UpdateUserRequestDto userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Name = userDto.Name;
            existingUser.Surname = userDto.Surname;
            existingUser.NickName = userDto.NickName;
            existingUser.Email = userDto.Email;
            existingUser.Phone = userDto.Phone;
            existingUser.Adress = userDto.Adress;

            await _context.SaveChangesAsync();

            return existingUser;
        }
    }

}