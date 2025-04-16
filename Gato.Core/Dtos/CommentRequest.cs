namespace Gato.Core.Dtos
{
    public class CommentRequest
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

    }
}
