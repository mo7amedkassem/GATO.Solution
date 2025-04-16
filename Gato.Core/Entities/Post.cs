using Gato.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Entities
{
    public class Post : BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; } //nav prop

        //public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Like> likes { get; set;} = new HashSet<Like>();
    }
}
