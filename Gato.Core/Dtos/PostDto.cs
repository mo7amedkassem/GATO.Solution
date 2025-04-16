using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        

        public int UserId { get; set; }
        public string User { get; set; }
        public int LikesCount { get; set; }
    }
}
