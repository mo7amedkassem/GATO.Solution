using Gato.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Entities
{
    public class User : IdentityUser<int>
    {
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public ICollection<Like> likes { get; set; } = new HashSet<Like>();
    }
}
