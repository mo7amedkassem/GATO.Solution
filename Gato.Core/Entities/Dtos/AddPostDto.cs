using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Entities.Dtos
{
    public class AddPostDto
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
