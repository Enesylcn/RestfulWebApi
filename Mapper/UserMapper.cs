using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Dtos.User;
using RestfulWebApi.Models;

namespace RestfulWebApi.Mapper
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User UserModel)
        {
            return new UserDto
            {
                Id = UserModel.Id,
                Name = UserModel.Name,
                Surname = UserModel.Surname,
                NickName = UserModel.NickName,
                Email = UserModel.Email,
                Phone = UserModel.Phone,
                Adress =UserModel.Adress,
                Comments = UserModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static User ToUserFromCreateDTO(this CreateUserRequestDto userDto)
        {
            return new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                NickName = userDto.NickName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Adress =userDto.Adress
            };
        }
    }
}