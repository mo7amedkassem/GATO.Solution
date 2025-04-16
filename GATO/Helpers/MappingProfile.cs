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


            CreateMap<Like,likedto>().
                ForMember(d => d.user, O => O.MapFrom(s => s.user.UserName)).ReverseMap();
        }
    }
}
