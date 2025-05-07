using System;
using System.Collections.Generic;

namespace JHGF.Models;

public partial class Tour
{
    public int Id { get; set; }

    public string? TourName { get; set; }

    public string? Location { get; set; }

    public int? DurationDays { get; set; }

    public string? SmallDescription { get; set; }

    public string? FullDescription { get; set; }

    public string? ItineraryTime1 { get; set; }

    public string? ItineraryTitle1 { get; set; }

    public string? ItineraryDesc1 { get; set; }

    public string? ItineraryTime2 { get; set; }

    public string? ItineraryTitle2 { get; set; }

    public string? ItineraryDesc2 { get; set; }

    public string? ItineraryTime3 { get; set; }

    public string? ItineraryTitle3 { get; set; }

    public string? ItineraryDesc3 { get; set; }

    public string? ItineraryTime4 { get; set; }

    public string? ItineraryTitle4 { get; set; }

    public string? ItineraryDesc4 { get; set; }

    public string? ItineraryTime5 { get; set; }

    public string? ItineraryTitle5 { get; set; }

    public string? ItineraryDesc5 { get; set; }

    public string? ItineraryTime6 { get; set; }

    public string? ItineraryTitle6 { get; set; }

    public string? ItineraryDesc6 { get; set; }

    public string? ItineraryTime7 { get; set; }

    public string? ItineraryTitle7 { get; set; }

    public string? ItineraryDesc7 { get; set; }

    public string? ItineraryTime8 { get; set; }

    public string? ItineraryTitle8 { get; set; }

    public string? ItineraryDesc8 { get; set; }

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public string? Image4 { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
