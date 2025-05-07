using System;
using System.Collections.Generic;

namespace JHGF.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? BookingId { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? PaidAmount { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual User? User { get; set; }
}
