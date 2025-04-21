using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedPostsController : ControllerBase
    {


       
        private readonly StoreDBContext _context;
        private readonly ISavedPosts _savedpost;

        public SavedPostsController(IPostRepo postRepo, StoreDBContext dBContext, ISavedPosts savedpost)
        {
            
            _context = dBContext;
            _savedpost = savedpost;
        }



        [HttpPost("save-post/{postId}")]
        public async Task<IActionResult> SavePost(int postId)
        {
            try
            {
              
                await _savedpost.SavePostAsync(postId);

                return Ok(new { message = "Post saved successfully." });
            }
            catch (ArgumentException ex)
            {
              
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, new { message = "An error occurred while saving the post.", details = ex.Message });
            }
        }



        [HttpGet("all-saved-posts")]
        public async Task<IActionResult> GetSavedPosts()
        {
            var savedPosts = await _savedpost.GetSavedPosts();

            if (savedPosts == null || savedPosts.Count == 0)
            {
                return NotFound(new { message = "No saved posts found for this user." });
            }

            return Ok(savedPosts);
        }




        //[HttpDelete("remove-saved-post/{postId}/{userId}")]
        //public async Task<IActionResult> RemoveSavedPost(int postId, int userId)
        //{
        //    // التحقق من وجود البوست المحفوظ لهذا اليوزر
        //    var savedPosts = await _savedpost.GetSavedPosts(userId);
        //    var savedPost = savedPosts.FirstOrDefault(sp => sp.PostId == postId);

        //    if (savedPost == null)
        //    {
        //        return NotFound(new { message = "Saved post not found." });
        //    }

        //    // حذف البوست المحفوظ
        //    await _savedpost.DeleteSavedPostAsync(postId);

        //    return Ok(new { message = "Saved post removed successfully." });
        //}





    }
}
