using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Entities
{
    public class Like_Post 
    {
        public int Id { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.Now;


        public int? UserId { get; set; }
        public User? user { get; set; } // nav prop one 



        public int? PostId { get; set; }
        public Post? post { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
