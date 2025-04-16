using Gato.Core.Dtos;
using Gato.Core.Entities.Dtos;
using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.IRepositories
{
    public interface IPostRepo
    {
        Task AddPostAsync(PostRequest postReq,int user_id);
        Task UpdateAsync(Post post,String new_content, User user_Par); 
        Task DeleteAsync(int id, User user_Par);    
        Task<IReadOnlyList<PostDto>> GetAllPosts();
    }
}
