using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class PostLike
{
    public int LikeId { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual CommunityPost? Post { get; set; }

    public virtual User? User { get; set; }
}
