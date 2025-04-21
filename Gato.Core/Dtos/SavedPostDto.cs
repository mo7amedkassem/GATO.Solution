using Gato.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Dtos
{
    public class SavedPostDto
    {
        public int Id { get; set; }

        public int PostCreatorId { get; set; }
        public string PostCreatorName { get; set; }

        public int? PostId { get; set; }
        
        public string Content { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    }
}
