using AutoMapper;
using Gato.Core.Dtos;
using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Microsoft.EntityFrameworkCore;
namespace Gato.Repository.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private readonly StoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public CommentRepo(StoreDBContext dBContext , IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }


        public async Task addCommentAsync(CommentRequest comment, int userId)
        {
            Comment newComment = new Comment
            {
                Content = comment.Content,
                CreatedAt = DateTime.Now,
                UserId = userId,  
                PostId = comment.PostId 
            };

            await _dbContext.Comments.AddAsync(newComment);
            await _dbContext.SaveChangesAsync(); 
        }

        public async Task DeleteCommentAsync(int id, User user_par)
        {
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment == null)
            {
                throw new Exception("Comment not found");
            }
            if (comment.UserId == user_par.Id)
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
            else
                throw new UnauthorizedAccessException("You can only delete your own Comment.");

        }


        public async Task<IReadOnlyList<CommentDto>> GetAllAsync(int postId)
        {
            var comments = await _dbContext.Comments.Where(x => x.PostId == postId)
                                 .Include(x => x.User)
                                 .Include(x=> x.Post).ToListAsync();

            var likes =  _dbContext.likes.ToList();

            var res = _mapper.Map<List<CommentDto>>(comments); 

            foreach (var comment in res)
            {
                comment.LikesCount = likes.Where(x => x.CommentId == comment.Id).Count();
            }
            if (comments.Any())
                return res;
            return default;

        }



        public async Task UpdateCommentAsync(int id, String new_content, User user_par)
        {
            var existingcomment = await _dbContext.Comments.FindAsync(id);
            if (existingcomment == null)
            {
                throw new Exception("Comment not found");
            }
            if (existingcomment.UserId == user_par.Id)
            {
                existingcomment.Content = new_content;
                _dbContext.Comments.Update(existingcomment);
                await _dbContext.SaveChangesAsync();
            }
            else
                throw new UnauthorizedAccessException("You can only delete your own posts.");
        }
    }
}
