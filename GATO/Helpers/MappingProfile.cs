using AutoMapper;

using Gato.Repository.Entities;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient.Server;
using Gato.Core.Dtos;
using Gato.Core.Entities;

namespace GATO.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>().
                ForMember(d => d.Post, O => O.MapFrom(s => s.Post.Content)).
              //  ForMember(d => d.LikesCount, O => O.MapFrom(s => s.Post.Content)).
                ForMember(d => d.User, O => O.MapFrom(s => s.User.UserName)).ReverseMap();



            CreateMap<Post, PostDto>().
                ForMember(d => d.User, O => O.MapFrom(s => s.User.UserName)).ReverseMap();


            CreateMap<Like_Comment,likedto>().
                ForMember(d => d.user, O => O.MapFrom(s => s.user.UserName)).ReverseMap();

            CreateMap<Like_Post, likedto>().
                ForMember(d => d.user, O => O.MapFrom(s => s.user.UserName)).ReverseMap();


            CreateMap<Saved_Posts, SavedPostDto>()
         .ForMember(d => d.PostCreatorId, O => O.MapFrom(s => s.Post.UserId)) // Map PostCreatorId from Post.UserId
         .ForMember(d => d.PostCreatorName, O => O.MapFrom(s => s.Post.User.UserName)) // Map PostCreatorName from Post.User.UserName
         .ForMember(d => d.PostId, O => O.MapFrom(s => s.Post.Id)) // Map PostId from Post.Id
         .ForMember(d => d.Content, O => O.MapFrom(s => s.Post.Content)) // Map PostContent from Post.Content
         .ReverseMap();
        }
    }
}
