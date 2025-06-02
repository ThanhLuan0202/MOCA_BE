using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class CommunityReply
{
    public int ReplyId { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string? Content { get; set; }

    public string? Status { get; set; }

    public int? ParentReplyId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<CommunityReply> InverseParentReply { get; set; } = new List<CommunityReply>();

    public virtual CommunityReply? ParentReply { get; set; }

    public virtual CommunityPost? Post { get; set; }

    public virtual User? User { get; set; }
}
