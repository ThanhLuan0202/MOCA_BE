using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class DoctorBooking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public DateTime? BookingDate { get; set; }

    public byte? ConsultationType { get; set; }

    public decimal? RequiredDeposit { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<BookingPayment> BookingPayments { get; set; } = new List<BookingPayment>();

    public virtual DoctorProfile? Doctor { get; set; }

    public virtual User? User { get; set; }
}
