using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JHGF.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<ContactU> ContactUs { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AMMAR;Database=JHG;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC0796C8D3FD");

            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Bookings__HotelI__440B1D61");

            entity.HasOne(d => d.Package).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Bookings__Packag__45F365D3");

            entity.HasOne(d => d.Tour).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Bookings__TourId__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Bookings__UserId__4316F928");
        });

        modelBuilder.Entity<ContactU>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactU__3214EC07C509CA5A");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Subject).HasMaxLength(50);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotels__3214EC0780C42802");

            entity.Property(e => e.DeluxeRoomImage1).HasMaxLength(255);
            entity.Property(e => e.DeluxeRoomImage2).HasMaxLength(255);
            entity.Property(e => e.DeluxeRoomImage3).HasMaxLength(255);
            entity.Property(e => e.DeluxeRoomImage4).HasMaxLength(255);
            entity.Property(e => e.DeluxeRoomName).HasMaxLength(100);
            entity.Property(e => e.DeluxeRoomPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DistanceFromCity).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.HotelImage1).HasMaxLength(255);
            entity.Property(e => e.HotelImage2).HasMaxLength(255);
            entity.Property(e => e.HotelImage3).HasMaxLength(255);
            entity.Property(e => e.HotelImage4).HasMaxLength(255);
            entity.Property(e => e.HotelName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SingleRoomImage1).HasMaxLength(255);
            entity.Property(e => e.SingleRoomImage2).HasMaxLength(255);
            entity.Property(e => e.SingleRoomImage3).HasMaxLength(255);
            entity.Property(e => e.SingleRoomImage4).HasMaxLength(255);
            entity.Property(e => e.SingleRoomName).HasMaxLength(100);
            entity.Property(e => e.SingleRoomPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SmallDescription).HasMaxLength(255);
            entity.Property(e => e.TwinRoomImage1).HasMaxLength(255);
            entity.Property(e => e.TwinRoomImage2).HasMaxLength(255);
            entity.Property(e => e.TwinRoomImage3).HasMaxLength(255);
            entity.Property(e => e.TwinRoomImage4).HasMaxLength(255);
            entity.Property(e => e.TwinRoomName).HasMaxLength(100);
            entity.Property(e => e.TwinRoomPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Packages__3214EC07C352078F");

            entity.Property(e => e.Image1).HasMaxLength(255);
            entity.Property(e => e.Image2).HasMaxLength(255);
            entity.Property(e => e.Image3).HasMaxLength(255);
            entity.Property(e => e.Image4).HasMaxLength(255);
            entity.Property(e => e.ItineraryTime1).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime2).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime3).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime4).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime5).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime6).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime7).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime8).HasMaxLength(20);
            entity.Property(e => e.ItineraryTitle1).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle2).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle3).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle4).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle5).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle6).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle7).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle8).HasMaxLength(150);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.PackageName).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SmallDescription).HasMaxLength(300);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC0790F5ED11");

            entity.Property(e => e.PaidAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaidAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payments__Bookin__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payments__UserId__6B24EA82");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC0720877AB0");

            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Reviews__HotelId__4AB81AF0");

            entity.HasOne(d => d.Package).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Reviews__Package__4CA06362");

            entity.HasOne(d => d.Tour).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Reviews__TourId__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserId__49C3F6B7");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Testimon__3214EC07A001485D");

            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tours__3214EC0721FFC4DB");

            entity.Property(e => e.Image1).HasMaxLength(255);
            entity.Property(e => e.Image2).HasMaxLength(255);
            entity.Property(e => e.Image3).HasMaxLength(255);
            entity.Property(e => e.Image4).HasMaxLength(255);
            entity.Property(e => e.ItineraryTime1).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime2).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime3).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime4).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime5).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime6).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime7).HasMaxLength(20);
            entity.Property(e => e.ItineraryTime8).HasMaxLength(20);
            entity.Property(e => e.ItineraryTitle1).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle2).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle3).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle4).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle5).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle6).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle7).HasMaxLength(150);
            entity.Property(e => e.ItineraryTitle8).HasMaxLength(150);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SmallDescription).HasMaxLength(300);
            entity.Property(e => e.TourName).HasMaxLength(150);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07511CA4E8");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534F02FD44F").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ProfilePic).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wishlist__3214EC07E583C048");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Wishlists__Hotel__5070F446");

            entity.HasOne(d => d.Package).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Wishlists__Packa__52593CB8");

            entity.HasOne(d => d.Tour).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__Wishlists__TourI__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Wishlists__UserI__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
