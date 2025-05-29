using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class DoctorContact
{
    public int ContactId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public DateTime? ContactDate { get; set; }

    public string? ContactMethod { get; set; }

    public string? Status { get; set; }

    public virtual DoctorProfile? Doctor { get; set; }

    public virtual ICollection<MessagesWithDoctor> MessagesWithDoctors { get; set; } = new List<MessagesWithDoctor>();

    public virtual User? User { get; set; }
}
