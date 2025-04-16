using AutoMapper;
using Gato.Core.Dtos;
using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using GATO.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly StoreDBContext _context;


        public CommentsController(ICommentRepo commentRepo, StoreDBContext context )
        {
            _commentRepo = commentRepo;
            _context = context;
        }



        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Gato.Core.Dtos.CommentRequest comment)
        {
            if (comment.UserId == 0 || comment.PostId == 0  || string.IsNullOrEmpty(comment.Content))
            {
                return BadRequest("Invalid data.");
            }

            await _commentRepo.addCommentAsync(comment, comment.UserId);

            return Ok();
        }




        [HttpGet]
        public async Task<IActionResult> GetAllComments(int postId)
        {
            
            var comments = await _commentRepo.GetAllAsync(postId);
            return Ok(comments);
        }

   


        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteComment (int id)
        {
            var comment = await _context.Comments.FindAsync(id);
          if(comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}
    

