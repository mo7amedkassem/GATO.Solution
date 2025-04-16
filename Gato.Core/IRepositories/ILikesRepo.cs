using Gato.Core.Dtos;
using Gato.Core.Entities;
using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.IRepositories
{
    public interface ILikesRepo
    {
        Task<IReadOnlyList<likedto>> Get_All_Likes_For_C_Async(int Comment_Id);
        Task<IReadOnlyList<likedto>> Get_All_Likes_For_P_Async(int Post_Id);

        Task addlikeForPostAsync(LikeForPRequest like, int userId);
        Task addlikeForCommentAsync(LikeForCRequest like, int userId);

        Task DeletelikeAsync(int id, User user_par);
    }
}
