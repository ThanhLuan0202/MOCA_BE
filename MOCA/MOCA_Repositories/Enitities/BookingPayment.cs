using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class BookingPayment
{
    public int PaymentId { get; set; }

    public int? BookingId { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentType { get; set; }

    public bool? IsPaid { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }
    public string? PaypalOrderId { get; set; }
    public DateTime? CreateDate { get; set; }

    public virtual DoctorBooking? Booking { get; set; }
}
