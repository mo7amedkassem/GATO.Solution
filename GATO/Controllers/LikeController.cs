using Gato.Core.Dtos;
using Gato.Core.Entities;
using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Gato.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikesRepo _likesRepo;
        private readonly StoreDBContext _context;


        public LikeController(ILikesRepo likesRepo, StoreDBContext context)
        {
            _likesRepo = likesRepo;
            _context = context;
        }


        [HttpPost("comment")]
        public async Task<IActionResult> AddLikeForComment([FromBody] LikeForCRequest like)
        {

            await _likesRepo.addlikeForCommentAsync(like, like.UserId);

            return Ok();
        }


        [HttpPost("post")]
        public async Task<IActionResult> AddLikForPost([FromBody] LikeForPRequest like)
        {

            await _likesRepo.addlikeForPostAsync(like, like.UserId);

            return Ok();
        }



        [HttpGet("comment/{Comment_Id}")]
        public async Task<IActionResult> GetAllLikesForComments(int Comment_Id)
        {
            var likes = await _likesRepo.Get_All_Likes_For_C_Async(Comment_Id);
            return Ok(likes);
        }


        [HttpGet("post/{Post_Id}")]
        public async Task<IActionResult> GetAllLikesForPosts(int Post_Id)
        {
            var likes = await _likesRepo.Get_All_Likes_For_P_Async(Post_Id);
            return Ok(likes);
        }   


     



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikeForComments(int id)
        {
            var like = await _context.likes_Comments.FindAsync(id);
            if (like == null)
            {
                throw new Exception("Like Not Found");
            }
            like.IsDeleted = true;
            _context.likes_Comments.Update(like);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{Post_id}")]
        public async Task<IActionResult> DeleteLikeForposts(int id)
        {
            var like = await _context.likes_Posts.FindAsync(id);
            if (like == null)
            {
                throw new Exception("Like Not Found");
            }
            like.IsDeleted = true;
            _context.likes_Posts.Update(like);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
