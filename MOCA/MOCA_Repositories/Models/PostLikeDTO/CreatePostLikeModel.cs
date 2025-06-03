using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PostLikeDTO
{
    public class CreatePostLikeModel
    {
        public int? PostId { get; set; }

        public int? UserId { get; set; }

    }
}
