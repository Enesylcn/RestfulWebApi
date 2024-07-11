using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Dtos.Comment;
using RestfulWebApi.Filters;
using RestfulWebApi.Helpers;
using RestfulWebApi.Interfaces;
using RestfulWebApi.Mapper;

namespace RestfulWebApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IUserRepository _userRepo;
        public CommentsController(ICommentRepository commentRepo,
        IUserRepository userRepo )
        {
            _commentRepo = commentRepo;
            _userRepo = userRepo;
        }

        // Retrieves all comments based on the given query object
        [HttpGet]
        [FakeAuthorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync(queryObject);

            var commentDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }

        // Retrieves the comment with the specified ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        // Creates a new comment and assigns it to the specified user
        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Create([FromRoute] int id, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
            {
                return BadRequest("user does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(user.Id);
            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        // Updates the comment with the specified ID
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate(id));

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        // Deletes the comment with the specified ID
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok(commentModel);
        }

    }
}