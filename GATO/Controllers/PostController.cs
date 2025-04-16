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
        public PostController(IPostRepo postRepo,StoreDBContext dBContext)
        {
            _postRepo = postRepo;
            _context = dBContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] PostRequest post)
        {
            if (post.UserId == 0 ||  string.IsNullOrEmpty(post.Content))
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






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var Post = await _context.Posts.FindAsync(id);
            if (Post == null)
            {
                return NotFound();
            }
            Post.IsDeleted = true;
            _context.Posts.Update(Post);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
