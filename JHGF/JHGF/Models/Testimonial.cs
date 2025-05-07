using System;
using System.Collections.Generic;

namespace JHGF.Models;

public partial class Testimonial
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Message { get; set; }

    public int? Rating { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }
}
