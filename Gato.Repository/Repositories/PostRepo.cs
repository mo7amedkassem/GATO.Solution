using AutoMapper;
using Gato.Core.Dtos;
using Gato.Core.Entities.Dtos;
using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Gato.Repository.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly StoreDBContext _dBContext;
        private readonly IMapper _mapper;

        public PostRepo(StoreDBContext dBContext,IMapper mapper)
        {
            _dBContext = dBContext;
            _mapper = mapper;
        }
        public async Task AddPostAsync(PostRequest postReq, int user_id)
        {
            Post newpost = new Post
            {
                UserId = user_id,
                CreatedAt = DateTime.UtcNow,    
                Content = postReq.Content,
            };

            await _dBContext.Posts.AddAsync(newpost);
            await _dBContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Post post, String new_content, User user_Par)
        {
            var existingPost = await _dBContext.Posts.FindAsync(post.Id);

            if (existingPost == null)
            {
                throw new Exception("Post not found");
            }
            if (existingPost.UserId == user_Par.Id)
            {
                existingPost.Content = new_content;
                _dBContext.Posts.Update(existingPost);
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedAccessException("You can only edit your own posts.");
            }
        }

        public async Task<IReadOnlyList<PostDto>> GetAllPosts()
        {
            var posts = await _dBContext.Posts.Include(x => x.User)
                                                    .ToListAsync();

            var likes = _dBContext.likes_Posts.ToList();

            var posts_ = _mapper.Map<List<PostDto>>(posts);

            foreach (var post in posts_)
            {
                post.LikesCount = likes.Where(x => x.PostId == post.Id).Count();
            }

            if (posts.Any())
                return posts_;
            return default;

        }

        public async Task DeleteAsync(int id, User user_par)
        {
            var post = await _dBContext.Posts.FindAsync(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            if (post.UserId == user_par.Id)
            {
                _dBContext.Posts.Remove(post);
                await _dBContext.SaveChangesAsync();
            }
            else
                throw new UnauthorizedAccessException("You can only delete your own posts.");


        }


    }
}
