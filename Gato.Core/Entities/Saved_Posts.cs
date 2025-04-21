using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Entities
{
    public class Saved_Posts
    {
        public int Id { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; }

        public int? PostId { get; set; } 
        public Post? Post { get; set; }

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
 
    }
}
