using AutoMapper;
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
    public class LikeRepo : ILikesRepo
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public LikeRepo(StoreDBContext context , IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task addlikeForPostAsync(LikeForPRequest like, int userId)
        {
            //if (like.IsPost) like.CommentId = null;
            //if (!like.IsPost) like.PostId = null;
            //if( like.IsPost || like.CommentId  > 0)
            //{

            if (!_context.likes_Posts.Any(x => x.PostId == like.PostId && x.UserId == userId))
            {

                Like_Post newlike = new Like_Post
                {
                    PostId = like.PostId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _context.likes_Posts.AddAsync(newlike);

                await _context.SaveChangesAsync();
            }
            else
            {
                // to do message to front end    
            }

            //}

        }



        public async Task addlikeForCommentAsync(LikeForCRequest like, int userId)
        {


            //if (like.IsPost) like.CommentId = null;
            //if (!like.IsPost) like.PostId = null;
            //if( like.IsPost || like.CommentId  > 0)
            //{

            if (!_context.likes_Comments.Any(x => x.CommentId == like.CommentId && x.UserId == userId))
            {
                Like_Comment newlike = new Like_Comment
                {
                    CommentId = like.CommentId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                };


                await _context.likes_Comments.AddAsync(newlike);

                await _context.SaveChangesAsync();

            }
            else
            {
                // to do message to front end 
            }



            //}
        }


        public async Task Deletelike_CommentsAsync(int id, User user_par)
        {
            var like = await _context.likes_Comments.FindAsync(id);

            if (like == null)
            {
                throw new Exception("like not found");
            }
            if (like.UserId == user_par.Id)
            {
                _context.likes_Comments.Remove(like);
                await _context.SaveChangesAsync();
            }
            else
                throw new UnauthorizedAccessException("You can only delete your own Like.");
        }

        public async Task Deletelike_PostsAsync(int id, User user_par)
        {
            var like = await _context.likes_Posts.FindAsync(id);

            if (like == null)
            {
                throw new Exception("like not found");
            }
            if (like.UserId == user_par.Id)
            {
                _context.likes_Posts.Remove(like);
                await _context.SaveChangesAsync();
            }
            else
                throw new UnauthorizedAccessException("You can only delete your own Like.");
        }


        public async Task<IReadOnlyList<likedto>> Get_All_Likes_For_C_Async(int Comment_Id)
        {

            var likes = await _context.likes_Comments.Include(x => x.user)
                           .Where(l => l.CommentId == Comment_Id)
                           .ToListAsync();
            if (likes.Any())
                return _mapper.Map<List<likedto>>(likes);
            return default;
            
        }



        public async Task<IReadOnlyList<likedto>> Get_All_Likes_For_P_Async(int Post_Id)
        {
            var likes = await _context.likes_Posts.Include(x => x.user)
                                 .Where(l => l.PostId == Post_Id)
                                 .ToListAsync();
            if (likes.Any())
                return _mapper.Map<List<likedto>>(likes);
            return default;
        }


    }
}
