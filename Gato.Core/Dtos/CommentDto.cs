
namespace Gato.Core.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int PostId { get; set; }
        public string Post { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int LikesCount { get; set; }
    }
}
