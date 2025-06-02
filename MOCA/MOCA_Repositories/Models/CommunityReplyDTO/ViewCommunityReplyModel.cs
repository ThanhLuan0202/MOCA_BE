using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.CommunityReplyDTO
{
    public class ViewCommunityReplyModel
    {
        public int ReplyId { get; set; }

        public int? PostId { get; set; }

        public int? UserId { get; set; }

        public string? Content { get; set; }

        public int? ParentReplyId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
