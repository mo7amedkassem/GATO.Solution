using Gato.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Entities
{
    public class Comment: BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;


        public int PostId { get; set; }
        public Post Post { get; set; }//nav prop


        public int? UserId { get; set; }
        public User? User { get; set; }//nav prop

        public ICollection<Like_Comment> likes { get; set; } = new HashSet<Like_Comment>();
    }
}
