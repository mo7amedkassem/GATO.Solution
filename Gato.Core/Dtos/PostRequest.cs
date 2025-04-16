using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Dtos
{
    public class PostRequest
    {
       
        public string Content { get; set; }
        public int UserId { get; set; }
  

    }
}
