using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulWebApi.Dtos.Comment;
using RestfulWebApi.Models;

namespace RestfulWebApi.Mapper
{
     public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int userId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                UserId = userId,
                CreatedOn = commentDto.CreatedOn

            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int userId)
        {
            return new Comment
            {
                Content = commentDto.Content,
                UserId = userId,
                CreatedOn = commentDto.CreatedOn
            };
        }

    }
    
}