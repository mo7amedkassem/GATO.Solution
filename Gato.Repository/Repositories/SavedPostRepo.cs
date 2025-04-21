using Gato.Core.Dtos;
using Gato.Core.Entities;
using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Repositories
{
    public class SavedPostRepo : ISavedPosts
    {
        private readonly StoreDBContext _context;

        public SavedPostRepo(StoreDBContext context)
        {
            _context = context;
        }

        public Task DeleteSavedPostAsync(int SavedPost_Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<SavedPostDto>> GetSavedPosts()
        {
            return await _context.savedposts
                .Include(s => s.Post) // Include the Post entity
                .ThenInclude(p => p.User) // Include the User who created the Post
                .Select(s => new SavedPostDto
                {
                    Id = s.Id,
                    PostId = s.Post.Id, // ID of the saved post
                    PostCreatorId = s.Post.UserId ?? 0, // ID of the user who created the post
                    PostCreatorName = s.Post.User.UserName, // Name of the user who created the post
                    Content = s.Post.Content, // Content of the post
                    SavedAt = s.SavedAt // Timestamp when the post was saved
                })
                .ToListAsync();
        }




        public async Task SavePostAsync(int postId)
        {
            var Post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId);

            if (Post == null)
            {
                throw new ArgumentException("Post not found.");
            }

            var userId = Post.UserId;

            var alreadySaved = await _context.savedposts
           .AnyAsync(s => s.PostId == postId && s.UserId == userId);

            if (!alreadySaved)
            {
                var savedPost = new Saved_Posts
                {
                    PostId = postId,
                    UserId = userId.Value,
                    SavedAt = DateTime.UtcNow
                };

                await _context.savedposts.AddAsync(savedPost);
                await _context.SaveChangesAsync();
            }
        }
    }
}
