using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class DoctorProfile
{
    public int? UserId { get; set; }

    public int DoctorId { get; set; }

    public string? FullName { get; set; }

    public string? Specialization { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<DoctorBooking> DoctorBookings { get; set; } = new List<DoctorBooking>();

    public virtual ICollection<DoctorContact> DoctorContacts { get; set; } = new List<DoctorContact>();

    public virtual User? User { get; set; }
}
