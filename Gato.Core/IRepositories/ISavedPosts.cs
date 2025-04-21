using Gato.Core.Dtos;
using Gato.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.IRepositories
{
   public interface ISavedPosts
    {
        Task SavePostAsync(int postId);
        Task DeleteSavedPostAsync(int SavedPost_Id);
        Task<IReadOnlyList<SavedPostDto>> GetSavedPosts();


    }
}
