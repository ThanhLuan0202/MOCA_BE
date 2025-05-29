using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class CommunityPost
{
    public int PostId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Tags { get; set; }

    public string? ImageUrl { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<CommunityReply> CommunityReplies { get; set; } = new List<CommunityReply>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual User? User { get; set; }
}
