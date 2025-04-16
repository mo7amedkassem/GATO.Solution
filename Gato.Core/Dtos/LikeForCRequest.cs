using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Dtos
{
    public class LikeForCRequest
    {
        public int UserId { get; set; }
     
        public int? CommentId { get; set; }

        //public int? PostId { get; set; }

        //public bool IsPost { get; set; } = false;

        //public bool IsValid()
        //{
        //    bool hasValidPost = IsPost && PostId.HasValue && PostId > 0;
        //    bool hasValidComment = !IsPost && CommentId.HasValue && CommentId > 0;

        //    return hasValidPost || hasValidComment; 
        //}

    }
}
