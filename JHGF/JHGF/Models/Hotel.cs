using System;
using System.Collections.Generic;

namespace JHGF.Models;

public partial class Hotel
{
    public int Id { get; set; }

    public string? HotelName { get; set; }

    public string? Location { get; set; }

    public string? SmallDescription { get; set; }

    public string? FullDescription { get; set; }

    public int? Rating { get; set; }

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public decimal? DistanceFromCity { get; set; }

    public bool? FreeWifi { get; set; }

    public bool? ParkingAvailable { get; set; }

    public string? HotelImage1 { get; set; }

    public string? HotelImage2 { get; set; }

    public string? HotelImage3 { get; set; }

    public string? HotelImage4 { get; set; }

    public string? DeluxeRoomName { get; set; }

    public decimal? DeluxeRoomPrice { get; set; }

    public string? DeluxeRoomDescription { get; set; }

    public string? DeluxeRoomImage1 { get; set; }

    public string? DeluxeRoomImage2 { get; set; }

    public string? DeluxeRoomImage3 { get; set; }

    public string? DeluxeRoomImage4 { get; set; }

    public string? TwinRoomName { get; set; }

    public decimal? TwinRoomPrice { get; set; }

    public string? TwinRoomDescription { get; set; }

    public string? TwinRoomImage1 { get; set; }

    public string? TwinRoomImage2 { get; set; }

    public string? TwinRoomImage3 { get; set; }

    public string? TwinRoomImage4 { get; set; }

    public string? SingleRoomName { get; set; }

    public decimal? SingleRoomPrice { get; set; }

    public string? SingleRoomDescription { get; set; }

    public string? SingleRoomImage1 { get; set; }

    public string? SingleRoomImage2 { get; set; }

    public string? SingleRoomImage3 { get; set; }

    public string? SingleRoomImage4 { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
