using Gato.Repository.Entities;

namespace Gato.Core.Entities
{
    public class Like_Comment 
    {
        public int Id { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.Now;


        public int? UserId { get; set; }
        public User? user { get; set; } // nav prop one 

        public int? CommentId { get; set; }
        public Comment? comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;


    }
}
