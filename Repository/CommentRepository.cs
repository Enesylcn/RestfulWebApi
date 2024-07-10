using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog.LayoutRenderers;
using RestfulWebApi.Data;
using RestfulWebApi.Helpers;
using RestfulWebApi.Interfaces;
using RestfulWebApi.Models;

namespace RestfulWebApi.Repository
{
   public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<CommentRepository> _logger;
        public CommentRepository(ApplicationDBContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
            {
                _logger.LogInformation("******DeleteAsync is not worked because there is no {id}'s comment", id);
                return null;
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comments.AsQueryable();

            if (queryObject.UserId == 0)
            {
                comments = comments.Where(s => s.User.Id == queryObject.UserId);
            };
            if (queryObject.IsDecsending == true)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
                _logger.LogInformation("******All comments are sorted in descending order");
            }
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            _logger.LogInformation("******{id}'s comment is updated", id);

            return existingComment;
        }
    }
}