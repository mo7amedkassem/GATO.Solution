
using Gato.Core.Dtos;
using Gato.Repository.Entities;


namespace Gato.Core.IRepositories
{
    public interface ICommentRepo
    {
        Task<IReadOnlyList<CommentDto>> GetAllAsync(int postId);
        Task addCommentAsync(CommentRequest comment, int userId);
        Task UpdateCommentAsync(int id, String new_content, User user_par);
        Task DeleteCommentAsync(int id, User user_par);
    }
}