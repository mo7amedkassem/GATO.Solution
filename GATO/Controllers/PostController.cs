using Gato.Core.Dtos;
using Gato.Core.Entities.Dtos;
using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Gato.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepo _postRepo;
        private readonly StoreDBContext _context;
        private readonly ISavedPosts _savedpost;

        public PostController(IPostRepo postRepo, StoreDBContext dBContext ,ISavedPosts savedpost )
        {
            _postRepo = postRepo;
            _context = dBContext;
            _savedpost = savedpost;
        }


           [HttpPost("add-Post")]

        public async Task<IActionResult> AddPost([FromBody] PostRequest post)
        {
            if (post.UserId == 0 || string.IsNullOrEmpty(post.Content))
            {
                return BadRequest("Invalid data.");
            }

            await _postRepo.AddPostAsync(post, post.UserId);

            return Ok();
        }





        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepo.GetAllPosts();
            return Ok(posts);
        }






        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePost(int id)
        //{
        //    var Post = await _context.Posts.FindAsync(id);
        //    if (Post == null)
        //    {
        //        return NotFound();
        //    }
        //    Post.IsDeleted = true;
        //    _context.Posts.Update(Post);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            // Get the post with its comments
            var post = await _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            // Soft delete the post 
            post.IsDeleted = true;

            // Soft delete likes_post
            var postLikes = await _context.likes_Posts
                .Where(l => l.PostId == post.Id)
                .ToListAsync();

            foreach (var like in postLikes)
            {
                like.IsDeleted = true;
            }

            // Soft delete comments and their likes
            foreach (var comment in post.Comments)
            {
                comment.IsDeleted = true;

                var commentLikes = await _context.likes_Comments
                    .Where(l => l.CommentId == comment.Id)
                    .ToListAsync();

                foreach (var clike in commentLikes)
                {
                    // Soft delete likes of the comment
                    clike.IsDeleted = true;
                }
            }

            
            await _context.SaveChangesAsync();

            return NoContent();
        }





    }
}
