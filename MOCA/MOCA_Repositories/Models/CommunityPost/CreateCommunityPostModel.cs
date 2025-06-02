using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.CommunityPost
{
    public class CreateCommunityPostModel
    {
        public int? UserId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Tags { get; set; }

        public string? ImageUrl { get; set; }

    }
}
