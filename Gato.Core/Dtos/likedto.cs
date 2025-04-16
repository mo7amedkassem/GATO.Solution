using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Dtos
{
    public class likedto
    {

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string user { get; set; }


        //public int? PostId { get; set; }
        //public string? post { get; set; }

        //public int UserId { get; set; }

        //public int? CommentId { get; set; }
        //public string? comment { get; set; }

    }

}
