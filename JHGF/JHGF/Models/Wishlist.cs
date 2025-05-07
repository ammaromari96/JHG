using System;
using System.Collections.Generic;

namespace JHGF.Models;

public partial class Wishlist
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? HotelId { get; set; }

    public int? TourId { get; set; }

    public int? PackageId { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Package? Package { get; set; }

    public virtual Tour? Tour { get; set; }

    public virtual User? User { get; set; }
}
