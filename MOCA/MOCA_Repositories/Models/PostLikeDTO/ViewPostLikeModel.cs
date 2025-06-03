using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PostLikeDTO
{
    public class ViewPostLikeModel
    {
        public int LikeId { get; set; }

        public int? PostId { get; set; }

        public int? UserId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
